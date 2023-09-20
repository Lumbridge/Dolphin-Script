using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.WindowsApi;

namespace DolphinScript.Core.Concrete
{
    public class MouseMovementService : IMouseMovementService
    {
        private readonly IRandomService _randomService;
        private readonly IPointService _pointService;
        private readonly IScriptState _scriptState;
        private readonly IMouseMathService _mouseMathService;
        private readonly IColourService _colourService;

        public MouseMovementService(IRandomService randomService, IPointService pointService, IScriptState scriptState, IMouseMathService mouseMathService, IColourService colourService)
        {
            _randomService = randomService;
            _pointService = pointService;
            _scriptState = scriptState;
            _mouseMathService = mouseMathService;
            _colourService = colourService;
        }

        // mouse movement variables
        //
        private double _gravity => _randomService.GetRandomDouble(8.0, 10.0);
        private double _pushForce => _randomService.GetRandomDouble(2.0, 4.0);
        private double _minWait => _randomService.GetRandomDouble(9.0, 11.0);
        private double _maxWait => _randomService.GetRandomDouble(14.0, 16.0);
        private double _maxStep => _randomService.GetRandomDouble(9.0, 11.0);
        private double _targetArea => _randomService.GetRandomDouble(14.0, 16.0);

        /// <summary>
        /// moves the mouse from it's current location to the end point passed in
        /// </summary>
        /// <param name="end"></param>
        public void MoveMouseToPoint(Point end)
        {
            // store the current mouse position to pass into the core mouse move loop
            //
            var start = _pointService.GetCursorPosition();

            // generate a random mouse speed close to the one set on the form
            //
            var randomSpeed = Math.Max((_randomService.GetRandomNumber(0, _scriptState.MinimumMouseSpeed) / 2.0 + _scriptState.MaximumMouseSpeed) / 10.0, 0.1);

            // call the main mouse move loop and pass in the global params
            //
            MouseMoveCoreLoop(
                start,
                end,
                _gravity,
                _pushForce,
                _minWait / randomSpeed,
                _maxWait / randomSpeed,
                _maxStep * randomSpeed,
                _targetArea * randomSpeed);
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
                    break;
            }
        }

        /// <summary>
        /// this function is the core mouse moving method, needs to be cleaned up...
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="gravity"></param>
        /// <param name="pushForce"></param>
        /// <param name="minWait"></param>
        /// <param name="maxWait"></param>
        /// <param name="maxStep"></param>
        /// <param name="targetArea"></param>
        public void MouseMoveCoreLoop(
            Point start,
            Point end,
            double gravity,
            double pushForce,
            double minWait, double maxWait,
            double maxStep, double targetArea)
        {
            double xs = start.X, ys = start.Y;
            double xe = end.X, ye = end.Y;

            double windX = 0, windY = 0, veloX = 0, veloY = 0;
            double veloMag, step;
            int oldY, newX = (int)Math.Round(xs), newY = (int)Math.Round(ys);

            var waitDiff = maxWait - minWait;
            var sqrt2 = Math.Sqrt(2.0);
            var sqrt3 = Math.Sqrt(3.0);
            var sqrt5 = Math.Sqrt(5.0);

            var distanceFromTargetPixel = _mouseMathService.CalculateHypotenuse(xe - xs, ye - ys);

            while (distanceFromTargetPixel > 1.0)
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
                        maxStep = _randomService.GetRandomNumber(0, 3) + 3.0;
                    else
                        maxStep /= sqrt5;
                }

                veloX += windX;
                veloY += windY;
                veloX += gravity * (xe - xs) / distanceFromTargetPixel;
                veloY += gravity * (ye - ys) / distanceFromTargetPixel;

                if (_mouseMathService.CalculateHypotenuse(veloX, veloY) > maxStep)
                {
                    var randomDist = maxStep / 2.0 + _randomService.GetRandomNumber(0, (int)Math.Round(maxStep) / 2);
                    veloMag = _mouseMathService.CalculateHypotenuse(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                var oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                xs += veloX;
                ys += veloY;
                distanceFromTargetPixel = _mouseMathService.CalculateHypotenuse(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);

                if (oldX != newX || oldY != newY)
                    PInvokeReferences.SetCursorPos(newX, newY);

                step = _mouseMathService.CalculateHypotenuse(xs - oldX, ys - oldY);
                var wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);

                Task.WaitAll(Task.Delay(wait));
            }

            var endX = (int)Math.Round(xe);
            var endY = (int)Math.Round(ye);

            if (!_scriptState.IsRunning)
                return;

            if (endX != newX || endY != newY)
                PInvokeReferences.SetCursorPos(endX, endY);
        }
    }
}
