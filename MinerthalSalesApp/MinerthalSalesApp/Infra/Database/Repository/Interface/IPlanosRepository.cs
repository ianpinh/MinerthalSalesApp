using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
     public interface IPlanosRepository
    {
         List<Plano> GetAll();

         void Add(Plano plano);

         void AddRange(List<Plano> planos);

         void DeleteById(int id);

         void DeleteAll();

         void Save(List<Plano> planos);

         Plano GetByCode(string planoPagamento);

         int GetTotal();
        void CriarTabela();
    }
}
