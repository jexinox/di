using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                var container = new StandardKernel();

                // start here
                // container.Bind<TService>().To<TImplementation>();
                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<PictureBoxImageHolder>().ToSelf().InSingletonScope();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(new IUiAction[]
                {
                    new SaveImageAction(),
                    new DragonFractalAction(),
                    new KochFractalAction(container.Get<PictureBoxImageHolder>(), container.Get<Palette>()),
                    new ImageSettingsAction(),
                    new PaletteSettingsAction()
                }, 
                    container.Get<PictureBoxImageHolder>(),
                    container.Get<Palette>()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}