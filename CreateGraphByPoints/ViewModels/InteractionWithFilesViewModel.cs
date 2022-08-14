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

namespace CreateGraphByPoints.ViewModels
{
    public class InteractionWithFilesViewModel : BaseViewModel
    {
        private InteractionOnCanvasViewModel interactionOnCanvasVM;

        public InteractionOnCanvasViewModel InteractionOnCanvasVM
        {
            get => interactionOnCanvasVM;
            set
            {
                interactionOnCanvasVM = value;
                OnPropertyChanged();
            }
        }

        public InteractionWithFilesViewModel(WorkWithJson WorkWithJson, WorkWithXml WorkWithXml)
        {
            LoadFunctionsInJson = new RelayCommand(param => LoadFunctionsInFile(WorkWithJson));
            UnloadFunctionsFromJson = new RelayCommand(param => UnloadFunctionsFromFile(WorkWithJson));
            LoadFunctionsInXml = new RelayCommand(param => LoadFunctionsInFile(WorkWithXml));
            UnloadFunctionsFromXml = new RelayCommand(param => UnloadFunctionsFromFile(WorkWithXml));
        }

        #region Commands

        public ICommand LoadFunctionsInJson { get; private set; }

        public ICommand LoadFunctionsInXml { get; private set; }

        private async void LoadFunctionsInFile(ILoadAndUnloadFunctionsInFile WorkFile)
        {
            await Task.Run(() => WorkFile.LoadInFile(SeriesCollectionConvertToListChartValues()));
            IsCanProjectChanged = false;
        }

        public ICommand UnloadFunctionsFromJson { get; private set; }

        public ICommand UnloadFunctionsFromXml { get; private set; }

        private async void UnloadFunctionsFromFile(ILoadAndUnloadFunctionsInFile WorkFile)
        {
            var listChartValues = await Task.Run(() => WorkFile.LoadFromFile());
            InteractionOnCanvasVM.SeriesCollection = ListChartValuesConvertToSeriesCollection(listChartValues);

            if (InteractionOnCanvasVM.SeriesCollection.Count == 0)
            {
                MessageBox.Show("The file is empty");
                return;
            }

            InteractionOnCanvasVM.CurrentFunction = (LineSeries)InteractionOnCanvasVM.SeriesCollection[0];
            IsCanProjectChanged = false;
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }

        #endregion Commands

        private List<ChartValues<ObservablePoint>> SeriesCollectionConvertToListChartValues()
        {
            var listChartValues = new List<ChartValues<ObservablePoint>>();
            foreach (LineSeries line in InteractionOnCanvasVM.SeriesCollection)
                listChartValues.Add((ChartValues<ObservablePoint>)line.Values);
            return listChartValues;
        }

        private SeriesCollection ListChartValuesConvertToSeriesCollection(List<ChartValues<ObservablePoint>> listChartValues)
        {
            var listLineSeries = new SeriesCollection();
            foreach (var line in listChartValues)
            {
                listLineSeries.Add(new LineSeries
                {
                    Values = line,
                    LineSmoothness = 0
                });
            }
            return listLineSeries;
        }
    }
}
