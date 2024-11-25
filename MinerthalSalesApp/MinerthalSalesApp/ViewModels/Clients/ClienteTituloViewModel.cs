using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;

namespace MinerthalSalesApp.ViewModels.Clients
{
    public class ClienteTituloViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        public ClienteTituloViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases, Cliente cliente)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            Cliente = cliente;
        }

        public Cliente Cliente { get; set; }
    }
}
