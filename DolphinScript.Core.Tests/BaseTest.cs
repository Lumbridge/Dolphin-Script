using AutoMapper;
using DolphinScript.Concrete;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Interfaces;
using DolphinScript.Core.Models;
using DolphinScript.Interfaces;
using System.Windows.Forms;
using Unity;

namespace DolphinScript.Core.Tests
{
    public class BaseTest
    {
        protected static UnityContainer UnityContainer;

        public BaseTest()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ScriptEvent, ScriptEvent>().ReverseMap();
                cfg.CreateMap<SaveFileDialog, FileDialogModel>().ReverseMap();
                cfg.CreateMap<OpenFileDialog, FileDialogModel>().ReverseMap();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            UnityContainer = new UnityContainer();

            UnityContainer.RegisterInstance(mapper, InstanceLifetime.Singleton);
            UnityContainer.RegisterType<IObjectFactory, ObjectFactory>();
            UnityContainer.RegisterType<IXmlSerializerService, XmlSerializerService>();
            UnityContainer.RegisterType<IUserInterfaceService, UserInterfaceService>();
            UnityContainer.RegisterType<IGlobalMethodService, GlobalMethodService>();
            UnityContainer.RegisterType<IListService, ListService>();
            UnityContainer.RegisterType<IRandomService, RandomService>();
            UnityContainer.RegisterType<IMouseMathService, MouseMathService>();
            UnityContainer.RegisterType<IPointService, PointService>();
            UnityContainer.RegisterType<IMouseMovementService, MouseMovementService>();
            UnityContainer.RegisterType<IScreenCaptureService, ScreenCaptureService>();
            UnityContainer.RegisterType<IColourService, ColourService>();
            UnityContainer.RegisterType<IWindowControlService, WindowControlService>();
            UnityContainer.RegisterType<IFormManager, FormManager>();
        }
    }
}