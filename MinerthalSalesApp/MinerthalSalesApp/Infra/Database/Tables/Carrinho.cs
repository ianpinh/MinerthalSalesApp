using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Carrinho")]
    public class Carrinho
    {
        [PrimaryKey,AutoIncrement, Column("Id")]
        public int Id { get; set; }


        [Column("PedidoId")]
        public Guid PedidoId { get; set; }

        [Column("ProdutoId")]
        public int ProdutoId { get; set; }

        [Column("CodProduto")]
        public string CodProduto { get; set; }

        [Column("CodigoNomeProduto")]
        public string CodigoNomeProduto { get; set; }

        [Column("Quantidade")]
        public int Quantidade { get; set; }

        [Column("ValorProduto")]
        public decimal ValorProduto { get; set; }

        [Column("ValorCombinado")]
        public decimal ValorCombinado { get; set; }

        [Column("Frete")]
        public decimal Frete { get; set; }

        [Column("Comissao")]
        public decimal Comissao{ get; set; }

        [Column("Desconto")]
        public decimal Desconto { get; set; }

        [Column("ImagemProduto")]
        public string ImagemProduto { get; set; }

        [Column("Encargos")]
        public decimal Encargos { get; set; }

        [Column("TaxaEncargos")]
        public decimal TaxaEncargos { get; set; }
    }
}
