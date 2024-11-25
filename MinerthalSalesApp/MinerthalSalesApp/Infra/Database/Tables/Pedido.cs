using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{

    [Table("Pedido")]
    public class Pedido
    {
        [PrimaryKey, Column("Id")]
        public Guid Id { get; set; }



        [Column("CodigoCliente")]
        public string CodigoCliente { get; set; }

        [Column("CodigoLoja")]
        public string CodigoLoja { get; set; }

        [Column("Filial")]
        public string FilialMinerthal { get; set; }

        [Column("TipoPedido")]
        public string TipoPedido { get; set; }

        [Column("TipoVenda")]
        public string TipoVenda { get; set; }

        [Column("PlanoPagamento")]
        public string PlanoPagamento { get; set; }

        [Column("TipoCobranca")]
        public string TipoCobranca { get; set; }

        [Column("ValorFrete25")]
        public decimal ValorFrete25 { get; set; }

        [Column("ValorFrete30")]
        public decimal ValorFrete30 { get; set; }

        [Column("Parcelas")]
        public string Parcelas { get; set; }

        [Column("ValorParcelas")]
        public decimal ValorParcelas { get; set; }

        [Column("CodProduto")]
        public string CodProduto { get; set; }

        [Column("Comissao")]
        public decimal Comissao { get; set; }

        [Column("Observacao")]
        public string Observacao { get; set; }

        [Column("PercentualDesconto")]
        public decimal PercentualDesconto { get; set; }

        [Column("PercentualJuros")]
        public decimal PercentualJuros { get; set; }


        [Column("NomeFilial")]
        public string NomeFilial { get; set; }

        [Column("NomeTipo")]
        public string NomeTipo { get; set; }

        [Column("NomeTipoVenda")]
        public string NomeTipoVenda { get; set; }

        [Column("NomeTipoCobranca")]
        public string NomeTipoCobranca { get; set; }

        [Column("NomePlanoPagamento")]
        public string NomePlanoPagamento { get; set; }

        public List<Carrinho> ItensDoPedido { get; set; } = new List<Carrinho>();
    }
}