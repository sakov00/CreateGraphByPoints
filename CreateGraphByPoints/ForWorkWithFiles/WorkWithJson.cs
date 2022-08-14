using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CreateGraphByPoints.ForWorkWithFiles
{
    public class WorkWithJson : ILoadAndUnloadFunctionsInFile
    {
        public void LoadInFile(List<ChartValues<ObservablePoint>> param)
        {
            using (StreamWriter file = File.CreateText(string.Format(@"{0}\PointsFunc.json", Environment.CurrentDirectory)))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, param);
            }
        }

        public List<ChartValues<ObservablePoint>> LoadFromFile()
        {
            List<ChartValues<ObservablePoint>> dataFromFile;
            using (StreamReader r = new StreamReader(string.Format(@"{0}\PointsFunc.json", Environment.CurrentDirectory)))
            {
                string json = r.ReadToEnd();
                dataFromFile = JsonConvert.DeserializeObject<List<ChartValues<ObservablePoint>>>(json);
            }
            return dataFromFile;
        }
    }
}
