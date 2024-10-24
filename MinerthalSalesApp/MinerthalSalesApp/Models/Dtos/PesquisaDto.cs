using MinerthalSalesApp.Infra.Database.Tables;
namespace MinerthalSalesApp.Models.Dtos
{
    public class PesquisaDto
    {
        public List<Cliente> ClientesInadinplentes { get; set; } = new List<Cliente>();
        public List<Faturamento> TitulosaVencer { get; set; } = new List<Faturamento>();
        public List<Faturamento> TitulosVencidos { get; set; } = new List<Faturamento>();
        public List<HistoricoDePedidos> PedidosEmAberto { get; set; } = new List<HistoricoDePedidos>();
        public List<HistoricoDePedidos> Carregamentos { get; set; } = new List<HistoricoDePedidos>();
        public List<MetaMensal> MetaMensal { get; set; } = new List<MetaMensal>();
    }
}
