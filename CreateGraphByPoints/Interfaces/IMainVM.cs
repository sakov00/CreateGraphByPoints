namespace CreateGraphByPoints.Interfaces
{
    public interface IMainVM
    {
        ISaveFile SaveFileVM { get; set; }

        IDrawFunc DrawFuncVM { get; set; }

        bool IsCanProjectChange { get; set; }
    }
}
