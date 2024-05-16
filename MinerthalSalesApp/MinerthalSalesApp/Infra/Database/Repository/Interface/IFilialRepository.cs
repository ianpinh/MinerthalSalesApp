using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IFilialRepository
    {
        Filial GetById(int id);
        List<Filial> GetAll();
        int Add(Filial filial);
        void AddRange(List<Filial> filial);
        int GetTotal();
        void DeleteAll();
        void DeleteById(int id);
        void SaveFilial(List<Filial> filial);
        Filial GetByCodigoFilial(string codFilial);
        void CriarTabela();
    }
}
