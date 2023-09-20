using System;
using System.Windows.Forms;
using DolphinScript.Core.Concrete;
using DolphinScript.Core.Interfaces;
using DolphinScript.Forms;
using Unity;

namespace DolphinScript
{
    static class Program
    {
        private static UnityContainer container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            container = new UnityContainer();
            container.RegisterSingleton<IScriptState, ScriptState>();
            container.RegisterType<IGlobalMethodService, GlobalMethodService>();
            container.RegisterType<IListService, ListService>();
            container.RegisterSingleton<IRandomService, RandomService>();
            container.RegisterType<IMouseMathService, MouseMathService>();
            container.RegisterType<IPointService, PointService>();
            container.RegisterType<IMouseMovementService, MouseMovementService>();
            container.RegisterType<IScreenCaptureService, ScreenCaptureService>();
            container.RegisterType<IColourService, ColourService>();
            container.RegisterType<IWindowControlService, WindowControlService>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<MainForm>());
        }
    }
}
