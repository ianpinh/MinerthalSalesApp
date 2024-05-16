namespace MinerthalSalesApp.Models.Dtos
{
    public class PedidoContentPageDto
    {
        //FORMULARIO PEDIDO
        public string NumPedido { get; set; }
        public string CodigoLoja { get; set; }
        public string FilialPedido { get; set; }
        public string PedidoMaxima { get; set; } //000705000001191120232154
        public string BancoPedido { get; set; }
        public string CondicaoPagamento { get; set; }
        public string PrecoFreteLiq { get; set; }
        public string TipoFrete { get; set; }
        public string TransportadoraCliente { get; set; }
        public string TipoBoleto { get; set; }
        public string ObservacaoInternaNF { get; set; }
        public List<ItemPedidoViewModel> ItensPedido { get; set; } = new List<ItemPedidoViewModel>();
    }
    public class ItemPedidoViewModel
    {
        public decimal PercComissao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string ProdutoPedido { get; set; }
        public int Quantidade { get; set; }
    }
}
