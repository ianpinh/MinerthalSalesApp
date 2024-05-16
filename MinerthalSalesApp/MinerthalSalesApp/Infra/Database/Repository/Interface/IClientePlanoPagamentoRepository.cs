using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IClientePlanoPagamentoRepository
    {
        ClientePlanoPagamento GetById(int id);
        List<ClientePlanoPagamento> GetAll();
        void Add(ClientePlanoPagamento plano);
        void AddRange(List<ClientePlanoPagamento> planos);
        void DeleteAll();
        void Delete(int id);
        void Save(List<ClientePlanoPagamento> plano);
        int RecuperarPlanoPadrao(string codigo, string loja);
        void CriarTabela();
    }
}
