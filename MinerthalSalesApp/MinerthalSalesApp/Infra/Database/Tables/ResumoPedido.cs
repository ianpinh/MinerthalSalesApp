using Newtonsoft.Json;
using SQLite;
using System.Globalization;
namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("ResumoPedido")]
    public class ResumoPedido
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("NR_PEDIDO")]
        public string NrPedido { get; set; }

        [JsonProperty("CD_PRODUTO")]
        public string CdProduto { get; set; }

        [JsonProperty("DS_PRODUTO")]
        public string DsProduto { get; set; }

        [JsonProperty("QT_PRODUTO")]
        public int QtProduto { get; set; }

        [JsonProperty("QT_ATEND")]
        public int QtAtend { get; set; }

        [JsonProperty("VL_VENDA")]
        public decimal VlVenda { get; set; }

        [JsonProperty("NUM_LOTE")]

        public string NumLote { get; set; }
        [JsonProperty("VL_UNIT")]

        public decimal VlUnit { get; set; }
        [JsonProperty("VL_FRETE")]

        public decimal VlFrete { get; set; }

        [JsonProperty("CD_PERCCOMISS")]
        public decimal CdPercComiss { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; }

        public string ImagemProduto { get; set; } = "product_icon.svg";

        [Ignore]
        public string VlUnitExtenso => VlUnit.ToString("c", _culture);

        [Ignore]
        public string VlVendaExtenso => VlVenda.ToString("c", _culture);

        [Ignore]
        public string VlFreteExtenso => VlFrete.ToString("c", _culture);


    }
}
