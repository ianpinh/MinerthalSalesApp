using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Visita")]
    public class Visita
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_CLIENTE")]
        public string CdCliente { get; set; } = string.Empty;

        [JsonProperty("DT_REG")]
        public string DtReg { get; set; } = string.Empty;

        [JsonProperty("IS_CLIENTE_NOVO")]
        public string IsClienteNovo { get; set; } = string.Empty;

        [JsonProperty("PROXIMA_VISITA")]
        public string ProximaVisita { get; set; } = string.Empty;

        [JsonProperty("NM_CLIENTE")]
        public string NmCliente { get; set; } = string.Empty;

        [JsonProperty("OCORRENCIA")]
        public string Ocorrencia { get; set; } = string.Empty;

        [JsonProperty("CIDADE")]
        public string Cidade { get; set; } = string.Empty;

        [JsonProperty("UF")]
        public string Uf { get; set; } = string.Empty;

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;

        [Ignore]
        public DateTime? DataReg => !string.IsNullOrWhiteSpace(DtReg) ? Convert.ToDateTime(DtReg) : default;

        public string DataVisitaFormatada => RecuperarData();

        private string RecuperarData()
        {
            if (!string.IsNullOrWhiteSpace(DtReg))
            {
                try
                {
                    var dateArray = DtReg.Split('/');
                    return $"{dateArray[2]}/{dateArray[1]}/{dateArray[0]}";
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }
    }
}


