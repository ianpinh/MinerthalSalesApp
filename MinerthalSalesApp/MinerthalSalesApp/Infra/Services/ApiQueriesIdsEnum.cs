using System.ComponentModel;

namespace MinerthalSalesApp.Infra.Services
{
    public enum ApiQueriesIdsEnum : byte
    {
        [Description("CLIENTE")]
        Cliente = 1,

        [Description("FVTABELAPRECO")]
        FvTabelaPreco = 2,

        [Description("FVPLANO(Plano Pagamento)")]
        FvPlano = 3,

        [Description("FVTITULOS(Titulos em aberto)")]
        FvTitulos = 4,

        [Description("HIST_ITEM(Compras históricas por item)")]
        HistItem =5,

        [Description("PRODUTOS")]
        Produtos = 6,

        [Description("HISTORICO DE PEDIDOS")]
        HistoricoDePedidos = 7,
     
        [Description("PE_RCA(REPRESENTANTES / VENDEDORES)")]
        PeRca = 8,

        [Description("REGIAO_RCA")]
        RegiaoRca = 9,

        [Description("CLIENTE_PLANO_PAG")]
        ClientePlanoPag = 10,

        [Description("MENSAGEM_RCA")]
        MemsagemRca = 11,

        [Description("CIDADES")]
        Cidades = 12,

        [Description("FILIAIS_MINERTHAL")]
        FiliaisMinerthal = 13,

        [Description("VISITAS")]
        Visitas = 14,

        [Description("META ANUAL")]
        MetaAnual = 15,

        [Description("VENDAS TOTAL ANO")]
        VendasTotalAno = 16,

        [Description("META MENSAL")]
        MetaMensal = 17,

        [Description("META TRIMESTRAL")]
        MetaTrimestral = 18,

        [Description("FATURAMENTO MES ATUAL")]
        FaturamentoMesAtual = 19,

        [Description("VENDAS POR TRIMESTRE")]
        VendasPorTrimestre = 20,

        [Description("BANCOS")]
        Bancos= 21
    }
}
