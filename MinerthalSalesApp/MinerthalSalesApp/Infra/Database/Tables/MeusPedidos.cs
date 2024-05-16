using Newtonsoft.Json;
using SQLite;


namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("MeusPedidos")]

    public class MeusPedidos
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_PRODUTO")]
        public string CdProduto { get; set; } = string.Empty;

        [JsonProperty("CD_PRAUX")]
        public string CdPraux { get; set; } = string.Empty;

        [JsonProperty("DS_EMABALA")]
        public string DsEmabala { get; set; } = string.Empty;

        [JsonProperty("VL_PESO")]
        public int VlPeso { get; set; }

        [JsonProperty("DS_PRODUTO")]

        public string DsProduto { get; set; } = string.Empty;
        [JsonProperty("NM_UNIDADE")]

        public string NmUnidade { get; set; } = string.Empty;
        [JsonProperty("NM_EMBALAG")]

        public string NmEmbalag { get; set; } = string.Empty;
        [JsonProperty("QT_UNIDEMB")]

        public string QtUnidemb { get; set; } = string.Empty;

        [JsonProperty("VL_PRECTAB")]
        public decimal VlPrecTab { get; set; }

        [JsonProperty("VL_PERCOM")]
        public decimal VlPercom { get; set; }

        [JsonProperty("IN_BLOQVEN")]
        public string InBloqVen { get; set; } = string.Empty;

        [JsonProperty("QT_EMBMAST")]
        public string QtEmbMast { get; set; } = string.Empty;

        [JsonProperty("TX_OBS")]
        public string TxObs { get; set; } = string.Empty;

        [JsonProperty("QT_ESTOQUE")]
        public string QtEstoque { get; set; } = string.Empty;

        [JsonProperty("CD_DEPTO")]
        public string CdDepto { get; set; } = string.Empty;

        [JsonProperty("CD_SECAO")]
        public string CdSecao { get; set; } = string.Empty;

        [JsonProperty("CD_BARRA")]
        public string CdBarra { get; set; } = string.Empty;

        [JsonProperty("IN_FRACI")]
        public string InFraci { get; set; } = string.Empty;

        [JsonProperty("QT_MULTIP")]
        public string QtMultip { get; set; } = string.Empty;

        [JsonProperty("IN_MIX")]
        public string InMix { get; set; } = string.Empty;

        [JsonProperty("CD_FILIAL")]
        public string CdFilial { get; set; } = string.Empty;

        [JsonProperty("CD_PRINC")]
        public string CdPrinc { get; set; } = string.Empty;

        [JsonProperty("CD_FORNEC")]
        public string CdFornec { get; set; } = string.Empty;

        [JsonProperty("NR_COR")]
        public string NrCor { get; set; } = string.Empty;

        [JsonProperty("DT_ULTALT")]
        public string DtUltalt { get; set; } = string.Empty;

        [JsonProperty("DT_ULTENTR")]
        public string DtUltentr { get; set; } = string.Empty;

        [JsonProperty("CD_FABRIC")]
        public string CdFabric { get; set; } = string.Empty;

        [JsonProperty("TX_PERCOM")]
        public decimal TxPercom { get; set; }

        [JsonProperty("CD_CATEGORIA")]
        public string CdCategoria { get; set; } = string.Empty;

        [JsonProperty("CD_SUBCATEGORIA")]
        public string CdSubcategoria { get; set; } = string.Empty;

        [JsonProperty("CD_RCAXXX")]
        public int CdRcaxxx { get; set; }

    }
}
