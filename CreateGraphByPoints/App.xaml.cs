using Autofac;
using Autofac.Core;
using CreateGraphByPoints.Containers;
using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.Interfaces;
using CreateGraphByPoints.ViewModels;
using CreateGraphByPoints.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace CreateGraphByPoints
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StartupUri = new Uri("/CreateGraphByPoints;component/Views/MainWindow.xaml", UriKind.Relative);
            AutofacConfig.ConfigureContainer();
        }
    }
}
