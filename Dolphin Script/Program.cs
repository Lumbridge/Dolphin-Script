using DolphinScript.Core.Concrete;
using DolphinScript.Core.Interfaces;
using DolphinScript.Forms;
using System;
using System.Windows.Forms;
using Unity;

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
            _container = new UnityContainer();
            _container.RegisterType<IEventFactory, EventFactory>();
            _container.RegisterType<IScriptPersistenceService, ScriptPersistenceService>();
            _container.RegisterType<IGlobalMethodService, GlobalMethodService>();
            _container.RegisterType<IListService, ListService>();
            _container.RegisterType<IRandomService, RandomService>();
            _container.RegisterType<IMouseMathService, MouseMathService>();
            _container.RegisterType<IPointService, PointService>();
            _container.RegisterType<IMouseMovementService, MouseMovementService>();
            _container.RegisterType<IScreenCaptureService, ScreenCaptureService>();
            _container.RegisterType<IColourService, ColourService>();
            _container.RegisterType<IWindowControlService, WindowControlService>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(_container.Resolve<MainForm>());
        }
    }
}
