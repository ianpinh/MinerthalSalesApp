using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Produtos
{
    public class ProdutoModel
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<Produto> Details { get; set; } = new List<Produto>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }
}