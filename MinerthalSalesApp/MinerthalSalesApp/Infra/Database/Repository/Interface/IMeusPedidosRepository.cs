using MinerthalSalesApp.Infra.Database.Tables;
namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IMeusPedidosRepository
    {
        MeusPedidos Get(int id);

        List<MeusPedidos> GetAll();

        void DeleteAll();

        void DeleteById(int id);

        void Add(MeusPedidos pedido);

        void AddRange(List<MeusPedidos> meusPedidos);
        void SaveMeusPedidos(List<MeusPedidos> pedidos);

        int GetTotal();
        void CriarTabela();
    }
}
