using AutoMapper;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Interfaces;
using DolphinScript.Factories;
using DolphinScript.Interfaces;
using DolphinScript.Mapping;
using DolphinScript.Services;
using DolphinScript.ViewModels;
using DolphinScript.Views;
using Microsoft.Extensions.Logging.Abstractions;
using Unity;

namespace DolphinScript.Composition
{
    public static class ApplicationComposition
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterInstance(CreateMapper(), InstanceLifetime.Singleton);
            container.RegisterType<IScriptState, ScriptStateService>();
            container.RegisterType<IObjectFactory, ObjectFactory>();
            container.RegisterType<IXmlSerializerService, XmlSerializerService>();
            container.RegisterType<IUserInterfaceService, UserInterfaceService>();
            container.RegisterType<IGlobalMethodService, GlobalMethodService>();
            container.RegisterType<IListService, ListService>();
            container.RegisterType<IRandomService, RandomService>();
            container.RegisterType<IMouseMathService, MouseMathService>();
            container.RegisterType<IPointService, PointService>();
            container.RegisterType<IMouseMovementService, MouseMovementService>();
            container.RegisterType<IHumanMouseTrajectoryPlanner, HumanMouseTrajectoryPlanner>();
            container.RegisterType<ICursorController, CursorController>();
            container.RegisterType<IScreenCaptureService, ScreenCaptureService>();
            container.RegisterType<IColourService, ColourService>();
            container.RegisterType<IWindowControlService, WindowControlService>();
            container.RegisterType<IFormFactory, FormFactory>();
            container.RegisterType<ISelectionOverlayService, SelectionOverlayService>();
            container.RegisterType<IScreenService, ScreenService>();
            container.RegisterType<IProcessChangeNotifier, WmiProcessChangeNotifier>();
            container.RegisterType<IScriptProgressReporter, ScriptProgressReporter>(TypeLifetime.Singleton);
            container.RegisterType<IScriptRunner, ScriptRunner>();
            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<MainWindow>();
        }

        private static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DolphinScriptMappingProfile>();
            }, NullLoggerFactory.Instance);

            return mapperConfiguration.CreateMapper();
        }
    }
}