namespace MinerthalSalesApp.Infra.Services
{
    public interface IMinerthalApiServices
    {

        string ApiRequestServiceAsync(string queryId, bool filter);
        //Task<string> ApiRequestServiceAsync(string queryId, bool filter);
        string RecuperarUsuarioAsync();
        string RecuperarRankingAsync();

        Task<(string sucesso, string Mensagem)> TransmitirPedidos();
        Task<(string sucesso, string Mensagem)> TransmitirPedidos(Guid pedidoId);

        //string ApiRequestService(string queryId, bool filter);
        //string RecuperarUsuario();
        //string RecuperarRanking();


        //Task<UsuarioDto> RecuperarDadosUsuariosAsync();
        //Task<RankingSalers> RecuperarRankingUsuariosAsync();

        //Task<FaturamentosModel> RecuperarRecuperarFaturamentoAsync();
        ////Task<ProdutoModel> RecuperarProdutosAsync();
        //Task<FiliaisMinerthalModel> RecuperarFiliaisMinherthal();



        //UsuarioDto RecuperarDadosUsuarios();
        //RankingSalers RecuperarRankingUsuarios();
        //Task<FaturamentosModel> RecuperarRecuperarFaturamento();
        ////ProdutoModel RecuperarProdutos();
        //MeusPedidosDto RecuperarMeusPedidos();
        //TabelaPrecoDto RecuperarTabelaDePrecosDto();
        //VendedoresDto RecuperarVendedores();
        //PlanosDto RecuperarPlanos();
        //Task<List<Banco>> RecuperarBancos();
    }
}




