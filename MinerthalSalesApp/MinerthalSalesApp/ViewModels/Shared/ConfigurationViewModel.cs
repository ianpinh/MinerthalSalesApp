using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models.Enums;

namespace MinerthalSalesApp.ViewModels.Shared
{
    public partial class ConfigurationViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        public ConfigurationViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases??throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            AtualizarTotais();
        }


		
		private int totalClientes;
		public int TotalClientes
		{
			get => totalClientes;
			set
			{
				totalClientes = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalClientes));
			}
		}
		
		private int totalBancos;
		public int TotalBancos
		{
			get => totalBancos;
			set
			{
				totalBancos = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalBancos));
			}
		}
	
		private int totalRanking;
		public int TotalRanking
		{
			get => totalRanking;
			set
			{
				totalRanking = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalRanking));
			}
		}
		
		private int totalFiliais;
		public int TotalFiliais
		{
			get => totalFiliais;
			set
			{
				totalFiliais = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalFiliais));
			}
		}
		
		private int totalTabelaDePrecos;
		public int TotalTabelaDePrecos
		{
			get => totalTabelaDePrecos;
			set
			{
				totalTabelaDePrecos = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalTabelaDePrecos));
			}
		}
		
		private int totalProdutos;
		public int TotalProdutos
		{
			get => totalProdutos;
			set
			{
				totalProdutos = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalProdutos));
			}
		}
		
		private int totalPlanos;
		public int TotalPlanos
		{
			get => totalPlanos;
			set
			{
				totalPlanos = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TotalPlanos));
			}
		}


		[RelayCommand]
        async Task AtualizarBaseDeDadosMinerthal(string tipo)
        {
            var _model = new PopupViewModel
            {
                PopupMessage = "Atualizando as bases..."
            };
            var pop = new PopupPage(_model);
            try
            {
                Shell.Current.CurrentPage.ShowPopup(pop);

                if (string.IsNullOrWhiteSpace(tipo))
                {
                    ApiMinertalTypes _tipo = (ApiMinertalTypes)Enum.Parse(typeof(ApiMinertalTypes), tipo, true);
                    await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(_tipo);
                }

                AtualizarTotais();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                pop.Close();
            }
        }


        public void AtualizarTotais()
        {
            TotalClientes = App.ClienteRepository.GetTotal();
            TotalBancos = App.BancoRepository.GetTotal();
            TotalRanking = App.RankingRepository.GetTotal();
            TotalFiliais = App.FilialRepository.GetTotal();
            TotalTabelaDePrecos = App.TabelaPrecoRepository.GetTotal();
            TotalProdutos = App.ProdutosRepository.GetTotal();
            TotalPlanos = App.PlanosRepository.GetTotal();

        }
    }
}