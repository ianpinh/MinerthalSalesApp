namespace MinerthalSalesApp.ViewModels.Shared
{
    public partial class PopupViewModel : BaseViewModel
    {

        private string popupMessage;
        public string PopupMessage
        {
            get => popupMessage;
            set
            {
                popupMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PopupMessage));
            }
        }
    }
}
