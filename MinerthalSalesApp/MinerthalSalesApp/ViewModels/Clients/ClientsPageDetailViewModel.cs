using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Views.Orders;

namespace MinerthalSalesApp.ViewModels.Clients
{
    public partial class ClientsPageDetailViewModel : BaseViewModel
    {

        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        public ClientsPageDetailViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases, Cliente cliente)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            Cliente =cliente;
        }

        [RelayCommand]
        async Task NovoPedidoCommand()
        {
            await Shell.Current.GoToAsync($"//{nameof(PedidoPage)}");
        }

        public Cliente Cliente { get; set; }
    }
}