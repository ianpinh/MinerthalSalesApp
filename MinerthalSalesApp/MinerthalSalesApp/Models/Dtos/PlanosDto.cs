using MinerthalSalesApp.Infra.Database.Tables;
namespace MinerthalSalesApp.Models.Dtos
{
    public class PlanosDto
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<Plano> Details { get; set; } = new List<Plano>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }
}
