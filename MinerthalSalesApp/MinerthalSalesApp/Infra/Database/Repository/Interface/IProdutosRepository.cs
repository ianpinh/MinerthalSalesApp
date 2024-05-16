using MinerthalSalesApp.Infra.Database.Tables;
namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IProdutosRepository
    {
        Produto GetById(int id);
        Produto GetByCodProduto(string codigo);

        IEnumerable<Produto> GetAll();
        IEnumerable<Produto> GetByCodigo(string codCliente);
        IEnumerable<Produto> GetProdutoPrecoDefault();

        void SaveProduto(List<Produto> produtos);
        void Add(Produto produto);
        void AddRange(List<Produto> produto);
        void Delete(int id);
        int GetTotal();
        void CriarTabela();
    }
}
