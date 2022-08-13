using CreateGraphByPoints.Commands;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class InteractionOnCanvasViewModel : BaseViewModel
    {
        private SeriesCollection seriesCollection;

        private LineSeries currentFunction;

        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set
            {
                seriesCollection = value;
                OnPropertyChanged();
            }
        }

        public LineSeries CurrentFunction
        {
            get => currentFunction;
            set
            {
                currentFunction = value;
                OnPropertyChanged();
            }
        }

        public InteractionOnCanvasViewModel()
        {
            SeriesCollection = new SeriesCollection();
            
            AddPoint = new RelayCommand(param => AddPoint_Executed(param));
            RemovePoint = new RelayCommand(param => RemovePoint_Executed(param));
            AddFunction = new RelayCommand(param => AddFunction_Executed(param));
            RemoveFunction = new RelayCommand(param => RemoveFunction_Executed(param));
            ChangeCurrentFunction = new RelayCommand(param => ChangeCurrentFunction_Executed(param));
            InverseCurrentFunction = new RelayCommand(param => InverseCurrentFunction_Executed(param));

            AddFunction_Executed(null);
        }

        #region Commands

        #region --- AddPoint ---

        public ICommand AddPoint { get; private set; }

        private void AddPoint_Executed(object param)
        {
            if (CurrentFunction == null)
            {
                MessageBox.Show("Please add function on canvas or select function");
                return;
            }
            CurrentFunction.Values.Add(new ObservablePoint());
            IsCanProjectChanged = true;
        }

        #endregion --- AddPoint ---

        #region --- RemovePoint ---

        public ICommand RemovePoint { get; private set; }

        private void RemovePoint_Executed(object param)
        {
            CurrentFunction.Values.Remove((ObservablePoint)param);
            IsCanProjectChanged = true;
        }

        #endregion --- RemovePoint ---

        #region --- AddFunction ---

        public ICommand AddFunction { get; private set; }

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
            int indexAddedFunction = SeriesCollection.Count - 1;
            CurrentFunction = (LineSeries)SeriesCollection[indexAddedFunction];
            IsCanProjectChanged = true;
        }
        #endregion --- AddFunction ---

        #region --- RemoveFunction ---

        public ICommand RemoveFunction { get; private set; }

        private void RemoveFunction_Executed(object param)
        {
            if (CurrentFunction == null)
                return;
            SeriesCollection.Remove(CurrentFunction);
            CurrentFunction = null;
            IsCanProjectChanged = true;
        }

        #endregion --- RemoveFunction ---

        #region --- ChangeCurrentFunction ---

        public ICommand ChangeCurrentFunction { get; private set; }

        private void ChangeCurrentFunction_Executed(object param) => CurrentFunction = (LineSeries)(param as ChartPoint).SeriesView;

        #endregion --- ChangeCurrentFunction ---

        #region --- InverseCurrentFunction ---

        public ICommand InverseCurrentFunction { get; private set; }

        private void InverseCurrentFunction_Executed(object param)
        {
            if (CurrentFunction == null)
                return;
            foreach (var obj in CurrentFunction.Values)
            {
                var point = (ObservablePoint)obj;
                double cloneX = point.X;
                double cloneY = point.Y;
                point.X = cloneY;
                point.Y= cloneX;
            }
            IsCanProjectChanged = true;
        }

        #endregion --- InverseCurrentFunction ---

        #endregion Commands
    }
}
