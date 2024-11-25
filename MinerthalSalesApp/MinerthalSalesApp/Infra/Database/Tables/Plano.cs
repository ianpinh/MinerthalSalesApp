using Newtonsoft.Json;

namespace MinerthalSalesApp.Infra.Database.Tables
{

    public class Plano
    {
        public int Id { get; set; }

        [JsonProperty("CD_PLANO")]
        public string CdPlano { get; set; }

        [JsonProperty("DS_PLANO")]
        public string DsPlano { get; set; }

        [JsonProperty("QT_DIAPRZ")]
        public string QtDiaPrz { get; set; }

        [JsonProperty("NR_COLPRE")]
        public string NrColPrec { get; set; }

        [JsonProperty("NV_PLANO")]
        public string NvPlano { get; set; }

        [JsonProperty("QT_MAXPAR")]
        public string QtMaxParc { get; set; }

        [JsonProperty("TX_PERFIN")]
        public decimal TxPerFin { get; set; }

        [JsonProperty("TP_PRAZO")]
        public string TpPrazo { get; set; }

        [JsonProperty("IN_ESPEC")]
        public string InEspec { get; set; }

        [JsonProperty("VL_DESCPL")]
        public decimal VlDescpl { get; set; }

        [JsonProperty("TX_OBS")]
        public string TxObs { get; set; }

        [JsonProperty("VL_MINPED")]
        public decimal VlMinped { get; set; }

        [JsonProperty("NR_ITMIN")]
        public string NrItmin { get; set; }

        [JsonProperty("TP_VENDA")]
        public string TpVenda { get; set; }

        [JsonProperty("TX_PERFINPROD")]
        public decimal TxPerfinProd { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public int CdRcaxxx { get; set; }
    }
}
