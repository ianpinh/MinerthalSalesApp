using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class MetaMensalRepository : IMetaMensalRepository
    {
        private readonly IAppthalContext _context;
        public MetaMensalRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS MetaMensal(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdRca VARCHAR(15)
                                                 ,TipoMeta VARCHAR(10)
                                                 ,VlMetaMes INTEGER
                                                 ,Mes VARCHAR(15)
                                                 ,Ano INTEGER
                                                 ,CdRcaxxx VARCHAR(15));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public MetaMensal GetById(int id)
        {
            var command = $@"SELECT * FROM MetaMensal WHERE Id = {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new MetaMensal();

            return new MetaMensal
            {
                Id = retorno.Id != null ? Convert.ToInt32(retorno.Id) : 0,
                CdRca = retorno.CdRca.ToString(),
                TipoMeta = retorno.TipoMeta.ToString(),
                VlMetaMes = retorno.VlMetaMes != null ? Convert.ToInt16(retorno.VlMetaMes) : 0,
                Mes = retorno.InVendan.ToString(),
                Ano = retorno.Ano != null ? Convert.ToInt16(retorno.Ano) : 0,
                CdRcaxxx = retorno.CdRcaxxx.ToString()
            };
        }
        public IEnumerable<MetaMensal> GetAll()
        {
            var command = $@"SELECT * FROM MetaMensal;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<MetaMensal>();

            var lstuser = new List<MetaMensal>();
            foreach (var item in retorno)
            {


                var meta = new MetaMensal();
                meta.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                meta.CdRca = item.CdRca.ToString();
                meta.TipoMeta = item.TipoMeta.ToString();
                meta.VlMetaMes = item.VlMetaMes != null ? Convert.ToInt16(item.VlMetaMes) : 0;
                meta.Mes = item.Mes.ToString();
                meta.Ano = item.Ano != null ? Convert.ToInt16(item.Ano) : 0;
                meta.CdRcaxxx = item.CdRcaxxx.ToString();


                lstuser.Add(meta);
                //lstuser.Add(new MetaMensal
                //{
                //    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                //    CdRca = item.CdRca.ToString(),
                //    TipoMeta = item.TipoMeta.ToString(),
                //    VlMetaMes = item.Mes != null ? Convert.ToInt16(item.VlMetaMes) : 0,
                //    Mes = item.InVendan.ToString(),
                //    Ano = item.Ano != null ? Convert.ToInt16(item.Ano) : 0,
                //    CdRcaxxx = item.CdRcaxxx.ToString()
                //});
            }
            return lstuser;

        }

        public void Add(MetaMensal MetaMensal)
        {

            if (MetaMensal != null)
            {
                var scriptCommand = new StringBuilder();
                var commandInsert = $@"INSERT INTO [MetaMensal](
                                                         CdRca
                                                        ,TipoMeta
                                                        ,VlMetaMes
                                                        ,Mes
                                                        ,Ano
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{MetaMensal.CdRca}'
                                                        ,'{MetaMensal.TipoMeta}'
                                                        , {MetaMensal.VlMetaMes}
                                                        ,'{MetaMensal.Mes}'
                                                        , {MetaMensal.Ano}
                                                        , {MetaMensal.CdRcaxxx});";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void AddRange(List<MetaMensal> MetaMensals)
        {
            if (MetaMensals != null && MetaMensals.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in MetaMensals)
                {
                    var commandInsert = $@"INSERT INTO [MetaMensal](
                                                         CdRca
                                                        ,TipoMeta
                                                        ,VlMetaMes
                                                        ,Mes
                                                        ,Ano
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{item.CdRca}'
                                                        ,'{item.TipoMeta}'
                                                        , {item.VlMetaMes}
                                                        ,'{item.Mes}'
                                                        , {item.Ano}
                                                        , {item.CdRcaxxx});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void DeleteAll()
        {
            var command = @$"DELETE FROM MetaMensal;";
            _context.ExcecutarComandoCrud(command);
        }
        public void Delete(int id)
        {
            var command = @$"DELETE FROM MetaMensal WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }
        public void SaveMeta(List<MetaMensal> MetaMensals)
        {
            if (MetaMensals != null && MetaMensals.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM MetaMensal;");

                foreach (var item in MetaMensals)
                {
                    var commandInsert = $@"INSERT INTO [MetaMensal](
                                                         CdRca
                                                        ,TipoMeta
                                                        ,VlMetaMes
                                                        ,Mes
                                                        ,Ano
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{item.CdRca}'
                                                        ,'{item.TipoMeta}'
                                                        , {item.VlMetaMes}
                                                        ,'{item.Mes}'
                                                        , {item.Ano}
                                                        , {item.CdRcaxxx});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public int GetTotal()
        {
            var command = $@"SELECT COUNT(*) FROM MetaMensal;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _ = int.TryParse(_total.ToString(), out int total);
            return total;
        }
        public void CriarTabela()
        {
            Init();
        }
    }
}
