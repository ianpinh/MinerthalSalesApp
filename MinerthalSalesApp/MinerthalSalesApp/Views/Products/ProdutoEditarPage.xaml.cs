using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.ViewModels.Products;

namespace MinerthalSalesApp.Views.Products;

public partial class ProdutoEditarPage : ContentPage
{
    private Produto _model;
    private readonly AppTheme theme;
    public ProdutoEditarPage(ProdutoEditarViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }

    public ProdutoEditarPage(Produto model)
    {
        _model = model;
        BindingContext = model;
        InitializeComponent();
        var produto = App.ProdutosRepository.GetByCodProduto(model.CdProduto);
        //InvoicingList.ItemsSource = lista;
        //InvoicingList.VerticalScrollBarVisibility = ScrollBarVisibility.Always;
    }

    public ProdutoEditarPage()
    {
        InitializeComponent();
    }
}