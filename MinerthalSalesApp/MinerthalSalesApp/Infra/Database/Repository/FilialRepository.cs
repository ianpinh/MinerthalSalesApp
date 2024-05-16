using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class FilialRepository : IFilialRepository
    {
        private readonly IAppthalContext _context;
        public FilialRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS Filial(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdFilial VARCHAR(20)
                                                 ,NmFilial VARCHAR(20)
                                                 ,NrRegiao VARCHAR(20)
                                                 ,CdRcaxxx VARCHAR(20));";
            _context.ExcecutarComandoCrud(command);
        }

        public Filial GetById(int id)
        {
            var command = $@"SELECT * FROM Filial Where Id= {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Filial();

            return new Filial
            {
                Id=Convert.ToInt32(retorno.Id),
                CdFilial=retorno.CdFilial.ToString(),
                NmFilial=retorno.NmFilial.ToString(),
                NrRegiao=retorno.NrRegiao.ToString(),
                CdRcaxxx=retorno.CdRcaxxx.ToString()
            };

        }

        public List<Filial> GetAll()
        {
            var command = $@"SELECT * FROM Filial";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Filial>();

            var lst = new List<Filial>();

            foreach (var item in retorno)
            {
                lst.Add(new Filial
                {
                    Id=Convert.ToInt32(item.Id),
                    CdFilial=item.CdFilial,
                    NmFilial=item.NmFilial,
                    NrRegiao=item.NrRegiao,
                    CdRcaxxx=item.CdRcaxxx
                });
            }

            return lst;
        }

        public int Add(Filial filial)
        {
            if (filial!=null)
            {
                var commandInsert = $@"INSERT INTO [Filial](
                                                   CdFilial
                                                  ,NmFilial
                                                  ,NrRegiao
                                                  ,CdRcaxxx)
                                                            VALUES (
                                                   '{filial.CdFilial}'
                                                  ,'{filial.NmFilial}'
                                                  ,'{filial.NrRegiao}'
                                                  ,'{filial.CdRcaxxx}');";
                return _context.ExcecutarComandoCrud(commandInsert);
            }
            return 0;
        }

        public void AddRange(List<Filial> filial)
        {
            if (filial!=null && filial.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in filial)
                {
                    var commandInsert = $@"INSERT INTO [Filial](
                                                   CdFilial
                                                  ,NmFilial
                                                  ,NrRegiao
                                                  ,CdRcaxxx)
                                                            VALUES (
                                                   '{item.CdFilial}'
                                                  ,'{item.NmFilial}'
                                                  ,'{item.NrRegiao}'
                                                  ,'{item.CdRcaxxx}');";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM Filial;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _=int.TryParse(_total.ToString(), out int total);
            return total;
        }
      
        public void DeleteAll()
        {
            var command = $"Delete from Filial;";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteById(int id)
        {
            var command = $"Delete from Filial WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public void SaveFilial(List<Filial> filial)
        {
            if (filial!=null && filial.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in filial)
                {
                    var commandInsert = $@"INSERT INTO [Filial](
                                                   CdFilial
                                                  ,NmFilial
                                                  ,NrRegiao
                                                  ,CdRcaxxx)
                                                            VALUES (
                                                   '{item.CdFilial}'
                                                  ,'{item.NmFilial}'
                                                  ,'{item.NrRegiao}'
                                                  ,'{item.CdRcaxxx}');";
                    scriptCommand.AppendLine(commandInsert);
                }

               
                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public Filial GetByCodigoFilial(string codFilial)
        {
            var command = $@"SELECT * FROM Filial Where CdFilial= {codFilial};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Filial();

            return new Filial
            {
                Id=Convert.ToInt32(retorno.Id),
                CdFilial=retorno.CdFilial,
                NmFilial=retorno.NmFilial,
                NrRegiao=retorno.NrRegiao,
                CdRcaxxx=retorno.CdRcaxxx
            };
        }

        public void CriarTabela()
        {
            Init();
        }
    }
}