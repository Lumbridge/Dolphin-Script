namespace DolphinScript.Core.Models
{
    public class BoxPlotModel
    {
        public double Target { get; set; }
        public double LowerBoundPercentile { get; set; }
        public double UpperBoundPercentile { get; set; }
        public double OutlierPercentageChance { get; set; }
        public double OutlierSkewPercentage { get; set; }
    }
}
