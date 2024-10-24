using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IAtualizacaoRepository
    {
        bool GetByTableName(string tableName);

        List<Atualizacoes> GetAll();

        void Add(Atualizacoes log);

        void AddRange(List<Atualizacoes> logs);

        void Delete(int id);

        DateTime? GetLastLog(string tabela);

        int GetTotal();
        
        void DeleteAllTables(string dropCommand);

		void ClearAllTables();

		void CriarTabela();
    }
}
