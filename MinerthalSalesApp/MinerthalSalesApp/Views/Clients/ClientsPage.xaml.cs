using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.ViewModels.Clients;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.ServiceModel.Channels;

namespace MinerthalSalesApp.Views.Clients;

public partial class ClientsPage : ContentPage, IAsyncInitialization
{
    ClientViewModel _model;
    public Task Initialization { get; private set; }
    private const string clientePageKey = "chaveClienteDetalhes";
    public ClientsPage(ClientViewModel viewModel)
    {
        InitializeComponent();
        _model = viewModel;
        BindingContext = _model;
        Initialization = InitializeAsync(viewModel);
    }
    public ClientsPage()
    {
        
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(500);
        ImgUserLoading.IsAnimationPlaying = false;

        await Task.Delay(500);
        ImgUserLoading.IsAnimationPlaying = true;


        ListaClientes.SelectedItem = null;
        await Task.Delay(1500);

        Loaded+=(s, e) =>
        {
            if (!string.IsNullOrWhiteSpace(FiltroCliente.Text))
                FiltroCliente.Text=string.Empty;

            RecarregarClientes();

            if (GridLoading.IsVisible)
                GridLoading.IsVisible = false;
        };
    }

    private async void RecarregarClientes()
    {
        var pop = new PopupPage(new ViewModels.Shared.PopupViewModel { PopupMessage="Aguarde..." });
        this.ShowPopup(pop);
        try
        {
            var clientes = await _model.FiltrarClientesAsync(string.Empty);
            var totalItensSource = ListaClientes.ItemsSource!=null ? ListaClientes.ItemsSource.Cast<object>().Count() : 0;
			ListaClientes.ItemsSource = clientes;
			//if (clientes.Count() > totalItensSource)
			//{
			//    ListaClientes.ItemsSource = clientes;
			//    await Task.Delay(1000);
			//    //FiltroCliente.Focus();
			//}
		}
        catch (Exception ex)
        {
            await DisplayAlert("Clientes", $"Erro ao recarregar a tela de clientes.  Erro:{ex.Message}", "Ok");
        }
        finally
        {
            await pop.CloseAsync();
        }

    }

    //protected override bool OnBackButtonPressed()
    //{
    //    if (WebView.CanGoBack)
    //    {
    //        WebView.GoBack();
    //        return true;
    //    }
    //    else
    //    {
    //        base.OnBackButtonPressed();
    //        return false;

    //}

    private void FiltroCliente_TextChanged(object sender, TextChangedEventArgs e)
    {
        CancellationTokenSource source = new CancellationTokenSource();
        source.CancelAfter(TimeSpan.FromSeconds(1));


        var lst = _model.FiltrarClientesAsync(FiltroCliente.Text).GetAwaiter();
        lst.OnCompleted(() =>
        {
            var clientes = lst.GetResult();
            ListaClientes.ItemsSource = clientes;
        });

        FiltroCliente.Focus();
    }

    private async Task InitializeAsync(ClientViewModel model)
    {
        //SetSessionValue(model);
    }

    [RelayCommand]
    void PerformSearch()
    {

    }

    private void ListaClientes_ItemSelected(object sender, SelectionChangedEventArgs e)
    {
        Cliente model = e.CurrentSelection[0] as Cliente;
        var alertService = App.AlertService;
        var servicoDeCarregamentoDasBases = App.ServicoDeCarregamentoDasBases;
        var viewModel = new ClientsPageDetailViewModel(alertService, servicoDeCarregamentoDasBases, model);
        Navigation.PushAsync(new ClientsPageDetail(viewModel));
    }

    private void ListViewListaClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Cliente model = e.SelectedItem as Cliente;
        if (model != null)
        {
            var alertService = App.AlertService;
            var servicoDeCarregamentoDasBases = App.ServicoDeCarregamentoDasBases;
            var viewModel = new ClientsPageDetailViewModel(alertService, servicoDeCarregamentoDasBases, model);
            Navigation.PushAsync(new ClientsPageDetail(viewModel));
        }
    }

    private async void BtnSelectedCliente_Clicked(object sender, EventArgs e)
    {
        var pop = new PopupPage(new ViewModels.Shared.PopupViewModel { PopupMessage="Aguarde..." });
        this.ShowPopup(pop);
        try
        {
            var btn = sender as Button;
            btn.BorderColor= Color.FromArgb("#2b4661");

            //var codCliente = btn.CommandParameter.ToString();

            // Extrair os parâmetros concatenados
            var parametrosConcatenados = btn.CommandParameter.ToString();
            var parametrosSeparados = parametrosConcatenados.Split('-');

            if (parametrosSeparados.Length != 2)
            {
                throw new InvalidOperationException("Parâmetros inválidos no CommandParameter.");
            }

            var codCliente = parametrosSeparados[0].Trim();
            var lojaCliente = parametrosSeparados[1].Trim();
            var model = App.ClienteRepository.GetByCodigo(codCliente+lojaCliente);
            var viewModel = new ClientsPageDetailViewModel(App.AlertService, App.ServicoDeCarregamentoDasBases, model);
            await Navigation.PushAsync(new ClientsPageDetail(viewModel));
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            await pop.CloseAsync();
        }
    }

    void SetSessionValue(ClientViewModel model)
    {
        var session = DependencyService.Get<ISession>();
        var data = JsonConvert.SerializeObject(model);
        SecureStorage.SetAsync(clientePageKey, data);
    }

    void ClearSessionValue(ClientViewModel model)
    {
        
        var session = DependencyService.Get<ISession>();
        SecureStorage.Remove(clientePageKey);

    }
}
