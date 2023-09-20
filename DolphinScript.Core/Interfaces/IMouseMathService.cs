using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IMouseMathService
    {
        double CalculateHypotenuse(double x, double y);
        double LineLength(Point a, Point b);
        double PointDirection(Point a, Point b);
        double LengthDirX(double distance, double direction);
        double LengthDirY(double distance, double direction);
        Point GetPointCurve(Point a, Point b);
    }
}