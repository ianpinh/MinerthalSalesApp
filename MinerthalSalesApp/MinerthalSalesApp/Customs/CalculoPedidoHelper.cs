using MinerthalSalesApp.ViewModels.Orders;
using System.Globalization;

namespace MinerthalSalesApp.Customs
{
    public class CalculoPedidoHelper
    {
        CultureInfo cultureInfo = new CultureInfo("pt-BR");
        private PedidoViewModel _model;

        public CalculoPedidoHelper(PedidoViewModel model)
        {
            _model=model;
        }

        private decimal CalcularComissaoDaVenda()
        {
            var comissao = 0M;
            try
            {
                foreach (var item in _model.Pedido.ItensPedido)
                    comissao += ((item.Quantidade * item.ValorCombinado) * (item.Comissao/100));
            }
            catch (Exception)
            {
                throw;
            }
            return comissao;
        }

    }
}
