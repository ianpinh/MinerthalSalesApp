using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MinerthalSalesApp.ViewModels.Orders
{
    public partial class MeusPedidosViewModel : BaseViewModel, IAsyncInitialization
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        public Task Initialization { get; private set; }

        CultureInfo cultureInfo = new CultureInfo("pt-BR");

        public MeusPedidosViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            //Initialization = InitializeAsync();
            //_=Initialize();
        }

        public async Task Initialize()
        {
            var taskPedidos = Task.Run(() => { _ListarPedidos(); });
            var taskPendentes = Task.Run(() => { _ListarPedidosPendentes(); });
            var taskHistorico = Task.Run(() => { _listarHistoricoPedido(); });

            await Task.WhenAll(taskPedidos,taskPendentes,taskHistorico);

        }


        public List<MeusPedidos> ListaMeusPedidos { get; set; } = new List<MeusPedidos>();
        public ObservableCollection<PedidosLocaisDto> PedidosPendentes { get; set; } = new ObservableCollection<PedidosLocaisDto>();


        private bool gridLoadingVisible = true;
        public bool GridLoadingVisible
        {
            get => gridLoadingVisible;
            set
            {
                if (!gridLoadingVisible.Equals(value))
                {
                    gridLoadingVisible = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(GridLoadingVisible));

                }
            }
        }

        public List<HistoricoDePedidos> HistoricoPedido
        {
            get => historicoPedido;
            set
            {
                if (!value.Equals(historicoPedido))
                {
                    historicoPedido = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HistoricoPedido));
                    GridLoadingVisible = false;
                }
            }
        }

        private List<HistoricoDePedidos> historicoPedido;

        private void _ListarPedidos()
        {
            var lista = App.MeusPedidosRepository.GetAll();
            ListaMeusPedidos = lista.Distinct().ToList();
        }

        public void _ListarPedidosPendentes()
        {
            try
            {
                PedidosPendentes.Clear();
                var _pedidos = new List<PedidosLocaisDto>();
                var pedidos = App.PedidoRepository.GetAll();

                if (pedidos.Any())
                {
                    foreach (var item in pedidos)
                    {
                        var carrinho = App.CartRepository.GetByOrderId(item.Id);
                        var cliente = App.ClienteRepository.GetByCodigo(item.CodigoCliente + item.CodigoLoja);

                        var order = new OrderDto
                        {
                            Id = item.Id,
                            CodigoCliente = item.CodigoCliente,
                            CodigoLoja = item.CodigoLoja,
                            FilialMinerthal = item.FilialMinerthal,
                            TipoCobranca = item.TipoCobranca,
                            TipoVenda = item.TipoVenda,
                            TipoPedido = item.TipoPedido,
                            PlanoPagamento = item.PlanoPagamento,
                            ValorFrete25 = item.ValorFrete25,
                            ValorFrete30 = item.ValorFrete30,
                            Parcelas = !string.IsNullOrWhiteSpace(item.Parcelas) ? JsonConvert.DeserializeObject<ObservableCollection<DictionaryDto>>(item.Parcelas) : new ObservableCollection<DictionaryDto>(),
                            QdtItens = carrinho.Sum(x => x.Quantidade),
                            TotalPedido = CalcularTotalCarrinho(item, carrinho),
                            Observacao = item.Observacao,
                            NomeFilial = item.NomeFilial,
                            NomeTipo = item.NomeTipo,
                            NomeTipoVenda = item.NomeTipoVenda,
                            NomeTipoCobranca = item.NomeTipoCobranca,
                            NomePlanoPagamento = item.NomePlanoPagamento,

                            ItensPedido = carrinho.Select(x => new ItensDto
                            {
                                CodigoNomeProduto = x.CodigoNomeProduto,
                                CodProduto = x.CodProduto,
                                FreteUnidade = x.Frete,
                                Id = x.Id,
                                ImagemProduto = x.ImagemProduto,
                                PedidoId = x.PedidoId,
                                Quantidade = x.Quantidade,
                                ValorBrutoProduto = x.ValorProduto,
                                ValorCombinado = x.ValorCombinado,
                                Comissao = x.Comissao,
                                Desconto = x.Desconto,
                                TaxaPlano = x.TaxaEncargos

                            }).ToList()
                        };
                        var ped = new PedidosLocaisDto
                        {
                            Pedido = order,
                            Cliente = cliente
                        };

                        var index = _pedidos.FindIndex(x => x.Pedido.Id == ped.Pedido.Id);
                        if (index < 0)
                        {
                            _pedidos.Add(ped);
                            PedidosPendentes.Add(ped);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void _listarHistoricoPedido()
        {
            try
            {
                if (HistoricoPedido != null) HistoricoPedido.Clear();
                var pedidos = App.HistoricoPedidoReposity.GetAllFromLastCurrentMonth();
                if (pedidos.Any())
                {
                    foreach (var item in pedidos)
                    {
                        var cdCliente = item.CdCliente;
                        var cliente = App.ClienteRepository.GetByCodigo(cdCliente);
                        var tpCobranca = App.BancoRepository.RecuperarNomeTipoCobranca(item.CdTipocob);
                        var _resumoPedido = App.ResumoPedidoRepository.GetByNumPedido(item.NrPedido);
                        if (cliente != null)
                        {
                            item.NomeCliente = cliente.A1Nome;
                            item.Loja = cliente.A1Loja;
                            item.ClienteCodigo = cliente.A1Cod;
                        }

                        if (_resumoPedido != null)
                            item.ResumoDoPedido = _resumoPedido;

                        if (tpCobranca != null)
                            item.NomeTipoCobranca = tpCobranca.DsTipocob;
                    }

                    HistoricoPedido = pedidos;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private decimal CalcularTotalCarrinho(Pedido pedido, List<Carrinho> carrinho)
        {
            var subtotalFrete = carrinho.Sum(x => x.Frete * x.Quantidade);
            var subTotalProduto = carrinho.Sum(x => x.ValorCombinado * x.Quantidade);
            var subtotal = subTotalProduto + subtotalFrete;

            var totalDescontos = 0M;
            var totalEncargos = 0M;

            if (pedido.PercentualJuros > 0)
                if (pedido.PlanoPagamento.Equals("88"))
                {
                    foreach (var item in carrinho)
                    {
                        totalEncargos += item.Encargos;
                    }
                }
                else
                {
                    totalEncargos = subtotal * (pedido.PercentualJuros / 100);
                }

            if (pedido.PercentualDesconto > 0)
                totalDescontos = subTotalProduto - (subTotalProduto / (1 + (pedido.PercentualDesconto / 100)));

            var totalGeral = (subtotal + totalEncargos) - totalDescontos;

            return totalGeral;
        }

        public async Task<bool> ReenviarPedidos()

        {
            var pedidoTransmisao = await _servicoDeCarregamentoDasBases.TransmitirPedidos();
            await _alertService.ShowAlertAsync("Enviar Pedido", $"Status: {pedidoTransmisao.Mensagem}. ", "Ok");
            return pedidoTransmisao.sucesso == "Sucesso";

            //if (pedidoTransmisao.sucesso=="Sucesso")
            //{
            //    var pedidoRepository = App.PedidoRepository;

            //    await App.CartRepository.DeleteAllAsync();
            //    await App.PedidoRepository.DeleteAllasync();


            //    var model = new MeusPedidosViewModel(_alertService, _servicoDeCarregamentoDasBases, pedidoRepository);
            //    await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(Models.Enums.ApiMinertalTypes.MeusPedidos);
            //    await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
            //}
        }


        public async Task AtualizarPedidosTrasmitidos()
        {
            await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(Models.Enums.ApiMinertalTypes.MeusPedidos);
            _listarHistoricoPedido();
        }


        [RelayCommand]
        private void ApagarTodosPedidos()
        {
            App.PedidoRepository.DeleteAll();
        }

        [RelayCommand]
        private void ApagarPedido()
        {
            //App.PedidoRepository.Delete(pedidoId);
        }

        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private bool isDataBusy = false;
        public bool IsDataBusy
        {
            get => isDataBusy;
            set
            {
                isDataBusy = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsDataBusy));
            }
        }

        //ICommand refreshCommand = new Command(() =>
        //{
        //    // IsRefreshing is true
        //    // Refresh data here
        //    IsRefreshing = false;
        //});
        //refreshView.Command = refreshCommand;
    }
}
