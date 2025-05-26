using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Ranking;

namespace MinerthalSalesApp.Views.Orders;

public partial class DetalhePedidoPage : ContentPage
{
    private PedidosLocaisDto _pedido;
    public DetalhePedidoPage(PedidosLocaisDto pedido)
    {
        _pedido = pedido;
        BindingContext = _pedido;
        InitializeComponent();
    }
    private async void BtnReenviarPedido_Clicked(object sender, EventArgs e)
    {
        try
        {
            var _model = new PopupViewModel
            {
                PopupMessage = "enviando pedido..."
            };
            var pop = new PopupPage(_model);
            this.ShowPopup(pop);

            var btn = sender as Button;

            //_=int.TryParse(btn.CommandParameter.ToString(), out int pedidoId);

            var pedidoId = Guid.Parse(btn.CommandParameter.ToString());

            var _servicoDeCarregamentoDasBases = App.ServicoDeCarregamentoDasBases;
            var pedidoTransmisao = _servicoDeCarregamentoDasBases.TransmitirPedidos(pedidoId).GetAwaiter();

            pop.Close();



            var model = new MeusPedidosViewModel(App.AlertService, App.ServicoDeCarregamentoDasBases);
            pedidoTransmisao.OnCompleted(() =>
            {
                var resultado = pedidoTransmisao.GetResult();
                if (resultado.sucesso == "Sucesso")
                {
                    //App.CartRepository.DeleteByPedido(pedidoId);
                    App.PedidoRepository.DeleteById(pedidoId);

                    var stack = Shell.Current.Navigation.NavigationStack;
                    FecharPaginasAbertas();
                    _ = model.AtualizarPedidosTrasmitidos();
                    _ = DisplayAlert("Enviar Pedido", $"Status: {resultado.sucesso}.  Menssagem retorno: {resultado.Mensagem}", "Ok");
                }
                else
                {
                    _ = DisplayAlert("Enviar Pedido", $"Status: {resultado.sucesso}.  Menssagem retorno: {resultado.Mensagem}", "Ok");
                }
            });

            await Navigation.PushAsync(new MeusPedidosPage(model));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Enviar Pedido", $"Erro: {ex.Message}", "Ok");
        }
    }

    private async void BtnExcluirPedido_Clicked(object sender, EventArgs e)
    {
        try
        {
            var btn = (Button)sender;
            var order = btn.CommandParameter as OrderDto;
            App.PedidoRepository.DeleteById(order.Id);
            var model = new MeusPedidosViewModel(App.AlertService, App.ServicoDeCarregamentoDasBases);
            await Navigation.PushAsync(new MeusPedidosPage(model));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Enviar Pedido", $"Erro: {ex.Message}", "Ok");
        }
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var itensPedido = (ItensDto)e.CurrentSelection[0];
            App.CartRepository.DeleteById(itensPedido.Id);

            await Shell.Current.GoToAsync($"//{nameof(DetalhePedidoPage)}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Enviar Pedido", $"Erro: {ex.Message}", "Ok");
        }
    }

    private async void BtnExcluirItemPedido_Clicked(object sender, EventArgs e)
    {
        try
        {
            var _object = (Button)sender;
            _ = int.TryParse(_object.CommandParameter.ToString(), out int _id);

            if (_id > 0)
                App.CartRepository.DeleteById(_id);

            FecharPaginasAbertas();
            await Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Enviar Pedido", $"Erro: {ex.Message}", "Ok");
        }


    }

    private void FecharPaginasAbertas()
    {
        try
        {
            var openedPages = Shell.Current.Navigation.NavigationStack.ToList();
            foreach (var item in openedPages)
            {
                if (item != null)
                {
                    if (item.GetType().Equals(typeof(ClientsPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(ProdutosPedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(CarrinhoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(RankingPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(PedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(MeusPedidosPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(DetalhePedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);
                }
            }
        }
        catch (Exception)
        {
        }
    }
}


