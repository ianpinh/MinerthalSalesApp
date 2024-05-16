using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IRankingRepository
    {
        List<Ranking> GetAll();

        Ranking GetByCodigo(string codigo);

        void SaveRanking(List<Ranking> users);

        void Add(Ranking ranking);

        void AddRange(List<Ranking> ranking);

        void Delete(int id);

        int GetTotal();
        void CriarTabela();
    }
}
