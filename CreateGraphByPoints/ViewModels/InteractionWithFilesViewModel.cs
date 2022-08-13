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

        public InteractionWithFilesViewModel(InteractionOnCanvasViewModel paramInteractionOnCanvasVM, WorkWithExcel WorkWithExcel, WorkWithXml WorkWithXml)
        {
            interactionOnCanvasVM = paramInteractionOnCanvasVM;

            LoadFunctionsInExcel = new RelayCommand(param => LoadFunctionsInFile(param, WorkWithExcel));
            UnloadFunctionsFromExcel = new RelayCommand(param => UnloadFunctionsFromFile(param, WorkWithExcel));
            LoadFunctionsInXml = new RelayCommand(param => LoadFunctionsInFile(param, WorkWithXml));
            UnloadFunctionsFromXml = new RelayCommand(param => UnloadFunctionsFromFile(param, WorkWithXml));
        }

        #region Commands

        public ICommand LoadFunctionsInExcel { get; private set; }

        public ICommand LoadFunctionsInXml { get; private set; }

        private async void LoadFunctionsInFile(object param, ILoadAndUnloadFunctionsInFile WorkFile)
        {
            List<ChartValues<ObservablePoint>> _listLineSeries = new List<ChartValues<ObservablePoint>>();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);

            await Task.Run(() => WorkFile.LoadInFile(_listLineSeries));
            IsCanProjectChanged = false;
        }

        public ICommand UnloadFunctionsFromExcel { get; private set; }

        public ICommand UnloadFunctionsFromXml { get; private set; }

        private async void UnloadFunctionsFromFile(object param, ILoadAndUnloadFunctionsInFile WorkFile)
        {
            var seriesCol = param as SeriesCollection;

            List<ChartValues<ObservablePoint>> _listLineSeries = new List<ChartValues<ObservablePoint>>();
            foreach (LineSeries line in seriesCol)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);

            await Task.Run(() => WorkFile.LoadFromFile(_listLineSeries));
            seriesCol.Clear();

            foreach (var line in _listLineSeries)
            {
                seriesCol.Add(new LineSeries
                {
                    Values = line,
                    LineSmoothness = 0
                });
            }

            if (seriesCol.Count == 0)
                return;

            InteractionOnCanvasVM.CurrentFunction = (LineSeries)seriesCol[0];
            IsCanProjectChanged = false;
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }

        #endregion Commands
    }
}
