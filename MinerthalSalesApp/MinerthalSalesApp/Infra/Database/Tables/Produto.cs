using Newtonsoft.Json;
using SQLite;
using System.Globalization;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Produto")]
    public class Produto
    {
        [PrimaryKey,AutoIncrement, Column("Id")]
        public int Id { get; set; }


        [JsonProperty("CD_PRODUTO")]
        public string CdProduto { get; set; } = string.Empty;

        [JsonProperty("CD_PRAUX")]
        public string CdPraux { get; set; } = string.Empty;

        [JsonProperty("DS_EMABALA")]
        public string DsEmabala { get; set; } = string.Empty;

        [JsonProperty("VL_PESO")]
        public decimal VlPeso { get; set; }

        [JsonProperty("DS_PRODUTO")]
        public string DsProduto { get; set; } = string.Empty;

        [JsonProperty("NM_UNIDADE")]
        public string NmUnidade { get; set; } = string.Empty;

        [JsonProperty("NM_EMBALAG")]
        public string NmEmbalag { get; set; } = string.Empty;

        [JsonProperty("QT_UNIDEMB")]
        public string QtUnidemb { get; set; } = string.Empty;

        [JsonProperty("VL_PRECTAB")]
        public decimal VlPrectab { get; set; } = 0M;

        [JsonProperty("VL_PERCOM")]
        public decimal VlPercom { get; set; } = 0M;

        [JsonProperty("IN_BLOQVEN")]
        public string InBloqven { get; set; } = string.Empty;

        [JsonProperty("QT_EMBMAST")]
        public string QtEmbmast { get; set; } = string.Empty;

        [JsonProperty("TX_OBS")]
        public string Txobs { get; set; } = string.Empty;

        [JsonProperty("QT_ESTOQUE")]
        public string QtEstoque { get; set; } = string.Empty;

        [JsonProperty("CD_DEPTO")]
        public string CdDepto { get; set; } = string.Empty;

        [JsonProperty("CD_SECAO")]
        public string CdSecao { get; set; } = string.Empty;

        [JsonProperty("CD_FORNEC")]
        public string CdFornec { get; set; } = string.Empty;

        [JsonProperty("CD_BARRA")]
        public string CdBarra { get; set; } = string.Empty;

        [JsonProperty("NR_COR")]
        public string NrCor { get; set; } = string.Empty;

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

        [JsonProperty("DT_ULTALT")]
        public string DtUltalt { get; set; } = string.Empty;

        [JsonProperty("DT_ULTENTR")]
        public string DtULTentr { get; set; } = string.Empty;

        [JsonProperty("CD_FABRIC")]
        public string CdFabric { get; set; } = string.Empty;

        [JsonProperty("TX_PERCOM")]
        public decimal TxPercom { get; set; } = 0M;

        [JsonProperty("CD_CATEGORIA")]
        public string CdCategoria { get; set; } = string.Empty;

        [JsonProperty("CD_SUBCATEGORIA")]
        public string CdSubcategoria { get; set; } = string.Empty;

        [JsonProperty("CD_RCAXXX")]
        public int CdRcaxxX { get; set; }

        public string ImagemProduto { get; set; } = "product_icon.svg";

        [Ignore]
        public string CodigoProduto => $"{CdProduto} - {DsProduto}";
        [Ignore]
        public string ProdutoPesp => $"{NmEmbalag}  {VlPeso} KG";
        [Ignore]
        public string PrecTab => VlPrectab.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));
        [Ignore]
        public string PerCom => VlPercom.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));
        [Ignore]
        public decimal ValorCombinado { get; set; }
        [Ignore]
        public int Quantidade { get; set; }
        [Ignore]
        public decimal Frete { get; set; }

    }
}
