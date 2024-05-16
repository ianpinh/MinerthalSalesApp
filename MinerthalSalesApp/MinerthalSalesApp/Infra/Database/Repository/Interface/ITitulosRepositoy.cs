using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface ITitulosRepositoy
    {
         void SaveTitulos(List<Titulo> titulos);

         List<Titulo> RecuperarTodosTitulos();

         List<Titulo> RecuperarTodosTitulosDoCliente(string codCliente);
        void CriarTabela();
    }
}
