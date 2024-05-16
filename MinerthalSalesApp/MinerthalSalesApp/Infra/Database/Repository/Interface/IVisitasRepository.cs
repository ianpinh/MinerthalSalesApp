using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface IVisitasRepository
    {
        void SaveVisitasAsync(List<Visita> visitas);
        IEnumerable<Visita> RecuperarTodasVisitas();
        IEnumerable<Visita> RecuperarTodasVisitasDoCliente(string codCliente);
        void CriarTabela();
    }
}
