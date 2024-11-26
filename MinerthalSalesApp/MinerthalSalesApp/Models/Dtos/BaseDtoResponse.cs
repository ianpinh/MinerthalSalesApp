using Newtonsoft.Json;
namespace MinerthalSalesApp.Models.Dtos
{
    public abstract class BaseDtoResponse
    {
        [JsonProperty("lHasError")]
        public bool LHasError { get; set; }
        public string Operator { get; set; }
    }
}
