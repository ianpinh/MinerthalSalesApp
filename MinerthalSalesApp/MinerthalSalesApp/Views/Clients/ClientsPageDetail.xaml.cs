using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics.Converters;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.ViewModels.Clients;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Orders;
using Newtonsoft.Json;

namespace MinerthalSalesApp.Views.Clients;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ClientsPageDetail : ContentPage, IAsyncInitialization
{
 
    private ClientsPageDetailViewModel _model;
    private readonly AppTheme theme;
    IPopupAppService _popupAppService;
    public Task Initialization { get; private set; }
    public ClientsPageDetail(ClientsPageDetailViewModel model)
    {
        _model = model;
        _popupAppService=App.PopupAppService;
        InitializeComponent();
        BindingContext = _model;
        var codigo = $"{_model.Cliente.A1Cod}{_model.Cliente.A1Loja}";
        Initialize(codigo);
      
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //await Task.Delay(500);
        //ImgUserLoading.IsAnimationPlaying = false;

        //await Task.Delay(500);
        //ImgUserLoading.IsAnimationPlaying = true;


        //ListaClientes.SelectedItem = null;
        //await Task.Delay(1500);

        //Loaded+=(s, e) =>
        //{
        //    if (!string.IsNullOrWhiteSpace(FiltroCliente.Text))
        //        FiltroCliente.Text=string.Empty;

        //    RecarregarClientes();
        //};


    }

   

    private void Initialize(string codigo)
    {
        ListaDeFaturamento(codigo);
        ListaDeVisitacoes(codigo);
    }

