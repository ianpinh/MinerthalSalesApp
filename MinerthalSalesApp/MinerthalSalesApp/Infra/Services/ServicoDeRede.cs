using MinerthalSalesApp.Customs.Exceptions;
using System.Net.Mime;
using System.Net;

namespace MinerthalSalesApp.Infra.Services
{
    public static class ServicoDeRede
    {
        /// <summary>
        /// Verifica se existe conexão com a internet, lançando uma excessão caso não haja.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> IsInternectConnected()
        {
            try
            {
                var existeConexao = false;

                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                CustomExceptions.LancarExcecaoQuando(accessType != NetworkAccess.Internet, "");

                if (accessType == NetworkAccess.Internet)
                {
                    var urlWebService = "https://www.google.com/";
                    if (urlWebService.Contains("https://"))
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


                    using var client = new HttpClient();

                    try
                    {
                        var task = await client.GetAsync(urlWebService);
                        existeConexao = true;
                    }
                    catch (Exception ex)
                    {
                        var val = ex.Message;
                    }
                }



                return existeConexao;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Retorna se existe conexão
        /// </summary>
        /// <returns></returns>
        public static bool HaveInternectConnected()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                return accessType == NetworkAccess.Internet;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
