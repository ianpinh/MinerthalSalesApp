using MinerthalSalesApp.ViewModels.Usuarios;

namespace MinerthalSalesApp.Views.Ranking;

public partial class RankingPage : ContentPage
{
	public RankingPage(RankingViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}
