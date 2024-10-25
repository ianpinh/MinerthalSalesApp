using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using System.Windows.Input;

namespace MinerthalSalesApp.ViewModels.DadosEquipe
{
	public class DadosEquipeViewModel : BaseViewModel
	{
		private readonly IPopupAppService _popupAppService;
		private readonly IAlertService _alertService;
		private bool _isBusy = false;
		public DadosEquipeViewModel(IPopupAppService popupAppService, IAlertService alertService)
		{
			_popupAppService = popupAppService ?? throw new ArgumentNullException(nameof(popupAppService));
			_alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
			CarregarEquipe();
		}


		private IEnumerable<Vendedor> listaVendedores;
		public IEnumerable<Vendedor> ListaVendedores
		{
			get => listaVendedores;
			set
			{
				listaVendedores = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ListaVendedores));
			}
		}

		public void CarregarEquipe()
		{
			var user = App.UserDetails;
			var codigoRca = user.Codigo;
			var vendedores = App.VendedorRepository.GetByCodigoSupervisor(codigoRca);

			if (listaVendedores != null)
				listaVendedores = Enumerable.Empty<Vendedor>();

			ListaVendedores = vendedores;
		}


		private bool isRefreshing;
		public bool IsRefreshing
		{
			get => isRefreshing;
			set
			{
				isRefreshing = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsRefreshing));
			}
		}
		public ICommand RefreshCommand => new Command(async () =>
		{
			CarregarEquipe();
			IsRefreshing = false;

		});

		public void FiltrarVendedores(string textoBusca)
		{
			var lista = Enumerable.Empty<Vendedor>();
			var lst = App.VendedorRepository.GetByCodigoSupervisor(App.UserDetails.Codigo);

			if (lst.Any())
			{
				if (string.IsNullOrWhiteSpace(textoBusca))
				{
					lista = lst;
				}
				else
				{
					lista = lst.Where(x =>
								  x.CdRca.ToLower().Contains(textoBusca.ToLower())
							   || x.NmRca.ToLower().Contains(textoBusca.ToLower())
							   || x.NmCidade.ToLower().Contains(textoBusca.ToLower())
							   || x.CdUf.ToLower().Contains(textoBusca.ToLower())
							   || x.NrCpf.ToLower().Contains(textoBusca.ToLower())
							   ).OrderBy(x => x.NmRca).ToList();
				}
			}

			if (listaVendedores != null)
				listaVendedores = Enumerable.Empty<Vendedor>();

			ListaVendedores = lista;
		}
	}
}
