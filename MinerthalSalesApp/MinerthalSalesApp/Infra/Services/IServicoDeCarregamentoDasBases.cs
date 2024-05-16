using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.ViewModels.Startup;

namespace MinerthalSalesApp.Infra.Services
{
    public interface IServicoDeCarregamentoDasBases
    {
        bool AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum tipo);
        Task AtualizarBaseDeDados(ApiMinertalTypes tipo);
        int AtualizarBaseDeDadosPrimeiraCarga(AtualizacaoViewModel model);

		Task AtualizarBaseDeDados();

		Task<int> CarregarClientesAsync();

        Task<int> CarregarUsuariosAsync();

        Task<(string sucesso, string Mensagem)> TransmitirPedidos();

        Task<(string sucesso, string Mensagem)> TransmitirPedidos(Guid pedidoId);
        void DeleteAllTables(string dropCommand);
    }
}
