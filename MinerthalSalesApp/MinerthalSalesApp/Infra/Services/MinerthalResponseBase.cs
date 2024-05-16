using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Services
{
    public abstract class MinerthalResponseBase
    {
        public int TotalPag { get; set; }
        public int PagAtual { get; set; }
        public int TotalReg { get; set; }
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }
    
    public class ResponseApiBanco : MinerthalResponseBase
    {
        public List<Banco> Details { get; set; } = new List<Banco>();
    }
    public class ResponseApiClientePlanoPagamento : MinerthalResponseBase
    {
        public List<ClientePlanoPagamento> Details { get; set; } = new List<ClientePlanoPagamento>();
    }
    public class ResponseApiPlano: MinerthalResponseBase
    {
        public List<Plano> Details { get; set; } = new List<Plano>();
    }
    public class ResponseApiProduto: MinerthalResponseBase
    {
        public List<Produto> Details { get; set; } = new List<Produto>();
    }
    public class ResponseApiCliente : MinerthalResponseBase
    {
        public List<Cliente> Details { get; set; } = new List<Cliente>();
    }
    public class ResponseApiUsuario: MinerthalResponseBase
    {
        public List<Usuario> Details { get; set; } = new List<Usuario>();
    }
    public class ResponseApiFaturamento: MinerthalResponseBase
    {
        public List<Faturamento> Details { get; set; } = new List<Faturamento>();
    }
    public class ResponseApiPedido: MinerthalResponseBase
    {
        public List<Pedido> Details { get; set; } = new List<Pedido>();
    }
    public class ResponseApiRanking: MinerthalResponseBase
    {
        public List<Ranking> Details { get; set; } = new List<Ranking>();
    }
    public class ResponseApiTabelaPreco : MinerthalResponseBase
    {
        public List<TabelaPreco> Details { get; set; } = new List<TabelaPreco>();
    }

    public class ResponseApiVendedor: MinerthalResponseBase
    {
        public List<Vendedor> Details { get; set; } = new List<Vendedor>();
    }

    public class ResponseApiMeusPedidos : MinerthalResponseBase
    {
        public List<MeusPedidos> Details { get; set; } = new List<MeusPedidos>();
    }
    public class ResponseApiFilial: MinerthalResponseBase
    {
        public List<Filial> Details { get; set; } = new List<Filial>();
    }

    public class ResponseApiHistoricoPedido: MinerthalResponseBase
    {
        public List<HistoricoDePedidos> Details { get; set; } = new List<HistoricoDePedidos>();
    }

    public class ResponseApiResumoPedido : MinerthalResponseBase
    {
        public List<ResumoPedido> Details { get; set; } = new List<ResumoPedido>();
    }

    public class ResponseApiTitulos: MinerthalResponseBase
    {
        public List<Titulo> Details { get; set; } = new List<Titulo>();
    }

    public class ResponseApiVisitas: MinerthalResponseBase
    {
        public List<Visita> Details { get; set; } = new List<Visita>();
    }
}
