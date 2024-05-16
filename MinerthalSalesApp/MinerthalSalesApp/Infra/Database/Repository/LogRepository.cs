using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class LogRepository : ILogRepository
    {
        string _dbPath;
        private readonly IAppthalContext _context;

        public LogRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }

        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Log(
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT
                                               , Data DateTime
                                               ,Pagina VARCHAR(50) null
                                               ,Tipo VARCHAR(100) null
                                               ,Descricao VARCHAR(2) null );";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Log> GetAll()
        {
            try
            {
                var command = $@"SELECT * FROM Log;";
                var lst = _context.ExcecutarSelect(command);
                if (lst == null)
                    return new List<Log>();

                var lstLog = new List<Log>();
                foreach (var item in lst)
                {
                    DateTime.TryParse(item.Data.ToString(), out DateTime _data);

                    lstLog.Add(new Log
                    {
                        Data = _data,
                        Descricao = item.Data.ToString(),
                        Pagina = item.Pagina.ToString(),
                        Tipo = item.Tipo.ToString()
                    });
                }
                return lstLog;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Add(Log log)
        {
            try
            {
                var command = $@"INSERT INTO Log(Data,Pagina,Tipo,Descricao)VALUES('{log.Data}','{log.Pagina}','{log.Tipo}','{log.Descricao}')";
                var rows = _context.ExcecutarComandoCrud(command);
                if (rows <= 0)
                    CustomExceptions.LancarExcecaoQuando(rows <= 0, "Não foram salvos novos logos");
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void AddRange(List<Log> logs)
        {
            try
            {
                if (logs!=null && logs.Count>0)
                    foreach (var item in logs)
                        Add(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var command = $@"DELETE FROM Log WHERE Id = {id}";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Log> GetLog(DateTime data)
        {
            try
            {
                var command = $@"SELECT * FROM Log WHERE Data = {data}";
                var logs = _context.ExcecutarSelect(command);

                if (logs == null)
                    return new List<Log>();

                var lista = new List<Log>();
                foreach (var item in logs)
                {
                    DateTime.TryParse(item.Data.ToString(), out DateTime _data);

                    lista.Add(new Log
                    {
                        Data = _data,
                        Descricao = item.Data.ToString(),
                        Pagina = item.Pagina.ToString(),
                        Tipo = item.Tipo.ToString()
                    });
                }

                return lista;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int GetTotal()
        {
            try
            {
                var command = $@"SELECT COUNT(*) FROM Log;";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return 0;

                var fields = retorno as IDictionary<string, object>;
                var _total = fields["COUNT(*)"];

                _=int.TryParse(_total.ToString(), out int total);
                return total;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void CriarTabela()
        {
            Init();
        }
    }
}
