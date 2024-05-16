using MinerthalSalesApp.Customs.Exceptions;

namespace MinerthalSalesApp.Infra.Services
{
    public static class ServicoDeRede
    {
        /// <summary>
        /// Verifica se existe conexão com a internet, lançando uma excessão caso não haja.
        /// </summary>
        /// <returns></returns>
        public static bool IsInternectConnected()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                CustomExceptions.LancarExcecaoQuando(accessType != NetworkAccess.Internet, "");

                return accessType == NetworkAccess.Internet;
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
