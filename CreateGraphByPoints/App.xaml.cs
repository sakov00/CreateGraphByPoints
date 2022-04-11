using System;
using System.Windows;

namespace CreateGraphByPoints
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StartupUri = new Uri("/CreateGraphByPoints;component/Views/MainWindow.xaml", UriKind.Relative);
        }
    }
}
