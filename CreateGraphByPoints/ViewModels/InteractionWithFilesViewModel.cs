using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.Commands;
using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using CreateGraphByPoints.Extensions;

namespace CreateGraphByPoints.ViewModels
{
    public class InteractionWithFilesViewModel : BaseViewModel
    {
        public InteractionWithFilesViewModel(WorkWithJson WorkWithJson, WorkWithXml WorkWithXml)
        {
            LoadFunctionsInJson = new RelayCommand(param => LoadFunctionsInFile(param, WorkWithJson));
            UnloadFunctionsFromJson = new RelayCommand(param => UnloadFunctionsFromFile(param, WorkWithJson));
            LoadFunctionsInXml = new RelayCommand(param => LoadFunctionsInFile(param, WorkWithXml));
            UnloadFunctionsFromXml = new RelayCommand(param => UnloadFunctionsFromFile(param, WorkWithXml));
        }

        #region Commands

        public ICommand LoadFunctionsInJson { get; private set; }

        public ICommand LoadFunctionsInXml { get; private set; }

        private async void LoadFunctionsInFile(object paramSeriesCollection, ILoadAndUnloadFunctionsInFile WorkFile)
        {
            var seriesCollection = (SeriesCollection)paramSeriesCollection;
            await Task.Run(() => WorkFile.LoadInFile(seriesCollection.SeriesCollectionConvertToListChartValues()));
            IsCanProjectChanged = false;
        }

        public ICommand UnloadFunctionsFromJson { get; private set; }

        public ICommand UnloadFunctionsFromXml { get; private set; }

        private async void UnloadFunctionsFromFile(object paramSeriesCollection, ILoadAndUnloadFunctionsInFile WorkFile)
        {
            var listChartValues = await Task.Run(() => WorkFile.LoadFromFile());
            var seriesCollection = (SeriesCollection)paramSeriesCollection;
            seriesCollection.ReplaceListChartValuesInSeriesCollection(listChartValues);

            if (seriesCollection.Count == 0)
            {
                MessageBox.Show("The file is empty");
                return;
            }

            IsCanProjectChanged = false;
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }

        #endregion Commands
    }
}
