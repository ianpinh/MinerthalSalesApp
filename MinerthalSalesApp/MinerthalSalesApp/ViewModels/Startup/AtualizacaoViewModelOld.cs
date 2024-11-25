using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.Views.Dashboard;

namespace MinerthalSalesApp.ViewModels.Startup
{
    public partial class AtualizacaoViewModelOld : BaseViewModel, IAsyncInitialization
    {

        private readonly DatabaseContext _context;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        private const string imagLoading = "loading.gif";
        private const string imagFinished = "check_btn.png";
        public string imagError = "close_btn.png";
        public Task Initialization { get; private set; }

        public AtualizacaoViewModelOld(DatabaseContext context, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _context = context;
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases;
            Initialization = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await CarregarBaseInicial();
        }

        private async Task CarregarBaseInicial()
        {
            try
            {
                await Task.Delay(1500);
                //await _servicoDeCarregamentoDasBases.AtualizarBaseDeDadosPrimeiraCarga(this);
                //var totalTabelas = await App.AtualizacaoRepository.GetTotalAsync();

                //if (totalTabelas>1)
                //{
                //    TotalAtualizacoesRealizadas=11;
                //    TotalAtualizadoPercentual = "100%";
                //    IsEnabled =true;
                //}
                //else
                //{
                //    await _servicoDeCarregamentoDasBases.AtualizarBaseDeDadosPrimeiraCarga(this);
                //}

            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = "",
                });
            }
        }

        [RelayCommand]
        async Task NavegarParaHome()
        {
            await Task.Delay(20000);
            await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
        }

        private void SalvarLog(ApiMinertalTypes tabela)
        {
            try
            {
                App.AtualizacaoRepository.Add(new Atualizacoes
                {
                    NomeTabela = tabela.ToString(),
                    DataAtualizacao = DateTime.Now
                });
            }
            catch (Exception)
            {

            }
        }


        private string totalAtualizadoPercentual = "0%";
        public string TotalAtualizadoPercentual
        {
            get => totalAtualizadoPercentual;
            set
            {
                totalAtualizadoPercentual = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalAtualizadoPercentual));
            }
        }

        private decimal totalAtualizado = 0;
        public decimal TotalAtualizado
        {
            get => totalAtualizado;
            set
            {
                totalAtualizado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalAtualizado));

                int _total = (int)(totalAtualizado * 100);
                TotalAtualizadoPercentual = $"{_total}%";
            }
        }


        private string messageUpdate = "Inicializando o sistema";
        public string MessageUpdate
        {
            get => messageUpdate;
            set
            {
                messageUpdate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MessageUpdate));
            }
        }

        private bool isEnabled = false;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEnabled));

                if (IsEnabled)
                {
                    MessageUpdate = "Bem vindo ao AppThal";
                    _ = NavegarParaHome();
                }
            }
        }


        public int TotalAtualizacoes => 11;

        private int totalAtualizacoesRealizadas = 0;
        public int TotalAtualizacoesRealizadas
        {
            get => totalAtualizacoesRealizadas;
            set
            {
                totalAtualizacoesRealizadas = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalAtualizacoesRealizadas));
                CalcularPercenturalAtualizacao();
                IsEnabled = (TotalAtualizacoesRealizadas - TotalErros) == TotalAtualizacoes;

            }
        }

        private void CalcularPercenturalAtualizacao()
        {
            var totalRealizado = TotalAtualizacoesRealizadas - TotalErros;
            var percentual = (totalRealizado * 100) / TotalAtualizacoes;
            TotalAtualizado = (percentual / 100M);
        }

        private int totalErros = 0;
        public int TotalErros
        {
            get => totalErros;
            set
            {
                totalErros = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalErros));
                IsEnabled = (TotalAtualizacoesRealizadas - TotalErros) == TotalAtualizacoes;
                CalcularPercenturalAtualizacao();
            }
        }

        #region ::. USUÁRIOS.::
        private string imageSourceLoadingUsuario = imagLoading;
        public string ImageSourceLoadingUsuario
        {
            get => imageSourceLoadingUsuario;
            set
            {
                imageSourceLoadingUsuario = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingUsuario));
            }
        }
        private int totalUsuarios;
        public int TotalUsuarios
        {
            get => totalUsuarios;
            set
            {
                if (value > 0 && !TotalUsuarios.Equals(value))
                {
                    totalUsuarios = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalUsuarios));
                    ImageSourceLoadingUsuario = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Usuarios);
                }
            }
        }
        #endregion

        #region ::. CLIENTES .::
        private string imageSourceLoadingCliente = imagLoading;
        public string ImageSourceLoadingCliente
        {
            get => imageSourceLoadingCliente;
            set
            {
                imageSourceLoadingCliente = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingCliente));
            }
        }
        private int totalClientes;
        public int TotalClientes
        {
            get => totalClientes;
            set
            {
                if (value > 0 && !TotalClientes.Equals(value))
                {
                    totalClientes = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalClientes));

                    ImageSourceLoadingCliente = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Cliente);
                }
            }
        }
        #endregion

        #region ::. FILIAIS .::
        private string imageSourceLoadingFilial = imagLoading;
        public string ImageSourceLoadingFilial
        {
            get => imageSourceLoadingFilial;
            set
            {
                imageSourceLoadingFilial = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingFilial));
            }
        }
        private int totalFiliais;
        public int TotalFiliais
        {
            get => totalFiliais;
            set
            {
                if (value > 0 && !TotalFiliais.Equals(value))
                {
                    totalFiliais = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalFiliais));

                    ImageSourceLoadingFilial = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Filiais);
                }
            }
        }
        #endregion

        #region ::. PLANOS .::

        private string imageSourceLoadingPlano = imagLoading;
        public string ImageSourceLoadingPlano
        {
            get => imageSourceLoadingPlano;
            set
            {
                imageSourceLoadingPlano = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingPlano));
            }
        }
        private int totalPlanos;
        public int TotalPlanos
        {
            get => totalPlanos;
            set
            {
                if (value > 0 && !TotalPlanos.Equals(value))
                {
                    totalPlanos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPlanos));
                    ImageSourceLoadingPlano = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Planos);
                }
            }
        }
        #endregion

        #region ::. BANCOS .::

        private string imageSourceLoadingBanco = imagLoading;
        public string ImageSourceLoadingBanco
        {
            get => imageSourceLoadingBanco;
            set
            {
                imageSourceLoadingBanco = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingBanco));
            }
        }
        private int totalBancos;
        public int TotalBancos
        {
            get => totalBancos;
            set
            {
                if (value > 0 && !TotalBancos.Equals(value))
                {
                    totalBancos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalBancos));
                    ImageSourceLoadingBanco = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Bancos);
                }
            }
        }

        #endregion

        #region ::. MEUS PEDIDOS .::

        private string imageSourceLoadingPedidos = imagLoading;
        public string ImageSourceLoadingPedidos
        {
            get => imageSourceLoadingPedidos;
            set
            {
                imageSourceLoadingPedidos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingPedidos));
            }
        }
        private int totalPedidos;
        public int TotalPedidos
        {
            get => totalPedidos;
            set
            {
                if (value > 0 && !TotalPedidos.Equals(value))
                {
                    totalPedidos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPedidos));
                    imageSourceLoadingPedidos = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.MeusPedidos);
                }
            }
        }

        #endregion

        #region ::. TABELA DE PREÇO .::

        private string imageSourceLoadingPreco = imagLoading;
        public string ImageSourceLoadingPreco
        {
            get => imageSourceLoadingPreco;
            set
            {
                imageSourceLoadingPreco = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingPreco));
            }
        }
        private int totalTbPrecos;
        public int TotalTbPrecos
        {
            get => totalTbPrecos;
            set
            {
                if (value > 0 && !TotalTbPrecos.Equals(value))
                {
                    totalTbPrecos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalTbPrecos));
                    ImageSourceLoadingPreco = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.TabelaDePrecos);
                }
            }
        }

        #endregion

        #region ::. PRODUTOS .::

        private string imageSourceLoadingProduto = imagLoading;
        public string ImageSourceLoadingProduto
        {
            get => imageSourceLoadingProduto;
            set
            {
                imageSourceLoadingProduto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingProduto));
            }
        }
        private int totalProdutos;
        public int TotalProdutos
        {
            get => totalProdutos;
            set
            {
                if (value > 0 && !TotalProdutos.Equals(value))
                {
                    totalProdutos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalProdutos));
                    ImageSourceLoadingProduto = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Produtos);
                }
            }
        }
        #endregion

        #region ::. RANKING .::

        private string imageSourceLoadingRanking = imagLoading;
        public string ImageSourceLoadingRanking
        {
            get => imageSourceLoadingRanking;
            set
            {
                imageSourceLoadingRanking = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingRanking));
            }
        }
        private int totalRanking;
        public int TotalRanking
        {
            get => totalRanking;
            set
            {
                if (value > 0 && !TotalRanking.Equals(value))
                {
                    totalRanking = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalRanking));
                    ImageSourceLoadingRanking = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Ranking);
                }
            }
        }
        #endregion

        #region ::. VENDEDORES .::

        private string imageSourceLoadingVendedor = imagLoading;
        public string ImageSourceLoadingVendedor
        {
            get => imageSourceLoadingVendedor;
            set
            {
                imageSourceLoadingVendedor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingVendedor));
            }
        }

        private int totalVendedores;
        public int TotalVendedores
        {
            get => totalVendedores;
            set

            {
                if (value > 0 && !TotalVendedores.Equals(value))
                {
                    totalVendedores = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalVendedores));
                    ImageSourceLoadingVendedor = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Vendedor);
                }
            }
        }

        #endregion

        #region ::. HISTÓRICO DE PEDIDOS .::

        private string imageSourceLoadingHistoricoPedido = imagLoading;
        public string ImageSourceLoadingHistoricoPedido
        {
            get => imageSourceLoadingHistoricoPedido;
            set
            {
                imageSourceLoadingHistoricoPedido = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingHistoricoPedido));
            }
        }

        private int totalHistoricoPedidos;
        public int TotalHistoricoPedidos
        {
            get => totalHistoricoPedidos;
            set

            {
                if (value > 0 && !totalHistoricoPedidos.Equals(value))
                {
                    totalHistoricoPedidos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalHistoricoPedidos));
                    ImageSourceLoadingHistoricoPedido = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.HistoricoPedido);
                }
            }
        }

        #endregion

        #region ::. RESUMO DE PEDIDOS .::

        private string imageSourceLoadingResumoPedido = imagLoading;
        public string ImageSourceLoadingResumoPedido
        {
            get => imageSourceLoadingResumoPedido;
            set
            {
                imageSourceLoadingResumoPedido = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageSourceLoadingResumoPedido));
            }
        }

        private int totalResumoPedidos;
        public int TotalResumoPedidos
        {
            get => totalResumoPedidos;
            set

            {
                if (value > 0 && !totalResumoPedidos.Equals(value))
                {
                    totalResumoPedidos = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalResumoPedidos));
                    ImageSourceLoadingResumoPedido = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.ResumoPedido);
                }
            }
        }

        #endregion
    }
}
