using System.Windows;
using Caliburn.Micro;
using JirkaDb.ViewModels;

namespace JirkaDb.AppBootstrapper
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }


        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}