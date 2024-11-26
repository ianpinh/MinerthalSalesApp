using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.ViewModels.Dashboard;

namespace MinerthalSalesApp.Views.Dashboard;

public partial class AdminDashboardPage : ContentPage
{
    IPopupAppService _popupAppService;

    public AdminDashboardPage(DashboardPageViewModel viewModel, IPopupAppService popupAppService)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
        _popupAppService = popupAppService;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Navigation.RemovePage(this);
    }
}