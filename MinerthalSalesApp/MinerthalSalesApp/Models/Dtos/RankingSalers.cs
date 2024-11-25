using Newtonsoft.Json;

namespace MinerthalSalesApp.Models.Dtos
{
    public class RankingSalers : BaseDtoResponse
    {
        public List<RankingDetails> Details { get; set; } = new List<RankingDetails>();
    }

    public class RankingDetails
    {
        [JsonProperty("COD")]
        public string Cod { get; set; }

        [JsonProperty("NOME_RC")]
        public string NomeRC { get; set; }

        [JsonProperty("PESO_TON")]
        public decimal PesoTon { get; set; }

        [JsonProperty("PONTOS_VOLUME")]
        public decimal PontosVolume { get; set; }

        [JsonProperty("PONTOS_PROD")]

        public decimal PontosProd { get; set; }

        [JsonProperty("PONTS_CLI_ATENDIDO")]
        public decimal PontsCliAtendido { get; set; }

        [JsonProperty("PONTOS_CLI_NOVO")]
        public decimal PontosCliNovo { get; set; }

        [JsonProperty("PONTOS_CLI_RECUPERADO")]
        public decimal PontosCliRecuperados { get; set; }

        [JsonProperty("RANKING")]
        public decimal Ranking { get; set; }
    }

}
