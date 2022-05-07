﻿using CreateGraphByPoints.ViewModels;
using System.Windows;

namespace CreateGraphByPoints.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            bool showCancel = false;
            if ((DataContext as MainViewModel).IsCanProjectChange)
            {
                var res = MessageBox.Show("Data is not saving.\nDo you really want to exit the app without saving the data?", "", showCancel ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo);
                if (res == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            (DataContext as MainViewModel).IsCanProjectChange = true;
        }
    }
}
