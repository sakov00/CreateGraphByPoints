using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.Generic;

namespace CreateGraphByPoints.Interfaces
{
    public interface IWorkWithFiles
    {
        void LoadInFile(List<ChartValues<ObservablePoint>> param);

        void LoadFromFile(List<ChartValues<ObservablePoint>> param);
    }
}
