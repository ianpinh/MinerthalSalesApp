using MinerthalSalesApp.Infra.Database.Tables;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MinerthalSalesApp.Models.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string CodigoCliente { get; set; }
        public string FilialMinerthal { get; set; }
        public string TipoPedido { get; set; }
        public string TipoVenda { get; set; }
        public string PlanoPagamento { get; set; }
        public string TipoCobranca { get; set; }
        public decimal ValorFrete25 { get; set; } = 0M;
        public decimal ValorFrete30 { get; set; } = 0M;
        public decimal PercentualTaxaPlano { get; set; } = 0M;
        public decimal PercentualDesconto { get; set; }


        public string NomeFilial { get; set; }
        public string NomeTipo { get; set; }
        public string NomeTipoVenda { get; set; }
        public string NomeTipoCobranca { get; set; }
        public string NomePlanoPagamento { get; set; }

        public ObservableCollection<DictionaryDto> Parcelas { get; set; } = new ObservableCollection<DictionaryDto>();

        public List<ItensDto> ItensPedido { get; set; } = new List<ItensDto>();
        public decimal TotalPedido { get; set; }
        public decimal Frete { get; set; }
        public int QdtItens { get; set; }

        public Plano Plano { get; set; }
        public string Observacao { get; set; }

    }
    public class ItensDto
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        public int Id { get; set; }
        public Guid PedidoId { get; set; }
        public Produto Produto { get; set; }
        public string CodProduto { get; set; }
        public string CodigoNomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorBrutoProduto { get; set; }
        public decimal ValorCombinado { get; set; }
        public decimal Frete { get; set; } = 0M;
        public decimal FreteUnidade { get; set; } = 0M;
        public decimal Comissao { get; set; }
        public decimal TaxaPlano { get; set; } = 0M;
        public decimal Desconto { get; set; }
        public string ImagemProduto { get; set; }
        public decimal ValorDescontoMaximo { get; set; } = 0M;
        public decimal ValorDescontoMinimo { get; set; } = 0M;
        public int QuantidadeMin { get; set; } = 0;
        public int QuantidadeMax { get; set; } = 0;
        public byte Ordem { get; set; } = 0;

        public decimal ValorDesconto => CalculoDesconto();
        public string ProdutoPesp { get; set; }
        public decimal ValorTaxaPlano => CalcularTaxa();
        public decimal Subtotal => CalcularSubTotal();
        public decimal ValorTotalProduto => CalcularValorTotalProduto();
        public decimal ValorTotalFrete => CalcularValorFrete();
        public decimal ValorTotal => CalcularValorTotal();


        public string ValorTaxaPlanoString => ValorTaxaPlano.ToString("c", _culture);
        public string SubtotalString => Subtotal.ToString("c", _culture);
        public string ValorTotalProduToString => ValorTotalProduto.ToString("c", _culture);
        public string ValorTotalFreteString => ValorTotalFrete.ToString("c", _culture);
        public string ValorTotalString => ValorTotal.ToString("c", _culture);
        public string DesconToString => Desconto.ToString("c", _culture);
        public string FreteUnidadeString => FreteUnidade.ToString("c", _culture);
        public string ValorBrutoProduToString => ValorBrutoProduto.ToString("c", _culture);
        public string ValorCombinadoString => ValorCombinado.ToString("c", _culture);
        public string ValorDesconToString=> ValorDesconto.ToString("c", _culture);


        private decimal CalcularSubTotal()
        {
            var produtos = Quantidade * ValorCombinado;
            var fretes = Quantidade * FreteUnidade;
            return produtos+fretes;
        }
        private decimal CalcularValorTotalProduto()
        {
            return Quantidade * ValorCombinado;
        }

        private decimal CalcularValorFrete()
        {
            return Quantidade * FreteUnidade;
        }

        private decimal CalcularValorTotal()
        {
            var valorTotal = (Quantidade * ValorCombinado)+(Quantidade * FreteUnidade);
            if (TaxaPlano>0)
                valorTotal+=(valorTotal*(TaxaPlano/100));
            else if (Desconto >0)
                valorTotal -= (valorTotal*(Desconto/100));

            return valorTotal;
        }

        private decimal CalcularTaxa()
        {
            var _valorTotal = (Quantidade * ValorCombinado)+(Quantidade * FreteUnidade);
            return TaxaPlano>0 ? _valorTotal*(TaxaPlano/100) : 0M;
        }

        private decimal CalculoDesconto()
        {
            var _valorTotal = (Quantidade * ValorCombinado)+(Quantidade * FreteUnidade);
            return Desconto >0 ? _valorTotal*(Desconto/100) : 0M;
        }

        public List<TabelaPreco> TbPrecosProduto { get; set; } = new List<TabelaPreco>();

        public void CalcularComissaoTotal()
        {
            var faixaComissao = 0M;
            var totalDescontos = ValorBrutoProduto - ValorCombinado;

            foreach (var item in TbPrecosProduto)
            {
                if ((totalDescontos >=  item.PerMin) && (totalDescontos <=item.PerMax))
                {
                    faixaComissao=item.PerComissao;
                    break;
                }
            }
            Comissao = faixaComissao;
        }


    }
}
