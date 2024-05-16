namespace MinerthalSalesApp.Models.Produtos
{
    public class FiliaisMinerthalModel
    {
        public int TotalPag { get; set; } = 0;
        public int PagAtual { get; set; } = 0;
        public int TotalReg { get; set; } = 0;
        public List<FiliaisMinerthal> Details { get; set; } = new List<FiliaisMinerthal>();
        public List<string[]> NamesFields { get; set; } = new List<string[]>();
    }

    public class FiliaisMinerthal
    {
        public Guid Id { get; set; }
        public string CD_FILIAL { get; set; } = string.Empty;
        public string NM_FILIAL { get; set; } = string.Empty;
        public string NR_REGIAO { get; set; } = string.Empty;
        public string CD_RCAXXX { get; set; }
    }
}
