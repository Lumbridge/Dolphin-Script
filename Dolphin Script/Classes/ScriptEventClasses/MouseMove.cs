using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static DolphinScript.Classes.Backend.Common;
using static DolphinScript.Classes.Backend.MouseMoveMath;
using static DolphinScript.Classes.Backend.PointReturns;
using static DolphinScript.Classes.Backend.RandomNumber;
using static DolphinScript.Classes.Backend.WinApi;

namespace DolphinScript.Classes.ScriptEventClasses
{
    /// <summary>
    /// This class provides the core mouse moving functionality.
    /// </summary>
    [Serializable]
    public class MouseMove : ScriptEvent
    {
        // mouse movement variables
        //
        private static double
            _gravity = GetRandomDouble(8.0, 10.0),
            _pushForce = GetRandomDouble(2.0, 4.0),
            _minWait = GetRandomDouble(9.0, 11.0),
            _maxWait = GetRandomDouble(14.0, 16.0),
            _maxStep = GetRandomDouble(9.0, 11.0),
            _targetArea = GetRandomDouble(14.0, 16.0);

        /// <summary>
        /// moves the mouse from it's current location to the end point passed in
        /// </summary>
        /// <param name="end"></param>
        public static void MoveMouse(Point end)
        {
            // store the current mouse position to pass into the core mouse move loop
            //
            var start = GetCursorPosition();

            // generate a random mouse speed close to the one set on the form
            //
            var randomSpeed = Math.Max((GetRandomNumber(0, MinMouseSpeed) / 2.0 + MaxMouseSpeed) / 10.0, 0.1);

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
        public static void MouseMoveCoreLoop(
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

            var distanceFromTargetPixel = CalculateHypotenuse(xe - xs, ye - ys);

            while (distanceFromTargetPixel > 1.0)
            {
                pushForce = Math.Min(pushForce, distanceFromTargetPixel);

                if (distanceFromTargetPixel >= targetArea)
                {
                    var w = GetRandomNumber(0,(int)Math.Round(pushForce) * 2 + 1);
                    windX = windX / sqrt3 + (w - pushForce) / sqrt5;
                    windY = windY / sqrt3 + (w - pushForce) / sqrt5;
                }
                else
                {
                    windX /= sqrt2;
                    windY /= sqrt2;
                    if (maxStep < 3)
                        maxStep = GetRandomNumber(0,3) + 3.0;
                    else
                        maxStep /= sqrt5;
                }

                veloX += windX;
                veloY += windY;
                veloX += gravity * (xe - xs) / distanceFromTargetPixel;
                veloY += gravity * (ye - ys) / distanceFromTargetPixel;

                if (CalculateHypotenuse(veloX, veloY) > maxStep)
                {
                    var randomDist = maxStep / 2.0 + GetRandomNumber(0, (int)Math.Round(maxStep) / 2);
                    veloMag = CalculateHypotenuse(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                var oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                xs += veloX;
                ys += veloY;
                distanceFromTargetPixel = CalculateHypotenuse(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);

                if (oldX != newX || oldY != newY)
                    SetCursorPos(newX, newY);

                step = CalculateHypotenuse(xs - oldX, ys - oldY);
                var wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);

                Task.WaitAll(Task.Delay(wait));
            }

            var endX = (int)Math.Round(xe);
            var endY = (int)Math.Round(ye);

            if (!IsRunning)
                return;

            if (endX != newX || endY != newY)
                SetCursorPos(endX, endY);
        }

        /// <summary>
        /// main overriden method used to perform this script event
        /// </summary>
        public override void Invoke()
        {
            // update the status label on the main form
            //
            Status = $"Mouse move: {CoordsToMoveTo}.";

            MoveMouse(CoordsToMoveTo);
        }

        /// <summary>
        /// returns a string which is added to the listbox to give information about the event which was added to the event list
        /// </summary>
        /// <returns></returns>
        public override string GetEventListBoxString()
        {
            if (GroupId == -1)
                return "Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + ".";
            return "[Group " + GroupId + " Repeat x" + NumberOfCycles + "] Move mouse to Point X: " + CoordsToMoveTo.X + " Y: " + CoordsToMoveTo.Y + ".";
        }

        /// <summary>
        /// Imported method which allows us to set the position of the mouse cursor.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);
    }
}