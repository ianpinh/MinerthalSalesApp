using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class TabelaPrecoRepository : ITabelaPrecoRepository
    {
        private readonly IAppthalContext _context;
        public TabelaPrecoRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS TabelaPreco(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdProduto VARCHAR(15)
                                                  ,IdTabPrec VARCHAR(15)
                                                  ,CdFilial  VARCHAR(15)
                                                  ,CdRcaxxx  VARCHAR(15)
                                                  ,DtValIni  VARCHAR(12)
                                                  ,DtValFin VARCHAR(12)
                                                  ,QtdMin INT
                                                  ,QtdMax INT
                                                  ,PerComissao DECIMAL(7,2)
                                                  ,VlVvenda DECIMAL(7,2)
                                                  ,PerMin DECIMAL(7,2)
                                                  ,PerMax DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        public TabelaPreco GetById(int id)
        {
            var command = $@"SELECT * FROM TabelaPreco WHERE Id = {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);
            if (retorno == null)
                return new TabelaPreco();


            return new TabelaPreco
            {
                Id = Convert.ToInt32(retorno.Id),
                CdProduto = retorno.CdProduto.ToString(),
                IdTabPrec = retorno.IdTabPrec.ToString(),
                CdFilial = retorno.CdFilial.ToString(),
                CdRcaxxx = retorno.CdRcaxxx.ToString(),
                DtValIni = retorno.DtValIni.ToString(),
                DtValFin = retorno.DtValFin.ToString(),
                QtdMin = retorno.QtdMin != null ? Convert.ToInt32(retorno.Id) : 0,
                QtdMax = retorno.QtdMax != null ? Convert.ToInt32(retorno.Id) : 0,
                PerComissao = retorno.PerComissao != null ? Convert.ToDecimal(retorno.PerComissao) : 0M,
                VlVvenda = retorno.VlVvenda != null ? Convert.ToDecimal(retorno.VlVvenda) : 0M,
                PerMin = retorno.PerMin != null ? Convert.ToDecimal(retorno.PerMin) : 0M,
                PerMax = retorno.PerMax != null ? Convert.ToDecimal(retorno.PerMax) : 0M
            };
        }

        public List<TabelaPreco> GetAll()
        {
            var command = $@"SELECT * FROM TabelaPreco;";
            var retorno = _context.ExcecutarSelect(command);
            if (retorno == null)
                return new List<TabelaPreco>();

            var lst = new List<TabelaPreco>();

            foreach (var item in retorno)
            {
                lst.Add(new TabelaPreco
                {
                    Id = Convert.ToInt32(item.Id),
                    CdProduto = item.CdProduto.ToString(),
                    IdTabPrec = item.IdTabPrec.ToString(),
                    CdFilial = item.CdFilial.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    DtValIni = item.DtValIni.ToString(),
                    DtValFin = item.DtValFin.ToString(),
                    QtdMin = item.QtdMin != null ? Convert.ToInt32(item.Id) : 0,
                    QtdMax = item.QtdMax != null ? Convert.ToInt32(item.Id) : 0,
                    PerComissao = item.PerComissao != null ? Convert.ToDecimal(item.PerComissao) : 0M,
                    VlVvenda = item.VlVvenda != null ? Convert.ToDecimal(item.VlVvenda) : 0M,
                    PerMin = item.PerMin != null ? Convert.ToDecimal(item.PerMin) : 0M,
                    PerMax = item.PerMax != null ? Convert.ToDecimal(item.PerMax) : 0M
                });
            }
            return lst;
        }

        public void Add(TabelaPreco tbpreco)
        {
            var command = $@"INSERT INTO [TabelaPreco](
                                                 CdProduto
                                                ,IdTabPrec
                                                ,CdFilial
                                                ,CdRcaxxx
                                                ,DtValIni
                                                ,DtValFin
                                                ,QtdMin
                                                ,QtdMax
                                                ,PerComissao
                                                ,VlVvenda
                                                ,PerMin
                                                ,PerMax)
                                            VALUES(
                                                 '{tbpreco.CdProduto.Trim()}'
                                                ,'{tbpreco.IdTabPrec.Trim()}'
                                                ,'{tbpreco.CdFilial.Trim()}'
                                                ,'{tbpreco.CdRcaxxx.Trim()}'
                                                ,'{tbpreco.DtValIni.Trim()}'
                                                ,'{tbpreco.DtValFin.Trim()}'
                                                , {tbpreco.QtdMin}
                                                , {tbpreco.QtdMax}
                                                , {tbpreco.PerComissao.ToStringInvariant("0.00")}
                                                , {tbpreco.VlVvenda.ToStringInvariant("0.00")}
                                                , {tbpreco.PerMin.ToStringInvariant("0.00")}
                                                , {tbpreco.PerMax.ToStringInvariant("0.00")});";
            _context.ExcecutarComandoCrud(command);
        }

        public void AddRange(List<TabelaPreco> tbpreco)
        {
            if (tbpreco != null && tbpreco.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in tbpreco)
                {
                    var commandInsert = $@"INSERT INTO[TabelaPreco](
                                                  CdProduto
                                                , IdTabPrec
                                                , CdFilial
                                                , CdRcaxxx
                                                , DtValIni
                                                , DtValFin
                                                , QtdMin
                                                , QtdMax
                                                , PerComissao
                                                , VlVvenda
                                                , PerMin
                                                , PerMax)
                                            VALUES(
                                                  '{item.CdProduto.Trim()}'
                                                , '{item.IdTabPrec.Trim()}'
                                                , '{item.CdFilial.Trim()}'
                                                , '{item.CdRcaxxx.Trim()}'
                                                , '{item.DtValIni.Trim()}'
                                                , '{item.DtValFin.Trim()}'
                                                , {item.QtdMin}
                                                , {item.QtdMax}
                                                , {item.PerComissao.ToStringInvariant("0.00")}
                                                , {item.VlVvenda.ToStringInvariant("0.00")}
                                                , {item.PerMin.ToStringInvariant("0.00")}
                                                , {item.PerMax.ToStringInvariant("0.00")}); ";

                    scriptCommand.AppendLine(commandInsert);
                }



                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void DeleteById(int id)
        {
            var scriptCommand = new StringBuilder();
            scriptCommand.AppendLine($"Delete from TabelaPreco WHERE Id = {id};");

            var command = scriptCommand.ToString();
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteAll()
        {
            var scriptCommand = new StringBuilder();
            scriptCommand.AppendLine($"Delete from TabelaPreco;");

            var command = scriptCommand.ToString();
            _context.ExcecutarComandoCrud(command);
        }

        public List<TabelaPreco> Get(string cdProduto, string filialMinerthal, string tipoTabela, string codigoTabela = "2")
        {
            var tabPrec = $"{tipoTabela}{codigoTabela}";
            var command = $@"SELECT * FROM TabelaPreco 
                                          WHERE CdProduto = '{cdProduto}'
                                          AND CdFilial= '{filialMinerthal}'
                                          AND IdTabPrec LIKE '{tabPrec}%';";

            var retorno = _context.ExcecutarSelect(command);
            if (retorno == null)
                return new List<TabelaPreco>();

            var lst = new List<TabelaPreco>();

            foreach (var item in retorno)
            {
                lst.Add(new TabelaPreco
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdProduto = item.CdProduto != null ? item.CdProduto.ToString() : "",
                    IdTabPrec = item.IdTabPrec != null ? item.IdTabPrec.ToString() : "",
                    CdFilial = item.CdFilial != null ? item.CdFilial.ToString() : "",
                    CdRcaxxx = item.CdRcaxxx != null ? item.CdRcaxxx.ToString() : "",
                    DtValIni = item.DtValIni != null ? item.DtValIni.ToString() : "",
                    DtValFin = item.DtValFin != null ? item.DtValFin.ToString() : "",
                    QtdMin = item.QtdMin != null ? Convert.ToInt32(item.QtdMin) : 0,
                    QtdMax = item.QtdMax != null ? Convert.ToInt32(item.QtdMax) : 0,
                    PerComissao = item.PerComissao != null ? Convert.ToDecimal(item.PerComissao) : 0M,
                    VlVvenda = item.VlVvenda != null ? Convert.ToDecimal(item.VlVvenda) : 0M,
                    PerMin = item.PerMin != null ? Convert.ToDecimal(item.PerMin) : 0M,
                    PerMax = item.PerMax != null ? Convert.ToDecimal(item.PerMax) : 0M
                });
            }
            return lst;

            // return conn.Table<TabelaPreco>().Where(x => x.CdProduto==cdProduto && x.CdFilial==filialMinerthal && x.IdTabPrec.StartsWith(tabPrec)).ToListAsync().Result;
        }

        public static void Validar(TabelaPreco tabela)
        {
            var diValid = DateTime.TryParse(tabela.DtValIni, out DateTime datainicial);
            var dfValid = DateTime.TryParse(tabela.DtValFin, out DateTime datafinal);
            var dataAtual = DateTime.Today;

            CustomExceptions.LancarExcecaoQuando(!diValid, "Data de início da validade da tabela de preço inválida");
            CustomExceptions.LancarExcecaoQuando(!dfValid, "Data de final da validade da tabela de preço inválida");
            CustomExceptions.LancarExcecaoQuando(DateTime.Compare(dataAtual, datainicial) < 0, $"A tabela de preços pra esse produto tem iníncio em {tabela.DtValIni} ");
            CustomExceptions.LancarExcecaoQuando(DateTime.Compare(dataAtual, datafinal) > 0, $"A tabela de preços pra esse produto venceu em {tabela.DtValFin} ");
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM TabelaPreco;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];


            _ = int.TryParse(_total.ToString(), out int total);
            return total;
        }

        public void SaveTabelaPreco(List<TabelaPreco> tbpreco)
        {
            if (tbpreco != null && tbpreco.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM TabelaPreco;");

                foreach (var item in tbpreco)
                {

                    var commandInsert = $@"INSERT INTO[TabelaPreco](
                                                  CdProduto
                                                , IdTabPrec
                                                , CdFilial
                                                , CdRcaxxx
                                                , DtValIni
                                                , DtValFin
                                                , QtdMin
                                                , QtdMax
                                                , PerComissao
                                                , VlVvenda
                                                , PerMin
                                                , PerMax)
                                            VALUES(
                                                  '{item.CdProduto.Trim()}'
                                                , '{item.IdTabPrec.Trim()}'
                                                , '{item.CdFilial.Trim()}'
                                                , '{item.CdRcaxxx.Trim()}'
                                                , '{item.DtValIni.Trim()}'
                                                , '{item.DtValFin.Trim()}'
                                                ,  {item.QtdMin}
                                                ,  {item.QtdMax}
                                                ,  {item.PerComissao.ToStringInvariant("0.00")}
                                                ,  {item.VlVvenda.ToStringInvariant("0.00")}
                                                ,  {item.PerMin.ToStringInvariant("0.00")}
                                                ,  {item.PerMax.ToStringInvariant("0.00")}); ";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void CriarTabela()
        {
            Init();
        }
    }
}