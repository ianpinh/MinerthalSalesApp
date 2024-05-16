using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Titulo")]
    public class Titulo
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_RCA")]
        public string CdRca { get; set; } = string.Empty;

        [JsonProperty("QT_FAT_SC")]
        public decimal QtFatSc { get; set; } = 0M;

        [JsonProperty("QT_FAT_CABSUPL")]
        public decimal QtFatCabsupl { get; set; } = 0M;

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;
    }
}


