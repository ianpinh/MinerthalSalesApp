using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IHistoricoPedidoReposity
    {
        HistoricoDePedidos GetById(int id);
        List<HistoricoDePedidos> GetAll();
        List<HistoricoDePedidos> GetAllFromLastYear();
        List<HistoricoDePedidos> GetAllFromLastCurrentMonth();
        IEnumerable<HistoricoDePedidos> GetAllFromClient(string codigoCliente);
        void Add(HistoricoDePedidos historico);
        void AddRange(List<HistoricoDePedidos> historico);
        void Delete(int id);
        void DeleteAll();
        List<HistoricoDePedidos> Pesquisa(string termo);
        void SaveHistorico(List<HistoricoDePedidos> historico);
        int GetTotal();
        void CriarTabela();
        List<HistoricoDePedidos> PedidosEmAberto();
        List<HistoricoDePedidos> CarregamentoDePedidos();
    }
}
