using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CreateGraphByPoints.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected static bool IsCanProjectChanged { get; set; }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public bool GetIsProjectChanged() => IsCanProjectChanged;

    }
}
