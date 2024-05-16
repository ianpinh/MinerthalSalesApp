using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("ClientePlanoPagamento")]
    public class ClientePlanoPagamento
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("CD_CLIENTE")]
        public string CdCliente { get; set; }
        [JsonProperty("LOJA")]
        public string Loja { get; set; }
        [JsonProperty("CD_PLPAG")]
        public string CdPlpag { get; set; }
        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; }
    }
}
