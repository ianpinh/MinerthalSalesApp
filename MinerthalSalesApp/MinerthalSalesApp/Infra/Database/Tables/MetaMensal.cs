using Newtonsoft.Json;
using SQLite;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    [Table("MetaMensal")]
    public class MetaMensal
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("CD_RCA")]
        public string CdRca { get; set; } = string.Empty;

        [JsonProperty("TIPO_META")]
        public string TipoMeta { get; set; } = string.Empty;

        [JsonProperty("VL_META_MES")]
        public short VlMetaMes { get; set; }

        [JsonProperty("MES")]
        public string Mes { get; set; } = string.Empty;

        [JsonProperty("ANO")]
        public short Ano { get; set; }

        [JsonProperty("CD_RCAXXX")]
        public string CdRcaxxx { get; set; } = string.Empty;

        public int NumeroMes => GetMesAno();
        public string DescricaoMes => GetMesDescricao();

        private int GetMesAno()
        {
            int.TryParse(Mes, out int mes);

            return mes;
        }

        private string GetMesDescricao()
        {
            int.TryParse(Mes, out int mes);

            return mes switch
            {
                1 => "Janeiro",
                2 => "Fevereiro",
                3 => "Março",
                4 => "Abril",
                5 => "Maio",
                6 => "Junho",
                7 => "Julho",
                8 => "Agosto",
                9 => "Setembro",
                10 => "Outubro",
                11 => "Novembro",
                12 => "Dezembro",
                _ => ""
            };
        }
    }
}
