using DolphinScript.Core.Concrete;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Core.WindowsApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DolphinScript.Core.Tests
{
    [TestClass]
    public class MouseMovementServiceTests
    {
        [TestMethod]
        public void ShouldKeepCursorUpdatesOnPhysicalScreensWhenPathCrossesVirtualGap()
        {
            var screenBounds = new[]
            {
                new Rectangle(0, 0, 1920, 1080),
                new Rectangle(2020, 300, 1920, 1080)
            };
            var start = new Point(1910, 250);
            var target = new Point(2040, 340);
            var cursorController = new RecordingCursorController();
            var movementService = new MouseMovementService(
                new TestRandomService(),
                new FixedPointService(start),
                new MouseMathService(new TestRandomService()),
                new FixedTrajectoryPlanner(new[]
                {
                    new MouseMovementStep(new Point(1918, 268), 0),
                    new MouseMovementStep(new Point(1925, 285), 0),
                    new MouseMovementStep(new Point(1932, 318), 0),
                    new MouseMovementStep(target, 0)
                }),
                cursorController,
                new FixedScreenService(screenBounds),
                new TestScriptState());

            movementService.MoveMouseToPoint(target);

            Assert.IsTrue(cursorController.Positions.Count > 0);
            Assert.IsTrue(cursorController.Positions.All(position => IsOnAnyScreen(position, screenBounds)));
            Assert.IsFalse(cursorController.Positions.Contains(new Point(1925, 285)));
            AssertProgressDoesNotMoveBackward(start, target, cursorController.Positions);
        }

        [TestMethod]
        public void ShouldRouteMixedResolutionCrossMonitorMovementThroughSharedSeam()
        {
            var screenBounds = new[]
            {
                new Rectangle(0, 0, 2560, 1440),
                new Rectangle(2560, 0, 1920, 1080)
            };
            var start = new Point(2500, 1300);
            var target = new Point(2700, 900);
            var cursorController = new RecordingCursorController();
            var trajectoryPlanner = new RecordingTrajectoryPlanner();
            var movementService = new MouseMovementService(
                new TestRandomService(),
                new FixedPointService(start),
                new MouseMathService(new TestRandomService()),
                trajectoryPlanner,
                cursorController,
                new FixedScreenService(screenBounds),
                new TestScriptState());

            movementService.MoveMouseToPoint(target);

            Assert.AreEqual(2, trajectoryPlanner.Segments.Count);
            Assert.AreEqual(new Point(2500, 1300), trajectoryPlanner.Segments[0].Start);
            Assert.AreEqual(new Point(2559, 1079), trajectoryPlanner.Segments[0].Target);
            Assert.AreEqual(new Point(2560, 1079), trajectoryPlanner.Segments[1].Start);
            Assert.AreEqual(target, trajectoryPlanner.Segments[1].Target);
            Assert.IsTrue(cursorController.Positions.All(position => IsOnAnyScreen(position, screenBounds)));
            Assert.IsTrue(cursorController.Positions.Contains(new Point(2559, 1079)));
            Assert.IsTrue(cursorController.Positions.Contains(new Point(2560, 1079)));
        }

        private static void AssertProgressDoesNotMoveBackward(Point start, Point target, IEnumerable<Point> positions)
        {
            var distance = Math.Sqrt(Math.Pow(target.X - start.X, 2) + Math.Pow(target.Y - start.Y, 2));
            var unitX = (target.X - start.X) / distance;
            var unitY = (target.Y - start.Y) / distance;
            var previousProgress = 0.0;

            foreach (var position in positions)
            {
                var progress = (position.X - start.X) * unitX + (position.Y - start.Y) * unitY;
                Assert.IsTrue(progress >= previousProgress);
                previousProgress = progress;
            }
        }

        private static bool IsOnAnyScreen(Point point, IEnumerable<Rectangle> screenBounds)
        {
            return screenBounds.Any(screen => screen.Left <= point.X
                && point.X < screen.Right
                && screen.Top <= point.Y
                && point.Y < screen.Bottom);
        }

        private class RecordingCursorController : ICursorController
        {
            public List<Point> Positions { get; } = new List<Point>();

            public void SetPosition(Point point)
            {
                Positions.Add(point);
            }
        }

        private class FixedPointService : IPointService
        {
            private readonly Point _cursorPosition;

            public FixedPointService(Point cursorPosition)
            {
                _cursorPosition = cursorPosition;
            }

            public Point FindAreaCenter(Point p1, Point p2) => throw new NotSupportedException();
            public Point GetRandomPointInArea(Point topleftPoint, Point bottomRightPoint) => throw new NotSupportedException();
            public Point GetRandomPointInArea(CommonTypes.Rect area) => throw new NotSupportedException();
            public Point GetCursorPosition() => _cursorPosition;
            public Point GetCursorPositionOnWindow(IntPtr window) => throw new NotSupportedException();
            public CommonTypes.Rect GetClickAreaPositionOnWindow(IntPtr window, CommonTypes.Rect clickArea) => throw new NotSupportedException();
            public CommonTypes.Rect GetWindowPosition(IntPtr window) => throw new NotSupportedException();
            public CommonTypes.Rect GetRectAroundCenterPoint(Point centerPoint, int areaSize) => throw new NotSupportedException();
        }

        private class FixedTrajectoryPlanner : IHumanMouseTrajectoryPlanner
        {
            private readonly IReadOnlyList<MouseMovementStep> _steps;

            public FixedTrajectoryPlanner(IReadOnlyList<MouseMovementStep> steps)
            {
                _steps = steps;
            }

            public IReadOnlyList<MouseMovementStep> CreateTrajectory(Point start, Point target, MouseMovementProfile profile)
            {
                return _steps;
            }
        }

        private class RecordingTrajectoryPlanner : IHumanMouseTrajectoryPlanner
        {
            public List<TrajectorySegment> Segments { get; } = new List<TrajectorySegment>();

            public IReadOnlyList<MouseMovementStep> CreateTrajectory(Point start, Point target, MouseMovementProfile profile)
            {
                Segments.Add(new TrajectorySegment(start, target));
                return new List<MouseMovementStep> { new MouseMovementStep(target, 0) };
            }
        }

        private class TrajectorySegment
        {
            public TrajectorySegment(Point start, Point target)
            {
                Start = start;
                Target = target;
            }

            public Point Start { get; }
            public Point Target { get; }
        }

        private class FixedScreenService : IScreenService
        {
            private readonly IReadOnlyList<Rectangle> _screenBounds;

            public FixedScreenService(IReadOnlyList<Rectangle> screenBounds)
            {
                _screenBounds = screenBounds;
            }

            public Point GetWorkspaceTopLeftPoint() => throw new NotSupportedException();
            public Size GetTotalScreenSize() => throw new NotSupportedException();
            public IReadOnlyList<Rectangle> GetScreenBounds() => _screenBounds;
        }

        private class TestScriptState : IScriptState
        {
            public bool IsRunning { get; set; } = true;
            public bool IsRegistering { get; set; }
            public int MinimumMouseSpeed { get; set; } = 40;
            public int MaximumMouseSpeed { get; set; } = 60;
            public double SearchPause { get; set; }
            public BindingSource AllEventsSource { get; set; }
            public BindingList<ScriptEvent> AllEvents { get; } = new BindingList<ScriptEvent>();
            public MouseMovementService.MouseMovementMode MouseMovementMode { get; set; } = MouseMovementService.MouseMovementMode.Realistic;
            public bool FreeMouse { get; set; }
            public bool QuickRegistrationEnabled { get; set; }
            public string CurrentAction { get; set; }
            public CommonTypes.Rect LastSavedArea { get; set; }
            public EventProcess LastSelectedProcess { get; set; }
        }

        private class TestRandomService : IRandomService
        {
            public int GetRandomNumber(int min, int max) => min;
            public double GetRandomDouble(double minimum, double maximum) => minimum;
            public BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model) => new BoxPlotResult { Result = model.Target };
        }
    }
}