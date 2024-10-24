namespace MinerthalSalesApp.Infra.Database.Base
{
    public interface IAppthalContext
    {
         int ExcecutarComandoCrud(string command);
        int ExcecutarComandoCrudNoCommit(string command);

		 IEnumerable<dynamic> ExcecutarSelect(string command);
        List<Dictionary<string, object>> ExecutarComandoConsulta(string query);
         dynamic ExcecutarSelectFirstOrDefault(string command);
    }
}
