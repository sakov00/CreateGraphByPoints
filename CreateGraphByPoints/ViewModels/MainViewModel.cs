using CreateGraphByPoints.Commands;
using System.Windows.Input;

namespace CreateGraphByPoints.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private DrawFuncViewModel _drawFuncVM;

        private SaveFileViewModel _saveFileVM;

        public static bool _isCanProjectChange;

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

        public MainViewModel()
        {
            DrawFuncVM = new DrawFuncViewModel();
            SaveFileVM = new SaveFileViewModel(DrawFuncVM);
        }


        #region Commands

        #region --- IsProjectChanged ---

        public ICommand IsProjectChanged
        {
            get
            {
                if (_cmdIsProjectChanged == null)
                {
                    _cmdIsProjectChanged = new RelayCommand(
                        param => IsProjectChanged_Executed(param)
                        );
                }
                return _cmdIsProjectChanged;
            }
        }
        private RelayCommand _cmdIsProjectChanged;

        private void IsProjectChanged_Executed(object param)
        {
            _isCanProjectChange = true;
        }

        #endregion --- IsProjectChanged ---

        #endregion Commands
    }
}
