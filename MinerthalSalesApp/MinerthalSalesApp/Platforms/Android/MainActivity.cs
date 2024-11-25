using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.Activity;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Dashboard;
using MinerthalSalesApp.Views.Orders;

namespace MinerthalSalesApp;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        OnBackPressedDispatcher.AddCallback(this, new BackPress(this));
    }
}

class BackPress : OnBackPressedCallback
{
    private readonly Activity activity;
    private long backPressed;

    public BackPress(Activity activity) : base(true)
    {
        this.activity = activity;
    }

    public override void HandleOnBackPressed()
    {
        var shell = Shell.Current.CurrentItem.Route.ToString();
        var context = Microsoft.Maui.ApplicationModel.Platform.AppContext;
        var current = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
        var callingActive = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.CallingActivity;
        var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;
        var lastPageNavigation = navigation.NavigationStack.Count > 0 && navigation.NavigationStack[navigation.NavigationStack.Count - 1] != null ? navigation.NavigationStack[navigation.NavigationStack.Count - 1].ToString() : string.Empty;


        if (shell.Contains("AdminDashboardPage"))
        {
            Process.KillProcess(Process.MyPid());
        }
        else if (lastPageNavigation.Contains("ClientsPageDetail") || shell.Contains("ClientsPageDetail"))
        {
            Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
        }
        else if (lastPageNavigation.Contains("DetalhePedidoPage") || shell.Contains("DetalhePedidoPage"))
        {
            Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
        }
        else if (lastPageNavigation.Contains("ProdutosPedidoPage") || shell.Contains("ProdutosPedidoPage"))
        {
            // Shell.Current.GoToAsync($"//{nameof(PedidoPage)}");
        }
        else if (lastPageNavigation.Contains("PedidoPage") || shell.Contains("PedidoPage"))
        {
            Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");

        }
        else if (lastPageNavigation.Contains("CarrinhoPage") || shell.Contains("CarrinhoPage"))
        {
            var _alertService = App.AlertService;
            _alertService.ShowConfirmation("Carrinho", "Deseja abandonar o pedido?", (result =>
            {
                if (result)
                {
                    _alertService.ShowAlertAsync("Carrinho", "Pedido cancelado", "OK");

                    Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
                }
            }));
            // Shell.Current.GoToAsync($"//{nameof(ProdutosPedidoPage)}");
        }

        else if ((lastPageNavigation.Contains("MeusPedidosPage") || shell.Contains("MeusPedidosPage"))
                || (lastPageNavigation.Contains("ClientsPage") || shell.Contains("ClientsPage"))
                || (lastPageNavigation.Contains("AtualizacaoPage") || shell.Contains("AtualizacaoPage"))
                || (lastPageNavigation.Contains("RankingPage") || shell.Contains("RankingPage"))
                || (lastPageNavigation.Contains("ProdutosPage") || shell.Contains("ProdutosPage")))
        {
            Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
        }
        else
        {
            Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
        }
    }
}



/// <summary>  
/// IAndroidMethods  
/// </summary>  
public interface IAndroidMethods
{
    void CloseApp();
}


public class AndroidMethods : IAndroidMethods
{
    /// <summary>  
    /// CloseApp  
    /// </summary>  
    public void CloseApp()
    {
        Process.KillProcess(Process.MyPid());
    }
}


