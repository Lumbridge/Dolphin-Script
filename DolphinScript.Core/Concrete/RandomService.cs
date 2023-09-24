using DolphinScript.Core.Interfaces;
using System;
using DolphinScript.Core.Models;

namespace DolphinScript.Core.Concrete
{
    public class RandomService : IRandomService
    {
        private readonly Random _random = new Random(Guid.NewGuid().GetHashCode());

        // lock object to use in the class methods
        private readonly object _lock = new object();

        /// <summary>
        /// This method returns a random integer inside the range passed in.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int GetRandomNumber(int min, int max)
        {
            // lock this method so that we ensure that the method only returns the same number once
            // and that a unique number is returned every time the method is called
            lock (_lock)
            {
                return _random.Next(min, max + 1);
            }
        }

        /// <summary>
        /// This method returns a random double inside the range passed in.
        /// </summary>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <returns></returns>
        public double GetRandomDouble(double minimum, double maximum)
        {
            // lock this method so that we ensure that the method only returns the same number once
            // and that a unique number is returned every time the method is called
            lock (_lock)
            {
                // return the random number using the private random object
                return _random.NextDouble() * (maximum - minimum) + minimum;
            }
        }

        public BoxPlotResult GetRandomNumberBoxPlot(BoxPlotModel model)
        {
            var result = new BoxPlotResult
            {
                LowerBound = model.Target * (model.LowerBoundPercentile / 10) - model.Target,
                UpperBound = model.Target * (model.UpperBoundPercentile / 10) + model.Target
            };

            result.Result = GetRandomDouble(result.LowerBound, result.UpperBound);

            var outlier = GetRandomDouble(0, 100);

            if (outlier >= model.OutlierPercentageChance)
            {
                return result;
            }

            result.WasOutlier = true;

            var skewResultAmount = result.Result * (model.OutlierSkewPercentage / 100);

            if (result.Result < model.Target)
            {
                var lowerBoundCalc = result.LowerBound - skewResultAmount;
                result.Result = lowerBoundCalc > 0 ? lowerBoundCalc : skewResultAmount;
            }
            else if (result.Result > model.Target)
            {
                result.Result = result.UpperBound + skewResultAmount;
            }

            return result;
        }
    }
}