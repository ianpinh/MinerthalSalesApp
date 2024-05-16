using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface ICartRepository
    {
        List<Carrinho> GetAll();
        void Add(Carrinho cart);
        void AddRange(List<Carrinho> cart);
        void DeleteAll();
        void DeleteById(int id);
        void DeleteByPedido(Guid pedidoId);
        List<Carrinho> GetByOrderId(Guid pedidoId);
        int GetTotal();
        void CriarTabela();
    }
}
