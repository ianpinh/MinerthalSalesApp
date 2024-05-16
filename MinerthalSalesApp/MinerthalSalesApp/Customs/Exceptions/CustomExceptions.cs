using MinerthalSalesApp.Models.Enums;

namespace MinerthalSalesApp.Customs.Exceptions
{
    public class CustomExceptions : Exception
    {
        public ApiMinertalTypes? ErroLancadoPor { get; private set; }
        public CustomExceptions(string mensagem, ApiMinertalTypes? erroLancadoPor = default ): base(mensagem)
        {
            ErroLancadoPor=erroLancadoPor;
        }

        public CustomExceptions(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
            if (innerException != null && innerException.InnerException != null)
                DetalheDoErro = innerException.InnerException.Message;
        }

        public static void LancarExcecaoQuando(bool regraInvalida, string mensagem)
        {
            if (regraInvalida)
                throw new CustomExceptions(mensagem);
        }

        public string DetalheDoErro { get; private set; } = string.Empty;
    }
}
