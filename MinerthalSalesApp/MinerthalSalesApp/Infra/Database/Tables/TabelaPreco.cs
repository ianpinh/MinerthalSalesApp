using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("TabelaPreco")]
    public class TabelaPreco
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_PRODUTO")]
        public string CdProduto { get; set; } = string.Empty;

        [JsonProperty("ID_TABPREC")]
        public string IdTabPrec { get; set; } = string.Empty;

        [JsonProperty("PER_MIN")]
        public decimal PerMin { get; set; }

        [JsonProperty("PER_MAX")]
        public decimal PerMax { get; set; }

        [JsonProperty("CD_FILIAL")]
        public string CdFilial { get; set; } = string.Empty;

        [JsonProperty("VL_VENDA")]
        public decimal VlVvenda { get; set; }

        [JsonProperty("PER_COMISSAO")]
        public decimal PerComissao { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;

        [JsonProperty("QTD_MIN")]
        public int QtdMin { get; set; }

        [JsonProperty("QTD_MAX")]
        public int QtdMax { get; set; }

        [JsonProperty("DT_VALINI")]
        public string DtValIni { get; set; } = string.Empty;

        [JsonProperty("DT_VALFIN")]
        public string DtValFin { get; set; } = string.Empty;
    }
}


