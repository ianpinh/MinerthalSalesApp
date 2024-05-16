using MinerthalSalesApp.ViewModels.Clients;

namespace MinerthalSalesApp.Views.Clients;

public partial class HistoricoPage : ContentPage
{
	public HistoricoPage(HistoricoPageViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
	}
}