using CreateGraphByPoints.Commands;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DrawFuncViewModel _drawFuncVM;

        private SaveFileViewModel _saveFileVM;

        private bool _isCanProjectChange;

        public SaveFileViewModel SaveFileVM
        {
            get => _saveFileVM;
            set
            {
                _saveFileVM = value;
                OnPropertyChanged();  
            }
        }

        public DrawFuncViewModel DrawFuncVM
        {
            get => _drawFuncVM;
            set
            {
                _drawFuncVM = value;
                OnPropertyChanged();
            }
        }

        public bool IsCanProjectChange
        {
            get => _isCanProjectChange;
            set
            {
                _isCanProjectChange = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            DrawFuncVM = new DrawFuncViewModel(this);
            SaveFileVM = new SaveFileViewModel(DrawFuncVM, this);
        }
    }
}
