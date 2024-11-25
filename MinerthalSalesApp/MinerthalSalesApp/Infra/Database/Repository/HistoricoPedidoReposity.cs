using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class HistoricoPedidoReposity : IHistoricoPedidoReposity
    {
        private string NomeTabelaHistoricoPedidos => RecuperarNomeDaTabela();
        private string NomeTabelaCliente => RecuperarNomeDaTabelaCliente();
        private string NomeTabelaResumoPedido => RecuperarNomeDaTabelaResumoPedido();

        private readonly IAppthalContext _context;
        public HistoricoPedidoReposity(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private void Init(string tableName)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS {tableName}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdCliente  VARCHAR(15)
                                                  ,NrPedido  VARCHAR(15)
                                                  ,DtPedido VARCHAR(15)
                                                  ,TpPedido  VARCHAR(15)
                                                  ,NrNota  VARCHAR(15)
                                                  ,DtEmissao VARCHAR(15)
                                                  ,TxObs1  VARCHAR(150)
                                                  ,X5Chave  VARCHAR(15)
                                                  ,NrCarreg  VARCHAR(15)
                                                  ,CdTipocob  VARCHAR(15)
                                                  ,CdPlano  VARCHAR(15)
                                                  ,DsPlano  VARCHAR(150)
                                                  ,PedidoMaxima  VARCHAR(15)
                                                  ,ZyMinum  VARCHAR(15)
                                                  ,DtSaidaMerc  VARCHAR(15)
                                                  ,Transportador  VARCHAR(150)
                                                  ,Credito  VARCHAR(15)
                                                  ,Ordem  VARCHAR(15)
                                                  ,CdRcaxxx  VARCHAR(15)
                                                  ,VlFinal DECIMAL(7,2)
                                                  ,VlTotal DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        private void InitResumoPedido_(string tableName)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS {tableName}(
                                                   Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,NrPedido  VARCHAR(20)
                                                  ,CdProduto VARCHAR(20)
                                                  ,DsProduto VARCHAR(120)
                                                  ,NumLote VARCHAR(20)
                                                  ,CdRcaxxx VARCHAR(20)
                                                  ,ImagemProduto VARCHAR(100)
                                                  ,QtProduto INT
                                                  ,QtAtend INT
                                                  ,VlVenda DECIMAL(7,2)
                                                  ,VlUnit DECIMAL(7,2)
                                                  ,VlFrete DECIMAL(7,2)
                                                  ,CdPercComiss DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }
        private void InitCliente(string tableName)
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS {tableName}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,A1Cgc VARCHAR(20) NULL
                                                 ,A1Cod VARCHAR(20) NULL
                                                 ,A1Loja VARCHAR(30) NULL
                                                 ,A1Nome VARCHAR(150) NULL
                                                 ,A1Nreduz VARCHAR(150) NULL
                                                 ,A1Nomprp1 VARCHAR(100) NULL
                                                 ,A1Nomprp2 VARCHAR(100) NULL
                                                 ,A1Tipo VARCHAR(100) NULL
                                                 ,A1Pessoa VARCHAR(100) NULL
                                                 ,A1Msblql VARCHAR(100) NULL
                                                 ,A1Condpag VARCHAR(100) NULL
                                                 ,A1Inscr VARCHAR(30) NULL
                                                 ,A1Observ VARCHAR(500) NULL
                                                 ,A1Ddd VARCHAR(20) NULL
                                                 ,A1Telex VARCHAR(20) NULL
                                                 ,A1Email VARCHAR(100) NULL
                                                 ,A1Este VARCHAR(20) NULL
                                                 ,A1Mune VARCHAR(100) NULL
                                                 ,A1Bairroe VARCHAR(100) NULL
                                                 ,A1Endent VARCHAR(300) NULL
                                                 ,A1Ultcom VARCHAR(200) NULL
                                                 ,A1Lc DECIMAL(7,2)
                                                 ,LcDisponivel DECIMAL(7,2)
                                                 ,AVencer DECIMAL(7,2)
                                                 ,A1Atr DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public HistoricoDePedidos GetById(int id)
        {
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} WHERE Id = {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new HistoricoDePedidos();

            return new HistoricoDePedidos
            {
                Id = retorno.Id != null ? Convert.ToInt32(retorno.Id) : 0,
                CdCliente = retorno.CdCliente.ToString(),
                NrPedido = retorno.NrPedido.ToString(),
                DtPedido = retorno.dtPedido.ToString(),
                TpPedido = retorno.TpPedido.ToString(),
                NrNota = retorno.NrNota.ToString(),
                DtEmissao = retorno.DtEmissao.ToString(),
                TxObs1 = retorno.TxObs1.ToString(),
                X5Chave = retorno.X5Chave.ToString(),
                NrCarreg = retorno.NrCarreg.ToString(),
                CdTipocob = retorno.CdTipocob.ToString(),
                CdPlano = retorno.CdPlano.ToString(),
                DsPlano = retorno.DsPlano.ToString(),
                PedidoMaxima = retorno.PedidoMaxima.ToString(),
                ZyMinum = retorno.ZyMinum.ToString(),
                DtSaidaMerc = retorno.DtSaidaMerc.ToString(),
                Transportador = retorno.Transportador.ToString(),
                Credito = retorno.Credito.ToString(),
                Ordem = retorno.Ordem.ToString(),
                CdRcaxxx = retorno.CdRcaxxx.ToString(),
                VlFinal = retorno.VlFinal != null ? Convert.ToDecimal(retorno.VlFinal) : 0M,
                VlTotal = retorno.VlTotal != null ? Convert.ToDecimal(retorno.VlTotal) : 0M
            };
        }

        public List<HistoricoDePedidos> GetAll()
        {
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} Order by Id Desc;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {
                try
                {
                    var historico = new HistoricoDePedidos();
                    historico.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    historico.CdCliente = item.CdCliente != null ? item.CdCliente.ToString() : "";
                    historico.NrPedido = item.NrPedido != null ? item.NrPedido.ToString() : "";
                    historico.DtPedido = item.DtPedido != null ? item.DtPedido.ToString() : "";
                    historico.TpPedido = item.TpPedido != null ? item.TpPedido.ToString() : "";
                    historico.NrNota = item.NrNota != null ? item.NrNota.ToString() : "";
                    historico.DtEmissao = item.DtEmissao != null ? item.DtEmissao.ToString() : "";
                    historico.TxObs1 = item.TxObs1 != null ? item.TxObs1.ToString() : "";
                    historico.X5Chave = item.X5Chave != null ? item.X5Chave.ToString() : "";
                    historico.NrCarreg = item.NrCarreg != null ? item.NrCarreg.ToString() : "";
                    historico.CdTipocob = item.CdTipocob != null ? item.CdTipocob.ToString() : "";
                    historico.CdPlano = item.CdPlano != null ? item.CdPlano.ToString() : "";
                    historico.DsPlano = item.DsPlano != null ? item.DsPlano.ToString() : "";
                    historico.PedidoMaxima = item.PedidoMaxima != null ? item.PedidoMaxima.ToString() : "";
                    historico.ZyMinum = item.ZyMinum != null ? item.ZyMinum.ToString() : "";
                    historico.DtSaidaMerc = item.DtSaidaMerc != null ? item.DtSaidaMerc.ToString() : "";
                    historico.Transportador = item.Transportador != null ? item.Transportador.ToString() : "";
                    historico.Credito = item.Credito != null ? item.Credito.ToString() : "";
                    historico.Ordem = item.Ordem != null ? item.Ordem.ToString() : "";
                    historico.CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "";
                    historico.VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M;
                    historico.VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0;
                    lstHistorico.Add(historico);
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Command = command
                    });
                }
            }
            return lstHistorico;
        }

        public List<HistoricoDePedidos> GetAllFromLastYear()
        {
            //  return lista!=null && lista.Any() ? lista.OrderByDescending(x => x.DataPedido).Take(50).ToList() : new List<HistoricoDePedidos>();
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos};";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {

                lstHistorico.Add(new HistoricoDePedidos
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdCliente = item.CdCliente.ToString(),
                    NrPedido = item.NrPedido.ToString(),
                    DtPedido = item.dtPedido.ToString(),
                    TpPedido = item.TpPedido.ToString(),
                    NrNota = item.NrNota.ToString(),
                    DtEmissao = item.DtEmissao.ToString(),
                    TxObs1 = item.TxObs1.ToString(),
                    X5Chave = item.X5Chave.ToString(),
                    NrCarreg = item.NrCarreg.ToString(),
                    CdTipocob = item.CdTipocob.ToString(),
                    CdPlano = item.CdPlano.ToString(),
                    DsPlano = item.DsPlano.ToString(),
                    PedidoMaxima = item.PedidoMaxima.ToString(),
                    ZyMinum = item.ZyMinum.ToString(),
                    DtSaidaMerc = item.DtSaidaMerc.ToString(),
                    Transportador = item.Transportador.ToString(),
                    Credito = item.Credito.ToString(),
                    Ordem = item.Ordem.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M,
                    VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0M
                });
            }
            return lstHistorico != null && lstHistorico.Any() ? lstHistorico.OrderByDescending(x => x.DataPedido).Take(50).ToList() : new List<HistoricoDePedidos>();

        }

        public List<HistoricoDePedidos> GetAllFromLastCurrentMonth()
        {
            var mesAnterior = DateTime.Today.AddMonths(-1);
            var lista = GetAll();
            var historico = lista.Where(x => x.DataPedido.Value >= mesAnterior).ToList();
            return lista != null && lista.Any() ? lista.OrderByDescending(x => x.DataPedido).Take(20).ToList() : new List<HistoricoDePedidos>();
        }

        public void Add(HistoricoDePedidos historico)
        {
            if (historico != null)
            {
                var scriptCommand = new StringBuilder();

                var commandInsert = $@"INSERT INTO [{NomeTabelaHistoricoPedidos}](
                                                    CdCliente 
                                                   ,NrPedido 
                                                   ,dtPedido
                                                   ,TpPedido 
                                                   ,NrNota 
                                                   ,DtEmissao
                                                   ,TxObs1 
                                                   ,X5Chave 
                                                   ,NrCarreg 
                                                   ,CdTipocob 
                                                   ,CdPlano 
                                                   ,DsPlano 
                                                   ,PedidoMaxima 
                                                   ,ZyMinum 
                                                   ,DtSaidaMerc 
                                                   ,Transportador 
                                                   ,Credito 
                                                   ,Ordem 
                                                   ,CdRcaxxx 
                                                   ,VlFinal
                                                   ,VlTotal)
                                                            VALUES (
                                                    '{historico.CdCliente}'
                                                   ,'{historico.NrPedido}'
                                                   ,'{historico.DtPedido}'
                                                   ,'{historico.TpPedido}'
                                                   ,'{historico.NrNota}'
                                                   ,'{historico.DtEmissao}'
                                                   ,'{historico.TxObs1}'
                                                   ,'{historico.X5Chave}'
                                                   ,'{historico.NrCarreg}'
                                                   ,'{historico.CdTipocob}'
                                                   ,'{historico.CdPlano}'
                                                   ,'{historico.DsPlano}'
                                                   ,'{historico.PedidoMaxima}'
                                                   ,'{historico.ZyMinum}'
                                                   ,'{historico.DtSaidaMerc}'
                                                   ,'{historico.Transportador}'
                                                   ,'{historico.Credito}'
                                                   ,'{historico.Ordem}'
                                                   ,'{historico.CdRcaxxx}'
                                                   , {historico.VlFinal.ToStringInvariant("0.00")}
                                                   , {historico.VlTotal.ToStringInvariant("0.00")});";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void AddRange(List<HistoricoDePedidos> historico)
        {
            if (historico != null && historico.Any())
            {
                var scriptCommand = new StringBuilder();


                foreach (var item in historico)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaHistoricoPedidos}](
                                                    CdCliente 
                                                   ,NrPedido 
                                                   ,dtPedido
                                                   ,TpPedido 
                                                   ,NrNota 
                                                   ,DtEmissao
                                                   ,TxObs1 
                                                   ,X5Chave 
                                                   ,NrCarreg 
                                                   ,CdTipocob 
                                                   ,CdPlano 
                                                   ,DsPlano 
                                                   ,PedidoMaxima 
                                                   ,ZyMinum 
                                                   ,DtSaidaMerc 
                                                   ,Transportador 
                                                   ,Credito 
                                                   ,Ordem 
                                                   ,CdRcaxxx 
                                                   ,VlFinal
                                                   ,VlTotal)
                                                            VALUES (
                                                    '{item.CdCliente}'
                                                   ,'{item.NrPedido}'
                                                   ,'{item.DtPedido}'
                                                   ,'{item.TpPedido}'
                                                   ,'{item.NrNota}'
                                                   ,'{item.DtEmissao}'
                                                   ,'{item.TxObs1}'
                                                   ,'{item.X5Chave}'
                                                   ,'{item.NrCarreg}'
                                                   ,'{item.CdTipocob}'
                                                   ,'{item.CdPlano}'
                                                   ,'{item.DsPlano}'
                                                   ,'{item.PedidoMaxima}'
                                                   ,'{item.ZyMinum}'
                                                   ,'{item.DtSaidaMerc}'
                                                   ,'{item.Transportador}'
                                                   ,'{item.Credito}'
                                                   ,'{item.Ordem}'
                                                   ,'{item.CdRcaxxx}'
                                                   , {item.VlFinal.ToStringInvariant("0.00")}
                                                   , {item.VlTotal.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM {NomeTabelaHistoricoPedidos} WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteAll()
        {
            var command = @$"DELETE FROM {NomeTabelaHistoricoPedidos};";
            _context.ExcecutarComandoCrud(command);
        }

        public List<HistoricoDePedidos> Pesquisa(string termo)
        {
            //return conn.Table<HistoricoDePedidos>().Where(x =>
            //    x.CdCliente.Contains(termo) ||
            //    x.NrNota.Contains(termo) ||
            //    x.DsPlano.Contains(termo)
            //).ToList().Result;

            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} 
                                      WHERE CdCliente LIKE '{termo}%'
                                      OR NrNota LIKE '{termo}%' 
                                      OR x.DsPlano LIKE '{termo}%';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {

                lstHistorico.Add(new HistoricoDePedidos
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdCliente = item.CdCliente.ToString(),
                    NrPedido = item.NrPedido.ToString(),
                    DtPedido = item.dtPedido.ToString(),
                    TpPedido = item.TpPedido.ToString(),
                    NrNota = item.NrNota.ToString(),
                    DtEmissao = item.DtEmissao.ToString(),
                    TxObs1 = item.TxObs1.ToString(),
                    X5Chave = item.X5Chave.ToString(),
                    NrCarreg = item.NrCarreg.ToString(),
                    CdTipocob = item.CdTipocob.ToString(),
                    CdPlano = item.CdPlano.ToString(),
                    DsPlano = item.DsPlano.ToString(),
                    PedidoMaxima = item.PedidoMaxima.ToString(),
                    ZyMinum = item.ZyMinum.ToString(),
                    DtSaidaMerc = item.DtSaidaMerc.ToString(),
                    Transportador = item.Transportador.ToString(),
                    Credito = item.Credito.ToString(),
                    Ordem = item.Ordem.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M,
                    VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0M
                });
            }
            return lstHistorico;

        }

        public void SaveHistorico(List<HistoricoDePedidos> historico)
        {
            if (historico != null && historico.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM {NomeTabelaHistoricoPedidos};");

                foreach (var item in historico)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaHistoricoPedidos}](
                                                    CdCliente 
                                                   ,NrPedido 
                                                   ,dtPedido
                                                   ,TpPedido 
                                                   ,NrNota 
                                                   ,DtEmissao
                                                   ,TxObs1 
                                                   ,X5Chave 
                                                   ,NrCarreg 
                                                   ,CdTipocob 
                                                   ,CdPlano 
                                                   ,DsPlano 
                                                   ,PedidoMaxima 
                                                   ,ZyMinum 
                                                   ,DtSaidaMerc 
                                                   ,Transportador 
                                                   ,Credito 
                                                   ,Ordem 
                                                   ,CdRcaxxx 
                                                   ,VlFinal
                                                   ,VlTotal)
                                                            VALUES (
                                                    '{item.CdCliente}'
                                                   ,'{item.NrPedido}'
                                                   ,'{item.DtPedido}'
                                                   ,'{item.TpPedido}'
                                                   ,'{item.NrNota}'
                                                   ,'{item.DtEmissao}'
                                                   ,'{item.TxObs1}'
                                                   ,'{item.X5Chave}'
                                                   ,'{item.NrCarreg}'
                                                   ,'{item.CdTipocob}'
                                                   ,'{item.CdPlano}'
                                                   ,'{item.DsPlano}'
                                                   ,'{item.PedidoMaxima}'
                                                   ,'{item.ZyMinum}'
                                                   ,'{item.DtSaidaMerc}'
                                                   ,'{item.Transportador}'
                                                   ,'{item.Credito}'
                                                   ,'{item.Ordem}'
                                                   ,'{item.CdRcaxxx}'
                                                   , {item.VlFinal.ToStringInvariant("0.00")}
                                                   , {item.VlTotal.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM {NomeTabelaHistoricoPedidos};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _ = int.TryParse(_total.ToString(), out int total);
            return total;

        }

        public IEnumerable<HistoricoDePedidos> GetAllFromClient(string codigoCliente)
        {
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} WHERE CdCliente = '{codigoCliente}' ORDER BY Id DESC;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {
                try
                {
                    var historico = new HistoricoDePedidos();
                    historico.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    historico.CdCliente = item.CdCliente != null ? item.CdCliente.ToString() : "";
                    historico.NrPedido = item.NrPedido != null ? item.NrPedido.ToString() : "";
                    historico.DtPedido = item.DtPedido != null ? item.DtPedido.ToString() : "";
                    historico.TpPedido = item.TpPedido != null ? item.TpPedido.ToString() : "";
                    historico.NrNota = item.NrNota != null ? item.NrNota.ToString() : "";
                    historico.DtEmissao = item.DtEmissao != null ? item.DtEmissao.ToString() : "";
                    historico.TxObs1 = item.TxObs1 != null ? item.TxObs1.ToString() : "";
                    historico.X5Chave = item.X5Chave != null ? item.X5Chave.ToString() : "";
                    historico.NrCarreg = item.NrCarreg != null ? item.NrCarreg.ToString() : "";
                    historico.CdTipocob = item.CdTipocob != null ? item.CdTipocob.ToString() : "";
                    historico.CdPlano = item.CdPlano != null ? item.CdPlano.ToString() : "";
                    historico.DsPlano = item.DsPlano != null ? item.DsPlano.ToString() : "";
                    historico.PedidoMaxima = item.PedidoMaxima != null ? item.PedidoMaxima.ToString() : "";
                    historico.ZyMinum = item.ZyMinum != null ? item.ZyMinum.ToString() : "";
                    historico.DtSaidaMerc = item.DtSaidaMerc != null ? item.DtSaidaMerc.ToString() : "";
                    historico.Transportador = item.Transportador != null ? item.Transportador.ToString() : "";
                    historico.Credito = item.Credito != null ? item.Credito.ToString() : "";
                    historico.Ordem = item.Ordem != null ? item.Ordem.ToString() : "";
                    historico.CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "";
                    historico.VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M;
                    historico.VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0;
                    lstHistorico.Add(historico);
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Command = command
                    });
                }
            }
            return lstHistorico;
        }

        public void CriarTabela()
        {
            Init(NomeTabelaHistoricoPedidos);
        }

        public List<HistoricoDePedidos> PedidosEmAberto()
        {
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} where NrNota is null or NrNota='';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {

                var cdCliente = "";
                var clienteLoja = "";
                if (item.CdCliente != null)
                {
                    string codigo = item.CdCliente.ToString();
                    cdCliente = codigo.Substring(0, 6);
                    clienteLoja = codigo.Substring(6, 2);
                }


                var commandCliente = $@"SELECT * FROM {NomeTabelaCliente} WHERE A1Cod = '{cdCliente}' AND A1Loja='{clienteLoja}'";
                var _cliente = _context.ExcecutarSelectFirstOrDefault(commandCliente);

                try
                {
                    var historico = new HistoricoDePedidos();
                    historico.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    historico.CdCliente = item.CdCliente != null ? item.CdCliente.ToString() : "";
                    historico.NrPedido = item.NrPedido != null ? item.NrPedido.ToString() : "";
                    historico.DtPedido = item.DtPedido != null ? item.DtPedido.ToString() : "";
                    historico.TpPedido = item.TpPedido != null ? item.TpPedido.ToString() : "";
                    historico.NrNota = item.NrNota != null ? item.NrNota.ToString() : "";
                    historico.DtEmissao = item.DtEmissao != null ? item.DtEmissao.ToString() : "";
                    historico.TxObs1 = item.TxObs1 != null ? item.TxObs1.ToString() : "";
                    historico.X5Chave = item.X5Chave != null ? item.X5Chave.ToString() : "";
                    historico.NrCarreg = item.NrCarreg != null ? item.NrCarreg.ToString() : "";
                    historico.CdTipocob = item.CdTipocob != null ? item.CdTipocob.ToString() : "";
                    historico.CdPlano = item.CdPlano != null ? item.CdPlano.ToString() : "";
                    historico.DsPlano = item.DsPlano != null ? item.DsPlano.ToString() : "";
                    historico.PedidoMaxima = item.PedidoMaxima != null ? item.PedidoMaxima.ToString() : "";
                    historico.ZyMinum = item.ZyMinum != null ? item.ZyMinum.ToString() : "";
                    historico.DtSaidaMerc = item.DtSaidaMerc != null ? item.DtSaidaMerc.ToString() : "";
                    historico.Transportador = item.Transportador != null ? item.Transportador.ToString() : "";
                    historico.Credito = item.Credito != null ? item.Credito.ToString() : "";
                    historico.Ordem = item.Ordem != null ? item.Ordem.ToString() : "";
                    historico.CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "";
                    historico.VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M;
                    historico.VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0;
                    historico.NomeCliente = _cliente != null ? _cliente.A1Nome.ToString() : "";
                    historico.Municipio = _cliente != null ? _cliente.A1Mune.ToString() : "";
                    lstHistorico.Add(historico);
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Command = command
                    });
                }
            }
            return lstHistorico;
        }

        public List<HistoricoDePedidos> CarregamentoDePedidos()
        {
            var command = $@"SELECT * FROM {NomeTabelaHistoricoPedidos} Where NrNota is not null AND TRIM(NrNota,' ') != '';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<HistoricoDePedidos>();

            var lstHistorico = new List<HistoricoDePedidos>();
            foreach (var item in retorno)
            {
                var commandResumo = $@"SELECT * FROM {NomeTabelaResumoPedido} WHERE NrPedido = '{item.NrPedido}'";
                var resumo = _context.ExcecutarSelect(commandResumo);

                var cdCliente = "";
                var clienteLoja = "";
                if (item.CdCliente != null)
                {
                    string codigo = item.CdCliente.ToString();
                    cdCliente = codigo.Substring(0, 6);
                    clienteLoja = codigo.Substring(6, 2);
                }


                var commandCliente = $@"SELECT * FROM {NomeTabelaCliente} WHERE A1Cod = '{cdCliente}' AND A1Loja='{clienteLoja}'";
                var _cliente = _context.ExcecutarSelectFirstOrDefault(commandCliente);
                try
                {
                    var historico = new HistoricoDePedidos();
                    historico.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    historico.CdCliente = item.CdCliente != null ? item.CdCliente.ToString() : "";
                    historico.NrPedido = item.NrPedido != null ? item.NrPedido.ToString() : "";
                    historico.DtPedido = item.DtPedido != null ? item.DtPedido.ToString() : "";
                    historico.TpPedido = item.TpPedido != null ? item.TpPedido.ToString() : "";
                    historico.NrNota = item.NrNota != null ? item.NrNota.ToString() : "";
                    historico.DtEmissao = item.DtEmissao != null ? item.DtEmissao.ToString() : "";
                    historico.TxObs1 = item.TxObs1 != null ? item.TxObs1.ToString() : "";
                    historico.X5Chave = item.X5Chave != null ? item.X5Chave.ToString() : "";
                    historico.NrCarreg = item.NrCarreg != null ? item.NrCarreg.ToString() : "";
                    historico.CdTipocob = item.CdTipocob != null ? item.CdTipocob.ToString() : "";
                    historico.CdPlano = item.CdPlano != null ? item.CdPlano.ToString() : "";
                    historico.DsPlano = item.DsPlano != null ? item.DsPlano.ToString() : "";
                    historico.PedidoMaxima = item.PedidoMaxima != null ? item.PedidoMaxima.ToString() : "";
                    historico.ZyMinum = item.ZyMinum != null ? item.ZyMinum.ToString() : "";
                    historico.DtSaidaMerc = item.DtSaidaMerc != null ? item.DtSaidaMerc.ToString() : "";
                    historico.Transportador = item.Transportador != null ? item.Transportador.ToString() : "";
                    historico.Credito = item.Credito != null ? item.Credito.ToString() : "";
                    historico.Ordem = item.Ordem != null ? item.Ordem.ToString() : "";
                    historico.CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "";
                    historico.VlFinal = item.VlFinal != null ? Convert.ToDecimal(item.VlFinal) : 0M;
                    historico.VlTotal = item.VlTotal != null ? Convert.ToDecimal(item.VlTotal) : 0;
                    historico.NomeCliente = _cliente != null ? _cliente.A1Nome.ToString() : "";
                    historico.Municipio = _cliente != null ? _cliente.A1Mune.ToString() : "";

                    if (resumo != null && resumo.Count() > 0)
                    {
                        foreach (var res in resumo)
                        {
                            var resumoPedido = new ResumoPedido
                            {
                                Id = Convert.ToInt32(res.Id),
                                NrPedido = res.NrPedido.ToString(),
                                CdProduto = res.CdProduto.ToString(),
                                DsProduto = res.DsProduto.ToString(),
                                NumLote = res.NumLote.ToString(),
                                CdRcaxxx = res.CdRcaxxx.ToString(),
                                ImagemProduto = res.ImagemProduto.ToString(),
                                QtProduto = res.QtProduto != null ? Convert.ToInt32(res.QtProduto) : 0,
                                QtAtend = res.QtAtend != null ? Convert.ToInt32(res.QtAtend) : 0,
                                VlVenda = res.VlVenda != null ? Convert.ToDecimal(res.VlVenda) : 0M,
                                VlUnit = res.VlUnit != null ? Convert.ToDecimal(res.VlUnit) : 0M,
                                VlFrete = res.VlFrete != null ? Convert.ToDecimal(res.VlFrete) : 0M,
                                CdPercComiss = res.CdPercComiss != null ? Convert.ToDecimal(res.CdPercComiss) : 0M
                            };

                            historico.ResumoDoPedido.Add(resumoPedido);
                        }
                    }


                    lstHistorico.Add(historico);
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Command = command
                    });
                }
            }
            return lstHistorico;
        }

        public void SaveHistoricoVendedor(List<HistoricoDePedidos> historico, string codigoVendedor)
        {
            if (historico != null && historico.Any())
            {
                CriarTabelaHistoricoVendedor(codigoVendedor);
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM HistoricoDePedidos_{codigoVendedor};");

                foreach (var item in historico)
                {
                    var commandInsert = $@"INSERT INTO HistoricoDePedidos_{codigoVendedor}(
                                                    CdCliente 
                                                   ,NrPedido 
                                                   ,dtPedido
                                                   ,TpPedido 
                                                   ,NrNota 
                                                   ,DtEmissao
                                                   ,TxObs1 
                                                   ,X5Chave 
                                                   ,NrCarreg 
                                                   ,CdTipocob 
                                                   ,CdPlano 
                                                   ,DsPlano 
                                                   ,PedidoMaxima 
                                                   ,ZyMinum 
                                                   ,DtSaidaMerc 
                                                   ,Transportador 
                                                   ,Credito 
                                                   ,Ordem 
                                                   ,CdRcaxxx 
                                                   ,VlFinal
                                                   ,VlTotal)
                                                            VALUES (
                                                    '{item.CdCliente}'
                                                   ,'{item.NrPedido}'
                                                   ,'{item.DtPedido}'
                                                   ,'{item.TpPedido}'
                                                   ,'{item.NrNota}'
                                                   ,'{item.DtEmissao}'
                                                   ,'{item.TxObs1}'
                                                   ,'{item.X5Chave}'
                                                   ,'{item.NrCarreg}'
                                                   ,'{item.CdTipocob}'
                                                   ,'{item.CdPlano}'
                                                   ,'{item.DsPlano}'
                                                   ,'{item.PedidoMaxima}'
                                                   ,'{item.ZyMinum}'
                                                   ,'{item.DtSaidaMerc}'
                                                   ,'{item.Transportador}'
                                                   ,'{item.Credito}'
                                                   ,'{item.Ordem}'
                                                   ,'{item.CdRcaxxx}'
                                                   , {item.VlFinal.ToStringInvariant("0.00")}
                                                   , {item.VlTotal.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        private void CriarTabelaHistoricoVendedor(string codigoVendedor)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS HistoricoDePedidos_{codigoVendedor}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdCliente  VARCHAR(15)
                                                  ,NrPedido  VARCHAR(15)
                                                  ,DtPedido VARCHAR(15)
                                                  ,TpPedido  VARCHAR(15)
                                                  ,NrNota  VARCHAR(15)
                                                  ,DtEmissao VARCHAR(15)
                                                  ,TxObs1  VARCHAR(150)
                                                  ,X5Chave  VARCHAR(15)
                                                  ,NrCarreg  VARCHAR(15)
                                                  ,CdTipocob  VARCHAR(15)
                                                  ,CdPlano  VARCHAR(15)
                                                  ,DsPlano  VARCHAR(150)
                                                  ,PedidoMaxima  VARCHAR(15)
                                                  ,ZyMinum  VARCHAR(15)
                                                  ,DtSaidaMerc  VARCHAR(15)
                                                  ,Transportador  VARCHAR(150)
                                                  ,Credito  VARCHAR(15)
                                                  ,Ordem  VARCHAR(15)
                                                  ,CdRcaxxx  VARCHAR(15)
                                                  ,VlFinal DECIMAL(7,2)
                                                  ,VlTotal DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        private string RecuperarNomeDaTabela()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"HistoricoDePedidos_{App.VendedorSelecionado.CodigoVendedor}";
                    Init(tableName);
                    return tableName;
                }

                return "HistoricoDePedidos";
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string RecuperarNomeDaTabelaCliente()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"Cliente_{App.VendedorSelecionado.CodigoVendedor}";
                    InitCliente(tableName);
                    return tableName;
                }

                return "Cliente";
            }
            catch (Exception)
            {
                throw;
            }
        }



        private string RecuperarNomeDaTabelaResumoPedido()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"ResumoPedido_{App.VendedorSelecionado.CodigoVendedor}";
                    InitResumoPedido_(tableName);
                    return tableName;
                }

                return "ResumoPedido";
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
