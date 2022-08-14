using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.Generic;

namespace CreateGraphByPoints.Interfaces
{
    public interface ILoadAndUnloadFunctionsInFile
    {
        void LoadInFile(List<ChartValues<ObservablePoint>> param);
        List<ChartValues<ObservablePoint>> LoadFromFile();
    }
}
