using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class BancoRepository : IBancoRepository
    {
        private readonly IAppthalContext _context;
        public BancoRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Banco(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdTipoCob VARCHAR(15)
                                                 ,DsTipocob VARCHAR(100)
                                                 ,NvCobranc VARCHAR(15)
                                                 ,InVendan VARCHAR(15)
                                                 ,InUtiliz VARCHAR(15)
                                                 ,InUtplano VARCHAR(15)
                                                 ,TxAcresc VARCHAR(15)
                                                 ,QtPrzmax VARCHAR(15)
                                                 ,InBoleto VARCHAR(15)
                                                 ,VlMinped VARCHAR(15)
                                                 ,CdRcaxxx INT);";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Banco GetById(int id)
        {
            var command = $@"SELECT * FROM Banco WHERE Id = {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Banco();

            return new Banco
            {
                Id = retorno.Id != null ? Convert.ToInt32(retorno.Id) : 0,
                CdTipoCob = retorno.CdTipoCob.ToString(),
                DsTipocob = retorno.DsTipocob.ToString(),
                NvCobranc = retorno.NvCobranc.ToString(),
                InVendan = retorno.InVendan.ToString(),
                InUtiliz = retorno.InUtiliz.ToString(),
                InUtplano = retorno.InUtplano.ToString(),
                TxAcresc = retorno.TxAcresc.ToString(),
                QtPrzmax = retorno.QtPrzmax.ToString(),
                InBoleto = retorno.InBoleto.ToString(),
                VlMinped = retorno.VlMinped.ToString(),
                CdRcaxxx = retorno.CdRcaxxx != null ? Convert.ToInt32(retorno.CdRcaxxx) : 0,
            };
        }
        public Banco RecuperarNomeTipoCobranca(string cdTipocob)
        {
            try
            {
                var command = $"SELECT * FROM Banco WHERE CdTipoCob='{cdTipocob}';";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return new Banco();

                return new Banco
                {
                    Id = retorno.Id != null ? Convert.ToInt32(retorno.Id) : 0,
                    CdTipoCob = retorno.CdTipoCob.ToString(),
                    DsTipocob = retorno.DsTipocob.ToString(),
                    NvCobranc = retorno.NvCobranc.ToString(),
                    InVendan = retorno.InVendan.ToString(),
                    InUtiliz = retorno.InUtiliz.ToString(),
                    InUtplano = retorno.InUtplano.ToString(),
                    TxAcresc = retorno.TxAcresc.ToString(),
                    QtPrzmax = retorno.QtPrzmax.ToString(),
                    InBoleto = retorno.InBoleto.ToString(),
                    VlMinped = retorno.VlMinped.ToString(),
                    CdRcaxxx = retorno.CdRcaxxx != null ? Convert.ToInt32(retorno.CdRcaxxx) : 0,
                };
            }
            catch (Exception)
            {
                return new Banco();
            }
        }
        public IEnumerable<Banco> GetAll()
        {
            var command = $@"SELECT * FROM Banco;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Banco>();

            var lstuser = new List<Banco>();
            foreach (var item in retorno)
            {
                lstuser.Add(new Banco
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdTipoCob = item.CdTipoCob.ToString(),
                    DsTipocob = item.DsTipocob.ToString(),
                    NvCobranc = item.NvCobranc.ToString(),
                    InVendan = item.InVendan.ToString(),
                    InUtiliz = item.InUtiliz.ToString(),
                    InUtplano = item.InUtplano.ToString(),
                    TxAcresc = item.TxAcresc.ToString(),
                    QtPrzmax = item.QtPrzmax.ToString(),
                    InBoleto = item.InBoleto.ToString(),
                    VlMinped = item.VlMinped.ToString(),
                    CdRcaxxx = item.CdRcaxxx != null ? Convert.ToInt32(item.CdRcaxxx) : 0,
                });
            }
            return lstuser;

        }
        public List<Banco> Pesquisa(string nomeBanco)
        {
            //return conn.Table<Banco>().Where(x => x.DsTipocob.StartsWith(nomeBanco)).ToList().Result;
            var command = $@"SELECT * FROM Banco WHERE DsTipocob LIKE '{nomeBanco}%';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Banco>();

            var lstuser = new List<Banco>();
            foreach (var item in retorno)
            {
                lstuser.Add(new Banco
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdTipoCob = item.CdTipoCob.ToString(),
                    DsTipocob = item.DsTipocob.ToString(),
                    NvCobranc = item.NvCobranc.ToString(),
                    InVendan = item.InVendan.ToString(),
                    InUtiliz = item.InUtiliz.ToString(),
                    InUtplano = item.InUtplano.ToString(),
                    TxAcresc = item.TxAcresc.ToString(),
                    QtPrzmax = item.QtPrzmax.ToString(),
                    InBoleto = item.InBoleto.ToString(),
                    VlMinped = item.VlMinped.ToString(),
                    CdRcaxxx = item.CdRcaxxx != null ? Convert.ToInt32(item.CdRcaxxx) : 0,
                });
            }
            return lstuser;
        }
        public void Add(Banco banco)
        {

            if (banco != null)
            {
                var scriptCommand = new StringBuilder();
                var commandInsert = $@"INSERT INTO [Banco](
                                                         CdTipoCob
                                                        ,DsTipocob
                                                        ,NvCobranc
                                                        ,InVendan
                                                        ,InUtiliz
                                                        ,InUtplano
                                                        ,TxAcresc
                                                        ,QtPrzmax
                                                        ,InBoleto
                                                        ,VlMinped
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{banco.CdTipoCob}'
                                                        ,'{banco.DsTipocob}'
                                                        ,'{banco.NvCobranc}'
                                                        ,'{banco.InVendan}'
                                                        ,'{banco.InUtiliz}'
                                                        ,'{banco.InUtplano}'
                                                        ,'{banco.TxAcresc}'
                                                        ,'{banco.QtPrzmax}'
                                                        ,'{banco.InBoleto}'
                                                        ,'{banco.VlMinped}'
                                                        , {banco.CdRcaxxx});";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void AddRange(List<Banco> bancos)
        {
            if (bancos != null && bancos.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in bancos)
                {
                    var commandInsert = $@"INSERT INTO [Banco](
                                                         CdTipoCob
                                                        ,DsTipocob
                                                        ,NvCobranc
                                                        ,InVendan
                                                        ,InUtiliz
                                                        ,InUtplano
                                                        ,TxAcresc
                                                        ,QtPrzmax
                                                        ,InBoleto
                                                        ,VlMinped
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{item.CdTipoCob}'
                                                        ,'{item.DsTipocob}'
                                                        ,'{item.NvCobranc}'
                                                        ,'{item.InVendan}'
                                                        ,'{item.InUtiliz}'
                                                        ,'{item.InUtplano}'
                                                        ,'{item.TxAcresc}'
                                                        ,'{item.QtPrzmax}'
                                                        ,'{item.InBoleto}'
                                                        ,'{item.VlMinped}'
                                                        , {item.CdRcaxxx});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void Delete(string codBanco)
        {
            var command = @$"DELETE FROM Banco WHERE DsTipocob = '{codBanco}';";
            _context.ExcecutarComandoCrud(command);
        }
        public void DeleteAll()
        {
            var command = @$"DELETE FROM Banco;";
            _context.ExcecutarComandoCrud(command);
        }
        public void Delete(int id)
        {
            var command = @$"DELETE FROM Banco WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }
        public void SaveProduto(List<Banco> bancos)
        {
            if (bancos != null && bancos.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM Banco;");

                foreach (var item in bancos)
                {
                    var commandInsert = $@"INSERT INTO [Banco](
                                                         CdTipoCob
                                                        ,DsTipocob
                                                        ,NvCobranc
                                                        ,InVendan
                                                        ,InUtiliz
                                                        ,InUtplano
                                                        ,TxAcresc
                                                        ,QtPrzmax
                                                        ,InBoleto
                                                        ,VlMinped
                                                        ,CdRcaxxx)
                                                    VALUES(
                                                         '{item.CdTipoCob}'
                                                        ,'{item.DsTipocob}'
                                                        ,'{item.NvCobranc}'
                                                        ,'{item.InVendan}'
                                                        ,'{item.InUtiliz}'
                                                        ,'{item.InUtplano}'
                                                        ,'{item.TxAcresc}'
                                                        ,'{item.QtPrzmax}'
                                                        ,'{item.InBoleto}'
                                                        ,'{item.VlMinped}'
                                                        , {item.CdRcaxxx});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM Banco;";
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