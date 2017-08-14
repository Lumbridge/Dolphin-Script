using System;
using System.Threading.Tasks;

using static RASL.Lib.Backend.MouseMoveMaths;
using static RASL.Lib.Backend.PointReturns;
using static RASL.Lib.Backend.RandomNumber;
using static RASL.Lib.Backend.GlobalVariables;
using static RASL.Lib.Backend.WinAPI;

namespace RASL.Lib.ScriptEventClasses
{
    class MouseMove : ScriptEvent
    {
        // mouse movement variables
        private static double
            _Gravity = 9.0,
            _PushForce = 3.0,
            _MinWait = 10.0,
            _MaxWait = 15.0,
            _MaxStep = 10.0,
            _TargetArea = 15.0;

        public MouseMove()
        {
            EventType = Event.Mouse_Move;
        }

        public static void MoveMouse(POINT end)
        {
            POINT start = GetCursorPosition();

            double randomSpeed = Math.Max((GetRandomNumber(0, MouseSpeed) / 2.0 + MouseSpeed) / 10.0, 0.1);

            MouseMoveCoreLoop(
                start,
                end,
                _Gravity,
                _PushForce,
                _MinWait / randomSpeed,
                _MaxWait / randomSpeed,
                _MaxStep * randomSpeed,
                _TargetArea * randomSpeed);
        }

        public static void MouseMoveCoreLoop(
            POINT start,
            POINT end,
            double gravity,
            double pushForce,
            double minWait, double maxWait,
            double maxStep, double targetArea)
        {
            double xs = start.X, ys = start.Y;
            double xe = end.X, ye = end.Y;

            double dist, windX = 0, windY = 0, veloX = 0, veloY = 0, randomDist, veloMag, step;
            int oldX, oldY, newX = (int)Math.Round(xs), newY = (int)Math.Round(ys);

            double waitDiff = maxWait - minWait;
            double sqrt2 = Math.Sqrt(2.0);
            double sqrt3 = Math.Sqrt(3.0);
            double sqrt5 = Math.Sqrt(5.0);

            dist = Hypot(xe - xs, ye - ys);

            while (dist > 1.0)
            {
                if (GetAsyncKeyState(VirtualKeyStates.VK_F5) < 0)
                {
                    IsRunning = false;
                    Write("Status: Idle");
                    return;
                }

                pushForce = Math.Min(pushForce, dist);

                if (dist >= targetArea)
                {
                    int w = GetRandomNumber(0,(int)Math.Round(pushForce) * 2 + 1);
                    windX = windX / sqrt3 + (w - pushForce) / sqrt5;
                    windY = windY / sqrt3 + (w - pushForce) / sqrt5;
                }
                else
                {
                    windX = windX / sqrt2;
                    windY = windY / sqrt2;
                    if (maxStep < 3)
                        maxStep = GetRandomNumber(0,3) + 3.0;
                    else
                        maxStep = maxStep / sqrt5;
                }

                veloX += windX;
                veloY += windY;
                veloX = veloX + gravity * (xe - xs) / dist;
                veloY = veloY + gravity * (ye - ys) / dist;

                if (Hypot(veloX, veloY) > maxStep)
                {
                    randomDist = maxStep / 2.0 + GetRandomNumber(0,(int)Math.Round(maxStep) / 2);
                    veloMag = Hypot(veloX, veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                xs += veloX;
                ys += veloY;
                dist = Hypot(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);

                if (oldX != newX || oldY != newY)
                    SetCursorPos(newX, newY);

                step = Hypot(xs - oldX, ys - oldY);
                int wait = (int)Math.Round(waitDiff * (step / maxStep) + minWait);

                Task.WaitAll(Task.Delay(wait));
            }

            int endX = (int)Math.Round(xe);
            int endY = (int)Math.Round(ye);

            if (!IsRunning)
                return;
            else if (endX != newX || endY != newY)
                SetCursorPos(endX, endY);
        }

        public override void DoEvent()
        {
            MoveMouse(DestinationPoint);

            base.DoEvent();
        }

        public override string GetEventListBoxString()
        {
            if (GroupID == -1)
                return "Move mouse to Point X: " + DestinationPoint.X + " Y: " + DestinationPoint.Y + ".";
            else
                return "[Group " + GroupID + "] Move mouse to Point X: " + DestinationPoint.X + " Y: " + DestinationPoint.Y + ".";
        }
    }
}