using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Banco")]
    public class Banco
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("CD_TIPOCOB")]
        public string CdTipoCob { get; set; } = string.Empty;

        [JsonProperty("DS_TIPOCOB")]
        public string DsTipocob { get; set; } = string.Empty;
        
        [JsonProperty("NV_COBRANC")]
        public string NvCobranc { get; set; } = string.Empty;
        
        [JsonProperty("IN_VENDAN")]
        public string InVendan { get; set; } = string.Empty;
        
        [JsonProperty("IN_UTILIZ")]
        public string InUtiliz { get; set; } = string.Empty;
        
        [JsonProperty("IN_UTPLANO")]
        public string InUtplano { get; set; } = string.Empty;
        
        [JsonProperty("TX_ACRESC")]
        public string TxAcresc { get; set; } = string.Empty;
        
        [JsonProperty("QT_PRZMAX")]
        public string QtPrzmax { get; set; } = string.Empty;
        
        [JsonProperty("IN_BOLETO")]
        public string InBoleto { get; set; } = string.Empty;
        
        [JsonProperty("VL_MINPED")]
        public string VlMinped { get; set; } = string.Empty;
        
        [JsonProperty("CD_RCAXXX")]
        public int CdRcaxxx { get; set; }
    }
}
