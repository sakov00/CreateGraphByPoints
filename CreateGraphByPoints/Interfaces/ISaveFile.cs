using System.Windows.Input;

namespace CreateGraphByPoints.Interfaces
{
    public interface ISaveFile
    {
        ICommand LoadInExcelFile { get; }

        ICommand LoadFromExcelFile { get; }

        ICommand LoadInXmlFile { get; }

        ICommand LoadFromXmlFile { get; }
    }
}
