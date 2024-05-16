using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Models.Dtos
{
    public class PedidosLocaisDto
    {
        public PedidosLocaisDto()
        {
                Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public OrderDto Pedido { get; set; } = new OrderDto();
        public Cliente Cliente { get; set; }
    }
}
