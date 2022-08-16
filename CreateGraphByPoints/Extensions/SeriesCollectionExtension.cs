using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateGraphByPoints.Extensions
{
    public static class SeriesCollectionExtension
    {
        public static List<ChartValues<ObservablePoint>> SeriesCollectionConvertToListChartValues(this SeriesCollection seriesCollection)
        {
            var listChartValues = new List<ChartValues<ObservablePoint>>();
            foreach (LineSeries line in seriesCollection)
                listChartValues.Add((ChartValues<ObservablePoint>)line.Values);
            return listChartValues;
        }

        public static void ReplaceListChartValuesInSeriesCollection(this SeriesCollection seriesCollection, List<ChartValues<ObservablePoint>> listChartValues)
        {
            seriesCollection.Clear();
            foreach (var line in listChartValues)
            {
                seriesCollection.Add(new LineSeries
                {
                    Values = line,
                    LineSmoothness = 0
                });
            }
        }
    }
}
