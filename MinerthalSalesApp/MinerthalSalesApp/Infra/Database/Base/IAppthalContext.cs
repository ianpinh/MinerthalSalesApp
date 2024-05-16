namespace MinerthalSalesApp.Infra.Database.Base
{
    public interface IAppthalContext
    {
         int ExcecutarComandoCrud(string command);
         IEnumerable<dynamic> ExcecutarSelect(string command);
         dynamic ExcecutarSelectFirstOrDefault(string command);
    }
}
