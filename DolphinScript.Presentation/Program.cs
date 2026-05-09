using DolphinScript.Composition;
using DolphinScript.Views;
using System;
using System.Windows;
using Unity;
using WinFormsApplication = System.Windows.Forms.Application;

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
            ApplicationComposition.Configure(_container);

            WinFormsApplication.EnableVisualStyles();
            WinFormsApplication.SetCompatibleTextRenderingDefault(false);

            var application = new Application
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose
            };

            application.Run(_container.Resolve<MainWindow>());
        }
    }
}
