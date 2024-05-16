using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Controls;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Orders;
using MinerthalSalesApp.Views.Pesquisa;
using MinerthalSalesApp.Views.Products;
using MinerthalSalesApp.Views.Ranking;
using MinerthalSalesApp.Views.Startup;

namespace MinerthalSalesApp.ViewModels.Dashboard
{
    public partial class DashboardPageViewModel : BaseViewModel
    {
        private readonly IPopupAppService _popupAppService;
        private readonly IAlertService _alertService;
        private bool _isBusy = false;
        public DashboardPageViewModel(IPopupAppService popupAppService, IAlertService alertService)
        {
            _popupAppService=popupAppService?? throw new ArgumentNullException(nameof(popupAppService));
            _alertService=alertService?? throw new ArgumentNullException(nameof(alertService));
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
        }


        [RelayCommand]
        async Task CarregarTelaDeRanking()
        {
            var _model = new PopupViewModel { PopupMessage = "Carregando ranking..." };
            await Task.Delay(15);
            var pop = new PopupPage(_model);
            try
            {
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);
                await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Home", $"Erro: {ex.Message}", "OK");
            }
            finally
            {
                await Task.Delay(2000);
                _popupAppService.ClosePopup(pop);
            }
        }


        [RelayCommand]
        async Task CarregarTelaDeConfiguracao()
        {
            if (!_isBusy)
            {
                _isBusy=true;

                var _model = new PopupViewModel { PopupMessage = "Carregando configurações..." };
                var pop = new PopupPage(_model);
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);

                try
                {
                    await Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
                }
                catch (Exception ex)
                {
                    await _alertService.ShowAlertAsync("Home", $"Erro: {ex.Message}", "OK");
                }
                finally
                {
                    _popupAppService.ClosePopup(pop);
                    _isBusy=false;
                }
            }
        }

        [RelayCommand]
        async Task CarregarTelaDeClientes()
        {

            var _model = new PopupViewModel { PopupMessage = "Carregando clientes..." };
            var pop = new PopupPage(_model);
            try
            {
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);
                await Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Home", $"Erro: {ex.Message}", "OK");
            }
            finally
            {
                await Task.Delay(10);
                _popupAppService.ClosePopup(pop);
            }
        }

        [RelayCommand]
        async Task CarregarTelaDeProdutos()
        {
            var _model = new PopupViewModel { PopupMessage = "Carregando  produtos..." };
            var pop = new PopupPage(_model);
            try
            {
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);
                await Shell.Current.GoToAsync($"//{nameof(ProdutosPage)}");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Home", $"Erro: {ex.Message}", "OK");
            }
            finally
            {
                await Task.Delay(2000);
                _popupAppService.ClosePopup(pop);
            }
        }

        [RelayCommand]
        async Task CarregarTelaDePedidos()
        {
            var _model = new PopupViewModel { PopupMessage = "Carregando pedidos..." };
            var pop = new PopupPage(_model);
            try
            {
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);
                await Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Home", ex.Message, "OK");
            }
            finally
            {
                await Task.Delay(2000);
                _popupAppService.ClosePopup(pop);
            }
        }

        [RelayCommand]
        async Task CarregarTelaDePesquisa()
        {
            var _model = new PopupViewModel { PopupMessage = "Carregando tela de pesquisa..." };
            var pop = new PopupPage(_model);
            try
            {
                _popupAppService.ShowPopup(pop);
                await Task.Delay(15);
                await Shell.Current.GoToAsync($"//{nameof(PesquisaPage)}");
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Home", ex.Message, "OK");
            }
            finally
            {
                await Task.Delay(2000);
                _popupAppService.ClosePopup(pop);
            }
        }
    }
}

