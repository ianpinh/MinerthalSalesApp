using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Dtos
{
    public class VendedoresDto
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<Vendedor> Details { get; set; } = new List<Vendedor>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }
}
