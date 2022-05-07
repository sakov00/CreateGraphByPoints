using CreateGraphByPoints.ClassesForWorkFiles;
using CreateGraphByPoints.Commands;
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

        private DrawFuncViewModel _drawFuncVM;

        private MainViewModel _MainVM;

        public SaveFileViewModel(DrawFuncViewModel drawFuncVM, MainViewModel mainVM)
        {
            _drawFuncVM = drawFuncVM;
            _MainVM = mainVM;
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
                        param => LoadInExcelFile_Executed(param)
                        );
                }
                return _cmdLoadInExcelFile;
            }
        }
        private RelayCommand _cmdLoadInExcelFile;

        private void LoadInExcelFile_Executed(object param)
        {
            if(param == null)
                return;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(new WorkForExcel());
            _listLineSeries.Clear();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            context.LoadInFile(_listLineSeries);
            _MainVM.IsCanProjectChange = false;
        }

        #endregion --- LoadInExcelFile ---

        #region --- LoadFromExcelFile ---
        public ICommand LoadFromExcelFile
        {
            get
            {
                if (_cmdLoadFromExcelFile == null)
                {
                    _cmdLoadFromExcelFile = new RelayCommand(
                        param => LoadFromExcelFile_Executed(param)
                        );
                }
                return _cmdLoadFromExcelFile;
            }
        }
        private RelayCommand _cmdLoadFromExcelFile;

        private void LoadFromExcelFile_Executed(object param)
        {
            var seriesCol = param as SeriesCollection;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(new WorkForExcel());

            _listLineSeries.Clear();
            foreach (LineSeries line in seriesCol)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            context.LoadFromFile(_listLineSeries);

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
            _drawFuncVM.CurrentFuncPoints = (LineSeries)seriesCol[0];
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }
        #endregion --- LoadFromXmlFile ---

        #region --- LoadInXmlFile ---
        public ICommand LoadInXmlFile
        {
            get
            {
                if (_cmdLoadInXmlFile == null)
                {
                    _cmdLoadInXmlFile = new RelayCommand(
                        param => LoadInXmlFile_Executed(param)
                        );
                }
                return _cmdLoadInXmlFile;
            }
        }
        private RelayCommand _cmdLoadInXmlFile;

        private void LoadInXmlFile_Executed(object param)
        {
            if (param == null)
                return;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(new WorkForXml());
            _listLineSeries.Clear();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            context.LoadInFile(_listLineSeries);
            _MainVM.IsCanProjectChange = false;
        }
        #endregion --- LoadInExcelFile ---

        #region --- LoadFromXmlFile ---

        public ICommand LoadFromXmlFile
        {
            get
            {
                if (_cmdLoadFromXmlFile == null)
                {
                    _cmdLoadFromXmlFile = new RelayCommand(
                        param => LoadFromXmlFile_Executed(param)
                        );
                }
                return _cmdLoadFromXmlFile;
            }
        }
        private RelayCommand _cmdLoadFromXmlFile;

        private void LoadFromXmlFile_Executed(object param)
        {
            var seriesCol = param as SeriesCollection;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(new WorkForXml());

            _listLineSeries.Clear();
            foreach (LineSeries line in seriesCol)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            context.LoadFromFile(_listLineSeries);

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
            _drawFuncVM.CurrentFuncPoints = (LineSeries)seriesCol[0];
            MessageBox.Show("The functions were successfully unloaded from the file.\nThe points of the blue function are now displayed");
        }

        #endregion --- LoadFromXmlFile ---

        #endregion Commands
    }
}
