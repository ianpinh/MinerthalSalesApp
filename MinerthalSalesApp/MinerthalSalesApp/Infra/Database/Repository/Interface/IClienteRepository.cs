using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IClienteRepository
    {
        List<Cliente> GetAll();
        Cliente GetByCpf(string cpf);

        Cliente GetByCodigo(string codCliente);

        void SaveClientes(List<Cliente> clientes);

        void Add(Cliente cliente);

        void AddRange(List<Cliente> cliente);

        void Delete(int id);

        int GetTotal();

        void CriarTabela();

        List<Cliente> RecuperarClientesInadimplentes();
        void SaveClientesVendedor(List<Cliente> details, string codigoVendedor);
    }
}
