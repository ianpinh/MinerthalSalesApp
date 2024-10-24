using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IVendedorRepository
    {
        List<Vendedor> GetAll();

        Vendedor GetById(int id);

        Vendedor GetByCodigo(string rca);
        IEnumerable<Vendedor> GetByCodigoSuperviso(string rcaxxx);

        void Add(Vendedor vendedor);

        void AddRange(List<Vendedor> salers);

        void DeleteById(int id);

        void DeleteAll();

        int GetTotal();

        void SaveVendedor(List<Vendedor> salers);
        void CriarTabela();
    }
}
