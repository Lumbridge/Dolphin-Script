using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DolphinScript.Core.Concrete
{
    public class HumanMouseTrajectoryPlanner : IHumanMouseTrajectoryPlanner
    {
        private readonly IRandomService _randomService;

        public HumanMouseTrajectoryPlanner(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public IReadOnlyList<MouseMovementStep> CreateTrajectory(Point start, Point target, MouseMovementProfile profile)
        {
            var distance = Distance(start, target);
            if (distance < 1.0)
            {
                return new List<MouseMovementStep> { new MouseMovementStep(target, 0) };
            }

            var speed = _randomService.GetRandomDouble(
                Math.Min(profile.MinimumSpeed, profile.MaximumSpeed),
                Math.Max(profile.MinimumSpeed, profile.MaximumSpeed));

            var durationMilliseconds = EstimateDurationMilliseconds(distance, speed);
            var trajectory = new List<MouseMovementStep>();

            if (ShouldOvershoot(distance, profile))
            {
                var overshootTarget = CreateOvershootTarget(start, target, distance);
                var primaryDuration = durationMilliseconds * _randomService.GetRandomDouble(0.72, 0.86);
                trajectory.AddRange(CreateSegment(start, overshootTarget, primaryDuration, profile, false));
                trajectory.AddRange(CreateSegment(overshootTarget, target, durationMilliseconds - primaryDuration + _randomService.GetRandomDouble(45.0, 115.0), profile, true));
            }
            else
            {
                trajectory.AddRange(CreateSegment(start, target, durationMilliseconds, profile, false));
            }

            if (trajectory.Count == 0 || trajectory.Last().Position != target)
            {
                trajectory.Add(new MouseMovementStep(target, 0));
            }

            return trajectory;
        }

        private double EstimateDurationMilliseconds(double distance, double speed)
        {
            var targetWidth = _randomService.GetRandomDouble(8.0, 18.0);
            var indexOfDifficulty = Math.Log(distance / targetWidth + 1.0, 2.0);
            var speedRatio = Clamp(speed / 100.0, 0.0, 1.0);
            var motorTempo = Lerp(1.45, 0.52, speedRatio);
            var baseDuration = 75.0 + 132.0 * indexOfDifficulty + Math.Sqrt(distance) * 4.0;

            return Clamp(baseDuration * motorTempo * _randomService.GetRandomDouble(0.88, 1.14), 75.0, 2200.0);
        }

        private bool ShouldOvershoot(double distance, MouseMovementProfile profile)
        {
            if (!profile.AllowCorrectiveOvershoot || distance < 120.0)
            {
                return false;
            }

            var probability = Lerp(0.18, 0.48, Clamp(distance / 1200.0, 0.0, 1.0));
            return _randomService.GetRandomDouble(0.0, 1.0) < probability;
        }

        private Point CreateOvershootTarget(Point start, Point target, double distance)
        {
            var unitX = (target.X - start.X) / distance;
            var unitY = (target.Y - start.Y) / distance;
            var normalX = -unitY;
            var normalY = unitX;

            var maximumOvershoot = Math.Min(22.0, Math.Max(3.0, distance * 0.04));
            var overshoot = _randomService.GetRandomDouble(2.0, maximumOvershoot);
            var lateralCorrection = _randomService.GetRandomDouble(-overshoot, overshoot);
            var longitudinalTarget = new Point(
                (int)Math.Round(target.X + unitX * overshoot),
                (int)Math.Round(target.Y + unitY * overshoot));

            return ClampToMovementBounds(new Point(
                (int)Math.Round(longitudinalTarget.X + normalX * lateralCorrection),
                (int)Math.Round(longitudinalTarget.Y + normalY * lateralCorrection)), start, longitudinalTarget);
        }

        private IEnumerable<MouseMovementStep> CreateSegment(Point start, Point target, double durationMilliseconds, MouseMovementProfile profile, bool isCorrection)
        {
            var distance = Distance(start, target);
            if (distance < 1.0)
            {
                yield return new MouseMovementStep(target, 0);
                yield break;
            }

            var sampleInterval = _randomService.GetRandomDouble(6.5, 11.5);
            var steps = Clamp((int)Math.Round(durationMilliseconds / sampleInterval), isCorrection ? 5 : 8, 260);

            var controlPoints = CreateControlPoints(start, target, distance, isCorrection);
            var noiseX = 0.0;
            var noiseY = 0.0;
            var tremorScale = Math.Min(isCorrection ? 1.35 : 3.25, Math.Max(0.20, distance * (isCorrection ? 0.006 : 0.012)));
            var unitX = (target.X - start.X) / distance;
            var unitY = (target.Y - start.Y) / distance;
            var previousProgress = 0.0;

            for (var stepIndex = 1; stepIndex <= steps; stepIndex++)
            {
                var t = stepIndex / (double)steps;
                var easedT = MinimumJerk(t);
                var basePoint = CubicBezier(start, controlPoints.controlOne, controlPoints.controlTwo, target, easedT);

                noiseX = noiseX * 0.72 + _randomService.GetRandomDouble(-1.0, 1.0) * tremorScale;
                noiseY = noiseY * 0.72 + _randomService.GetRandomDouble(-1.0, 1.0) * tremorScale;

                var endpointTaper = Math.Sin(Math.PI * t);
                var position = stepIndex == steps
                    ? target
                    : CreateBoundedProgressPoint(basePoint, noiseX, noiseY, endpointTaper, start, target, unitX, unitY, ref previousProgress);

                var delay = Clamp(
                    (int)Math.Round(durationMilliseconds / steps * _randomService.GetRandomDouble(0.72, 1.28)),
                    profile.MinimumDelayMilliseconds,
                    profile.MaximumDelayMilliseconds);

                yield return new MouseMovementStep(position, delay);
            }
        }

        private (PointF controlOne, PointF controlTwo) CreateControlPoints(Point start, Point target, double distance, bool isCorrection)
        {
            var unitX = (target.X - start.X) / distance;
            var unitY = (target.Y - start.Y) / distance;
            var normalX = -unitY;
            var normalY = unitX;
            var lateralSign = _randomService.GetRandomNumber(0, 1) == 0 ? -1.0 : 1.0;
            var lateralMagnitude = distance * _randomService.GetRandomDouble(isCorrection ? 0.012 : 0.045, isCorrection ? 0.055 : 0.16);

            lateralMagnitude = Math.Min(lateralMagnitude, isCorrection ? 28.0 : 150.0);

            var controlOneT = _randomService.GetRandomDouble(0.22, 0.38);
            var controlTwoT = _randomService.GetRandomDouble(0.62, 0.82);
            var counterCurve = _randomService.GetRandomDouble(0.35, 0.90);

            var controlOne = new PointF(
                (float)(start.X + unitX * distance * controlOneT + normalX * lateralMagnitude * lateralSign),
                (float)(start.Y + unitY * distance * controlOneT + normalY * lateralMagnitude * lateralSign));

            var controlTwo = new PointF(
                (float)(start.X + unitX * distance * controlTwoT - normalX * lateralMagnitude * lateralSign * counterCurve),
                (float)(start.Y + unitY * distance * controlTwoT - normalY * lateralMagnitude * lateralSign * counterCurve));

            return (controlOne, controlTwo);
        }

        private static PointF CubicBezier(Point start, PointF controlOne, PointF controlTwo, Point target, double t)
        {
            var inverseT = 1.0 - t;
            var x = inverseT * inverseT * inverseT * start.X
                + 3.0 * inverseT * inverseT * t * controlOne.X
                + 3.0 * inverseT * t * t * controlTwo.X
                + t * t * t * target.X;

            var y = inverseT * inverseT * inverseT * start.Y
                + 3.0 * inverseT * inverseT * t * controlOne.Y
                + 3.0 * inverseT * t * t * controlTwo.Y
                + t * t * t * target.Y;

            return new PointF((float)x, (float)y);
        }

        private static double MinimumJerk(double t)
        {
            return 10.0 * Math.Pow(t, 3.0) - 15.0 * Math.Pow(t, 4.0) + 6.0 * Math.Pow(t, 5.0);
        }

        private static double Distance(Point start, Point target)
        {
            var x = target.X - start.X;
            var y = target.Y - start.Y;
            return Math.Sqrt(x * x + y * y);
        }

        private static Point ClampToMovementBounds(Point point, Point start, Point target)
        {
            return new Point(
                Clamp(point.X, Math.Min(start.X, target.X), Math.Max(start.X, target.X)),
                Clamp(point.Y, Math.Min(start.Y, target.Y), Math.Max(start.Y, target.Y)));
        }

        private static Point CreateBoundedProgressPoint(PointF basePoint, double noiseX, double noiseY,
            double endpointTaper, Point start, Point target, double unitX, double unitY, ref double previousProgress)
        {
            var noisyPoint = new Point(
                (int)Math.Round(basePoint.X + noiseX * endpointTaper),
                (int)Math.Round(basePoint.Y + noiseY * endpointTaper));

            var boundedPoint = ClampToMovementBounds(noisyPoint, start, target);
            return KeepProgressMonotonic(boundedPoint, start, target, unitX, unitY, ref previousProgress);
        }

        private static Point KeepProgressMonotonic(Point point, Point start, Point target, double unitX, double unitY, ref double previousProgress)
        {
            var progress = ProjectProgress(point, start, unitX, unitY);
            if (progress < previousProgress)
            {
                var correction = previousProgress - progress;
                point = ClampToMovementBounds(new Point(
                    (int)Math.Round(point.X + unitX * correction),
                    (int)Math.Round(point.Y + unitY * correction)), start, target);
                progress = ProjectProgress(point, start, unitX, unitY);
            }

            previousProgress = Math.Max(previousProgress, progress);
            return point;
        }

        private static double ProjectProgress(Point point, Point start, double unitX, double unitY)
        {
            return (point.X - start.X) * unitX + (point.Y - start.Y) * unitY;
        }

        private static double Lerp(double start, double end, double amount)
        {
            return start + (end - start) * amount;
        }

        private static double Clamp(double value, double minimum, double maximum)
        {
            return Math.Max(minimum, Math.Min(maximum, value));
        }

        private static int Clamp(int value, int minimum, int maximum)
        {
            return Math.Max(minimum, Math.Min(maximum, value));
        }
    }
}