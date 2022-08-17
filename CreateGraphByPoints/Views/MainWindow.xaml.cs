using Autofac;
using CreateGraphByPoints.Containers;
using CreateGraphByPoints.ViewModels;
using System.Windows;

namespace CreateGraphByPoints.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = AutofacConfig.GetContainer.Resolve<MainViewModel>();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if ((DataContext as MainViewModel).GetIsProjectChanged())
            {
                bool showCancel = false;
                var res = MessageBox.Show("Data is not saving.\nDo you really want to exit the app without saving the data?",
                    "", showCancel ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo);
                if (res == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
