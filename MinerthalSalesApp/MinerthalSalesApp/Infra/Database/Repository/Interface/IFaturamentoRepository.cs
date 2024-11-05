using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IFaturamentoRepository
    {
        List<Faturamento> GetAll();
        List<Faturamento> RecuperarTitulosVencidos();
        List<Faturamento> GetByCodigo(string codCliente);

        void SaveFaturamento(List<Faturamento> fatura);
        void SaveFaturamentoVendedor(List<Faturamento> details, string codigoVendedor);

        void Add(Faturamento cliente);

        void AddRange(List<Faturamento> fatura);

        void Delete(int id);

        int GetTotal();
        void CriarTabela();
        List<Faturamento> RecuperarTitulosAVencer();
    }
}
