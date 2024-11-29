using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class FaturamentoRepository : IFaturamentoRepository
    {
        private string NomeTabelaFaturamento => RecuperarNomeDaTabela();
        private string NomeTabelaCliente => RecuperarNomeDaTabelaCliente();
        private readonly IAppthalContext _context;
        public FaturamentoRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init(NomeTabelaFaturamento);
        }

        private void Init(string nomeTabela)
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS {nomeTabela}(
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                ,CdCliente VARCHAR(20)
                                                ,NrDocum VARCHAR(20)
                                                ,NrParcel VARCHAR(20)
                                                ,DtEmissao VARCHAR(12)
                                                ,DtVenc VARCHAR(12)
                                                ,TpCobran VARCHAR(20)
                                                ,CdRca VARCHAR(20)
                                                ,CdRcaxxx VARCHAR(20)
                                                ,QtDiaatr INT
                                                ,Juros DECIMAL(7,2)
                                                ,VlDocum DECIMAL(7,2)
                                                ,VlJuro DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
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

        public List<Faturamento> GetAll()
        {
            try
            {
                var command = $@"SELECT F.* ,(SELECT C.A1Nome FROM {NomeTabelaCliente} C WHERE C.A1Loja = (SELECT substr(F.CdCliente,7,2)) AND C.A1Cod = (SELECT substr(F.CdCliente,0,7))) AS NomeCliente FROM {NomeTabelaFaturamento} F;";
                var retorno = _context.ExcecutarSelect(command);

                if (retorno == null)
                    return new List<Faturamento>();

                var lstuser = new List<Faturamento>();
                foreach (var item in retorno)
                {
                    lstuser.Add(new Faturamento
                    {
                        NomeCliente = item.NomeCliente?.ToString(),
                        Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                        CdCliente = item.CdCliente.ToString(),
                        NrDocum = item.NrDocum.ToString(),
                        NrParcel = item.NrParcel.ToString(),
                        DtEmissao = item.DtEmissao.ToString(),
                        DtVenc = item.DtVenc.ToString(),
                        TpCobran = item.TpCobran.ToString(),
                        CdRca = item.CdRca.ToString(),
                        CdRcaxxx = item.CdRcaxxx.ToString(),
                        QtDiaatr = item.QtDiaatr != null ? Convert.ToInt32(item.QtDiaatr) : 0,
                        Juros = item.Juros != null ? Convert.ToDecimal(item.Juros) : 0M,
                        VlDocum = item.VlDocum != null ? Convert.ToDecimal(item.VlDocum) : 0M,
                        VlJuro = item.VlJuro != null ? Convert.ToDecimal(item.VlJuro) : 0M,
                    });
                }
                return lstuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Faturamento> RecuperarTitulosVencidos()
        {
            try
            {

                var titulos = GetAll();
                if (titulos == null || titulos.Count == 0)
                    return new List<Faturamento>();

                var hoje = DateTime.Today;
                var titulosAvencer = titulos.Where(x => x.DataDeVencimento < hoje).OrderBy(x => x.DataDeVencimento).ToList();
                //var titulosAvencer = titulos/*.Where(x => x.DataDeVencimento>=hoje && x.DataDeVencimento.Value.Month==hoje.Month)*/.OrderBy(x => x.DataDeVencimento).ToList();

                return titulosAvencer;
                //var hoje = DateTime.Today;
                //var command = $@"SELECT F.* 
                //                       ,(SELECT C.A1Nome FROM {NomeTabelaCliente} C WHERE C.A1Loja = (SELECT substr(F.CdCliente,7,2)) 
                //                        AND C.A1Cod = (SELECT substr(F.CdCliente,0,7))) AS NomeCliente 
                //                        FROM {NomeTabelaFaturamento} F                                        
                //                        WHERE F.DtVenc <'{hoje.Year}-{hoje.Month}-{hoje.Day}';";
                //var retorno = _context.ExcecutarSelect(command);

                //if (retorno == null)
                //    return new List<Faturamento>();

                //var lstuser = new List<Faturamento>();
                //foreach (var item in retorno)
                //{
                //    lstuser.Add(new Faturamento
                //    {
                //        NomeCliente = item.NomeCliente?.ToString(),
                //        Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                //        CdCliente = item.CdCliente.ToString(),
                //        NrDocum = item.NrDocum.ToString(),
                //        NrParcel = item.NrParcel.ToString(),
                //        DtEmissao = item.DtEmissao.ToString(),
                //        DtVenc = item.DtVenc.ToString(),
                //        TpCobran = item.TpCobran.ToString(),
                //        CdRca = item.CdRca.ToString(),
                //        CdRcaxxx = item.CdRcaxxx.ToString(),
                //        QtDiaatr = item.QtDiaatr != null ? Convert.ToInt32(item.QtDiaatr) : 0,
                //        Juros = item.Juros != null ? Convert.ToDecimal(item.Juros) : 0M,
                //        VlDocum = item.VlDocum != null ? Convert.ToDecimal(item.VlDocum) : 0M,
                //        VlJuro = item.VlJuro != null ? Convert.ToDecimal(item.VlJuro) : 0M,
                //    });
                //}
                //return lstuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Faturamento> GetByCodigo(string codCliente)
        {
            var command = $@"SELECT * FROM {NomeTabelaFaturamento} WHERE CdCliente  = '{codCliente}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Faturamento>();

            var lstuser = new List<Faturamento>();
            foreach (var item in retorno)
            {

                try
                {
                    var teste = new Faturamento();

                    teste.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    teste.CdCliente = item.CdCliente != null ? item.CdCliente.ToString() : "";
                    teste.NrDocum = item.NrDocum != null ? item.NrDocum.ToString() : "";
                    teste.NrParcel = item.NrParcel != null ? item.NrParcel.ToString() : "";
                    teste.DtEmissao = item.DtEmissao != null ? item.DtEmissao.ToString() : "";
                    teste.DtVenc = item.DtVenc != null ? item.DtVenc.ToString() : "";
                    teste.TpCobran = item.TpCobran != null ? item.TpCobran.ToString() : "";
                    teste.CdRca = item.CdRca != null ? item.CdRca.ToString() : "";
                    teste.CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "";
                    teste.QtDiaatr = item.QtDiaatr != null ? Convert.ToInt32(item.QtDiaatr) : 0;
                    teste.Juros = item.Juros != null ? Convert.ToDecimal(item.Juros) : 0M;
                    teste.VlDocum = item.VlDocum != null ? Convert.ToDecimal(item.VlDocum) : 0M;
                    teste.VlJuro = item.VlJuro != null ? Convert.ToDecimal(item.VlJuro) : 0M;

                    lstuser.Add(teste);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return lstuser;
        }

        public void SaveFaturamento(List<Faturamento> fatura)
        {
            if (fatura != null && fatura.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM {NomeTabelaFaturamento};");

                foreach (var item in fatura)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaFaturamento}](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.NrDocum}'
                                                        ,'{item.NrParcel}'
                                                        ,'{item.DtEmissao}'
                                                        ,'{item.DtVenc}'
                                                        ,'{item.TpCobran}'
                                                        ,'{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtDiaatr}
                                                        , {item.Juros.ToStringInvariant("0.00")}
                                                        , {item.VlDocum.ToStringInvariant("0.00")}
                                                        , {item.VlJuro.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Add(Faturamento fatura)
        {
            var commandInsert = $@"INSERT INTO [{NomeTabelaFaturamento}](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{fatura.CdCliente}'
                                                        ,'{fatura.NrDocum}'
                                                        ,'{fatura.NrParcel}'
                                                        ,'{fatura.DtEmissao}'
                                                        ,'{fatura.DtVenc}'
                                                        ,'{fatura.TpCobran}'
                                                        ,'{fatura.CdRca}'
                                                        ,'{fatura.CdRcaxxx}'
                                                        , {fatura.QtDiaatr}
                                                        , {fatura.Juros.ToStringInvariant("0.00")}
                                                        , {fatura.VlDocum.ToStringInvariant("0.00")}
                                                        , {fatura.VlJuro.ToStringInvariant("0.00")});";
            _context.ExcecutarComandoCrud(commandInsert);
        }

        public void AddRange(List<Faturamento> fatura)
        {
            if (fatura != null && fatura.Any())
            {
                var scriptCommand = new StringBuilder();
                foreach (var item in fatura)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaFaturamento}](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.NrDocum}'
                                                        ,'{item.NrParcel}'
                                                        ,'{item.DtEmissao}'
                                                        ,'{item.DtVenc}'
                                                        ,'{item.TpCobran}'
                                                        ,'{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtDiaatr}
                                                        , {item.Juros.ToStringInvariant("0.00")}
                                                        , {item.VlDocum.ToStringInvariant("0.00")}
                                                        , {item.VlJuro.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM {NomeTabelaFaturamento} WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM {NomeTabelaFaturamento};";
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
            Init(NomeTabelaFaturamento);
        }

        public List<Faturamento> RecuperarTitulosAVencer()
        {
            var titulos = GetAll();
            if (titulos == null || titulos.Count == 0)
                return new List<Faturamento>();

            var hoje = DateTime.Today;
            var titulosAvencer = titulos.Where(x => x.DataDeVencimento >= hoje).OrderBy(x => x.DataDeVencimento).ToList();
            //var titulosAvencer = titulos/*.Where(x => x.DataDeVencimento>=hoje && x.DataDeVencimento.Value.Month==hoje.Month)*/.OrderBy(x => x.DataDeVencimento).ToList();

            return titulosAvencer;

        }

        private void CriarTabelaFaturamentoVendedor(string codigoVendedor)
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Faturamento_{codigoVendedor}(
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                ,CdCliente VARCHAR(20)
                                                ,NrDocum VARCHAR(20)
                                                ,NrParcel VARCHAR(20)
                                                ,DtEmissao VARCHAR(12)
                                                ,DtVenc VARCHAR(12)
                                                ,TpCobran VARCHAR(20)
                                                ,CdRca VARCHAR(20)
                                                ,CdRcaxxx VARCHAR(20)
                                                ,QtDiaatr INT
                                                ,Juros DECIMAL(7,2)
                                                ,VlDocum DECIMAL(7,2)
                                                ,VlJuro DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveFaturamentoVendedor(List<Faturamento> fatura, string codigoVendedor)
        {
            if (fatura != null && fatura.Any())
            {
                CriarTabelaFaturamentoVendedor(codigoVendedor);

                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM Faturamento_{codigoVendedor};");

                foreach (var item in fatura)
                {
                    var commandInsert = $@"INSERT INTO Faturamento_{codigoVendedor}(
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.NrDocum}'
                                                        ,'{item.NrParcel}'
                                                        ,'{item.DtEmissao}'
                                                        ,'{item.DtVenc}'
                                                        ,'{item.TpCobran}'
                                                        ,'{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtDiaatr}
                                                        , {item.Juros.ToStringInvariant("0.00")}
                                                        , {item.VlDocum.ToStringInvariant("0.00")}
                                                        , {item.VlJuro.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        private string RecuperarNomeDaTabela()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"Faturamento_{App.VendedorSelecionado.CodigoVendedor}";
                    Init(tableName);
                    return tableName;
                }

                return "Faturamento";
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
    }
}