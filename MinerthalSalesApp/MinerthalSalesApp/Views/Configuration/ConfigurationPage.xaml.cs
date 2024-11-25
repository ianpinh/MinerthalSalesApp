using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.ViewModels;
using MinerthalSalesApp.ViewModels.Shared;

namespace MinerthalSalesApp.Views.Configuration;

public partial class ConfigurationPage : ContentPage
{
    public PopupPage _popupPage;
    private readonly IAlertService _alertService;
    private readonly IServicoDeCarregamentoDasBases _servicoAtualizacaoBases;

    public ConfigurationPage(ConfigurationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _alertService = App.AlertService;
        _servicoAtualizacaoBases = App.ServicoDeCarregamentoDasBases;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }


    //EVENTOS DE ATUALIZAÇÃO
    public async void AtualizarBaseDeDadosMinerthal_Clicked(object sender, EventArgs e)
    {
        var _model = new PopupViewModel
        {
            PopupMessage = "Atualizando as bases..."
        };
        var popup = new PopupPage(_model);
        this.ShowPopup(popup);
        try
        {
            var imgButton = (ImageButton)sender;
            ApiMinertalTypes tipo = (ApiMinertalTypes)Enum.Parse(typeof(ApiMinertalTypes), imgButton.CommandParameter.ToString(), true);

            await _servicoAtualizacaoBases.AtualizarBaseDeDados(tipo);

            await DisplayAlert("Atualizar Bases", "Bases atualizadas com sucesso.", "OK");
            AtualizarTotais();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Atualizar Bases", ex.Message, "OK");
            await _alertService.ShowAlertAsync("Atualizar Todos", $"Tente novamente em alguns minutos", "OK");
        }
        finally
        {
            popup.Close();
        }
    }

    private void AtualizarTotais()
    {

        var totalTbPrecos = App.TabelaPrecoRepository.GetTotal();
        var totalProdutos = App.ProdutosRepository.GetTotal();
        var totalClientes = App.ClienteRepository.GetTotal();
        var totalPlanos = App.PlanosRepository.GetTotal();
        var totalRanking = App.RankingRepository.GetTotal();
        var totalFiliais = App.FilialRepository.GetTotal();
        var totalBancos = App.BancoRepository.GetTotal();

        LblProdutos.Text = $"Atualizar Produtos-  {totalProdutos}";
        LblPlanos.Text = $"Atualizar Planos -  {totalPlanos}";
        LblPClientes.Text = $"Atualizar Clientes -  {totalClientes}";
        LblRanking.Text = $"Atualizar Ranking -  {totalRanking}";
        LblFiliais.Text = $"Atualizar Filiais -  {totalFiliais}";
        LblBanco.Text = $"Atualizar Bancos -  {totalBancos}";
        LblTabelaPrecos.Text = $"Atualizar Tabela de Preços -  {totalTbPrecos}";
    }
}