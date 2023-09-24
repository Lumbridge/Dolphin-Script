using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;
using System;
using System.Drawing;
using System.Threading.Tasks;
using DolphinScript.Core.Classes;
using System.Threading;
using DolphinScript.Core.Models;

namespace DolphinScript.Core.Concrete
{
    public class MouseMovementService : IMouseMovementService
    {
        private readonly IRandomService _randomService;
        private readonly IPointService _pointService;
        private readonly IMouseMathService _mouseMathService;
        private readonly IColourService _colourService;

        public enum MouseMovementMode
        {
            Realistic,
            Linear,
            Teleport
        }

        public MouseMovementService(IRandomService randomService, IPointService pointService, IMouseMathService mouseMathService, IColourService colourService)
        {
            _randomService = randomService;
            _pointService = pointService;
            _mouseMathService = mouseMathService;
            _colourService = colourService;
        }

        // mouse movement variables
        private double Gravity => _randomService.GetRandomDouble(8.0, 10.0); // default 9.0
        private double PushForce => _randomService.GetRandomDouble(2.0, 4.0); // default 3.0
        private double MinWait => _randomService.GetRandomDouble(9.0, 11.0); // default 8.0
        private double MaxWait => _randomService.GetRandomDouble(14.0, 16.0); // default 15.0
        private double MaxStep => _randomService.GetRandomDouble(9.0, 11.0); // default 10.0
        private double TargetArea => _randomService.GetRandomDouble(13.0, 15.0); // default 10.0

        /// <summary>
        /// moves the mouse from it's current location to the target point passed in
        /// </summary>
        /// <param name="target"></param>
        /// <param name="mode">mouse movement mode to use</param>
        public void MoveMouseToPoint(Point target)
        {
            // store the current mouse position to pass into the core mouse move loop
            var start = _pointService.GetCursorPosition();

            // generate a random mouse speed close to the one set on the form
            var randomSpeed = Math.Max((_randomService.GetRandomNumber(0, ScriptState.MinimumMouseSpeed) / 2.0 + ScriptState.MaximumMouseSpeed) / 10.0, 0.1);

            // call the main mouse move loop and pass in the global params
            switch (ScriptState.MouseMovementMode)
            {
                case MouseMovementMode.Realistic:
                    WindMouse(
                        start,
                        target,
                        Gravity,
                        PushForce,
                        MinWait / randomSpeed,
                        MaxWait / randomSpeed,
                        MaxStep * randomSpeed,
                        TargetArea * randomSpeed);
                    break;
                case MouseMovementMode.Linear:
                    LinearSmoothMove(target);
                    break;
                case MouseMovementMode.Teleport:
                    TeleportMouse(target);
                    break;
                default:    
                    throw new ArgumentOutOfRangeException(nameof(ScriptState.MouseMovementMode), ScriptState.MouseMovementMode, null);
            }
            
        }

