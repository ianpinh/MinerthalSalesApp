using MinerthalSalesApp.ViewModels.Shared;

namespace MinerthalSalesApp.Views.Shared;

public partial class SharedLoadingPage : ContentPage
{
    public SharedLoadingPage(SharedLoadingViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}