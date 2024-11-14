using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Dtos
{
    public class VendedorSelecionadoDto
    {
        public int VendedorId { get; set; }
        public string NomeVendedor { get; set; }
        public string CodigoVendedor { get; set; }
        public List<Cliente> Clientes { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public List<Pedido> PedidosPendentesEnvio { get; set; } = new List<Pedido>();
    }
}
