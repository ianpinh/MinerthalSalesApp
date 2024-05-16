using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models.Dtos;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace MinerthalSalesApp.ViewModels.Orders
{
    public class OrderXmlViewModel
    {
        public string MontarXmlPedido(Pedido model)
        {

            var sbOrder = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //var carrinho = App.CartRepository.GetByOrderId(model.Id);
            //var cliente = App.ClienteRepository.GetById(model.CodigoCliente);
            //var totalFrete = carrinho.Sum(x => x.Frete);
            //sbOrder.Append($"<soapenv:Envelope xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:d=\"http://www.w3.org/2001/XMLSchema\" xmlns:c=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\">");
            //sbOrder.Append($"<soapenv:Header /><soapenv:Body>");
            //sbOrder.Append($"<ADDPEDIDOPECA xmlns=\"http://192.168.0.5:9595/ws/wspedidoservicepeca.apw/\">");
            //sbOrder.Append($"<CODIGOCLIENTE>{cliente.A1_Cod}{cliente.A1_Loja}</CODIGOCLIENTE>");
            //sbOrder.Append($"<FILIALPEDIDO>{model.FilialMinerthal}</FILIALPEDIDO>");
            //sbOrder.Append($"<PEDIDOMAXIMA>{model.Id}</PEDIDOMAXIMA>");
            //sbOrder.Append($"<TADDPEDIDO>");
            //sbOrder.Append($"<BANCOPEDIDO>{model.TipoCobranca}</BANCOPEDIDO>");
            //sbOrder.Append($"<CONDICAOPAGAMENTO>{model.PlanoPagamento}</CONDICAOPAGAMENTO>");
            //sbOrder.Append($"<PRECOFRETELIQ>{totalFrete}</PRECOFRETELIQ>");
            //sbOrder.Append($"<TIPOFRETE>C</TIPOFRETE>"); 
            //sbOrder.Append($"<TRANSPORTADORACLIENTE></TRANSPORTADORACLIENTE>");
            //sbOrder.Append($"<TZITENSDOPEDIDO i:type=\"d:anyType\">");
            //sbOrder.Append($"<TADDPEDIDODET>");
            //if (carrinho.Any())
            //{
            //    foreach (var item in carrinho)
            //    {
            //        sbOrder.Append($"<PERCCOMISSAO>{item.Comissao}</PERCCOMISSAO>");
            //        sbOrder.Append($"<PRECOUNITARIO>{item.ValorProduto}</PRECOUNITARIO>");
            //        sbOrder.Append($"<PRODUTOPEDIDO>{item.CodProduto}</PRODUTOPEDIDO>");
            //        sbOrder.Append($"<QUANTIDADEPRODUTO i:type=\"d:int\">{item.Quantidade}</QUANTIDADEPRODUTO>");
            //    }
            //}
            //sbOrder.Append($"</TADDPEDIDODET>");
            //sbOrder.Append("</TZITENSDOPEDIDO>");
            //if (!string.IsNullOrWhiteSpace(model.Parcelas))
            //{
            //    var _parcelas = JsonConvert.DeserializeObject<List<DictionaryDto>>(model.Parcelas);
            //    sbOrder.Append($"<DATASPARCELA>");
            //    foreach (var item in _parcelas)
            //        sbOrder.Append($"{item.Value};");
            //    sbOrder.Append($"</DATASPARCELA>");


            //    var valorPedido= carrinho.Sum(x => (x.Quantidade * x.ValorProduto) + totalFrete);
            //    var valorParcelas = valorPedido/_parcelas.Count;
            //    sbOrder.Append($"<VLRPARCELA i:type=\"d:string\">");
            //    for (var i = 0; i< _parcelas.Count; i++)
            //    {
            //        sbOrder.Append($"{valorParcelas};");// 80.01; 80.01;
            //    }
            //    sbOrder.Append($"</VLRPARCELA>");
            //}
            //sbOrder.Append($"<OBSPEDIDO>ObservacaoInternaNF</OBSPEDIDO>");
            //sbOrder.Append($"<TIPOBOLETO>E</TIPOBOLETO>");
            //sbOrder.Append($"</TADDPEDIDO>");
            //sbOrder.Append($"</ADDPEDIDOPECA>");
            //sbOrder.Append($"</soapenv:Body>");
            //sbOrder.Append($"</soapenv:Envelope>");
            return sbOrder.ToString();
        }
    }
}