using DolphinScript.Core.Models;

namespace DolphinScript.Core.Interfaces
{
    public interface IRandomService
    {
        int GetRandomNumber(int min, int max);
        double GetRandomDouble(double minimum, double maximum);
        BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model);
    }
}