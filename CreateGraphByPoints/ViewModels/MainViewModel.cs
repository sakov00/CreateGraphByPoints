namespace CreateGraphByPoints.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DrawFuncViewModel _drawFuncVM;

        private SaveFileViewModel _saveFileVM;

        private bool _isCanProjectChanged;

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

        public bool IsCanProjectChanged
        {
            get => _isCanProjectChanged;
            set
            {
                _isCanProjectChanged = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(DrawFuncViewModel drawFuncVM, SaveFileViewModel saveFileVM)
        {
            DrawFuncVM = drawFuncVM;
            SaveFileVM = saveFileVM;
        }
    }
}
