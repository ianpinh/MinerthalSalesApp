using MinerthalSalesApp.ViewModels.Orders;

namespace MinerthalSalesApp.Views.Orders;

public partial class GeraisPage : ContentPage
{
	public GeraisPage(PedidoViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}