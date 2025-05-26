using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.Views.Startup;
using System.Text;
using System.Timers;

namespace MinerthalSalesApp;

public partial class App : Application
{
    public static UserBasicInfo UserDetails;
    public static VendedorSelecionadoDto VendedorSelecionado;
    public static PedidoViewModel PedidoViewModel { get; private set; }
    public static IUserRepository UserRepository { get; private set; }
    public static IRankingRepository RankingRepository { get; private set; }
    public static IAtualizacaoRepository AtualizacaoRepository { get; private set; }
    public static IMetaMensalRepository MetaMensalRepository { get; private set; }
    public static ILogRepository LogRepository { get; private set; }
    public static IClienteRepository ClienteRepository { get; private set; }
    public static IFaturamentoRepository FaturamentoRepository { get; private set; }
    public static IProdutosRepository ProdutosRepository { get; private set; }
    public static ICartRepository CartRepository { get; private set; }
    public static IPedidoRepository PedidoRepository { get; private set; }
    public static IFilialRepository FilialRepository { get; private set; }
    public static IMeusPedidosRepository MeusPedidosRepository { get; private set; }
    public static ITabelaPrecoRepository TabelaPrecoRepository { get; private set; }
    public static IVendedorRepository VendedorRepository { get; private set; }
    public static IPlanosRepository PlanosRepository { get; private set; }
    public static IClientePlanoPagamentoRepository ClientePlanoPagamentoRepository { get; private set; }
    public static IBancoRepository BancoRepository { get; private set; }
    public static IHistoricoPedidoReposity HistoricoPedidoReposity { get; private set; }
    public static IResumoPedidoRepository ResumoPedidoRepository { get; private set; }
    public static IAlertService AlertService { get; private set; }
    public static IMinerthalApiServices MinerthalApiServices { get; private set; }
    public static IServicoDeCarregamentoDasBases ServicoDeCarregamentoDasBases { get; private set; }
    public static IPopupAppService PopupAppService { get; private set; }
    public static PedidoViewModel PedidoModel { get; internal set; }
    public static ITitulosRepositoy TitulosRepositoy { get; internal set; }
    public static IVisitasRepository VisitasRepository { get; internal set; }

