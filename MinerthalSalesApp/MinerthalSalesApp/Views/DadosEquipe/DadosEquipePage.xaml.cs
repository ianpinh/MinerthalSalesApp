namespace MinerthalSalesApp.Views.DadosEquipe;

using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Infra.Database.Tables;
using Newtonsoft.Json;
using ViewModels.DadosEquipe;
public partial class DadosEquipePage : ContentPage
{
    DadosEquipeViewModel _model;
    public DadosEquipePage(DadosEquipeViewModel model)
    {
        InitializeComponent();
        _model = model;
        BindingContext = _model;
    }

    private void BtnSelectedVendedor_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var parametrosConcatenados = btn.CommandParameter.ToString();
        var parametrosSeparados = parametrosConcatenados.Split('|');

        if (parametrosSeparados.Length > 1)
        {
            var dadosVendedor = JsonConvert.DeserializeObject<Vendedor>(parametrosSeparados[1]);
            Navigation.PushAsync(new DetalheVendedorPage(dadosVendedor));
        }
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        Loaded += (s, e) =>
        {
            _model.CarregarEquipe();
        };
    }

    private async void RecarregarVendedores()
    {
        var pop = new PopupPage(new ViewModels.Shared.PopupViewModel { PopupMessage = "Aguarde..." });
        this.ShowPopup(pop);
        try
        {
            _model.CarregarEquipe();
            //var totalItensSource = ListaEquipe.ItemsSource != null ? ListaEquipe.ItemsSource.Cast<object>().Count() : 0;
            //ListaEquipe.ItemsSource = clientes;
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

    private void FiltroEquipe_TextChanged(object sender, TextChangedEventArgs e)
    {
        _model.FiltrarVendedores(FiltroEquipe.Text);
        FiltroEquipe.Focus();

    }
}