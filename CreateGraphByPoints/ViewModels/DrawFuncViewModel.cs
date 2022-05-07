using CreateGraphByPoints.Commands;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class DrawFuncViewModel : BaseViewModel
    {
        private SeriesCollection _seriesCollection;

        private LineSeries _currentFuncPoints;

        private MainViewModel _mainVM;

        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public LineSeries CurrentFuncPoints
        {
            get => _currentFuncPoints;
            set
            {
                _currentFuncPoints = value;
                OnPropertyChanged();
            }
        }

        public DrawFuncViewModel(MainViewModel mainViewModel)
        {
            SeriesCollection = new SeriesCollection();
            SeriesCollection.Add(new LineSeries());
            SeriesCollection[0].Values = new ChartValues<ObservablePoint>();
            CurrentFuncPoints = (LineSeries)SeriesCollection[0];
            (SeriesCollection[0] as LineSeries).LineSmoothness = 0;
            SeriesCollection[0].Values.Insert(0, new ObservablePoint());
            _mainVM = mainViewModel;
        }

        #region Commands

        #region --- AddPoint ---

        public ICommand AddPoint
        {
            get
            {
                if (_cmdAddPoint == null)
                {
                    _cmdAddPoint = new RelayCommand(
                        param => AddPoint_Executed(param)
                        );
                }
                return _cmdAddPoint;
            }
        }
        private RelayCommand _cmdAddPoint;

        private void AddPoint_Executed(object param)
        {
            if (CurrentFuncPoints == null)
            {
                MessageBox.Show("Please add function on canvas");
                return;
            }
            CurrentFuncPoints.Values.Add(new ObservablePoint());
            _mainVM.IsCanProjectChange = true;
        }

        #endregion --- AddPoint ---

        #region --- RemovePoint ---

        public ICommand RemovePoint
        {
            get
            {
                if (_cmdRemovePoint == null)
                {
                    _cmdRemovePoint = new RelayCommand(
                        param => RemovePoint_Executed(param)
                        );
                }
                return _cmdRemovePoint;
            }
        }
        private RelayCommand _cmdRemovePoint;

        private void RemovePoint_Executed(object param)
        {
            CurrentFuncPoints.Values.Remove((ObservablePoint)param);
            _mainVM.IsCanProjectChange = true;
        }

        #endregion --- RemovePoint ---

        #region --- AddFunction ---

        public ICommand AddFunction
        {
            get
            {
                if (_cmdAddFunction == null)
                {
                    _cmdAddFunction = new RelayCommand(
                        param => AddFunction_Executed(param)
                        );
                }
                return _cmdAddFunction;
            }
        }
        private RelayCommand _cmdAddFunction;

        private void AddFunction_Executed(object param)
        {
            SeriesCollection.Add(new LineSeries 
            {
                Values = new ChartValues<ObservablePoint> 
                {
                    new ObservablePoint()
                },
                LineSmoothness = 0
            });
            CurrentFuncPoints = (LineSeries)SeriesCollection[SeriesCollection.Count-1];
            _mainVM.IsCanProjectChange = true;
        }
        #endregion --- AddFunction ---

        #region --- RemoveFunction ---

        public ICommand RemoveFunction
        {
            get
            {
                if (_cmdRemoveFunction == null)
                {
                    _cmdRemoveFunction = new RelayCommand(
                        param => RemoveFunction_Executed(param)
                        );
                }
                return _cmdRemoveFunction;
            }
        }
        private RelayCommand _cmdRemoveFunction;

        private void RemoveFunction_Executed(object param)
        {
            if (CurrentFuncPoints == null)
                return;
            foreach(var func in SeriesCollection)
                if(func.Values.Count==0)
                    SeriesCollection.Remove(func);
            SeriesCollection.Remove(CurrentFuncPoints);
            CurrentFuncPoints.Values.Clear();
            if(SeriesCollection.Count == 0)
                CurrentFuncPoints = null;
        }

        #endregion --- RemoveFunction ---

        #region --- ChangeCurrentFuncPoints ---

        public ICommand ChangeCurrentFuncPoints
        {
            get
            {
                if (_cmdChangeCurrentFunc == null)
                {
                    _cmdChangeCurrentFunc = new RelayCommand(
                        param => ChangeCurrentFuncPoints_Executed(param)
                        );
                }
                return _cmdChangeCurrentFunc;
            }
        }
        private RelayCommand _cmdChangeCurrentFunc;

        private void ChangeCurrentFuncPoints_Executed(object param) => CurrentFuncPoints = (LineSeries)(param as ChartPoint).SeriesView;

        #endregion --- ChangeCurrentFuncPoints ---

        #region --- InverseCurrentFunc ---

        public ICommand InverseCurrentFunc
        {
            get
            {
                if (_cmdInverseCurrentFunc == null)
                {
                    _cmdInverseCurrentFunc = new RelayCommand(
                        param => InverseCurrentFunc_Executed(param)
                        );
                }
                return _cmdInverseCurrentFunc;
            }
        }
        private RelayCommand _cmdInverseCurrentFunc;

        private void InverseCurrentFunc_Executed(object param)
        {
            if (CurrentFuncPoints == null)
                return;
            foreach (var obj in CurrentFuncPoints.Values)
            {
                var point = (ObservablePoint)obj;
                double cloneX = point.X;
                double cloneY = point.Y;
                point.X = cloneY;
                point.Y= cloneX;
            }
        }

        #endregion --- InverseCurrentFunc ---

        #endregion Commands

    }
}
