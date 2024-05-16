using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Filial")]
    public class Filial
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_FILIAL")]
        public string CdFilial { get; set; } = string.Empty;

        [JsonProperty("NM_FILIAL")]
        public string NmFilial { get; set; } = string.Empty;

        [JsonProperty("NR_REGIAO")]
        public string NrRegiao { get; set; } = string.Empty;

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;
    }
}
