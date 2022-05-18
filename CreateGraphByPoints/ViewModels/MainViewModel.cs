using CreateGraphByPoints.Interfaces;

namespace CreateGraphByPoints.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainVM
    {
        private IDrawFunc _drawFuncVM;

        private ISaveFile _saveFileVM;

        private bool _isCanProjectChange;

        public ISaveFile SaveFileVM
        {
            get => _saveFileVM;
            set
            {
                _saveFileVM = value;
                OnPropertyChanged();  
            }
        }

        public IDrawFunc DrawFuncVM
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
