using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IBancoRepository
    {
        Banco GetById(int id);
        Banco RecuperarNomeTipoCobranca(string cdTipocob);
        IEnumerable<Banco> GetAll();
        List<Banco> Pesquisa(string nomeBanco);
        void Add(Banco banco);
        void AddRange(List<Banco> bancos);
        void Delete(string codBanco);
        void DeleteAll();
        void Delete(int id);
        void SaveProduto(List<Banco> bancos);
        int GetTotal();
        void CriarTabela();
    }
}
