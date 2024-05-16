using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
     public interface IPedidoRepository
    {
         List<Pedido> GetAll();
         Pedido GetById(Guid id);
         Pedido GetByClientId(string codigoCliente);
         int Add(Pedido pedido);
         void AddRange(List<Pedido> produto);
         void SavePedido(List<Pedido> pedido);
         int GetTotal();
         void DeleteById(Guid id);
         void DeleteAll();
        void CriarTabela();
    }
}
