using CommunityToolkit.Mvvm.ComponentModel;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.Models.Servicos.Clientes;
using MinerthalSalesApp.Views.Startup;
using Newtonsoft.Json;

namespace MinerthalSalesApp.ViewModels.Startup
{
    public partial class LoadingPageViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        private readonly DatabaseContext _context;

        public LoadingPageViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases, DatabaseContext context)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            _context=context;
            CheckUserLoginDetails();
        }

        private string message;

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Message));
            }
        }

        private string messageRanking;

        public string MessageRanking
        {
            get => messageRanking;
            set
            {
                messageRanking = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageRanking));
            }
        }

        private string messageUsers;
        public string MessageUsers
        {
            get => messageUsers;
            set
            {
                messageUsers = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageUsers));
            }
        }

        private string messageInvoicing;

        public string MessageInvoicing
        {
            get => messageInvoicing;
            set
            {
                messageInvoicing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageInvoicing));
            }
        }

        private string messageProdutos;

        public string MessageProdutos
        {
            get => messageProdutos;
            set
            {
                messageProdutos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageProdutos));
            }
        }

        private async void CheckUserLoginDetails()
        {
            Message = "Iniciando atualização do sistema...";
            await CarregarUsuariosAsync();
            Message = string.Empty;

            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");


            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(() =>
                    {
                        Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                App.UserDetails = userInfo;
                AppConstant.AddFlyoutMenusDetails();
            }
        }

        private async Task CarregarProdutosAsync()
        {
            try
            {
                MessageUsers = "Verificando produtos...";
                var totalProdutos = App.ProdutosRepository.GetTotal();
                if (totalProdutos == 0)
                {
                    MessageUsers = "Sincronizando produtos...";
                    await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Produtos);
                    var produtos = App.ProdutosRepository.GetAll();

                    CustomExceptions.LancarExcecaoQuando(!produtos.Any(), "Não foi possivel carregar os produtos da API.");

                    if (produtos != null && produtos.Any())
                        App.ProdutosRepository.SaveProduto(produtos.ToList());
                    MessageUsers = "Sincronizando produtos... Concluído";

                    SalvarLog("Usuario");
                }
                else
                {
                    MessageUsers = "Verificação concluída...";
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Login", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Login", $"Erro ao fazer o upload da listagem de produtos. {ex.Message}", "OK");
            }
        }

        private int TotalUsuarios()
        {
            return App.UserRepository.GetTotal();

        }

        private async Task CarregarUsuariosAsync()
        {
            try
            {
                MessageUsers = "Verificando usuários...";
                var totalUsers = TotalUsuarios();
                if (totalUsers == 0)
                {
                    MessageUsers = "Sincronizando usuários...";
                    await _servicoDeCarregamentoDasBases.CarregarUsuariosAsync();
                    //var users = await App.UserRepository.GetTotalAsync();
                    MessageUsers = "Sincronizando usuários... Concluído";
                    SalvarLog("Usuario");
                }
                else
                {
                    MessageUsers = "Verificação concluída...";
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Loading", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Loading", $"Erro ao fazer o upload da listagem de usuários. {ex.Message}", "OK");
            }
        }

        private async Task CarregarUsuarios()
        {
            try
            {
                MessageUsers = "Verificando usuários...";
                var totalUsers = App.UserRepository.GetTotal();
                if (totalUsers == 0)
                {
                    MessageUsers = "Sincronizando usuários...";
                    await _servicoDeCarregamentoDasBases.CarregarUsuariosAsync();
                    MessageUsers = "Sincronizando usuários... Concluído";

                    SalvarLog("Usuario");
                }
                else
                {
                    MessageUsers = "Verificação concluída...";
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Loading", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Loading", $"Erro ao fazer o upload da listagem de usuários. {ex.Message}", "OK");
            }
        }

        private async Task CarregarRankingAsync()
        {

            try
            {
                MessageRanking = "Sincronizando ranking...";
                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Ranking);
                MessageRanking = "Sincronizando ranking... Concluído";

                SalvarLog("Ranking");
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Loading", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Loading", $"Erro ao fazer upolad do ranking. {ex.Message}", "OK");
            }
        }

        private async Task CarregarRanking()
        {
            try
            {
                MessageRanking = "Sincronizando ranking...";
                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Ranking);
                MessageRanking = "Sincronizando ranking... Concluído";
                SalvarLog("Ranking");
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Loading", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Loading", $"Erro ao fazer upolad do ranking. {ex.Message}", "OK");
            }
        }

        private async Task CarregarFaturamento()
        {
            try
            {
                MessageInvoicing = "Sincronizando faturamento...";
                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.ClienteFaturamento);
                MessageInvoicing = "Sincronizando faturamento... Concluído";
                SalvarLog("Faturamento");
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                    _alertService.ShowAlert("Loading", ex.Message, "OK");
                else
                    _alertService.ShowAlert("Loading", $"Erro ao fazer upolad do faturamento. {ex.Message}", "OK");
            }
        }

        private void SalvarLog(string nomeTabela)
        {
            try
            {
                App.AtualizacaoRepository.Add(new Atualizacoes
                {
                    NomeTabela = nomeTabela,
                    DataAtualizacao = DateTime.Now
                });
            }
            catch (Exception)
            {

            }
        }
    }
}

