using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels.Orders;

namespace MinerthalSalesApp.Views.Orders;

public partial class MeusPedidosPage : ContentPage
{
    private MeusPedidosViewModel _viewModel;

    public MeusPedidosPage(MeusPedidosViewModel viewModel)
    {

        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
        ListarPedidosViewModel();


    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(120);
        loadingImage.IsAnimationPlaying = false;

        await Task.Delay(120);
        loadingImage.IsAnimationPlaying = true;


        ListarPedidosViewModel();
        /*Loaded +=(s, e) =>
        {
            SetInvisibelContent();
            MeusPedidosContent.IsVisible=true;
            //  FecharLoading();
        };*/
    }


    //private void FecharLoading()
    //{
    //    if (loadingImage.IsVisible==true)
    //        loadingImage.IsVisible=false;

    //    if (loadingImagePendentes.IsVisible==true)
    //        loadingImagePendentes.IsVisible = false;
    //}

    private void ListaMeusPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void FiltroMeusPedios_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void BtnExcluirPedidosPendentes_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnPedidosNaoEnviados_Clicked(object sender, EventArgs e)
    {
        SetInvisibelContent();
        _viewModel._ListarPedidosPendentes();

        ListaPedidos.ItemsSource = _viewModel.PedidosPendentes;

        PedidosPendentesContent.IsVisible = true;
    }

    private void BtnPedidosEnviados_Clicked(object sender, EventArgs e)
    {
        SetInvisibelContent();
        MeusPedidosContent.IsVisible = true;
    }

    private async void BtnEnviarPedidosPendentes_Clicked(object sender, EventArgs e)
    {
        try
        {
            App.PedidoRepository.DeleteAll();
            var totalCarrinho = App.CartRepository.GetTotal();
            var totalPerdidos = App.PedidoRepository.GetTotal();
            await DisplayAlert("Pedidos", $"Total de pedidos {totalPerdidos}", "Ok");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Pedidos", ex.Message, "Ok");
        }
    }

    private void SetInvisibelContent()
    {
        MeusPedidosContent.IsVisible = false;
        PedidosPendentesContent.IsVisible = false;
    }

    private void ListaPedidos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        PedidosLocaisDto model = e.SelectedItem as PedidosLocaisDto;
        Navigation.PushAsync(new DetalhePedidoPage(model));

        //var popup = new PopupExcluirPedidoPage(model);
        //this.ShowPopup(popup);

    }

    private void ListarPedidosViewModel()
    {
        try
        {
            _viewModel.Initialize();
        }
        catch (Exception ex)
        {
            App.AlertService.ShowAlert($"Erro: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private void AtualizarListaDePedidosPendentes()
    {
        ListarPedidosViewModel();
    }

    [RelayCommand]
    private void AtualizarMeusPedidos()
    {

    }

    private void VisualizarPedido_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var numPedido = btn.CommandParameter.ToString();
        //var pedidoResumoDetalhe = new ResumoPedidoDetalhePage(numPedido);
        //this.ShowPopup(pop);

        //Shell.Current.GoToAsync($"//{nameof(pedidoResumoDetalhe)}");
        Navigation.PushAsync(new ResumoPedidoDetalhePage(numPedido));

    }

    private async void BtnSelectedPedido_Clicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var id = new Guid(btn.CommandParameter.ToString());
        if (_viewModel != null)
        {
            var model = _viewModel.PedidosPendentes.FirstOrDefault(x => x.Id.Equals(id));
            await Navigation.PushAsync(new DetalhePedidoPage(model));
            Navigation.RemovePage(this);
        }
    }

    private async void BtnReenviarPedidos_Clicked(object sender, EventArgs e)
    {
        var popup = new PopupPage(new ViewModels.Shared.PopupViewModel { PopupMessage = "Enviando pedidos..." });
        this.ShowPopup(popup);
        await Task.Delay(1000);
        try
        {
            var tramitido = await _viewModel.ReenviarPedidos();
            if (tramitido)
            {
                var pedidoRepository = App.PedidoRepository;
                App.PedidoRepository.DeleteAll();

                Navigation.RemovePage(this);
                await _viewModel.AtualizarPedidosTrasmitidos();
                await Navigation.PushAsync(new MeusPedidosPage(_viewModel));
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Pedidos", $"Não foi possível enviar os pedidos.  Erro:{ex.Message}", "Ok");
        }
        finally
        {
            await popup.CloseAsync();
        }
    }

    private async void RefreshView_Refreshing(object sender, EventArgs e)
    {
        _viewModel.IsRefreshing = true;
        await Task.Delay(20000);
        _viewModel.IsRefreshing = false;
    }
}