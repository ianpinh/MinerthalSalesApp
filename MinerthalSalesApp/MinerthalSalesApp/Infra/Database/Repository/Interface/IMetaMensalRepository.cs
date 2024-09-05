using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IMetaMensalRepository
    {
        MetaMensal GetById(int id);
        IEnumerable<MetaMensal> GetAll();
        void Add(MetaMensal MetaAnual);
        void AddRange(List<MetaMensal> MetaAnuals);
        void DeleteAll();
        void Delete(int id);
        void SaveMeta(List<MetaMensal> MetaAnuals);
        int GetTotal();
        void CriarTabela();
    }
}
