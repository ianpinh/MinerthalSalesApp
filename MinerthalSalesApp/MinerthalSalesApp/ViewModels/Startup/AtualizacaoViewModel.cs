using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.Views.Dashboard;

namespace MinerthalSalesApp.ViewModels.Startup
{
    public partial class AtualizacaoViewModel : BaseViewModel, IAsyncInitialization
    {

        bool _isbusy = false;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        private const string imagLoading = "loading.gif";
        private const string imagFinished = "check_btn.png";
        public string imagError = "close_btn.png";

        public Page PaginaAtual = default;
        public Task Initialization { get; private set; }

        public AtualizacaoViewModel(IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases;
        }

        List<(string, int)> Completos = new List<(string, int)>();

        public int InitializeAsync(Page pagina)
        {

            PaginaAtual = pagina;

            Completos.Clear();
            isEnabled = false;
            TotalAtualizado = 0;
            totalAtualizacoesRealizadas = 0;
            TotalAtualizadoPercentual = "0%";

            return CarregarBaseInicial();
        }

        private int CarregarBaseInicial()
        {
            var total = 0;
            if (!_isbusy)
            {
                _isbusy = true;
                try
                {
                    total = _servicoDeCarregamentoDasBases.AtualizarBaseDeDadosPrimeiraCarga(this);
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
                finally
                {
                    _isbusy = false;
                }
            }
            return total;
        }

        bool isCalling = false;
        [RelayCommand]
        async Task NavegarParaHome()
        {
            try
            {
                if (!isCalling)
                {
                    isCalling = true;
                    await Task.Delay(5000);
                    await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                    isCalling = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
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
                    //Completos.Clear();
                    //TotalAtualizado =0;
                    //totalAtualizacoesRealizadas=0;
                    //TotalAtualizadoPercentual="0%";
                    isEnabled = false;
                }
            }
        }


        public int TotalAtualizacoes => 15;

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

        List<string> TabelasCarregadas = new List<string>();

        string tabelaAtualizada;
        public string TabelaAtualizada
        {
            get => tabelaAtualizada;
            set
            {
                tabelaAtualizada = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalErros));
                TabelasCarregadas.Add(tabelaAtualizada);
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
                totalUsuarios = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalUsuarios));
                    ImageSourceLoadingUsuario = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Usuarios);
                    TabelaAtualizada = "Usuarios";
                    Completos.Add(("TotalUsuarios", value));
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
                totalClientes = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalClientes));

                    ImageSourceLoadingCliente = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Cliente);
                    TabelaAtualizada = "Clientes";
                    Completos.Add(("TotalClientes", value));
                }
            }
        }
        #endregion

        #region ::. FATURAMENTO .::
        private int totalFaturamento;
        public int TotalFaturamento
        {
            get => totalFaturamento;
            set
            {
                totalFaturamento = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalFaturamento));

                    //ImageSourceLoadingCliente = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Faturamento);
                    TabelaAtualizada = "Faturamento";
                    Completos.Add(("TotalFaturamento", value));
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
                totalFiliais = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalFiliais));

                    ImageSourceLoadingFilial = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Filiais);
                    TabelaAtualizada = "Filiais";
                    Completos.Add(("TotalFiliais", value));
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
                totalPlanos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPlanos));
                    ImageSourceLoadingPlano = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Planos);
                    TabelaAtualizada = "Planos";
                    Completos.Add(("TotalPlanos", value));
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
                totalBancos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalBancos));
                    ImageSourceLoadingBanco = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Bancos);
                    TabelaAtualizada = "Bancos";
                    Completos.Add(("TotalBancos", value));
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
                totalPedidos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPedidos));
                    imageSourceLoadingPedidos = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.MeusPedidos);
                    TabelaAtualizada = "Pedidos";
                    Completos.Add(("TotalPedidos", value));
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
                totalTbPrecos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalTbPrecos));
                    ImageSourceLoadingPreco = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.TabelaDePrecos);
                    TabelaAtualizada = "Tabela de preços";
                    Completos.Add(("TotalTbPrecos", value));
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
                totalProdutos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalProdutos));
                    ImageSourceLoadingProduto = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Produtos);
                    TabelaAtualizada = "Produtos";
                    Completos.Add(("TotalProdutos", value));
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
                totalRanking = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalRanking));
                    ImageSourceLoadingRanking = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Ranking);
                    TabelaAtualizada = "Ranking";
                    Completos.Add(("TotalRanking", value));
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
                totalVendedores = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalVendedores));
                    ImageSourceLoadingVendedor = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Vendedor);
                    TabelaAtualizada = "Vendedores";
                    Completos.Add(("TotalVendedores", value));
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
                totalHistoricoPedidos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalHistoricoPedidos));
                    ImageSourceLoadingHistoricoPedido = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.HistoricoPedido);
                    TabelaAtualizada = "Historico Pedidos";
                    Completos.Add(("TotalHistoricoPedidos", value));
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
                totalResumoPedidos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalResumoPedidos));
                    ImageSourceLoadingResumoPedido = imagFinished;
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.ResumoPedido);
                    TabelaAtualizada = "Resumo pedidos";
                    Completos.Add(("TotalResumoPedidos", value));
                }
            }
        }

        #endregion

        #region ::. TÍTULOS .::

        private int totalTitulos;
        public int TotalTitulos
        {
            get => totalTitulos;
            set

            {
                totalResumoPedidos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalTitulos));
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Titulos);
                    TabelaAtualizada = "Titulos";
                    Completos.Add(("TotalTitulos", value));
                }
            }
        }

        #endregion

        #region ::. VISITAS .::

        private int totalVisitas;
        public int TotalVisitas
        {
            get => totalVisitas;
            set
            {
                totalResumoPedidos = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalVisitas));
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.Visita);
                    TabelaAtualizada = "Visitas";
                    Completos.Add(("TotalVisitas", value));
                }
            }
        }

        #endregion

        #region ::. META MENSAL .::

        private int totalMetaMensal;
        public int TotalMetaMensal
        {
            get => totalMetaMensal;
            set

            {
                totalMetaMensal = value;
                if (value > 0)
                {
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalMetaMensal));
                    TotalAtualizacoesRealizadas += 1;
                    SalvarLog(ApiMinertalTypes.MetaMensal);
                    TabelaAtualizada = "Meta mensal";
                    Completos.Add(("TotalMetaMensal", value));
                }
            }
        }

        #endregion
    }
}