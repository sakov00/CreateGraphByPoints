using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.Generic;

namespace CreateGraphByPoints.ClassesForWorkFiles
{
    public class ContextForWorkFiles
    {
        private IWorkWithFiles _workWithFilese;

        public void SetWorkWithFiles(IWorkWithFiles strategy)
        {
            _workWithFilese = strategy;
        }

        public void LoadInFile(List<ChartValues<ObservablePoint>> param)
        {
            _workWithFilese.LoadInFile(param);
        }

        public void LoadFromFile(List<ChartValues<ObservablePoint>> param)
        {
            _workWithFilese.LoadFromFile(param);
        }
    }
}
