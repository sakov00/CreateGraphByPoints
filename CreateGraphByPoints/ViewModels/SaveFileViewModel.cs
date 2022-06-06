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
    public class SaveFileViewModel : BaseViewModel
    {
        private readonly WorkForExcel _workForExcel;

        private readonly WorkForXml _workForXml;

        private DrawFuncViewModel _drawFuncVM;

        public SaveFileViewModel(DrawFuncViewModel drawFuncVM, WorkForExcel workForExcel, WorkForXml workForXml)
        {
            _drawFuncVM = drawFuncVM;
            _workForExcel = workForExcel;
            _workForXml = workForXml;
        }

        public DrawFuncViewModel DrawFuncVM
        {
            get => _drawFuncVM;
            set
            {
                _drawFuncVM = value;
                OnPropertyChanged();
            }
        }

        private async void LoadInFile(object param, IForWorkWithFiles WorkFile)
        {
            if (param == null)
                return;

            List<ChartValues<ObservablePoint>> _listLineSeries = new List<ChartValues<ObservablePoint>>();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);

            await Task.Run(()=> WorkFile.LoadInFile(_listLineSeries));
            IsCanProjectChanged = false;
        }

        private async void LoadFromFile(object param, IForWorkWithFiles WorkFile)
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

            DrawFuncVM.CurrentFuncPoints = (LineSeries)seriesCol[0];
            IsCanProjectChanged = false;
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
                        param => LoadInFile(param, _workForExcel)
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
                        param => LoadFromFile(param, _workForExcel)
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
                        param => LoadInFile(param, _workForXml)
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
                        param => LoadFromFile(param, _workForXml)
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