    private void BtnGerais_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        detailsview.IsVisible = true;
        historyview.IsVisible = false;
        Filesview.IsVisible = false;
        titulosview.IsVisible = false;
        visitasview.IsVisible = false;
        BtnGerais.BorderColor = Color.FromArgb("#5d5d5d");
        BtnGerais.BorderWidth = 4;
    }

    private void BtnOutros_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        detailsview.IsVisible = false;
        historyview.IsVisible = true;
        Filesview.IsVisible = false;
        titulosview.IsVisible = false;
        visitasview.IsVisible = false;
        BtnOutros.BorderColor = Color.FromArgb("#5d5d5d");
        BtnOutros.BorderWidth = 4;
    }

    private void BtnFinanceiro_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        detailsview.IsVisible = false;
        historyview.IsVisible = false;
        Filesview.IsVisible = true;
        titulosview.IsVisible = false;
        visitasview.IsVisible = false;

        BtnFinanceiro.BorderColor = Color.FromArgb("#5d5d5d");
        BtnFinanceiro.BorderWidth = 4;
    }

    private void BtnTitulo_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        titulosview.IsVisible = true;
        detailsview.IsVisible = false;
        historyview.IsVisible = false;
        Filesview.IsVisible = false;
        visitasview.IsVisible = false;
        BtnTitulo.BorderColor = Color.FromArgb("#5d5d5d");
        BtnTitulo.BorderWidth = 4;

        //InvoicingList.ItemsSource = Teste.Testes;

    }

    private void BtnVisitas_Clicked(object sender, EventArgs e)
    {
        //var _popupModel = new PopupViewModel { PopupMessage = "Carregando visitas..." };
        //var pop = new PopupPage(_popupModel);
        //_popupAppService.ShowPopup(pop);
        //Thread.Sleep(500);

        SetSelectedBorder();
        titulosview.IsVisible = false;
        detailsview.IsVisible = false;
        historyview.IsVisible = false;
        Filesview.IsVisible = false;
        visitasview.IsVisible = true;
    }

    //INCLUIR NOVO PEDIDO
    private async void BtnNovoPedido_clicked(object sender, EventArgs e)
    {
        /*if (_model.Cliente.A1Atr>0)
        {
            await DisplayAlert("Novo Pedido","O cliente possui pendências","Ok");
        }
        else
        {*/
            var _popupModel = new PopupViewModel { PopupMessage = "Carregando novo pedido..." };
            var pop = new PopupPage(_popupModel);

            await Task.Delay(500);
            _popupAppService.ShowPopup(pop);

            try
            {
                var btn = sender as Button;
            //var codCliente = btn.CommandParameter.ToString();
            var codigo = $"{_model.Cliente.A1Cod}{_model.Cliente.A1Loja}";
            var codCliente = codigo.Substring(0, 6);
            var codLoja = codigo.Substring(codigo.Length - 2);
            var order = new Models.Dtos.OrderDto {Id=Guid.NewGuid(), CodigoCliente=codCliente };
            var pedidoViewModel = new PedidoViewModel(App.AlertService, App.ServicoDeCarregamentoDasBases, order);
            Microsoft.Maui.Storage.Preferences.Set("pedidoViewModel", JsonConvert.SerializeObject(pedidoViewModel));
            await Navigation.PushAsync(new PedidoPage(pedidoViewModel));
            //Navigation.RemovePage(this);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Pedido", ex.Message, "Ok");
            }
            finally
            {
                _popupAppService.ClosePopup(pop);
            }
        //}
    }

    private void SetSelectedBorder()
    {

        BtnGerais.BorderColor = Color.FromArgb("#eee");
        BtnGerais.BorderWidth = 1;

        BtnOutros.BorderColor = Color.FromArgb("#eee");
        BtnOutros.BorderWidth = 1;

        BtnFinanceiro.BorderColor = Color.FromArgb("#eee");
        BtnFinanceiro.BorderWidth = 1;

        BtnTitulo.BorderColor = Color.FromArgb("#eee");
        BtnTitulo.BorderWidth = 1;
    }

    private void ListaDeFaturamento(string codigo)
    {
        var lst = new List<Faturamento>();
        var theme = Application.Current.RequestedTheme;
        var lista = App.FaturamentoRepository.GetByCodigo(codigo);
        if (lista != null && lista.Any())
        {
            lista=lista.Select(c => { c.StatusVencimento = CorStatusVencimento(c.DtVenc); return c; }).OrderBy(x=>x.DataDeVencimento).ToList();
            InvoicingList.ItemsSource = lista;
            InvoicingList.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        }
    }

    private void ListaDeVisitacoes(string codigo)
    {
        var lista = App.VisitasRepository.RecuperarTodasVisitasDoCliente(codigo);
        if (lista != null && lista.Any())
        {
            var lst = lista.OrderByDescending(x => x.DataReg).Take(10).ToList();
            ListaVisitas.ItemsSource = lst;
            ListaVisitas.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
        }
    }

    private Color CorStatusVencimento(string vencimento)
    {
        var converterColor = new ColorTypeConverter();
        try
        {
            var color = (Color)converterColor.ConvertFromInvariantString("black");
            if (!string.IsNullOrWhiteSpace(vencimento))
            {
                if (DateTime.TryParse(vencimento, out DateTime data))
                {
                    if (DateTime.Compare(DateTime.Now.Date, data.Date) > 0)
                    {
                        color = (Color)converterColor.ConvertFromInvariantString("red");
                    }
                }
            }
            else
            {
                color = theme == AppTheme.Light ? (Color)converterColor.ConvertFromInvariantString("black") : (Color)converterColor.ConvertFromInvariantString("white");

            }
            return color;


        }
        catch (Exception)
        {
            throw;
        }
    }

    //CARREGAR HISTÓRICO DO CLIENTE
    private async void BtnHistorico_Clicked(object sender, EventArgs e)
    {
        var pop = new PopupPage(new PopupViewModel { PopupMessage="aguarde..." });
        this.ShowPopup(pop);
        await Task.Delay(1000);
        try
        {
            var btn = (Button)sender;
            var model = new HistoricoPageViewModel(btn.CommandParameter.ToString());
            await Navigation.PushAsync(new HistoricoPage(model));
            Navigation.RemovePage(this);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Clientes", ex.Message, "Ok");
        }
        finally
        {
            await pop.CloseAsync();
        }
    }


  
}
