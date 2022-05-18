using CreateGraphByPoints.Interfaces;
using LiveCharts;
using LiveCharts.Defaults;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;

namespace CreateGraphByPoints.ForWorkWithFiles
{
    internal class WorkForExcel : IForWorkWithFiles
    {
        public void LoadInFile(List<ChartValues<ObservablePoint>> param)
        {
            try
            {
                Application excelApp = new Application();
                excelApp.Workbooks.Add();
                _Worksheet workSheet = excelApp.ActiveSheet;
                int count=1;
                for (int i = 0; i < param.Count; i++)
                {
                    workSheet.Range[workSheet.Cells[1, count], workSheet.Cells[1, count + 1]].MergeCells = true;
                    workSheet.Cells[1, count] = $"Func{i}";
                    for (int j = 1; j < param[i].Count+1; j++)
                    {
                        workSheet.Cells[2, count] = "X";
                        workSheet.Cells[2, count + 1] ="Y";
                        workSheet.Cells[j+2, count] = param[i][j-1].X;
                        workSheet.Cells[j+2, count+1] = param[i][j-1].Y;
                    }
                    count += 2;
                }
                workSheet.SaveAs(string.Format(@"{0}\PointsFunc.xlsx", Environment.CurrentDirectory));                
                excelApp.Quit();
                GC.Collect();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void LoadFromFile(List<ChartValues<ObservablePoint>> param)
        {
            try 
            { 
                Application excelApp = new Application();
                Workbook excelWorkBook = excelApp.Workbooks.Open(string.Format(@"{0}\PointsFunc.xlsx", Environment.CurrentDirectory)); //открыть файл
                _Worksheet workSheet = excelApp.ActiveSheet;
                Range excelRange = workSheet.UsedRange;
                param.Clear();
                int count = 1;
                int numFun=0;
                while (true)
                {
                    if(string.IsNullOrEmpty(workSheet.Cells[1, count].Text))
                        break;
                    count += 2;
                    numFun++;
                }
                count = 1;
                for (int i = 0; i < numFun; i++)
                {
                    param.Add(new ChartValues<ObservablePoint>());
                    int j = 1;
                    while(true)
                    {
                        if (string.IsNullOrEmpty(workSheet.Cells[j + 2, count].Text))
                            break;
                        param[i].Add(new ObservablePoint());
                        param[i][j - 1].X = double.Parse(workSheet.Cells[j + 2, count].Text);
                        param[i][j - 1].Y = double.Parse(workSheet.Cells[j + 2, count + 1].Text);
                        j++;
                    }
                    count += 2;
                }
                excelApp.Quit();
                GC.Collect();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
}
    }
}
