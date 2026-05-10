using DolphinScript.Core.Concrete;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity;

namespace DolphinScript.Core.Tests
{
    [TestClass]
    public class HumanMouseTrajectoryPlannerTests : BaseTest
    {
        [TestMethod]
        public void ShouldResolveMouseMovementServiceFromComposition()
        {
            var movementService = UnityContainer.Resolve<IMouseMovementService>();

            Assert.IsNotNull(movementService);
        }

        [TestMethod]
        public void ShouldGenerateCurvedMinimumJerkTrajectoryToTarget()
        {
            var planner = new HumanMouseTrajectoryPlanner(new CenteredRandomService());
            var start = new Point(10, 20);
            var target = new Point(410, 220);

            var steps = planner.CreateTrajectory(start, target, CreateProfile()).ToList();

            Assert.IsTrue(steps.Count > 40);
            Assert.AreEqual(target, steps.Last().Position);
            Assert.IsTrue(steps.All(x => x.DelayMilliseconds >= 5 && x.DelayMilliseconds <= 18));

            var straightDistance = Distance(start, target);
            var pathDistance = PathDistance(start, steps);

            Assert.IsTrue(pathDistance > straightDistance);
            Assert.IsTrue(pathDistance < straightDistance * 1.8);
        }

        [TestMethod]
        public void ShouldScaleTrajectoryDurationWithConfiguredSpeed()
        {
            var planner = new HumanMouseTrajectoryPlanner(new CenteredRandomService());
            var start = new Point(80, 120);
            var target = new Point(880, 520);

            var slowSteps = planner.CreateTrajectory(start, target, new MouseMovementProfile
            {
                MinimumSpeed = 10,
                MaximumSpeed = 10
            });

            var fastSteps = planner.CreateTrajectory(start, target, new MouseMovementProfile
            {
                MinimumSpeed = 90,
                MaximumSpeed = 90
            });

            Assert.IsTrue(TotalDelay(fastSteps) < TotalDelay(slowSteps));
            Assert.AreEqual(target, slowSteps.Last().Position);
            Assert.AreEqual(target, fastSteps.Last().Position);
        }

        [TestMethod]
        public void ShouldHandleTinyMovements()
        {
            var planner = new HumanMouseTrajectoryPlanner(new CenteredRandomService());
            var target = new Point(101, 101);

            var steps = planner.CreateTrajectory(new Point(100, 100), target, CreateProfile()).ToList();

            Assert.IsTrue(steps.Count > 0);
            Assert.AreEqual(target, steps.Last().Position);
        }

        [TestMethod]
        public void ShouldKeepTrajectoryInsideEndpointBounds()
        {
            var planner = new HumanMouseTrajectoryPlanner(new EdgePullingRandomService());
            var start = new Point(1950, 120);
            var target = new Point(1950, 900);

            var steps = planner.CreateTrajectory(start, target, CreateProfile()).ToList();

            Assert.IsTrue(steps.Count > 0);
            Assert.AreEqual(target, steps.Last().Position);
            Assert.IsTrue(steps.All(x => x.Position.X >= Math.Min(start.X, target.X) && x.Position.X <= Math.Max(start.X, target.X)));
            Assert.IsTrue(steps.All(x => x.Position.Y >= Math.Min(start.Y, target.Y) && x.Position.Y <= Math.Max(start.Y, target.Y)));
        }

        [TestMethod]
        public void ShouldNotStepBackwardWhenCrossingMonitorBoundary()
        {
            var planner = new HumanMouseTrajectoryPlanner(new EdgePullingRandomService());
            var start = new Point(1900, 480);
            var target = new Point(2260, 480);

            var steps = planner.CreateTrajectory(start, target, new MouseMovementProfile
            {
                MinimumSpeed = 15,
                MaximumSpeed = 15,
                AllowCorrectiveOvershoot = false
            }).ToList();

            var previousX = start.X;
            foreach (var step in steps)
            {
                Assert.IsTrue(step.Position.X >= previousX);
                previousX = step.Position.X;
            }
        }

        [TestMethod]
        public void ShouldAllowCorrectiveOvershootPastTarget()
        {
            var planner = new HumanMouseTrajectoryPlanner(new OvershootRandomService());
            var start = new Point(100, 100);
            var target = new Point(520, 100);

            var steps = planner.CreateTrajectory(start, target, CreateProfile()).ToList();

            Assert.IsTrue(steps.Any(x => x.Position.X > target.X));
            Assert.AreEqual(target, steps.Last().Position);
        }

        private static MouseMovementProfile CreateProfile()
        {
            return new MouseMovementProfile
            {
                MinimumSpeed = 40,
                MaximumSpeed = 60
            };
        }

        private static double PathDistance(Point start, IEnumerable<MouseMovementStep> steps)
        {
            var previous = start;
            var distance = 0.0;

            foreach (var step in steps)
            {
                distance += Distance(previous, step.Position);
                previous = step.Position;
            }

            return distance;
        }

        private static int TotalDelay(IEnumerable<MouseMovementStep> steps)
        {
            return steps.Sum(x => x.DelayMilliseconds);
        }

        private static double Distance(Point start, Point target)
        {
            var x = target.X - start.X;
            var y = target.Y - start.Y;
            return Math.Sqrt(x * x + y * y);
        }

        private class CenteredRandomService : IRandomService
        {
            public int GetRandomNumber(int min, int max)
            {
                return min + (max - min) / 2;
            }

            public double GetRandomDouble(double minimum, double maximum)
            {
                return minimum + (maximum - minimum) / 2.0;
            }

            public BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model)
            {
                throw new NotSupportedException();
            }
        }

        private class EdgePullingRandomService : IRandomService
        {
            public int GetRandomNumber(int min, int max)
            {
                return max;
            }

            public double GetRandomDouble(double minimum, double maximum)
            {
                return maximum;
            }

            public BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model)
            {
                throw new NotSupportedException();
            }
        }

        private class OvershootRandomService : IRandomService
        {
            public int GetRandomNumber(int min, int max)
            {
                return min;
            }

            public double GetRandomDouble(double minimum, double maximum)
            {
                return minimum;
            }

            public BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model)
            {
                throw new NotSupportedException();
            }
        }
    }
}