namespace DolphinScript.Core.Models
{
    public class BoxPlotResult
    {
        public bool WasOutlier { get; set; }
        public double Result { get; set; }
        public double LowerBound { get; set; }
        public double UpperBound { get; set; }
    }
}
