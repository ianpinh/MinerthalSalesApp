using MinerthalSalesApp.ViewModels.Startup;

namespace MinerthalSalesApp.Views.Startup;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        try
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        catch (Exception ex)
        {
            DisplayAlert("Alert", $"Erro ao tentar fazer o login. Erro : {ex.Message}", "OK");
        }
    }
}