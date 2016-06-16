using System.Windows;
using Microsoft.Practices.Unity;
using Services.Interfaces;
using Services.Portable.Exceptions;

namespace Weather.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        Bootstrapper _bootstrapper = new Bootstrapper();

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            var apiException = e.Exception as ApiErrorException;
            if (apiException != null)
            {
                MessageBox.Show(apiException.Message);
                return;
            }

            MessageBox.Show("Ooops. Something went wrong.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var localStorage = _bootstrapper.Container.Resolve<ILocalStorageService>();
            localStorage.Save();
            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _bootstrapper.Run();
        }
    }
}
