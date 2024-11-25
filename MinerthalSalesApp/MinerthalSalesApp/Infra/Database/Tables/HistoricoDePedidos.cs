using Newtonsoft.Json;
using SQLite;
using System.Globalization;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("HistoricoDePedidos")]
    public class HistoricoDePedidos
    {
        CultureInfo _culture = new CultureInfo("pt-BR");

        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_CLIENTE")]
        public string CdCliente { get; set; }

        [JsonProperty("NR_PEDIDO")]
        public string NrPedido { get; set; }


        private string dtPedido;
        [JsonProperty("DT_PEDIDO")]
        public string DtPedido
        {
            get => dtPedido;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    dtPedido = value;
                    DataPedido = RecuperarDataPedido();
                }
            }
        }

        [JsonProperty("TP_PEDIDO")]
        public string TpPedido { get; set; }

        [JsonProperty("NR_NOTA")]
        public string NrNota { get; set; }

        private string dtEmissao;
        [JsonProperty("DT_EMISSAO")]
        public string DtEmissao
        {
            get => dtEmissao;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    dtEmissao = value;
                    Emissao = RecuperarDataEmissao();
                }
            }
        }
        [JsonProperty("VL_FINAL")]
        public decimal VlFinal { get; set; }

        [JsonProperty("VL_TOTAL")]
        public decimal VlTotal { get; set; }

        [JsonProperty("TX_OBS1")]
        public string TxObs1 { get; set; }

        [JsonProperty("X5_CHAVE")]
        public string X5Chave { get; set; }

        [JsonProperty("NR_CARREG")]
        public string NrCarreg { get; set; }

        [JsonProperty("CD_TIPOCOB")]
        public string CdTipocob { get; set; }

        [JsonProperty("CD_PLANO")]
        public string CdPlano { get; set; }

        [JsonProperty("DS_PLANO")]
        public string DsPlano { get; set; }

        [JsonProperty("PEDIDOMAXIMA")]
        public string PedidoMaxima { get; set; }

        [JsonProperty("ZY_MINUM")]
        public string ZyMinum { get; set; }

        [JsonProperty("DTSAIDAMERC")]
        public string DtSaidaMerc { get; set; }

        [JsonProperty("TRANSPORTADOR")]
        public string Transportador { get; set; }

        [JsonProperty("CREDITO")]
        public string Credito { get; set; }

        [JsonProperty("ORDEM")]
        public string Ordem { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; }

        [Ignore]
        public DateTime? Emissao { get; set; }

        [Ignore]
        public DateTime? DataPedido { get; set; }

        [Ignore]
        public string NomeCliente { get; set; }

        [Ignore]
        public string NomeTipoCobranca { get; set; }

        [Ignore]
        public string Loja { get; set; }

        [Ignore]
        public string ClienteCodigo { get; set; }

        [Ignore]
        public string VlTotalExtenso => VlTotal.ToString("c", _culture);

        [Ignore]
        public string VlFinalExtenso => VlFinal.ToString("c", _culture);

        private DateTime? RecuperarDataEmissao()
        {
            if (DateTime.TryParseExact(DtEmissao, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                return data;

            return null;
        }

        private DateTime? RecuperarDataPedido()
        {
            if (DateTime.TryParseExact(DtPedido, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime data))
                return data;

            return null;
        }

        [Ignore]
        public List<ResumoPedido> ResumoDoPedido { get; set; } = new List<ResumoPedido>();
        public string Municipio { get; set; }
    }
}