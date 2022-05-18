using CreateGraphByPoints.Containers;
using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.ViewModels;
using System;
using System.Windows;

namespace CreateGraphByPoints
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            StartupUri = new Uri("/CreateGraphByPoints;component/Views/MainWindow.xaml", UriKind.Relative);

            ViewModelsContainer.Register<MainViewModel>();
            ViewModelsContainer.Register<DrawFuncViewModel>();
            ViewModelsContainer.Register<SaveFileViewModel>();

            WorkFilesContainer.Register<WorkForExcel>();
            WorkFilesContainer.Register<WorkForXml>();

            ViewModelsContainer.CreateViewModel<MainViewModel>();
            ViewModelsContainer.CreateViewModel<DrawFuncViewModel>();
            ViewModelsContainer.CreateViewModel<SaveFileViewModel>();

            WorkFilesContainer.CreateForWorkWithFile<WorkForExcel>();
            WorkFilesContainer.CreateForWorkWithFile<WorkForXml>();
        }
    }
}
