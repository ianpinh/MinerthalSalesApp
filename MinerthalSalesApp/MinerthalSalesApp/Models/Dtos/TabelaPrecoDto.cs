using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Dtos
{
    public class TabelaPrecoDto
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<TabelaPreco> Details { get; set; } = new List<TabelaPreco>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }
}