        /// <summary>
        /// Moves mouse to a colour in a given search area
        /// </summary>
        /// <param name="searchArea"></param>
        /// <param name="searchColour"></param>
        /// <returns></returns>
        public void MoveMouseToColour(CommonTypes.Rect searchArea, int searchColour)
        {
            var temp = _colourService.GetMatchingPixelList(searchArea, searchColour); // add matching colour pixels to list
            var pos = _pointService.GetCursorPosition();

            while (_colourService.GetColourAtPoint(pos).ToArgb() != searchColour)
            {
                if (temp.Count > 0)
                {
                    temp = _colourService.GetMatchingPixelList(searchArea, searchColour); // add matching colour pixels to list

                    var endPoint = temp[_randomService.GetRandomNumber(0, temp.Count - 1)];

                    MoveMouseToPoint(endPoint); // move mouse to picked pixel

                    PInvokeReferences.GetCursorPos(out pos);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// this function is the core mouse moving method, needs to be cleaned up...
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <param name="gravity"></param>
        /// <param name="pushForce"></param>
        /// <param name="minWait"></param>
        /// <param name="maxWait"></param>
        /// <param name="maxStep"></param>
        /// <param name="targetArea"></param>
        private void WindMouse(
            Point start,
            Point target,
            double gravity,
            double pushForce,
            double minWait, double maxWait,
            double maxStep, double targetArea)
        {
            double xStart = start.X, yStart = start.Y;
            double xEnd = target.X, yEnd = target.Y;

            double windX = 0, windY = 0, veloX = 0, veloY = 0;
            int newX = (int)Math.Round(xStart), newY = (int)Math.Round(yStart);

            var waitDiff = maxWait - minWait;
            var sqrt2 = Math.Sqrt(2.0);
            var sqrt3 = Math.Sqrt(3.0);
            var sqrt5 = Math.Sqrt(5.0);

            var distanceFromTargetPixel = _mouseMathService.CalculateHypotenuse(start, target);

            while (distanceFromTargetPixel > 1.0 && ScriptState.IsRunning)
            {
                pushForce = Math.Min(pushForce, distanceFromTargetPixel);

                if (distanceFromTargetPixel >= targetArea)
                {
                    var w = _randomService.GetRandomNumber(0, (int)Math.Round(pushForce) * 2 + 1);
                    windX = windX / sqrt3 + (w - pushForce) / sqrt5;
                    windY = windY / sqrt3 + (w - pushForce) / sqrt5;
                }
                else
                {
                    windX /= sqrt2;
                    windY /= sqrt2;
                    if (maxStep < 3)
                    {
                        maxStep = _randomService.GetRandomNumber(0, 3) + 3.0;
                    }
                    else
                    {
                        maxStep /= sqrt5;
                    }
                }

                veloX += windX;
                veloY += windY;
                veloX += gravity * (xEnd - xStart) / distanceFromTargetPixel;
                veloY += gravity * (yEnd - yStart) / distanceFromTargetPixel;

                if (_mouseMathService.CalculateHypotenuse(veloX, veloY) > maxStep)
                {
                    var randomDist = maxStep / 2.0 + _randomService.GetRandomNumber(0, (int)Math.Round(maxStep) / 2);
                    var veloMag = _mouseMathService.CalculateHypotenuse(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                var oldX = (int)Math.Round(xStart);
                var oldY = (int)Math.Round(yStart);

                xStart += veloX;
                yStart += veloY;

                distanceFromTargetPixel = _mouseMathService.CalculateHypotenuse(xEnd - xStart, yEnd - yStart);

                newX = (int)Math.Round(xStart);
                newY = (int)Math.Round(yStart);

                if (oldX != newX || oldY != newY)
                {
                    PInvokeReferences.SetCursorPos(newX, newY);
                }

                var step = _mouseMathService.CalculateHypotenuse(xStart - oldX, yStart - oldY);
                var wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);

                Task.WaitAll(Task.Delay(wait));
            }

            var endX = (int)Math.Round(xEnd);
            var endY = (int)Math.Round(yEnd);

            if (endX != newX || endY != newY)
            {
                PInvokeReferences.SetCursorPos(endX, endY);
            }
        }

        private void LinearSmoothMove(Point target)
        {
            Point start = _pointService.GetCursorPosition();
            
            var totalDistance = _mouseMathService.LineLength(start, target);

            PointF currentPoint = start;

            // Find the slope of the line segment defined by start and newPosition
            PointF slope = new PointF(target.X - start.X, target.Y - start.Y);

            var stepsBoxPlotResult = _randomService.GetRandomNumberBoxPlot(new BoxPlotModel
            {
                Target = totalDistance,
                LowerBoundPercentile = 10,
                UpperBoundPercentile = 10,
                OutlierPercentageChance = 20,
                OutlierSkewPercentage = 20
            });

            var steps = (int)Math.Round(stepsBoxPlotResult.Result) / 2;

            // Divide by the number of steps
            slope.X /= steps;
            slope.Y /= steps;

            // Move the mouse to each iterative point.
            for (var i = 0; i < steps; i++)
            {
                if (!ScriptState.IsRunning)
                {
                    return;
                }

                currentPoint = new PointF(currentPoint.X + slope.X, currentPoint.Y + slope.Y);
                PInvokeReferences.SetCursorPos(Point.Round(currentPoint));
                Thread.Sleep(1);
            }

            // Move the mouse to the final destination.
            PInvokeReferences.SetCursorPos(target);
        }

        private void TeleportMouse(Point target)
        {
            PInvokeReferences.SetCursorPos(target);
        }
    }
}
