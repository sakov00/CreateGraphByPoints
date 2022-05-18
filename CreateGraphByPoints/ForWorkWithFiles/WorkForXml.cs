using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CreateGraphByPoints.ForWorkWithFiles
{
    internal class WorkForXml : IForWorkWithFiles
    {
        public void LoadInFile(List<ChartValues<ObservablePoint>> param)
        {
            File.WriteAllText(string.Format(@"{0}\PointsFunc.xml", Environment.CurrentDirectory), string.Empty);
            var xmlSerializer = new XmlSerializer(typeof(List<ChartValues<ObservablePoint>>));
            using (FileStream fs = new FileStream(string.Format(@"{0}\PointsFunc.xml", Environment.CurrentDirectory), FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, param);
            }
        }

        public void LoadFromFile(List<ChartValues<ObservablePoint>> param)
        {
            var dataFromFile = new List<ChartValues<ObservablePoint>>();
            var xmlSerializer = new XmlSerializer(typeof(List<ChartValues<ObservablePoint>>));
            using (FileStream fs = new FileStream(string.Format(@"{0}\PointsFunc.xml", Environment.CurrentDirectory), FileMode.OpenOrCreate))
            {
                dataFromFile = (List<ChartValues<ObservablePoint>>)xmlSerializer.Deserialize(fs);
            }
            param.Clear();
            foreach (var obj in dataFromFile)
            {
                param.Add(obj);
            }
        }
    }
}
