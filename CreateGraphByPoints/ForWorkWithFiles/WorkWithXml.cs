using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CreateGraphByPoints.ForWorkWithFiles
{
    public class WorkWithXml : ILoadAndUnloadFunctionsInFile
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

        public List<ChartValues<ObservablePoint>> LoadFromFile()
        {
            var dataFromFile = new List<ChartValues<ObservablePoint>>();
            var xmlSerializer = new XmlSerializer(typeof(List<ChartValues<ObservablePoint>>));
            using (FileStream fs = new FileStream(string.Format(@"{0}\PointsFunc.xml", Environment.CurrentDirectory), FileMode.OpenOrCreate))
            {
                dataFromFile = (List<ChartValues<ObservablePoint>>)xmlSerializer.Deserialize(fs);
            }
            return dataFromFile;
        }
    }
}
