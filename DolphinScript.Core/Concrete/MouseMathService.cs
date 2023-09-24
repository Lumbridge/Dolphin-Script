using DolphinScript.Core.Interfaces;
using System.Drawing;
using System;

namespace DolphinScript.Core.Concrete
{
    public class MouseMathService : IMouseMathService
    {
        private readonly IRandomService _randomService;

        public MouseMathService(IRandomService randomService)
        {
            _randomService = randomService;
        }

        /// <summary>
        /// returns the hypotenuse of two double points
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public double CalculateHypotenuse(double x, double y)
        {
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// returns the hypotenuse of two points
        /// </summary>
        /// <returns></returns>
        public double CalculateHypotenuse(Point p1, Point p2)
        {
            var x = p2.X - p1.X;
            var y = p2.Y - p1.Y;
            return Math.Sqrt(x * x + y * y);
        }

        /// <summary>
        /// Returns the length between two points (Euclidean calc)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public double LineLength(Point a, Point b)
        {
            return Math.Sqrt((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y));
        }

        /// <summary>
        /// Returns the angle of the line (direction)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double PointDirection(Point a, Point b)
        {
            return Math.Atan2(b.Y + a.Y, a.X + b.X);
        }

        /// <summary>
        /// Returns the cosine of the line on on the X axis using distance and direction
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private double LengthDirX(double distance, double direction)
        {
            return Math.Cos(direction) * distance;
        }

        /// <summary>
        /// Returns the cosine of the line on on the Y axis using distance and direction
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private double LengthDirY(double distance, double direction)
        {
            return Math.Sin(direction) * distance;
        }

        /// <summary>
        /// This method generates the next point in a curved path between two points
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Point GetPointCurve(Point a, Point b)
        {
            var p = 0.12;

            // calculate the remaining length between point A and B
            //
            var l = LineLength(a, b);

            var c = new Point((int)(p * b.X - p * a.X + b.X), (int)(p * b.Y - p * a.Y + b.Y));
            var d = new Point((int)(p * a.Y - p * b.X + a.X), (int)(p * a.Y - p * b.Y + a.Y));

            // calculate the direction of the path movement
            //
            var dir = PointDirection(b, a);

            var e = new Point((int)(c.X + LengthDirX(l * p * 2, dir + Math.PI / 2)), (int)(c.Y + LengthDirY(l * p * 2, dir + Math.PI / 2)));
            var f = new Point((int)(c.X + LengthDirX(l * p * 2, dir - Math.PI / 2)), (int)(c.Y + LengthDirY(l * p * 2, dir - Math.PI / 2)));
            var g = new Point((int)(d.X + LengthDirX(l * p * 2, dir + Math.PI / 2)), (int)(d.Y + LengthDirY(l * p * 2, dir + Math.PI / 2)));
            var h = new Point((int)(d.X + LengthDirX(l * p * 2, dir - Math.PI / 2)), (int)(d.Y + LengthDirY(l * p * 2, dir - Math.PI / 2)));

            // variables used to randomise curvature based on length of line left
            //
            var pa = _randomService.GetRandomDouble(0.0, 1.0) * LineLength(e, f);
            var pb = _randomService.GetRandomDouble(0.0, 1.0) * LineLength(e, g);

            var I = new Point((int)((pa / LineLength(e, f)) * (e.X - f.X) + f.X), (int)((pa / LineLength(e, f)) * (e.Y - f.Y) + f.Y));
            var j = new Point((int)((pa / LineLength(e, f)) * (g.X - h.X) + h.X), (int)((pa / LineLength(e, f)) * (g.Y - h.Y) + h.Y));
            var k = new Point((int)((pb / LineLength(I, j)) * (I.X - j.X) + j.X), (int)((pb / LineLength(I, j)) * (I.Y - j.Y) + j.Y));

            // return the next point in the path
            //
            return k;
        }
    }
}
