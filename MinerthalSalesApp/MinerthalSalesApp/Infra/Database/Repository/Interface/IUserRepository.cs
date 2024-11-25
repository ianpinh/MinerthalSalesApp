using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IUserRepository
    {
        List<Usuario> GetAll();

        Usuario GetByCpf(string cpf);

        Usuario GetByCodigo(string codigo);

        void SaveUsuers(List<Usuario> users);

        void Add(Usuario user);

        void AddRange(List<Usuario> users);

        void Delete(int id);

        int GetTotal();
        void CriarTabela();
    }
}
