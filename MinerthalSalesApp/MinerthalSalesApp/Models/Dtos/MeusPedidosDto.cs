using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Dtos
{
    public class MeusPedidosDto
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<MeusPedidos> Details { get; set; } = new List<MeusPedidos>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();

    }
}
