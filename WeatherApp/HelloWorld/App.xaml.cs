using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Practices.Unity;
using Services.Interfaces;

namespace HelloWorld
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Bootstrapper _bootstrapper = new Bootstrapper();

        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ooops. Something went wrong.");
            e.Handled = true;
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
