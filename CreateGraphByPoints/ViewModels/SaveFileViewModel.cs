using CreateGraphByPoints.ForWorkWithFiles;
using CreateGraphByPoints.Commands;
using CreateGraphByPoints.Containers;
using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class SaveFileViewModel : BaseViewModel
    {
        private List<ChartValues<ObservablePoint>> _listLineSeries = new List<ChartValues<ObservablePoint>>();

        private void LoadInFile(object param, IForWorkWithFiles WorkFile)
        {
            if (param == null)
                return;
            _listLineSeries.Clear();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            WorkFile.LoadInFile(_listLineSeries);
            ViewModelsContainer.GetViewModel<MainViewModel>().IsCanProjectChange = true;
        }

        private void LoadFromFile(object param, IForWorkWithFiles WorkFile)
        {
            var seriesCol = param as SeriesCollection;
            _listLineSeries.Clear();
            foreach (LineSeries line in seriesCol)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            WorkFile.LoadFromFile(_listLineSeries);

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
            ViewModelsContainer.GetViewModel<DrawFuncViewModel>().CurrentFuncPoints = (LineSeries)seriesCol[0];
            ViewModelsContainer.GetViewModel<MainViewModel>().IsCanProjectChange = true;
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }

        #region Commands

        #region --- LoadInExcelFile ---

        public ICommand LoadInExcelFile
        {
            get
            {
                if (_cmdLoadInExcelFile == null)
                {
                    _cmdLoadInExcelFile = new RelayCommand(
                        param => LoadInFile(param, WorkFilesContainer.GetForWorkWithFile<WorkForExcel>())
                        );
                }
                return _cmdLoadInExcelFile;
            }
        }
        private RelayCommand _cmdLoadInExcelFile;

        #endregion --- LoadInExcelFile ---

        #region --- LoadFromExcelFile ---

        public ICommand LoadFromExcelFile
        {
            get
            {
                if (_cmdLoadFromExcelFile == null)
                {
                    _cmdLoadFromExcelFile = new RelayCommand(
                        param => LoadFromFile(param, WorkFilesContainer.GetForWorkWithFile<WorkForExcel>())
                        );
                }
                return _cmdLoadFromExcelFile;
            }
        }
        private RelayCommand _cmdLoadFromExcelFile;

        #endregion --- LoadFromXmlFile ---

        #region --- LoadInXmlFile ---
        public ICommand LoadInXmlFile
        {
            get
            {
                if (_cmdLoadInXmlFile == null)
                {
                    _cmdLoadInXmlFile = new RelayCommand(
                        param => LoadInFile(param, WorkFilesContainer.GetForWorkWithFile<WorkForXml>())
                        );
                }
                return _cmdLoadInXmlFile;
            }
        }
        private RelayCommand _cmdLoadInXmlFile;

        #endregion --- LoadInExcelFile ---

        #region --- LoadFromXmlFile ---

        public ICommand LoadFromXmlFile
        {
            get
            {
                if (_cmdLoadFromXmlFile == null)
                {
                    _cmdLoadFromXmlFile = new RelayCommand(
                        param => LoadFromFile(param, WorkFilesContainer.GetForWorkWithFile<WorkForXml>())
                        );
                }
                return _cmdLoadFromXmlFile;
            }
        }
        private RelayCommand _cmdLoadFromXmlFile;

        #endregion --- LoadFromXmlFile ---

        #endregion Commands
    }
}
