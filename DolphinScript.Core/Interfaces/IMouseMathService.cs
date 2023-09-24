using System.Drawing;

namespace DolphinScript.Core.Interfaces
{
    public interface IMouseMathService
    {
        double CalculateHypotenuse(double x, double y);
        double CalculateHypotenuse(Point p1, Point p2);
        double LineLength(Point a, Point b);
    }
}