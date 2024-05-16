namespace MinerthalSalesApp.Views.Orders;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;

public partial class ResumoPedidoDetalhePage : ContentPage, IAsyncInitialization
{
    public Task Initialization { get; private set; }
    public ResumoPedidoDetalhePage(string nrPedido)
    {

        InitializeComponent();
        Initialization = InitializeAsync(nrPedido);
    }

    private async Task InitializeAsync(string numeroPedido)
    {
        var resumo = await RecuperarResumoPedido(numeroPedido);
        LstViewResumoPedido.ItemsSource = resumo;
        NumPedido.Text = numeroPedido;
    }

    public async Task<List<ResumoPedido>> RecuperarResumoPedido(string numeroPedido)
    {
        try
        {
            var repositorio = App.ResumoPedidoRepository;
            var resumo = repositorio.GetByNumPedido(numeroPedido);
            return resumo;

        }
        catch (Exception ex)
        {
            return new List<ResumoPedido>();
        }
    }
}