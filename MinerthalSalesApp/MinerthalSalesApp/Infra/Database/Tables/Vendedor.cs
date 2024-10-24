using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Vendedor")]
    public class Vendedor
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_RCA")]
        public string CdRca { get; set; } = string.Empty;

        [JsonProperty("NM_RCA")]
        public string NmRca { get; set; } = string.Empty;

        [JsonProperty("NM_CIDADE")]
        public string NmCidade { get; set; } = string.Empty;

        [JsonProperty("CD_UF")]
        public string CdUf { get; set; } = string.Empty;

        [JsonProperty("NR_CPF")]
        public string NrCpf { get; set; } = string.Empty;

        [JsonProperty("NR_FONE")]
        public string NrFone { get; set; } = string.Empty;

        [JsonProperty("NR_CELULAR")]
        public string NrCelular { get; set; } = string.Empty;

        [JsonProperty("NM_EMAIL")]
        public string NmEmail { get; set; } = string.Empty;

        [JsonProperty("TABPRECO")]
        public string TabPreco { get; set; } = string.Empty;

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;

        [JsonIgnore]
        public string DadosJson => JsonConvert.SerializeObject(this);
    }
}