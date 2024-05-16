using Newtonsoft.Json;
using SQLite;
using System.Globalization;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Faturamento")]
    public class Faturamento
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_CLIENTE")]
        public string CdCliente { get; set; }

        [JsonProperty("NR_DOCUM")]
        public string NrDocum { get; set; }

        [JsonProperty("NR_PARCEL")]
        public string NrParcel { get; set; }

        [JsonProperty("DT_EMISSAO")]
        public string? DtEmissao { get; set; }

        [JsonProperty("DT_VENC")]
        public string? DtVenc { get; set; }

        [JsonProperty("VL_DOCUM")]
        public decimal VlDocum { get; set; } = 0M;

        [JsonProperty("TP_COBRAN")]
        public string TpCobran { get; set; }

        [JsonProperty("QT_DIAATR")]
        public int QtDiaatr { get; set; }

        [JsonProperty("VL_JURO")]
        public decimal VlJuro { get; set; } = 0M;

        [JsonProperty("CD_RCA")]
        public string CdRca { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; }

        [JsonProperty("JUROS")]
        public decimal Juros { get; set; } = 0M;

        [JsonProperty("DATA_EMISSAO")]
        public DateTime? DataEmissao => RecuperarDataEmissao();

        public DateTime? DataDeVencimento => RecuperarDataVencimento();

        public string NomeCliente { get; set; }=string.Empty;

        [Ignore]
        public Color StatusVencimento { get; set; }
        [Ignore]
        public string ValorDocumento => VlDocum.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));
        public string ValorJuros=> Juros.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));
        public string ValorTotal => (VlDocum+Juros).ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));

        private DateTime? RecuperarDataEmissao()
        {
            if (DateTime.TryParse(DtEmissao, CultureInfo.CreateSpecificCulture("pt-BR"),out DateTime data))
                return data;

            return null;
        }
        private DateTime? RecuperarDataVencimento()
        {
            if (DateTime.TryParse(DtVenc, CultureInfo.CreateSpecificCulture("pt-BR"), out DateTime data))
                return data;

            return null;
        }
    }
}
