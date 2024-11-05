using MinerthalSalesApp.Infra.Database.Tables;
namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IResumoPedidoRepository
    {
        ResumoPedido GetById(int id);
        List<ResumoPedido> GetAll();
        void Add(ResumoPedido resumo);
        void AddRange(List<ResumoPedido> resumoPedidos);
        void Delete(string numPedido);
        void DeleteAll();
        void Delete(int id);
        List<ResumoPedido> Pesquisa(string termoBusca);
        void SavePedido(List<ResumoPedido> resumoPedidos);
        void SavePedidoVendedor(List<ResumoPedido> details, string codigoVendedor);
        int GetTotal();
        List<ResumoPedido> GetByNumPedido(string numeroPedido);
        void CriarTabela();
    }
}
