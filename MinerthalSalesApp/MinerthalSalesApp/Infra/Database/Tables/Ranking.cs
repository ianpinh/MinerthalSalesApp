using Newtonsoft.Json;
using SQLite;
using System.Globalization;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("Ranking")]
    public class Ranking
    {

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
       
        [JsonProperty("COD")]
        public string Codigo { get; set; } = string.Empty;

        [JsonProperty("NOME_RC")]
        public string NomeRC { get; set; } = string.Empty;

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
        public decimal PosicaoRanking { get; set; }

        [Ignore]
        public int Rank { get; set; }

        [Ignore]
        public string PositionFormatada => PosicaoRanking.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));

        [Ignore]
        public string PProdFormatada => PontosProd.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));
        
        [Ignore]
        public string PVolumeFormatada => PontosVolume.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));

        [Ignore]
        public decimal PercentRanking { get; set; }

       
    }
}
