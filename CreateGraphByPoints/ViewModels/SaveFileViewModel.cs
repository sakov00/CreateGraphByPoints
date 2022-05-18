using CreateGraphByPoints.ClassesForWorkFiles;
using CreateGraphByPoints.Commands;
using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class SaveFileViewModel : BaseViewModel, ISaveFile
    {
        private List<ChartValues<ObservablePoint>> _listLineSeries = new List<ChartValues<ObservablePoint>>();

        private IDrawFunc _drawFuncVM;

        private IMainVM _mainVM;

        public SaveFileViewModel(IDrawFunc drawFuncVM, IMainVM mainVM)
        {
            _drawFuncVM = drawFuncVM;
            _mainVM = mainVM;
        }

        private void LoadInFile(object param, IWorkWithFiles WorkFile)
        {
            if (param == null)
                return;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(WorkFile);
            _listLineSeries.Clear();
            foreach (LineSeries line in (SeriesCollection)param)
                _listLineSeries.Add((ChartValues<ObservablePoint>)line.Values);
            context.LoadInFile(_listLineSeries);
            _mainVM.IsCanProjectChange = false;
        }

        private void LoadFromFile(object param, IWorkWithFiles WorkFile)
        {
            var seriesCol = param as SeriesCollection;
            var context = new ContextForWorkFiles();
            context.SetWorkWithFiles(WorkFile);

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

        #region Commands

        #region --- LoadInExcelFile ---

        public ICommand LoadInExcelFile
        {
            get
            {
                if (_cmdLoadInExcelFile == null)
                {
                    _cmdLoadInExcelFile = new RelayCommand(
                        param => LoadInFile(param, new WorkForExcel())
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
                        param => LoadFromFile(param, new WorkForExcel())
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
                        param => LoadInFile(param, new WorkForXml())
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
                        param => LoadFromFile(param, new WorkForXml())
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
