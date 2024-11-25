namespace MinerthalSalesApp.ViewModels.Shared
{
    public partial class SharedLoadingViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;

        public SharedLoadingViewModel(IAlertService alertService)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
        }
    }
}
