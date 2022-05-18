using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Input;

namespace CreateGraphByPoints.Interfaces
{
    public interface IDrawFunc
    {
        SeriesCollection SeriesCollection { get; set; }

        LineSeries CurrentFuncPoints { get; set; }

        ICommand AddPoint { get; }

        ICommand RemovePoint { get; }

        ICommand AddFunction { get; }

        ICommand RemoveFunction { get; }

        ICommand ChangeCurrentFuncPoints { get; }

        ICommand InverseCurrentFunc { get; }
    }
}
