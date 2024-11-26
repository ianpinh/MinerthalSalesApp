using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class CartRepository : ICartRepository
    {
        private string NomeTabelaCarrinho => RecuperarNomeDaTabelaCarrinho();
        private readonly IAppthalContext _context;
        public CartRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //Init();
        }
        private void Init(string nomeTabela)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS {nomeTabela}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,PedidoId UNIQUEIDENTIFIER NOT NULL
                                                 ,ProdutoId INT
                                                 ,Quantidade INT
                                                 ,CodProduto VARCHAR(50)
                                                 ,CodigoNomeProduto VARCHAR(50)
                                                 ,ImagemProduto VARCHAR(80)
                                                 ,ValorProduto DECIMAL(7,2)
                                                 ,ValorCombinado DECIMAL(7,2)
                                                 ,Frete DECIMAL(7,2)
                                                 ,Comissao DECIMAL(7,2)
                                                 ,Desconto DECIMAL(7,2)
                                                 ,Encargos DECIMAL(7,2)
                                                 ,TaxaEncargos DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        public List<Carrinho> GetAll()
        {
            var command = $@"SELECT * FROM {NomeTabelaCarrinho}";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Carrinho>();

            var lstCart = new List<Carrinho>();
            foreach (var item in retorno)
            {
                lstCart.Add(new Carrinho
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    PedidoId = item.PedidoId != null ? Guid.Parse(item.PedidoId) : Guid.Empty,
                    ProdutoId = item.ProdutoId != null ? Convert.ToInt32(item.ProdutoId) : 0,
                    Quantidade = item.Quantidade != null ? Convert.ToInt32(item.Quantidade) : 0,
                    CodProduto = item.CodProduto.ToString(),
                    CodigoNomeProduto = item.CodigoNomeProduto.ToString(),
                    ImagemProduto = item.ImagemProduto.ToString(),
                    ValorProduto = item.ValorProduto != null ? Convert.ToDecimal(item.ValorProduto) : 0M,
                    ValorCombinado = item.ValorCombinado != null ? Convert.ToDecimal(item.ValorCombinado) : 0M,
                    Frete = item.Frete != null ? Convert.ToDecimal(item.Frete) : 0M,
                    Comissao = item.Comissao != null ? Convert.ToDecimal(item.Comissao) : 0M,
                    Desconto = item.Desconto != null ? Convert.ToDecimal(item.Desconto) : 0M,
                    Encargos = item.Encargos != null ? Convert.ToDecimal(item.Encargos) : 0M,
                    TaxaEncargos = item.TaxaEncargos != null ? Convert.ToDecimal(item.TaxaEncargos) : 0M
                });
            }
            return lstCart;
        }

        public void Add(Carrinho cart)
        {
            var commandInsert = $@"INSERT INTO [{NomeTabelaCarrinho}](
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
                                                   {cart.PedidoId}
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
                                                 , {cart.TaxaEncargos.ToStringInvariant("0.00")});";
            _context.ExcecutarComandoCrud(commandInsert);
        }

        public void AddRange(List<Carrinho> cart)
        {
            if (cart != null && cart.Count > 0)
                foreach (var item in cart)
                    Add(item);
        }

        public void DeleteAll()
        {
            var command = $"Delete from {NomeTabelaCarrinho};";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteById(int id)
        {
            var command = $"Delete from {NomeTabelaCarrinho} WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteByPedido(Guid pedidoId)
        {
            var command = $"Delete from {NomeTabelaCarrinho} WHERE PedidoId = '{pedidoId}';";
            _context.ExcecutarComandoCrud(command);
        }

        public List<Carrinho> GetByOrderId(Guid pedidoId)
        {
            var command = $@"SELECT * FROM {NomeTabelaCarrinho} WHERE PedidoId = '{pedidoId}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Carrinho>();

            var lstCart = new List<Carrinho>();
            foreach (var item in retorno)
            {
                lstCart.Add(new Carrinho
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    PedidoId = item.PedidoId != null ? Guid.Parse(item.PedidoId) : Guid.Empty,
                    ProdutoId = item.ProdutoId != null ? Convert.ToInt32(item.ProdutoId) : 0,
                    Quantidade = item.Quantidade != null ? Convert.ToInt32(item.Quantidade) : 0,
                    CodProduto = item.CodProduto.ToString(),
                    CodigoNomeProduto = item.CodigoNomeProduto.ToString(),
                    ImagemProduto = item.ImagemProduto.ToString(),
                    ValorProduto = item.ValorProduto != null ? Convert.ToDecimal(item.ValorProduto) : 0M,
                    ValorCombinado = item.ValorCombinado != null ? Convert.ToDecimal(item.ValorCombinado) : 0M,
                    Frete = item.Frete != null ? Convert.ToDecimal(item.Frete) : 0M,
                    Comissao = item.Comissao != null ? Convert.ToDecimal(item.Comissao) : 0M,
                    Desconto = item.Desconto != null ? Convert.ToDecimal(item.Desconto) : 0M,
                    Encargos = item.Encargos != null ? Convert.ToDecimal(item.Encargos) : 0M,
                    TaxaEncargos = item.TaxaEncargos != null ? Convert.ToDecimal(item.TaxaEncargos) : 0M
                });
            }
            return lstCart;
        }

        public int GetTotal()
        {
            var command = $@"SELECT COUNT(*) FROM {NomeTabelaCarrinho};";
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
            Init(NomeTabelaCarrinho);
        }

        private string RecuperarNomeDaTabelaCarrinho()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"Carrinho_{App.VendedorSelecionado.CodigoVendedor}";
                    Init(tableName);
                    return tableName;
                }

                return "Carrinho";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}