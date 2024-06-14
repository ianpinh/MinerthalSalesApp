using Microsoft.Data.Sqlite;
using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Data.Common;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IAppthalContext _context;
        public PedidoRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }

        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS Pedido(
                                                  Id UNIQUEIDENTIFIER PRIMARY KEY  NOT NULL
                                                 ,CodigoCliente VARCHAR(20)
                                                 ,CodigoLoja VARCHAR(20)
                                                 ,FilialMinerthal VARCHAR(20)
                                                 ,TipoPedido VARCHAR(20)
                                                 ,TipoVenda VARCHAR(20)
                                                 ,PlanoPagamento VARCHAR(20)
                                                 ,TipoCobranca VARCHAR(20)
                                                 ,Parcelas VARCHAR(20)
                                                 ,CodProduto VARCHAR(20)
                                                 ,Observacao VARCHAR(300)
                                                 ,NomeFilial VARCHAR(150)
                                                 ,NomeTipo VARCHAR(50)
                                                 ,NomeTipoVenda VARCHAR(20)
                                                 ,NomeTipoCobranca VARCHAR(20)
                                                 ,NomePlanoPagamento VARCHAR(100)
                                                 ,PercentualDesconto DECIMAL(7,2)
                                                 ,PercentualJuros DECIMAL(7,2)
                                                 ,Comissao DECIMAL(7,2)
                                                 ,ValorFrete25 DECIMAL(7,2)
                                                 ,ValorParcelas DECIMAL(7,2)
                                                 ,ValorFrete30 DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);


            var checkColumnCommand = "PRAGMA table_info(Pedido);";
            var columns = _context.ExecutarComandoConsulta(checkColumnCommand);

            bool columnExists = false;
            foreach (var column in columns)
            {
                if (column["name"].ToString() == "CodigoLoja")
                {
                    columnExists = true;
                    break;
                }
            }

            // Adicionar a coluna se ela não existir
            if (!columnExists)
            {
                var addColumnCommand = "ALTER TABLE Pedido ADD COLUMN CodigoLoja VARCHAR(20);";
                _context.ExcecutarComandoCrud(addColumnCommand);
            }
        }

        public List<Pedido> GetAll()
        {
            var command = $@"SELECT * FROM Pedido";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Pedido>();

            var lst = new List<Pedido>();

            foreach (var item in retorno)
            {
                lst.Add(new Pedido
                {
                    Id=item.Id!=null ? Guid.Parse(item.Id) : Guid.Empty,
                    CodigoCliente=item.CodigoCliente.ToString(),
                    CodigoLoja=item.CodigoLoja.ToString(),
                    FilialMinerthal=item.FilialMinerthal.ToString(),
                    TipoPedido=item.TipoPedido.ToString(),
                    TipoVenda=item.TipoVenda.ToString(),
                    PlanoPagamento=item.PlanoPagamento.ToString(),
                    TipoCobranca=item.TipoCobranca.ToString(),
                    Parcelas=item.Parcelas.ToString(),
                    CodProduto=item.CodProduto.ToString(),
                    Observacao=item.Observacao.ToString(),
                    NomeFilial=item.NomeFilial.ToString(),
                    NomeTipo=item.NomeTipo.ToString(),
                    NomeTipoVenda=item.NomeTipoVenda.ToString(),
                    NomeTipoCobranca=item.NomeTipoCobranca.ToString(),
                    NomePlanoPagamento=item.NomePlanoPagamento.ToString(),
                    PercentualDesconto=item.PercentualDesconto!=null ? Convert.ToDecimal(item.PercentualDesconto) : 0M,
                    PercentualJuros=item.PercentualJuros!=null ? Convert.ToDecimal(item.PercentualJuros) : 0M,
                    Comissao=item.Comissao!=null ? Convert.ToDecimal(item.Comissao) : 0M,
                    ValorFrete25=item.VlPrectab!=null ? Convert.ToDecimal(item.VlPrectab) : 0M,
                    ValorParcelas=item.VlPrectab!=null ? Convert.ToDecimal(item.VlPrectab) : 0M,
                    ValorFrete30=item.VlPrectab!=null ? Convert.ToDecimal(item.VlPrectab) : 0M,
                });
            }

            return lst;
        }

        public int Add(Pedido pedido)
        {
            if (pedido!=null)
            {
                if (pedido.Id.Equals(Guid.Empty))
                    pedido.Id = Guid.NewGuid();


                var sb = new StringBuilder();
                sb.AppendLine($@"INSERT INTO [Pedido](
                                                     Id
                                                    ,CodigoCliente
                                                    ,CodigoLoja
                                                    ,FilialMinerthal
                                                    ,TipoPedido
                                                    ,TipoVenda
                                                    ,PlanoPagamento
                                                    ,TipoCobranca
                                                    ,Parcelas
                                                    ,CodProduto
                                                    ,Observacao
                                                    ,NomeFilial
                                                    ,NomeTipo
                                                    ,NomeTipoVenda
                                                    ,NomeTipoCobranca
                                                    ,NomePlanoPagamento
                                                    ,PercentualDesconto
                                                    ,PercentualJuros
                                                    ,Comissao
                                                    ,ValorFrete25
                                                    ,ValorParcelas
                                                    ,ValorFrete30)
                                                            VALUES (
                                                     '{pedido.Id}'
                                                    ,'{pedido.CodigoCliente}'
                                                    ,'{pedido.CodigoLoja}'
                                                    ,'{pedido.FilialMinerthal}'
                                                    ,'{pedido.TipoPedido}'
                                                    ,'{pedido.TipoVenda}'
                                                    ,'{pedido.PlanoPagamento}'
                                                    ,'{pedido.TipoCobranca}'
                                                    ,'{pedido.Parcelas}'
                                                    ,'{pedido.CodProduto}'
                                                    ,'{pedido.Observacao}'
                                                    ,'{pedido.NomeFilial}'
                                                    ,'{pedido.NomeTipo}'
                                                    ,'{pedido.NomeTipoVenda}'
                                                    ,'{pedido.NomeTipoCobranca}'
                                                    ,'{pedido.NomePlanoPagamento}'
                                                    , {pedido.PercentualDesconto}
                                                    , {pedido.PercentualJuros.ToStringInvariant("0.00")}
                                                    , {pedido.Comissao.ToStringInvariant("0.00")}
                                                    , {pedido.ValorFrete25.ToStringInvariant("0.00")}
                                                    , {pedido.ValorParcelas.ToStringInvariant("0.00")}
                                                    , {pedido.ValorFrete30.ToStringInvariant("0.00")});");

                sb.AppendLine();
                if (pedido.ItensDoPedido.Count>0)
                {
                    foreach (var cart in pedido.ItensDoPedido)
                    {
                        sb.AppendLine($@"INSERT INTO [Carrinho](
                                                  PedidoId
                                                 ,ProdutoId
                                                 ,Quantidade
                                                 ,CodProduto
                                                 ,CodigoNomeProduto
                                                 ,ImagemProduto
                                                 ,ValorProduto
                                                 ,ValorCombinado
                                                 ,Frete
                                                 ,Comissao
                                                 ,Desconto
                                                 ,Encargos
                                                 ,TaxaEncargos)
                                                        VALUES(
                                                  '{pedido.Id}'
                                                 , {cart.ProdutoId}
                                                 , {cart.Quantidade}
                                                 ,'{cart.CodProduto}'
                                                 ,'{cart.CodigoNomeProduto}'
                                                 ,'{cart.ImagemProduto}'
                                                 , {cart.ValorProduto.ToStringInvariant("0.00")}
                                                 , {cart.ValorCombinado.ToStringInvariant("0.00")}
                                                 , {cart.Frete.ToStringInvariant("0.00")}
                                                 , {cart.Comissao.ToStringInvariant("0.00")}
                                                 , {cart.Desconto.ToStringInvariant("0.00")}
                                                 , {cart.Encargos.ToStringInvariant("0.00")}
                                                 , {cart.TaxaEncargos.ToStringInvariant("0.00")});");
                    }
                }

                var commandInsert = sb.ToString();
                return _context.ExcecutarComandoCrud(commandInsert);
            }
            return 0;
        }

        public void AddRange(List<Pedido> pedido)
        {
            if (pedido!=null && pedido.Any())
            {
                var sb = new StringBuilder();
                foreach (var item in pedido)
                {
                    
                    var commandInsert = $@"INSERT INTO [Pedido](Id
                                                    ,CodigoCliente
                                                    ,CodigoLoja
                                                    ,FilialMinerthal
                                                    ,TipoPedido
                                                    ,TipoVenda
                                                    ,PlanoPagamento
                                                    ,TipoCobranca
                                                    ,Parcelas
                                                    ,CodProduto
                                                    ,Observacao
                                                    ,NomeFilial
                                                    ,NomeTipo
                                                    ,NomeTipoVenda
                                                    ,NomeTipoCobranca
                                                    ,NomePlanoPagamento
                                                    ,PercentualDesconto
                                                    ,PercentualJuros
                                                    ,Comissao
                                                    ,ValorFrete25
                                                    ,ValorParcelas
                                                    ,ValorFrete30)
                                                            VALUES (
                                                     '{item.Id}'
                                                    ,'{item.CodigoCliente}'
                                                    ,'{item.CodigoLoja}'
                                                    ,'{item.FilialMinerthal}'
                                                    ,'{item.TipoPedido}'
                                                    ,'{item.TipoVenda}'
                                                    ,'{item.PlanoPagamento}'
                                                    ,'{item.TipoCobranca}'
                                                    ,'{item.Parcelas}'
                                                    ,'{item.CodProduto}'
                                                    ,'{item.Observacao}'
                                                    ,'{item.NomeFilial}'
                                                    ,'{item.NomeTipo}'
                                                    ,'{item.NomeTipoVenda}'
                                                    ,'{item.NomeTipoCobranca}'
                                                    ,'{item.NomePlanoPagamento}'
                                                    , {item.PercentualDesconto.ToStringInvariant("0.00")}
                                                    , {item.PercentualJuros.ToStringInvariant("0.00")}
                                                    , {item.Comissao.ToStringInvariant("0.00")}
                                                    , {item.ValorFrete25.ToStringInvariant("0.00")}
                                                    , {item.ValorParcelas.ToStringInvariant("0.00")}
                                                    , {item.ValorFrete30.ToStringInvariant("0.00")});";
                    sb.AppendLine(commandInsert);

                    sb.AppendLine();
                    if (item.ItensDoPedido.Count>0)
                    {
                        foreach (var cart in item.ItensDoPedido)
                        {
                            sb.AppendLine($@"INSERT INTO [Carrinho](
                                                  PedidoId
                                                 ,ProdutoId
                                                 ,Quantidade
                                                 ,CodProduto
                                                 ,CodigoNomeProduto
                                                 ,ImagemProduto
                                                 ,ValorProduto
                                                 ,ValorCombinado
                                                 ,Frete
                                                 ,Comissao
                                                 ,Desconto
                                                 ,Encargos
                                                 ,TaxaEncargos)
                                                        VALUES(
                                                  '{item.Id}'
                                                 , {cart.ProdutoId}
                                                 , {cart.Quantidade}
                                                 ,'{cart.CodProduto}'
                                                 ,'{cart.CodigoNomeProduto}'
                                                 ,'{cart.ImagemProduto}'
                                                 , {cart.ValorProduto.ToStringInvariant("0.00")}
                                                 , {cart.ValorCombinado.ToStringInvariant("0.00")}
                                                 , {cart.Frete.ToStringInvariant("0.00")}
                                                 , {cart.Comissao.ToStringInvariant("0.00")}
                                                 , {cart.Desconto.ToStringInvariant("0.00")}
                                                 , {cart.Encargos.ToStringInvariant("0.00")}
                                                 , {cart.TaxaEncargos.ToStringInvariant("0.00")});");
                        }
                    }
                }

                var command = sb.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public Pedido GetById(Guid id)
        {
            var command = $@"SELECT * FROM Pedido WHERE Id = '{id}';";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Pedido();

            return new Pedido
            {
                Id=retorno.Id!=null ? Guid.Parse(retorno.Id) : Guid.Empty,
                CodigoCliente=retorno.CodigoCliente.ToString(),
                CodigoLoja=retorno.CodigoLoja.ToString(),
                FilialMinerthal=retorno.FilialMinerthal.ToString(),
                TipoPedido=retorno.TipoPedido.ToString(),
                TipoVenda=retorno.TipoVenda.ToString(),
                PlanoPagamento=retorno.PlanoPagamento.ToString(),
                TipoCobranca=retorno.TipoCobranca.ToString(),
                Parcelas=retorno.Parcelas.ToString(),
                CodProduto=retorno.CodProduto.ToString(),
                Observacao=retorno.Observacao.ToString(),
                NomeFilial=retorno.NomeFilial.ToString(),
                NomeTipo=retorno.NomeTipo.ToString(),
                NomeTipoVenda=retorno.NomeTipoVenda.ToString(),
                NomeTipoCobranca=retorno.NomeTipoCobranca.ToString(),
                NomePlanoPagamento=retorno.NomePlanoPagamento.ToString(),
                PercentualDesconto=retorno.PercentualDesconto!=null ? Convert.ToDecimal(retorno.PercentualDesconto) : 0M,
                PercentualJuros=retorno.PercentualJuros!=null ? Convert.ToDecimal(retorno.PercentualJuros) : 0M,
                Comissao=retorno.Comissao!=null ? Convert.ToDecimal(retorno.Comissao) : 0M,
                ValorFrete25=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                ValorParcelas=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                ValorFrete30=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
            };
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM Pedido;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _=int.TryParse(_total.ToString(), out int total);
            return total;
        }

        public Pedido GetByClientId(string codigoCliente)
        {
            var command = $@"SELECT * FROM Pedido WHERE CodigoCliente = {codigoCliente};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Pedido();

            return new Pedido
            {
                Id=retorno.Id!=null ? Guid.Parse(retorno.Id) : Guid.Empty,
                CodigoCliente=retorno.CodigoCliente.ToString(),
                CodigoLoja=retorno.CodigoLoja.ToString(),
                FilialMinerthal=retorno.FilialMinerthal.ToString(),
                TipoPedido=retorno.TipoPedido.ToString(),
                TipoVenda=retorno.TipoVenda.ToString(),
                PlanoPagamento=retorno.PlanoPagamento.ToString(),
                TipoCobranca=retorno.TipoCobranca.ToString(),
                Parcelas=retorno.Parcelas.ToString(),
                CodProduto=retorno.CodProduto.ToString(),
                Observacao=retorno.Observacao.ToString(),
                NomeFilial=retorno.NomeFilial.ToString(),
                NomeTipo=retorno.NomeTipo.ToString(),
                NomeTipoVenda=retorno.NomeTipoVenda.ToString(),
                NomeTipoCobranca=retorno.NomeTipoCobranca.ToString(),
                NomePlanoPagamento=retorno.NomePlanoPagamento.ToString(),
                PercentualDesconto=retorno.PercentualDesconto!=null ? Convert.ToDecimal(retorno.PercentualDesconto) : 0M,
                PercentualJuros=retorno.PercentualJuros!=null ? Convert.ToDecimal(retorno.PercentualJuros) : 0M,
                Comissao=retorno.Comissao!=null ? Convert.ToDecimal(retorno.Comissao) : 0M,
                ValorFrete25=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                ValorParcelas=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                ValorFrete30=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
            };
        }

        public void DeleteAll()
        {
            var scriptCommand = new StringBuilder();
            scriptCommand.AppendLine($"Delete from Carrinho;");
            scriptCommand.AppendLine($"Delete from Pedido;");
            var command = scriptCommand.ToString();
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteById(Guid id)
        {
            var scriptCommand = new StringBuilder();
            scriptCommand.AppendLine($"Delete from Carrinho WHERE PedidoId = '{id}';");
            scriptCommand.AppendLine($"Delete from Pedido WHERE Id = '{id}';");

            var command = scriptCommand.ToString();
            _context.ExcecutarComandoCrud(command);
        }

        public void SavePedido(List<Pedido> pedido)
        {
            if (pedido!=null && pedido.Any())
            {
                var sb = new StringBuilder();
                sb.AppendLine("DELETE FROM Pedido;");

                foreach (var item in pedido)
                {
                    
                    var commandInsert = $@"INSERT INTO [Pedido](Id,
                                                     CodigoCliente
                                                    ,CodigoLoja
                                                    ,FilialMinerthal
                                                    ,TipoPedido
                                                    ,TipoVenda
                                                    ,PlanoPagamento
                                                    ,TipoCobranca
                                                    ,Parcelas
                                                    ,CodProduto
                                                    ,Observacao
                                                    ,NomeFilial
                                                    ,NomeTipo
                                                    ,NomeTipoVenda
                                                    ,NomeTipoCobranca
                                                    ,NomePlanoPagamento
                                                    ,PercentualDesconto
                                                    ,PercentualJuros
                                                    ,Comissao
                                                    ,ValorFrete25
                                                    ,ValorParcelas
                                                    ,ValorFrete30)
                                                            VALUES (
                                                     '{item.Id}'
                                                    ,'{item.CodigoCliente}'
                                                    ,'{item.CodigoLoja}'
                                                    ,'{item.FilialMinerthal}'
                                                    ,'{item.TipoPedido}'
                                                    ,'{item.TipoVenda}'
                                                    ,'{item.PlanoPagamento}'
                                                    ,'{item.TipoCobranca}'
                                                    ,'{item.Parcelas}'
                                                    ,'{item.CodProduto}'
                                                    ,'{item.Observacao}'
                                                    ,'{item.NomeFilial}'
                                                    ,'{item.NomeTipo}'
                                                    ,'{item.NomeTipoVenda}'
                                                    ,'{item.NomeTipoCobranca}'
                                                    ,'{item.NomePlanoPagamento}'
                                                    , {item.PercentualDesconto.ToStringInvariant("0.00")}
                                                    , {item.PercentualJuros.ToStringInvariant("0.00")}
                                                    , {item.Comissao.ToStringInvariant("0.00")}
                                                    , {item.ValorFrete25.ToStringInvariant("0.00")}
                                                    , {item.ValorParcelas.ToStringInvariant("0.00")}
                                                    , {item.ValorFrete30.ToStringInvariant("0.00")});";
                    sb.AppendLine(commandInsert);

                    sb.AppendLine();
                    if (item.ItensDoPedido.Count>0)
                    {
                        foreach (var cart in item.ItensDoPedido)
                        {
                            sb.AppendLine($@"INSERT INTO [Carrinho](
                                                  PedidoId
                                                 ,ProdutoId
                                                 ,Quantidade
                                                 ,CodProduto
                                                 ,CodigoNomeProduto
                                                 ,ImagemProduto
                                                 ,ValorProduto
                                                 ,ValorCombinado
                                                 ,Frete
                                                 ,Comissao
                                                 ,Desconto
                                                 ,Encargos
                                                 ,TaxaEncargos)
                                                        VALUES(
                                                  '{item.Id}'
                                                 , {cart.ProdutoId}
                                                 , {cart.Quantidade}
                                                 ,'{cart.CodProduto}'
                                                 ,'{cart.CodigoNomeProduto}'
                                                 ,'{cart.ImagemProduto}'
                                                 , {cart.ValorProduto.ToStringInvariant("0.00")}
                                                 , {cart.ValorCombinado.ToStringInvariant("0.00")}
                                                 , {cart.Frete.ToStringInvariant("0.00")}
                                                 , {cart.Comissao.ToStringInvariant("0.00")}
                                                 , {cart.Desconto.ToStringInvariant("0.00")}
                                                 , {cart.Encargos.ToStringInvariant("0.00")}
                                                 , {cart.TaxaEncargos.ToStringInvariant("0.00")});");
                        }
                    }
                }

                var command = sb.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void CriarTabela()
        {
            Init();
        }
    }
}