    public App(IUserRepository userRepository, IRankingRepository rankingRepository, IAtualizacaoRepository atualizacaoRepository, IMetaMensalRepository metaMensalRepository, ILogRepository logRepository, IClienteRepository clienteRepository, IFaturamentoRepository faturamentoRepository,
    IProdutosRepository produtosRepository, ICartRepository cartRepository, IPedidoRepository pedidoRepository, IFilialRepository filialRepository, IMeusPedidosRepository meusPedidosRepository, ITabelaPrecoRepository tabelaPrecoRepository,
    IVendedorRepository vendedorRepository, IPlanosRepository planosRepository, IClientePlanoPagamentoRepository clientePlanoPagamentoRepository, IBancoRepository bancoRepository, IHistoricoPedidoReposity historicoPedidoReposity,
    IResumoPedidoRepository resumoPedidoRepository, IMinerthalApiServices minerthalApiServices, ITitulosRepositoy titulosRepositoy, IVisitasRepository visitasRepository,
    IPopupAppService popupAppService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases, IAlertService alertService)
    {
        try
        {
            InitializeComponent();

            //Border less entry
            //		Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
            //		{
            //			if (view is BorderlessEntry)
            //			{
            //#if __ANDROID__
            //				handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
            //#elif __IOS__
            //                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            //#endif
            //			}
            //		});
            MainPage = new AppShell();
            UserRepository = userRepository;
            RankingRepository = rankingRepository;
            AtualizacaoRepository = atualizacaoRepository;
            MetaMensalRepository = metaMensalRepository;
            LogRepository = logRepository;
            ClienteRepository = clienteRepository;
            FaturamentoRepository = faturamentoRepository;
            CartRepository = cartRepository;
            ProdutosRepository = produtosRepository;
            PedidoRepository = pedidoRepository;
            MinerthalApiServices = minerthalApiServices;
            AlertService = alertService;
            FilialRepository = filialRepository;
            MeusPedidosRepository = meusPedidosRepository;
            TabelaPrecoRepository = tabelaPrecoRepository;
            VendedorRepository = vendedorRepository;
            PlanosRepository = planosRepository;
            ClientePlanoPagamentoRepository = clientePlanoPagamentoRepository;
            BancoRepository = bancoRepository;
            HistoricoPedidoReposity = historicoPedidoReposity;
            ResumoPedidoRepository = resumoPedidoRepository;
            ServicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases;
            PopupAppService = popupAppService;
            TitulosRepositoy = titulosRepositoy;
            VisitasRepository = visitasRepository;
            BuildDatabase();


        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public static void AtualizarModel(PedidoViewModel pedidoViewModel)
    {
        PedidoViewModel = pedidoViewModel;
    }
    protected override void OnResume()
    {
        //if (DashboardPage.FakePageVisible)
        //{
        //    HelpPage.FakePageVisible = false;
        //    var shell = MainPage as AppShell;
        //    if (shell != null)
        //    {
        //        shell.Navigation.PopAsync();
        //    }
        //}
    }


    static DateTime lastHour = DateTime.Now;
    static int tempoAtualizacaoBanco = 5;
    async Task BuildDatabase()
    {
        try
        {
            var hasInternetConnection = await ServicoDeRede.IsInternectConnected();
            if (hasInternetConnection)
            {
                //DeleteAllTables();
                await ServicoDeCarregamentoDasBases.AtualizarBaseDeDados(Models.Enums.ApiMinertalTypes.Usuarios);
            }





            UpdateDatabase();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void UpdateDatabase()
    {
        if (!string.IsNullOrWhiteSpace(Preferences.Get(nameof(App.UserDetails), "")))
        {
            Thread t = new Thread(CarregarDadosApi);
            t.Start();
        }


        var aTimer = new System.Timers.Timer(tempoAtualizacaoBanco * 60 * 1000); //20 MINUTOS
        aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        aTimer.Start();
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {

        if (!Preferences.ContainsKey(nameof(App.UserDetails)))
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

        if (!string.IsNullOrWhiteSpace(Preferences.Get(nameof(App.UserDetails), "")))
        {
            Thread t = new Thread(CarregarDadosApi);
            t.Start();
        }

        TimeSpan difference = DateTime.Now - lastHour;
        if (difference.Minutes > tempoAtualizacaoBanco)
            lastHour = DateTime.Now.AddMinutes(tempoAtualizacaoBanco);

        //else if (lastHour < DateTime.Now.Hour || (lastHour == 23 && DateTime.Now.Hour == 0))
        //{
        //    lastHour = DateTime.Now.Hour;
        //    _= ServicoDeCarregamentoDasBases.AtualizarBaseDeDados();
        //}

    }

    private void CarregarDadosApi()
    {
        ServicoDeCarregamentoDasBases.AtualizarBaseDeDados().GetAwaiter().OnCompleted(() =>
        {
            ServicoDeCarregamentoDasBases.AtualizarBaseDeDadosVendedores();
        });
    }

    private void DeleteAllTables()
    {
        var sb = new StringBuilder();
        sb.AppendLine("DROP TABLE Atualizacoes;");
        sb.AppendLine("DROP TABLE HistoricoDePedidos;");
        sb.AppendLine("DROP TABLE ResumoPedido;");
        sb.AppendLine("DROP TABLE Banco;");
        sb.AppendLine("DROP TABLE Log;");
        sb.AppendLine("DROP TABLE TabelaPreco;");
        sb.AppendLine("DROP TABLE Carrinho;");
        sb.AppendLine("DROP TABLE MeusPedidos;");
        sb.AppendLine("DROP TABLE Titulo;");
        sb.AppendLine("DROP TABLE Cliente;");
        sb.AppendLine("DROP TABLE Pedido;");
        sb.AppendLine("DROP TABLE Usuario;");
        sb.AppendLine("DROP TABLE ClientePlanoPagamento;");
        sb.AppendLine("DROP TABLE Plano;");
        sb.AppendLine("DROP TABLE Vendedor;");
        sb.AppendLine("DROP TABLE Faturamento;");
        sb.AppendLine("DROP TABLE Produto;");
        sb.AppendLine("DROP TABLE Visita;");
        sb.AppendLine("DROP TABLE Filial;");
        sb.AppendLine("DROP TABLE Ranking;");
        ServicoDeCarregamentoDasBases.DeleteAllTables(sb.ToString());
    }
}