using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface ILogRepository
    {
        List<Log> GetAll();

        void Add(Log log);

        void AddRange(List<Log> logs);

        void Delete(int id);

        List<Log> GetLog(DateTime data);

        int GetTotal();

        void CriarTabela();
    }
}
