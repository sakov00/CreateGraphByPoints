namespace CreateGraphByPoints.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private InteractionOnCanvasViewModel interactionOnCanvasVM;

        private InteractionWithFilesViewModel interactionWithFilesVM;

        public InteractionWithFilesViewModel InteractionWithFilesVM
        {
            get => interactionWithFilesVM;
            set
            {
                interactionWithFilesVM = value;
                OnPropertyChanged();  
            }
        }

        public InteractionOnCanvasViewModel InteractionOnCanvasVM
        {
            get => interactionOnCanvasVM;
            set
            {
                interactionOnCanvasVM = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(InteractionOnCanvasViewModel paramInteractionOnCanvasVM, InteractionWithFilesViewModel paramInteractionWithFilesVM)
        {
            interactionOnCanvasVM = paramInteractionOnCanvasVM;
            interactionWithFilesVM = paramInteractionWithFilesVM;
        }
    }
}
