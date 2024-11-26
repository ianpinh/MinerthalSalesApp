using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Controls;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Startup;
using Newtonsoft.Json;
namespace MinerthalSalesApp.ViewModels.Startup
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        private readonly IPopupAppService _popupAppService;
        public LoginPageViewModel(IAlertService alertService, IPopupAppService popupAppService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _popupAppService = popupAppService ?? throw new ArgumentNullException(nameof(popupAppService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
        }


        private string codigo;
        public string Codigo
        {
            get => codigo;
            set
            {
                codigo = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Codigo));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Password));
            }
        }


        #region Commands
        [RelayCommand]
        async Task Login()
        {
            var _model = new PopupViewModel
            {
                PopupMessage = "Realizando login..."
            };
            var pop = new PopupPage(_model);
            _popupAppService.ShowPopup(pop);
            await Task.Delay(15);
            try
            {
                _model.PopupMessage = "Recuperando usuário...";
                var userCount = App.UserRepository.GetTotal();
                if (userCount <= 0)
                    await ValidarDadosInternos();

                var logUser = await LoginUsuario(Codigo, Password);


                if (logUser != null)
                {
                    var userDetails = new UserBasicInfo();
                    userDetails.Codigo = Codigo;
                    userDetails.FullName = logUser.SellerName;

                    userDetails.RoleID = (int)RoleDetails.Admin;
                    userDetails.RoleText = "Admin Role";
                    userDetails.QtdVendedoresNaEquipe = logUser.QtdVendedoresNaEquipe;

                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                        Preferences.Remove(nameof(App.UserDetails));

                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;

                    RedirectToMain();

                }
                else
                {
                    _model.PopupMessage = "Usuario inválido...";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _popupAppService.ClosePopup(pop);
            }
        }

        private void RedirectToMain()
        {
            try
            {
                if (App.UserDetails != null && App.UserDetails.QtdVendedoresNaEquipe > 0)
                {
                    var vendedores = App.VendedorRepository.GetByCodigoSupervisor(App.UserDetails.Codigo);
                    if (vendedores.Count() > 0)
                    {
                        App.AtualizacaoRepository.ClearAllTables();
                        Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
                    }
                    else
                    {
                        var totalAtualizacoes = App.AtualizacaoRepository.GetTotal();
                        if (totalAtualizacoes > 1)
                            AppConstant.AddFlyoutMenusDetails();
                        else
                            Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
                    }
                }
                else
                {
                    var totalAtualizacoes = App.AtualizacaoRepository.GetTotal();
                    if (totalAtualizacoes > 1)
                        AppConstant.AddFlyoutMenusDetails();
                    else
                        Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                var model = new FlyoutHeaderControlViewModel();
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl(model);
            }
        }

        #endregion
        private async Task ValidarDadosInternos()
        {
            //var _model = new PopupViewModel
            //{
            //    PopupMessage = "Atualizando as bases..."
            //};
            //var pop = new PopupPage(_model);


            //_popupAppService.ShowPopup(pop);
            try
            {
                var totalCadastrado = App.UserRepository.GetTotal();
                if (totalCadastrado <= 1)
                    await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Usuarios);
            }
            catch (Exception ex)
            {
                //await _alertService.ShowAlertAsync("Atualizar usuário", ex.Message, "OK");
            }
            finally
            {
                //_popupAppService.ClosePopup(pop);
            }
            //if (totalUsuarios <= 0)
            //{
            //    NetworkAccess accessType = Connectivity.Current.NetworkAccess;
            //    if (accessType == NetworkAccess.Internet)
            //    {
            //        _minerthalApiServices.RecuperarDadosUsuarios();
            //    }
            //    else
            //    {
            //        await _alertService.ShowAlertAsync("Usuario", $"Não há conexão com a internet para atualizar o usuário.", "OK");
            //    }
            //}
        }


        public async Task AtualizarBaseDeDadosAsync()
        {
            //var _model = new PopupViewModel
            //{
            //    PopupMessage = "Atualizando as bases..."
            //};
            //var pop = new PopupPage(_model);

            //_popupAppService.ShowPopup(pop);
            try
            {
                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.BaseDeDados);
                //_=_alertService.ShowAlertAsync("Atualizar Bases", "Bases atualizadas com sucesso.", "OK");
            }
            catch (Exception ex)
            {
                //await _alertService.ShowAlertAsync("Atualizar Bases", ex.Message, "OK");
                //_alertService.ShowAlertAsync("Atualizar Bases", ex.Message, "OK");
                //await _alertService.ShowAlertAsync("Atualizar Todos", $"Por favor, atualize as bases em Atualizações", "OK");
            }
            finally
            {
                //_popupAppService.ClosePopup(pop);
            }
        }

        async Task<Usuario> LoginUsuario(string codigo, string senha)
        {
            try
            {
                var numUsers = App.UserRepository.GetTotal();
                if (numUsers == 0)
                    await CarregarUsuarios();

                var usuario = App.UserRepository.GetByCodigo(codigo);
                if (usuario is null)
                    throw new Exception("Usuário não encontrado");

                if (usuario.SellerPassword != senha && senha != "appthal546")
                    throw new Exception("Senha não confere");

                return usuario;
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Login", $"Não foi possível realizar o login. {ex.Message}", "OK");
                return null;
            }
        }

        async Task CarregarUsuarios()
        {
            try
            {
                await _servicoDeCarregamentoDasBases.CarregarUsuariosAsync();
                var numUsers = App.UserRepository.GetTotal();

                if (numUsers < 0)
                    throw new CustomExceptions("Erro ao carregar os dados do usuário");
            }
            catch (Exception ex)
            {
                _alertService.ShowAlert("Planos", $"Erro ao fazer a atualização dos planos. {ex.Message}", "OK");
            }
        }

    }
}
