using System;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace DolphinScript.Core.Tests
{
    [TestClass]
    public class RandomServiceTests : BaseTest
    {
        private IRandomService _randomService;
        private IXmlSerializerService _xmlSerializerService;

        [TestInitialize]
        public void Startup()
        {
            _randomService = UnityContainer.Resolve<IRandomService>();
            _xmlSerializerService = UnityContainer.Resolve<IXmlSerializerService>();
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(200)]
        [DataRow(3500)]
        [DataRow(83000)]
        public void ShouldGetRandomNumberBoxPlotNoOutlier(int targetNumber)
        {
            BoxPlotModel model = new BoxPlotModel
            {
                LowerBoundPercentile = 20,
                UpperBoundPercentile = 10,
                OutlierPercentageChance = 0,
                Target = targetNumber
            };

            var result = _randomService.GetRandomNumberBoxPlot(model);

            Console.WriteLine(_xmlSerializerService.Serialize(result));

            Assert.AreEqual(false, result.WasOutlier);
            Assert.IsTrue(result.Result >= result.LowerBound && result.Result <= result.UpperBound);
        }

        [TestMethod]
        [DataRow(100)]
        [DataRow(200)]
        [DataRow(3500)]
        [DataRow(83000)]
        public void ShouldGetRandomNumberBoxPlotGuaranteedOutlier(int targetNumber)
        {
            BoxPlotModel model = new BoxPlotModel
            {
                LowerBoundPercentile = 20,
                UpperBoundPercentile = 10,
                OutlierPercentageChance = 100,
                Target = targetNumber,
                OutlierSkewPercentage = 30
            };

            var result = _randomService.GetRandomNumberBoxPlot(model);

            Console.WriteLine(_xmlSerializerService.Serialize(result));

            Assert.AreEqual(true, result.WasOutlier);
            Assert.IsTrue(result.Result <= result.LowerBound || result.Result >= result.UpperBound);
        }
    }
}
