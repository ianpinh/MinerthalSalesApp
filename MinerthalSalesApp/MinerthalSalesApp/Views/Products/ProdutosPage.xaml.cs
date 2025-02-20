using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.ViewModels.Products;

namespace MinerthalSalesApp.Views.Products;

public partial class ProdutosPage : ContentPage
{
    ProdutosPageViewModel _model;

    public ProdutosPage(ProdutosPageViewModel viewModel)
    {
        InitializeComponent();
        _model = viewModel;
        BindingContext = _model;
    }

    private void FiltroProduto_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textoFiltro = FiltroProduto.Text;
        var lst = _model.FiltrarProdutos(textoFiltro);
        ListaProdutos.ItemsSource = lst;
        LblTotalProdutos.Text = $"Produtos encontrados: {lst.Count}";
        FiltroProduto.Focus();
    }

    private void EditarProduto_Clicked(object sender, SelectedItemChangedEventArgs e)
    {
        var model = e.SelectedItem as Produto;
        Navigation.PushAsync(new ProdutoEditarPage(model));
    }
}