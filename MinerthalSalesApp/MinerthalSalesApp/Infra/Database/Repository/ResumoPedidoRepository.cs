using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;
namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class ResumoPedidoRepository : IResumoPedidoRepository
    {
        private string NomeTabelaResumoPedido => RecuperarNomeDaTabelaResumoPedido();
        private readonly IAppthalContext _context;
        public ResumoPedidoRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        private void Init(string nomeTabela)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS {nomeTabela}(
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

        public ResumoPedido GetById(int id)
        {
            var command = $@"SELECT * FROM {NomeTabelaResumoPedido} Where Id= '{id}';";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new ResumoPedido();

            return new ResumoPedido
            {
                Id = Convert.ToInt32(retorno.Id),
                NrPedido = retorno.NrPedido.ToString(),
                CdProduto = retorno.CdProduto.ToString(),
                DsProduto = retorno.DsProduto.ToString(),
                NumLote = retorno.NumLote.ToString(),
                CdRcaxxx = retorno.CdRcaxxx.ToString(),
                ImagemProduto = retorno.ImagemProduto.ToString(),
                QtProduto = retorno.QtProduto != null ? Convert.ToInt32(retorno.QtProduto) : 0,
                QtAtend = retorno.QtAtend != null ? Convert.ToInt32(retorno.QtAtend) : 0,
                VlVenda = retorno.VlVenda != null ? Convert.ToDecimal(retorno.VlVenda) : 0M,
                VlUnit = retorno.VlUnit != null ? Convert.ToDecimal(retorno.VlUnit) : 0M,
                VlFrete = retorno.VlFrete != null ? Convert.ToDecimal(retorno.VlFrete) : 0M,
                CdPercComiss = retorno.CdPercComiss != null ? Convert.ToDecimal(retorno.CdPercComiss) : 0M
            };
        }
        public List<ResumoPedido> GetAll()
        {
            var command = $@"SELECT * FROM {NomeTabelaResumoPedido};";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<ResumoPedido>();

            var lstRenumo = new List<ResumoPedido>();

            foreach (var item in retorno)
            {
                lstRenumo.Add(new ResumoPedido
                {
                    Id = Convert.ToInt32(item.Id),
                    NrPedido = item.NrPedido.ToString(),
                    CdProduto = item.CdProduto.ToString(),
                    DsProduto = item.DsProduto.ToString(),
                    NumLote = item.NumLote.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    ImagemProduto = item.ImagemProduto.ToString(),
                    QtProduto = item.QtProduto != null ? Convert.ToInt32(item.QtProduto) : 0,
                    QtAtend = item.QtAtend != null ? Convert.ToInt32(item.QtAtend) : 0,
                    VlVenda = item.VlVenda != null ? Convert.ToDecimal(item.VlVenda) : 0M,
                    VlUnit = item.VlUnit != null ? Convert.ToDecimal(item.VlUnit) : 0M,
                    VlFrete = item.VlFrete != null ? Convert.ToDecimal(item.VlFrete) : 0M,
                    CdPercComiss = item.CdPercComiss != null ? Convert.ToDecimal(item.CdPercComiss) : 0M
                });
            }

            return lstRenumo;
        }
        public void Add(ResumoPedido resumo)
        {
            if (resumo != null)
            {
                var commandInsert = $@"INSERT INTO [{NomeTabelaResumoPedido}](
                                                    NrPedido 
                                                   ,CdProduto
                                                   ,DsProduto
                                                   ,NumLote
                                                   ,CdRcaxxx
                                                   ,ImagemProduto
                                                   ,QtProduto
                                                   ,QtAtend
                                                   ,VlUnit 
                                                   ,VlVenda
                                                   ,VlFrete
                                                   ,CdPercComiss)
                                                            VALUES (
                                                    '{resumo.NrPedido}'
                                                   ,'{resumo.CdProduto}'
                                                   ,'{resumo.DsProduto}'
                                                   ,'{resumo.NumLote}'
                                                   ,'{resumo.CdRcaxxx}'
                                                   ,'{resumo.ImagemProduto}'
                                                   , {resumo.QtProduto}
                                                   , {resumo.QtAtend}
                                                   , {resumo.VlUnit}
                                                   , {resumo.VlVenda}
                                                   , {resumo.VlFrete}
                                                   , {resumo.CdPercComiss});";
                _context.ExcecutarComandoCrud(commandInsert);
            }
        }
        public void AddRange(List<ResumoPedido> resumoPedidos)
        {
            if (resumoPedidos != null && resumoPedidos.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in resumoPedidos)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaResumoPedido}](
                                                 NrPedido 
                                                 ,CdProduto
                                                 ,DsProduto
                                                 ,NumLote
                                                 ,CdRcaxxx
                                                 ,ImagemProduto
                                                 ,QtProduto
                                                 ,QtAtend,
                                                 ,VlVenda
                                                 ,VlUnit
                                                 ,VlFrete
                                                 ,CdPercComiss)
                                                            VALUES (
                                                   '{item.NrPedido}'
                                                  ,'{item.CdProduto}'
                                                  ,'{item.DsProduto}'
                                                  ,'{item.NumLote}'
                                                  ,'{item.CdRcaxxx}'
                                                  ,'{item.ImagemProduto}'
                                                  , {item.QtProduto}'
                                                  , {item.QtAtend}'
                                                  , {item.VlVenda.ToStringInvariant("0.00")}
                                                  , {item.VlUnit.ToStringInvariant("0.00")}
                                                  , {item.VlFrete.ToStringInvariant("0.00")}
                                                  , {item.CdPercComiss.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void Delete(string numPedido)
        {
            var command = $"Delete FROM {NomeTabelaResumoPedido} WHERE numPedido = '{numPedido}';";
            _context.ExcecutarComandoCrud(command);
        }
        public void DeleteAll()
        {
            var command = $"Delete FROM {NomeTabelaResumoPedido};";
            _context.ExcecutarComandoCrud(command);
        }
        public void Delete(int id)
        {
            var command = $"Delete FROM {NomeTabelaResumoPedido} WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }
        public List<ResumoPedido> Pesquisa(string termoBusca)
        {
            //return conn.Table<ResumoPedido>().Where(x => x.NrPedido.StartsWith(termoBusca) || x.DsProduto.StartsWith(termoBusca)).ToList().Result;
            var command = $@"SELECT * FROM {NomeTabelaResumoPedido} WHERE NrPedido LIKE '{termoBusca}%' OR DsProduto LIKE '{termoBusca}%';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<ResumoPedido>();

            var lstRenumo = new List<ResumoPedido>();

            foreach (var item in retorno)
            {
                lstRenumo.Add(new ResumoPedido
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    NrPedido = item.NrPedido.ToString(),
                    CdProduto = item.CdProduto.ToString(),
                    DsProduto = item.DsProduto.ToString(),
                    NumLote = item.NumLote.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    ImagemProduto = item.ImagemProduto.ToString(),
                    QtProduto = item.QtProduto != null ? Convert.ToInt32(item.QtProduto) : 0,
                    QtAtend = item.QtAtend != null ? Convert.ToInt32(item.QtAtend) : 0,
                    VlVenda = item.VlVenda != null ? Convert.ToDecimal(item.VlVenda) : 0M,
                    VlUnit = item.VlUnit != null ? Convert.ToDecimal(item.VlUnit) : 0M,
                    VlFrete = item.VlFrete != null ? Convert.ToDecimal(item.VlFrete) : 0M,
                    CdPercComiss = item.CdPercComiss != null ? Convert.ToDecimal(item.CdPercComiss) : 0M
                });
            }

            return lstRenumo;

        }
        public void SavePedido(List<ResumoPedido> resumoPedidos)
        {
            if (resumoPedidos != null && resumoPedidos.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM {NomeTabelaResumoPedido};");

                foreach (var item in resumoPedidos)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaResumoPedido}](
                                                  NrPedido 
                                                 ,CdProduto
                                                 ,DsProduto
                                                 ,NumLote
                                                 ,CdRcaxxx
                                                 ,ImagemProduto
                                                 ,QtProduto
                                                 ,QtAtend
                                                 ,VlVenda
                                                 ,VlUnit
                                                 ,VlFrete
                                                 ,CdPercComiss)
                                                            VALUES (
                                                   '{item.NrPedido}'
                                                  ,'{item.CdProduto}'
                                                  ,'{item.DsProduto}'
                                                  ,'{item.NumLote}'
                                                  ,'{item.CdRcaxxx}'
                                                  ,'{item.ImagemProduto}'
                                                  , {item.QtProduto}
                                                  , {item.QtAtend}
                                                  , {item.VlVenda.ToStringInvariant("0.00")}
                                                  , {item.VlUnit.ToStringInvariant("0.00")}
                                                  , {item.VlFrete.ToStringInvariant("0.00")}
                                                  , {item.CdPercComiss.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM {NomeTabelaResumoPedido};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _ = int.TryParse(_total.ToString(), out int total);
            return total;
        }
        public List<ResumoPedido> GetByNumPedido(string numeroPedido)
        {
            var command = $@"SELECT * FROM {NomeTabelaResumoPedido} Where NrPedido= '{numeroPedido}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<ResumoPedido>();

            var lstRenumo = new List<ResumoPedido>();

            foreach (var item in retorno)
            {
                try
                {
                    var resumo = new ResumoPedido();
                    resumo.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    resumo.NrPedido = item.NrPedido.ToString();
                    resumo.CdProduto = item.CdProduto.ToString();
                    resumo.DsProduto = item.DsProduto.ToString();
                    resumo.NumLote = item.NumLote.ToString();
                    resumo.CdRcaxxx = item.CdRcaxxx.ToString();
                    resumo.ImagemProduto = item.ImagemProduto.ToString();
                    resumo.QtProduto = item.QtProduto != null ? Convert.ToInt32(item.QtProduto) : 0;
                    resumo.QtAtend = item.QtAtend != null ? Convert.ToInt32(item.QtAtend) : 0;
                    resumo.VlUnit = item.VlUnit != null ? Convert.ToDecimal(item.VlUnit) : 0M;
                    resumo.VlVenda = item.VlVenda != null ? Convert.ToDecimal(item.VlVenda) : 0M;
                    resumo.VlFrete = item.VlFrete != null ? Convert.ToDecimal(item.VlFrete) : 0M;
                    resumo.CdPercComiss = item.CdPercComiss != null ? Convert.ToDecimal(item.CdPercComiss) : 0M;
                    lstRenumo.Add(resumo);
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        Descricao = ex.Message
                    });
                }
            }

            return lstRenumo;
        }

        public void CriarTabela()
        {
            Init(NomeTabelaResumoPedido);
        }

        public void SavePedidoVendedor(List<ResumoPedido> resumoPedidos, string codigoVendedor)
        {
            if (resumoPedidos != null && resumoPedidos.Any())
            {
                CriarTabelaResumoPedidoVendedor(codigoVendedor);
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM ResumoPedido_{codigoVendedor};");

                foreach (var item in resumoPedidos)
                {
                    var commandInsert = $@"INSERT INTO ResumoPedido_{codigoVendedor}(
                                                  NrPedido 
                                                 ,CdProduto
                                                 ,DsProduto
                                                 ,NumLote
                                                 ,CdRcaxxx
                                                 ,ImagemProduto
                                                 ,QtProduto
                                                 ,QtAtend
                                                 ,VlVenda
                                                 ,VlUnit
                                                 ,VlFrete
                                                 ,CdPercComiss)
                                                            VALUES (
                                                   '{item.NrPedido}'
                                                  ,'{item.CdProduto}'
                                                  ,'{item.DsProduto}'
                                                  ,'{item.NumLote}'
                                                  ,'{item.CdRcaxxx}'
                                                  ,'{item.ImagemProduto}'
                                                  , {item.QtProduto}
                                                  , {item.QtAtend}
                                                  , {item.VlVenda.ToStringInvariant("0.00")}
                                                  , {item.VlUnit.ToStringInvariant("0.00")}
                                                  , {item.VlFrete.ToStringInvariant("0.00")}
                                                  , {item.CdPercComiss.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        private void CriarTabelaResumoPedidoVendedor(string codigoVendedor)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS ResumoPedido_{codigoVendedor}(
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

        private string RecuperarNomeDaTabelaResumoPedido()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"ResumoPedido_{App.VendedorSelecionado.CodigoVendedor}";
                    Init(tableName);
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
