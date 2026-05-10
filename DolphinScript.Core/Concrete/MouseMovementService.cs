using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace DolphinScript.Core.Concrete
{
    public class MouseMovementService : IMouseMovementService
    {
        private readonly IRandomService _randomService;
        private readonly IPointService _pointService;
        private readonly IMouseMathService _mouseMathService;
        private readonly IHumanMouseTrajectoryPlanner _trajectoryPlanner;
        private readonly ICursorController _cursorController;
        private readonly IScreenService _screenService;
        private readonly IScriptState _scriptState;

        public enum MouseMovementMode
        {
            Realistic,
            Linear,
            Teleport
        }

        public MouseMovementService(IRandomService randomService, IPointService pointService, IMouseMathService mouseMathService,
            IHumanMouseTrajectoryPlanner trajectoryPlanner, ICursorController cursorController, IScreenService screenService, IScriptState scriptState)
        {
            _randomService = randomService;
            _pointService = pointService;
            _mouseMathService = mouseMathService;
            _trajectoryPlanner = trajectoryPlanner;
            _cursorController = cursorController;
            _screenService = screenService;
            _scriptState = scriptState;
        }

        public void MoveMouseToPoint(Point target)
        {
            var start = _pointService.GetCursorPosition();
            if (start == target)
            {
                return;
            }

            switch (_scriptState.MouseMovementMode)
            {
                case MouseMovementMode.Realistic:
                    HumanMotorMove(start, target);
                    break;
                case MouseMovementMode.Linear:
                    LinearSmoothMove(start, target);
                    break;
                case MouseMovementMode.Teleport:
                    TeleportMouse(target);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_scriptState.MouseMovementMode), _scriptState.MouseMovementMode, null);
            }
        }

        private void HumanMotorMove(Point start, Point target)
        {
            var screenBounds = _screenService.GetScreenBounds();
            var route = CreateMovementRoute(start, target, screenBounds);
            var profile = CreateMovementProfile();

            for (var index = 0; index < route.Count - 1; index++)
            {
                var segmentStart = route[index];
                var segmentTarget = route[index + 1];
                if (segmentStart == segmentTarget)
                {
                    continue;
                }

                var segmentProfile = index < route.Count - 2 ? CreateTransitionProfile(profile) : profile;
                MoveAlongTrajectory(CreateSegmentTrajectory(segmentStart, segmentTarget, segmentProfile), segmentStart, segmentTarget, screenBounds);
            }
        }

        private MouseMovementProfile CreateMovementProfile()
        {
            return new MouseMovementProfile
            {
                MinimumSpeed = Math.Min(_scriptState.MinimumMouseSpeed, _scriptState.MaximumMouseSpeed),
                MaximumSpeed = Math.Max(_scriptState.MinimumMouseSpeed, _scriptState.MaximumMouseSpeed)
            };
        }

        private static MouseMovementProfile CreateTransitionProfile(MouseMovementProfile profile)
        {
            return new MouseMovementProfile
            {
                MinimumSpeed = profile.MinimumSpeed,
                MaximumSpeed = profile.MaximumSpeed,
                MinimumDelayMilliseconds = profile.MinimumDelayMilliseconds,
                MaximumDelayMilliseconds = profile.MaximumDelayMilliseconds,
                AllowCorrectiveOvershoot = false
            };
        }

        private IReadOnlyList<MouseMovementStep> CreateSegmentTrajectory(Point start, Point target, MouseMovementProfile profile)
        {
            if (_mouseMathService.LineLength(start, target) <= 2.0)
            {
                return new List<MouseMovementStep> { new MouseMovementStep(target, 0) };
            }

            return _trajectoryPlanner.CreateTrajectory(start, target, profile);
        }

        private void MoveAlongTrajectory(IEnumerable<MouseMovementStep> trajectory, Point start, Point target, IReadOnlyList<Rectangle> screenBounds)
        {
            var movementDistance = _mouseMathService.LineLength(start, target);
            var unitX = movementDistance > 0 ? (target.X - start.X) / movementDistance : 0.0;
            var unitY = movementDistance > 0 ? (target.Y - start.Y) / movementDistance : 0.0;
            var previousRawProgress = 0.0;
            var previousVisibleProgress = 0.0;
            var hasPreviousProgress = false;
            Point? lastPosition = null;

            foreach (var step in trajectory)
            {
                if (!_scriptState.IsRunning)
                {
                    return;
                }

                var rawProgress = ProjectProgress(step.Position, start, unitX, unitY);
                var isIntentionalCorrection = hasPreviousProgress && rawProgress < previousRawProgress;
                var minimumProgress = isIntentionalCorrection ? double.NegativeInfinity : previousVisibleProgress;
                var visiblePosition = ResolveVisiblePoint(step.Position, screenBounds, start, unitX, unitY, minimumProgress);
                var visibleProgress = ProjectProgress(visiblePosition, start, unitX, unitY);

                if (!lastPosition.HasValue || visiblePosition != lastPosition.Value)
                {
                    _cursorController.SetPosition(visiblePosition);
                    lastPosition = visiblePosition;
                }

                previousRawProgress = rawProgress;
                previousVisibleProgress = visibleProgress;
                hasPreviousProgress = true;

                if (step.DelayMilliseconds > 0)
                {
                    Thread.Sleep(step.DelayMilliseconds);
                }
            }
        }

        private static IReadOnlyList<Point> CreateMovementRoute(Point start, Point target, IReadOnlyList<Rectangle> screenBounds)
        {
            if (screenBounds == null || screenBounds.Count == 0)
            {
                return new List<Point> { start, target };
            }

            var startScreen = FindContainingScreen(start, screenBounds);
            var targetScreen = FindContainingScreen(target, screenBounds);
            if (!startScreen.HasValue || !targetScreen.HasValue || startScreen.Value == targetScreen.Value)
            {
                return new List<Point> { start, target };
            }

            if (!TryCreateSeamCrossing(start, target, startScreen.Value, targetScreen.Value, out var startSeam, out var targetSeam))
            {
                return new List<Point> { start, target };
            }

            return CreateDistinctRoute(start, startSeam, targetSeam, target);
        }

        private static Rectangle? FindContainingScreen(Point point, IEnumerable<Rectangle> screenBounds)
        {
            foreach (var screen in screenBounds)
            {
                if (screen.Left <= point.X && point.X < screen.Right && screen.Top <= point.Y && point.Y < screen.Bottom)
                {
                    return screen;
                }
            }

            return null;
        }

        private static bool TryCreateSeamCrossing(Point start, Point target, Rectangle startScreen, Rectangle targetScreen,
            out Point startSeam, out Point targetSeam)
        {
            if (TryCreateHorizontalSeamCrossing(start, target, startScreen, targetScreen, out startSeam, out targetSeam))
            {
                return true;
            }

            return TryCreateVerticalSeamCrossing(start, target, startScreen, targetScreen, out startSeam, out targetSeam);
        }

        private static bool TryCreateHorizontalSeamCrossing(Point start, Point target, Rectangle startScreen, Rectangle targetScreen,
            out Point startSeam, out Point targetSeam)
        {
            startSeam = default;
            targetSeam = default;

            var targetIsRight = startScreen.Right == targetScreen.Left;
            var targetIsLeft = targetScreen.Right == startScreen.Left;
            if (!targetIsRight && !targetIsLeft)
            {
                return false;
            }

            var overlapTop = Math.Max(startScreen.Top, targetScreen.Top);
            var overlapBottom = Math.Min(startScreen.Bottom, targetScreen.Bottom) - 1;
            if (overlapTop > overlapBottom)
            {
                return false;
            }

            var boundaryX = targetIsRight ? startScreen.Right : startScreen.Left;
            var seamY = Clamp(EstimateLineYAtX(start, target, boundaryX), overlapTop, overlapBottom);

            startSeam = targetIsRight
                ? new Point(startScreen.Right - 1, seamY)
                : new Point(startScreen.Left, seamY);
            targetSeam = targetIsRight
                ? new Point(targetScreen.Left, seamY)
                : new Point(targetScreen.Right - 1, seamY);

            return true;
        }

        private static bool TryCreateVerticalSeamCrossing(Point start, Point target, Rectangle startScreen, Rectangle targetScreen,
            out Point startSeam, out Point targetSeam)
        {
            startSeam = default;
            targetSeam = default;

            var targetIsBelow = startScreen.Bottom == targetScreen.Top;
            var targetIsAbove = targetScreen.Bottom == startScreen.Top;
            if (!targetIsBelow && !targetIsAbove)
            {
                return false;
            }

            var overlapLeft = Math.Max(startScreen.Left, targetScreen.Left);
            var overlapRight = Math.Min(startScreen.Right, targetScreen.Right) - 1;
            if (overlapLeft > overlapRight)
            {
                return false;
            }

            var boundaryY = targetIsBelow ? startScreen.Bottom : startScreen.Top;
            var seamX = Clamp(EstimateLineXAtY(start, target, boundaryY), overlapLeft, overlapRight);

            startSeam = targetIsBelow
                ? new Point(seamX, startScreen.Bottom - 1)
                : new Point(seamX, startScreen.Top);
            targetSeam = targetIsBelow
                ? new Point(seamX, targetScreen.Top)
                : new Point(seamX, targetScreen.Bottom - 1);

            return true;
        }

        private static int EstimateLineYAtX(Point start, Point target, int x)
        {
            if (target.X == start.X)
            {
                return start.Y;
            }

            return (int)Math.Round(start.Y + (x - start.X) * (target.Y - start.Y) / (double)(target.X - start.X));
        }

        private static int EstimateLineXAtY(Point start, Point target, int y)
        {
            if (target.Y == start.Y)
            {
                return start.X;
            }

            return (int)Math.Round(start.X + (y - start.Y) * (target.X - start.X) / (double)(target.Y - start.Y));
        }

        private static IReadOnlyList<Point> CreateDistinctRoute(params Point[] points)
        {
            var route = new List<Point>();
            foreach (var point in points)
            {
                if (route.Count == 0 || route[route.Count - 1] != point)
                {
                    route.Add(point);
                }
            }

            return route;
        }

        private void LinearSmoothMove(Point start, Point target)
        {
            var route = CreateMovementRoute(start, target, _screenService.GetScreenBounds());
            for (var index = 0; index < route.Count - 1; index++)
            {
                LinearSmoothMoveSegment(route[index], route[index + 1]);
            }
        }

        private void LinearSmoothMoveSegment(Point start, Point target)
        {
            var totalDistance = _mouseMathService.LineLength(start, target);
            if (totalDistance <= 1)
            {
                TeleportMouse(target);
                return;
            }

            PointF currentPoint = start;
            PointF slope = new PointF(target.X - start.X, target.Y - start.Y);

            var stepsBoxPlotResult = _randomService.GetRandomNumberBoxPlot(new BoxPlotModel
            {
                Target = totalDistance,
                LowerBoundPercentile = 10,
                UpperBoundPercentile = 10,
                OutlierPercentageChance = 20,
                OutlierSkewPercentage = 20
            });

            var steps = Math.Max((int)Math.Round(stepsBoxPlotResult.Result) / 2, 1);

            slope.X /= steps;
            slope.Y /= steps;

            for (var i = 0; i < steps; i++)
            {
                if (!_scriptState.IsRunning)
                {
                    return;
                }

                currentPoint = new PointF(currentPoint.X + slope.X, currentPoint.Y + slope.Y);
                SetVisiblePosition(Point.Round(currentPoint));
                Thread.Sleep(1);
            }

            TeleportMouse(target);
        }

        private void TeleportMouse(Point target)
        {
            if (_scriptState.IsRunning)
            {
                SetVisiblePosition(target);
            }
        }

        private void SetVisiblePosition(Point point)
        {
            _cursorController.SetPosition(ResolveVisiblePoint(point, _screenService.GetScreenBounds(), point, 0.0, 0.0, double.NegativeInfinity));
        }

        private static Point ResolveVisiblePoint(Point point, IReadOnlyList<Rectangle> screenBounds, Point progressOrigin,
            double unitX, double unitY, double minimumProgress)
        {
            if (screenBounds == null || screenBounds.Count == 0)
            {
                return point;
            }

            if (IsOnScreen(point, screenBounds) && ProjectProgress(point, progressOrigin, unitX, unitY) >= minimumProgress)
            {
                return point;
            }

            var candidates = screenBounds
                .Select(screen => ClampToScreen(point, screen))
                .Distinct()
                .ToList();

            var forwardCandidates = candidates
                .Where(candidate => ProjectProgress(candidate, progressOrigin, unitX, unitY) >= minimumProgress)
                .ToList();

            var candidatePool = forwardCandidates.Count > 0 ? forwardCandidates : candidates;
            return candidatePool
                .OrderBy(candidate => DistanceSquared(point, candidate))
                .First();
        }

        private static bool IsOnScreen(Point point, IEnumerable<Rectangle> screenBounds)
        {
            return screenBounds.Any(screen => screen.Left <= point.X
                && point.X < screen.Right
                && screen.Top <= point.Y
                && point.Y < screen.Bottom);
        }

        private static Point ClampToScreen(Point point, Rectangle screen)
        {
            return new Point(
                Math.Max(screen.Left, Math.Min(point.X, screen.Right - 1)),
                Math.Max(screen.Top, Math.Min(point.Y, screen.Bottom - 1)));
        }

        private static double ProjectProgress(Point point, Point origin, double unitX, double unitY)
        {
            return (point.X - origin.X) * unitX + (point.Y - origin.Y) * unitY;
        }

        private static int DistanceSquared(Point start, Point target)
        {
            var deltaX = start.X - target.X;
            var deltaY = start.Y - target.Y;
            return deltaX * deltaX + deltaY * deltaY;
        }

        private static int Clamp(int value, int minimum, int maximum)
        {
            return Math.Max(minimum, Math.Min(value, maximum));
        }
    }
}
