using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using MinerthalSalesApp.Controls;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.ViewModels;
using MinerthalSalesApp.ViewModels.Clients;
using MinerthalSalesApp.ViewModels.DadosEquipe;
using MinerthalSalesApp.ViewModels.Dashboard;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Pesquisa;
using MinerthalSalesApp.ViewModels.Products;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.ViewModels.Startup;
using MinerthalSalesApp.ViewModels.Usuarios;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Configuration;
using MinerthalSalesApp.Views.DadosEquipe;
using MinerthalSalesApp.Views.Dashboard;
using MinerthalSalesApp.Views.Orders;
using MinerthalSalesApp.Views.Pesquisa;
using MinerthalSalesApp.Views.Products;
using MinerthalSalesApp.Views.Ranking;
using MinerthalSalesApp.Views.Shared;
using MinerthalSalesApp.Views.Startup;

namespace MinerthalSalesApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureEssentials(essencials =>
            {
                essencials.UseVersionTracking();
            })
            .ConfigureLifecycleEvents(events =>
            {
#if ANDROID
                events.AddAndroid(android => android
                           .OnActivityResult((activity, requestCode, resultCode, data) => LogEvent(nameof(AndroidLifecycle.OnActivityResult), requestCode.ToString()))
                           .OnStart((activity) => LogEvent(nameof(AndroidLifecycle.OnStart)))
                           .OnCreate((activity, bundle) => LogEvent(nameof(AndroidLifecycle.OnCreate)))
                           .OnBackPressed((activity) => LogEvent(nameof(AndroidLifecycle.OnBackPressed)) && false)
                           .OnStop((activity) => LogEvent(nameof(AndroidLifecycle.OnStop))));
#endif

#if IOS
                    events.AddiOS(ios => ios
                        .OnActivated((app) => LogEvent(nameof(iOSLifecycle.OnActivated)))
                        .OnResignActivation((app) => LogEvent(nameof(iOSLifecycle.OnResignActivation)))
                        .DidEnterBackground((app) => LogEvent(nameof(iOSLifecycle.DidEnterBackground)))
                        .WillTerminate((app) => LogEvent(nameof(iOSLifecycle.WillTerminate))));
#endif

#if WINDOWS
                   events.AddWindows(windows => windows
                       .OnActivated((window, args) => LogEvent(nameof(WindowsLifecycle.OnActivated)))
                       .OnClosed((window, args) => LogEvent(nameof(WindowsLifecycle.OnClosed)))
                       .OnLaunched((window, args) => LogEvent(nameof(WindowsLifecycle.OnLaunched)))
                       .OnLaunching((window, args) => LogEvent(nameof(WindowsLifecycle.OnLaunching)))
                       .OnVisibilityChanged((window, args) => LogEvent(nameof(WindowsLifecycle.OnVisibilityChanged)))
                       .OnPlatformMessage((window, args) =>
                       {
                           if (args.MessageId == Convert.ToUInt32("031A", 16))
                           {
                               // System theme has changed
                           }
                       }));
#endif

                static bool LogEvent(string eventName, string type = null)
                {
                    System.Diagnostics.Debug.WriteLine($"Lifecycle event: {eventName}{(type == null ? string.Empty : $" ({type})")}");
                    return true;
                }
            });

        //Views
        builder.Services.AddTransient<IPopupAppService, PopupAppService>();
        builder.Services.AddSingleton<IAlertService, AlertService>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<DashboardPage>();
        builder.Services.AddSingleton<AdminDashboardPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<RankingPage>();
        builder.Services.AddSingleton<SharedLoadingPage>();
        builder.Services.AddSingleton<ClientsPage>();
        builder.Services.AddSingleton<ClientsPageDetail>();
        builder.Services.AddSingleton<ProdutosPage>();
        builder.Services.AddSingleton<ConfigurationPage>();
        builder.Services.AddSingleton<PedidoPage>();
        builder.Services.AddSingleton<CarrinhoPage>();
        builder.Services.AddSingleton<GeraisPage>();
        builder.Services.AddSingleton<MeusPedidosPage>();
        builder.Services.AddSingleton<DetalhePedidoPage>();
        builder.Services.AddSingleton<PopupPage>();
        builder.Services.AddSingleton<AtualizacaoPage>();
        builder.Services.AddSingleton<ResumoPedidoDetalhePage>();
        builder.Services.AddSingleton<HistoricoPage>();
        builder.Services.AddSingleton<ClienteTituloPage>();
        builder.Services.AddSingleton<PesquisaPage>();
        builder.Services.AddSingleton<DadosEquipePage>();
        builder.Services.AddSingleton<DetalheVendedorPage>();
        builder.Services.AddSingleton<FlyoutHeaderControl>();

        //View Models
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<DashboardPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<RankingViewModel>();
        builder.Services.AddSingleton<SharedLoadingViewModel>();
        builder.Services.AddSingleton<ClientViewModel>();
        builder.Services.AddSingleton<ClientsPageDetailViewModel>();
        builder.Services.AddSingleton<ProdutosPageViewModel>();
        builder.Services.AddSingleton<ConfigurationViewModel>();
        builder.Services.AddSingleton<PedidoViewModel>();
        builder.Services.AddSingleton<ProdutosPedidoPage>();
        builder.Services.AddSingleton<MeusPedidosViewModel>();
        builder.Services.AddSingleton<PopupViewModel>();
        builder.Services.AddSingleton<AtualizacaoViewModel>();
        builder.Services.AddSingleton<HistoricoPageViewModel>();
        builder.Services.AddSingleton<ClienteTituloViewModel>();
        builder.Services.AddSingleton<PesquisaViewModel>();
        builder.Services.AddSingleton<DadosEquipeViewModel>();
        builder.Services.AddSingleton<FlyoutHeaderControlViewModel>();


        //Database
        builder.Services.AddSingleton<IMinerthalApiServices, MinerthalApiServices>();
        builder.Services.AddSingleton<IServicoDeCarregamentoDasBases, ServicoDeCarregamentoDasBases>();

        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<ILogRepository, LogRepository>();
        builder.Services.AddSingleton<IAtualizacaoRepository, AtualizacaoRepository>();
        builder.Services.AddSingleton<IMetaMensalRepository, MetaMensalRepository>();
        builder.Services.AddSingleton<IRankingRepository, RankingRepository>();
        builder.Services.AddSingleton<IClienteRepository, ClienteRepository>();

        builder.Services.AddSingleton<IFaturamentoRepository, FaturamentoRepository>();
        builder.Services.AddSingleton<IProdutosRepository, ProdutosRepository>();
        builder.Services.AddSingleton<IPedidoRepository, PedidoRepository>();
        builder.Services.AddSingleton<ICartRepository, CartRepository>();
        builder.Services.AddSingleton<IFilialRepository, FilialRepository>();
        builder.Services.AddSingleton<IMeusPedidosRepository, MeusPedidosRepository>();
        builder.Services.AddSingleton<ITabelaPrecoRepository, TabelaPrecoRepository>();
        builder.Services.AddSingleton<IVendedorRepository, VendedorRepository>();
        builder.Services.AddSingleton<IPlanosRepository, PlanosRepository>();
        builder.Services.AddSingleton<IVisitasRepository, VisitasRepository>();
        builder.Services.AddSingleton<IBancoRepository, BancoRepository>();
        builder.Services.AddSingleton<IClientePlanoPagamentoRepository, ClientePlanoPagamentoRepository>();
        builder.Services.AddSingleton<IHistoricoPedidoReposity, HistoricoPedidoReposity>();
        builder.Services.AddSingleton<IResumoPedidoRepository, ResumoPedidoRepository>();
        builder.Services.AddSingleton<ITitulosRepositoy, TitulosRepositoy>();




        string databaseName = "minerthal.db3";
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);
        builder.Services.AddSingleton<DatabaseContext>();
        builder.Services.AddSingleton<IAppthalContext>(new AppthalContext(dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<UserRepository>());
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<AtualizacaoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<LogRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ClienteRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<RankingRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<FaturamentoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProdutosRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<PedidoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CartRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<FilialRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<MeusPedidosRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TabelaPrecoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<VendedorRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<PlanosRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<BancoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ClientePlanoPagamentoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<HistoricoPedidoReposity>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ResumoPedidoRepository>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TitulosRepositoy>(s, dbPath));
        //builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<VisitasRepository>(s, dbPath));

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}