using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Shared;
using Newtonsoft.Json;
using SQLitePCL;

namespace MinerthalSalesApp.Views.Orders;

public partial class ProdutosPedidoPage : ContentPage
{
    private readonly PedidoViewModel model;
    public ProdutosPedidoPage(PedidoViewModel viewModel)
    {
        model = viewModel != null ? viewModel : App.PedidoViewModel;
        BindingContext = model;
        InitializeComponent();
    }

    public ProdutosPedidoPage()
    {
        model = App.PedidoViewModel;
        BindingContext = model;
        InitializeComponent();
    }

    private void FecharLoading()
    {
        GridLoading.IsVisible = false;
        if (ListaProdutos.SelectedItem != null)
            ListaProdutos.SelectedItem = null;
    }

    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(500);
        ImgUserLoading.IsAnimationPlaying = false;

        await Task.Delay(500);
        ImgUserLoading.IsAnimationPlaying = true;

        Loaded += (s, e) =>
        {
            if (!string.IsNullOrWhiteSpace(FiltroProduto.Text))
                FiltroProduto.Text = string.Empty;

            RecarregarProdutos();

        };
    }

    private async void RecarregarProdutos()
    {
        var pop = new PopupPage(new ViewModels.Shared.PopupViewModel { PopupMessage = "Aguarde..." });
        this.ShowPopup(pop);
        try
        {
            var lst = model.FiltrarProdutos(string.Empty);
            ListaProdutos.ItemsSource = lst;
            LblTotalProdutos.Text = $"Produtos encontrados: {lst.Count}";
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

    private void FiltroProduto_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textoFiltro = FiltroProduto.Text;
        var lst = model.FiltrarProdutos(textoFiltro);

        var totalItensSource = ListaProdutos.ItemsSource != null ? ListaProdutos.ItemsSource.Cast<object>().Count() : 0;

        if (lst.Count > totalItensSource)
        {
            ListaProdutos.ItemsSource = lst;
            LblTotalProdutos.Text = $"Produtos encontrados: {lst.Count}";
        }
    }

    public async Task<(decimal ValorMinimo, decimal ValorMaximo, decimal PrecoProduto, int QtdMin, int QtdMax, decimal ProdutoPrecoMaximo, List<TabelaPreco> TbPrecosProduto)> RecuperarValoresComissoes(Produto produto)
    {
        try
        {
            Vendedor saler;
            if (App.VendedorSelecionado != null)
            {
                var codigo = App.VendedorSelecionado.CodigoVendedor;
                saler = App.VendedorRepository.GetByCodigo(codigo);
            }
            else
            {

                var userDetailStr = new UserBasicInfo();
                if (Preferences.ContainsKey(nameof(App.UserDetails)))
                { }

                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);

                saler = App.VendedorRepository.GetByCodigo(userDetailStr.Codigo);
            }

            if (saler.Id == 0)
            {
                var totalVendedores = App.VendedorRepository.GetTotal();
                if (totalVendedores == 0)
                    throw new CustomExceptions("A tabela de vendedores esta vazia, por favor atualizar as bases");
                else
                    throw new CustomExceptions("Dados do vendedor não encontrado");
            }

            var tipoTabela = saler.TabPreco;
            var codigoProduto = produto.CdProduto;
            var usuario = App.UserRepository.GetByCodigo(saler.CdRca);
            var tbPrecos = App.TabelaPrecoRepository.Get(produto.CdProduto, model.Pedido.FilialMinerthal, tipoTabela, model.Pedido.TipoVenda);

            if (tbPrecos == null || !tbPrecos.Any())
                throw new CustomExceptions($"Não foram encontradas as faixas de valores para o produto {produto.CodigoProduto}-{produto.DsProduto}");

            var precoProduto = tbPrecos.FirstOrDefault().VlVvenda;
            var valorMinimo = tbPrecos.Min(x => x.PerMin); ;
            var valorMaximo = tbPrecos.Max(x => x.PerMax);
            var qtdMin = tbPrecos.Min(x => x.QtdMin);
            var qtdMax = tbPrecos.Max(x => x.QtdMax);

            var produtoPrecoMaximo = precoProduto + valorMaximo;

            return (valorMinimo, valorMaximo, precoProduto, qtdMin, qtdMax, produtoPrecoMaximo, tbPrecos);

        }
        catch (Exception)
        {
            throw;
        }
    }




    private async void BtnSelectedProduto_Clicked(object sender, EventArgs e)
    {
        try
        {
            var btn = sender as Button;
            var cdProduto = btn.CommandParameter.ToString();
            var produto = App.ProdutosRepository.GetByCodProduto(cdProduto);
            CarregarTelaDeProdutos(produto);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Carregar produtos", $"{ex.Message}", "Ok");
        }
    }

    private async void CarregarTelaDeProdutos(Produto produto)
    {
        var _popmodel = new PopupViewModel { PopupMessage = "carregando carrinho..." };
        var pop = new PopupPage(_popmodel);
        this.ShowPopup(pop);
        await Task.Delay(1000);
        try
        {
            if (produto != null)
            {
                var ordem = 0;
                if (model.Pedido.ItensPedido.Any())
                    ordem = model.Pedido.ItensPedido.Max(x => x.Ordem);

                var (ValorMinimo, ValorMaximo, PrecoProduto, QtdMin, QtdMax, ProdutoPrecoMaximo, TbPrecosProduto) = await RecuperarValoresComissoes(produto);

                //var index = model.Pedido.ItensPedido.FindIndex(x => x.CodProduto == produto.CdProduto);

                //if (index<0)
                //{

                model.ItemDeCalculoCarrinho = new ItensDto
                {
                    CodProduto = produto.CdProduto,
                    ImagemProduto = produto.ImagemProduto,
                    Produto = produto,
                    CodigoNomeProduto = produto.CodigoProduto,
                    Ordem = (byte)(ordem + 1),
                    FreteUnidade = produto.VlPeso == 25 ? model.Pedido.ValorFrete25 : model.Pedido.ValorFrete30,
                    ValorDescontoMinimo = ValorMinimo,
                    ValorDescontoMaximo = ValorMaximo,
                    ValorBrutoProduto = PrecoProduto,
                    ValorCombinado = PrecoProduto,
                    QuantidadeMin = QtdMin,
                    QuantidadeMax = QtdMax,
                    TbPrecosProduto = TbPrecosProduto,
                    Quantidade = 1,
                    ProdutoPesp = produto.ProdutoPesp
                };

                await Navigation.PushAsync(new CarrinhoPage(model));
                await pop.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            await pop.CloseAsync();
            await DisplayAlert("Carregar produtos", $"{ex.Message}", "Ok");
        }
    }

    private void FiltroProduto_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        var textoFiltro = FiltroProduto.Text;
        var lst = model.FiltrarProdutos(textoFiltro);
        ListaProdutos.ItemsSource = lst;
        LblTotalProdutos.Text = $"Produtos encontrados: {lst.Count}";
        //var totalItensSource = ListaProdutos.ItemsSource!=null ? ListaProdutos.ItemsSource.Cast<object>().Count() : 0;

        //if (lst.Count > totalItensSource)
        //{
        //    ListaProdutos.ItemsSource = lst;
        //    LblTotalProdutos.Text = $"Produtos encontrados: {lst.Count}";
        //}
    }
}