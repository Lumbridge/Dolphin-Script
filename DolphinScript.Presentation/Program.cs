using DolphinScript.Core.Concrete;
using DolphinScript.Core.Interfaces;
using DolphinScript.Forms;
using System;
using System.Windows.Forms;
using AutoMapper;
using DolphinScript.Concrete;
using Unity;
using DolphinScript.Core.Events.BaseEvents;
using DolphinScript.Core.Models;
using DolphinScript.Interfaces;

namespace DolphinScript
{
    static class Program
    {
        private static UnityContainer _container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ScriptEvent, ScriptEvent>().ReverseMap()
                    .ForMember(x => x.GroupSize, y => y.Ignore())
                    .ForMember(x => x.GroupSiblings, y => y.Ignore());
                cfg.CreateMap<SaveFileDialog, FileDialogModel>().ReverseMap();
                cfg.CreateMap<OpenFileDialog, FileDialogModel>().ReverseMap();
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            _container = new UnityContainer();

            _container.RegisterInstance(mapper, InstanceLifetime.Singleton);
            _container.RegisterType<IObjectFactory, ObjectFactory>();
            _container.RegisterType<IXmlSerializerService, XmlSerializerService>();
            _container.RegisterType<IUserInterfaceService, UserInterfaceService>();
            _container.RegisterType<IGlobalMethodService, GlobalMethodService>();
            _container.RegisterType<IListService, ListService>();
            _container.RegisterType<IRandomService, RandomService>();
            _container.RegisterType<IMouseMathService, MouseMathService>();
            _container.RegisterType<IPointService, PointService>();
            _container.RegisterType<IMouseMovementService, MouseMovementService>();
            _container.RegisterType<IScreenCaptureService, ScreenCaptureService>();
            _container.RegisterType<IColourService, ColourService>();
            _container.RegisterType<IWindowControlService, WindowControlService>();
            _container.RegisterType<IFormManager, FormManager>();
            _container.RegisterType<IFormFactory, FormFactory>();
            _container.RegisterType<IScreenService, ScreenService>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_container.Resolve<MainForm>());
        }
    }
}
