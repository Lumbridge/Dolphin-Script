using DolphinScript.Composition;
using Unity;

namespace DolphinScript.Core.Tests
{
    public class BaseTest
    {
        protected static UnityContainer UnityContainer;

        public BaseTest()
        {
            UnityContainer = new UnityContainer();
            ApplicationComposition.Configure(UnityContainer);
        }
    }
}