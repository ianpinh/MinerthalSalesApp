using MinerthalSalesApp.Infra.Database.Repository;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.Views.Dashboard;
using Newtonsoft.Json;

namespace MinerthalSalesApp.ViewModels.Orders
{
    public partial class PedidoViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly AppTheme theme;
        private readonly CartRepository _cartRepository;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        protected PedidoViewModel()
        {

        }

        public PedidoViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases, OrderDto pedido)
        {
            theme = Application.Current.RequestedTheme;
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases=servicoDeCarregamentoDasBases;
            Pedido= pedido;
            CarregarCliente(pedido.CodigoCliente);
            CarregarListagens();
        }




        public async Task SendAlert(string title, string msg)
        {
            await _alertService.ShowAlertAsync(title, msg, "Ok");
        }

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

        //DESCRIÇÃO DE INFORMAÇÕES
        public Cliente Cliente { get; set; }

        public DateTime DataPedido { get; set; }

        public List<DictionaryDto> ListaFiliais { get; private set; } = new List<DictionaryDto>();

        public List<DictionaryDto> ListaTipoPedido { get; private set; } = new List<DictionaryDto>();

        public List<DictionaryDto> ListaTipoVenda { get; private set; } = new List<DictionaryDto>();

        public List<DictionaryDto> ListaPlanoPagamento { get; private set; } = new List<DictionaryDto>();

        public List<DictionaryDto> ListaTipoCobranca { get; private set; } = new List<DictionaryDto>();

        public List<Produto> ListaProdutos { get; private set; } = new List<Produto>();

        public Plano PlanoPadraoCliente => RecuperarPercentualEncargos();

        public decimal Total => ListaProdutos.Count;

        public string TotalProdutos => Total.ToString();

        public OrderDto Pedido { get; private set; } = new OrderDto();

        public ItensDto ItemDeCalculoCarrinho { get; set; }

        public void CancelarPedido(Guid pedidoId)
        {
            App.PedidoRepository.DeleteById(pedidoId);

        }

        public void AtualizarListaDeProdutos()
        {
            _ListaProdutos();
        }

        public List<Produto> FiltrarProdutos(string textoBusca)
        {
            var lst = _ListaProdutos();
            if (ListaProdutos.Any())
            {
                if (!string.IsNullOrWhiteSpace(textoBusca))
                {
                    lst = ListaProdutos.Where(x =>
                                x.CdProduto.ToLower().Contains(textoBusca.ToLower())
                                || x.DsProduto.ToLower().Contains(textoBusca.ToLower())
                                || x.CdCategoria.ToLower().Contains(textoBusca.ToLower())
                                ).OrderBy(x => x.DsProduto).ToList();

                    ListaProdutos = new List<Produto>(lst.Distinct());
                }
            }
            return lst;
        }

        public async Task ExcluirPedido()
        {

            await Task.Delay(1000);
            _alertService.ShowConfirmation("Carrinho", "Deseja abandonar o pedido?", (result =>
            {
                if (result)
                {
                    Pedido =new OrderDto();
                    _alertService.ShowAlertAsync("Carrinho", "Pedido excluido", "OK");

                    Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                }
            }));

        }

        public bool AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum tipoApi)
        {
            return _servicoDeCarregamentoDasBases.AtualizarBaseDeDadosPedido(tipoApi);
        }


        private List<DictionaryDto> _ListaFiliais()
        {
            var _filiais = new List<DictionaryDto>();
            var filiais = App.FilialRepository.GetAll();
            if (filiais.Any())
            {
                _filiais = filiais.Select(x => new DictionaryDto
                {
                    Key = x.CdFilial,
                    Value = x.NmFilial
                }).ToList();
            }
            else
            {
                _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Filiais);

            }
            return _filiais;
        }

        private List<DictionaryDto> _ListaTipoPedido()
        {
            return new List<DictionaryDto>
            {
                new DictionaryDto { Key="1", Value="Venda Normal"},
                new DictionaryDto { Key="2", Value="Venda Prazo"},
            };
        }

        private List<DictionaryDto> _ListaTipoVenda()
        {
            return new List<DictionaryDto>
            {
               new DictionaryDto { Key="2", Value="2"}
               //new DictionaryDto { Key="1", Value="1"},
               //new DictionaryDto { Key="3", Value="3"}
            };
        }

        private List<DictionaryDto> _ListaPlanos()
        {
            var _planos = new List<DictionaryDto>();
            var planos = App.PlanosRepository.GetAll();
            if (planos.Any())
            {
                _planos = planos.Select(x => new DictionaryDto
                {
                    Key = x.CdPlano,
                    Value = x.DsPlano
                }).ToList();
            }
            return _planos;
        }

        private List<DictionaryDto> _ListaTipoCobranca()
        {
            var lst = new List<DictionaryDto>();
            var bancos = App.BancoRepository.GetAll();

            if (bancos.Any())
            {
                lst=bancos.Select(x => new DictionaryDto
                {
                    Key=x.CdTipoCob,
                    Value=x.DsTipocob
                }).ToList();
            }
            return lst;
        }

        private List<Produto> _ListaProdutos()
        {
            var produtos = new List<Produto>();
            try
            {
                var lista = App.ProdutosRepository.GetAll();
                var tabelaPreco = App.TabelaPrecoRepository.GetAll();

                if (lista == null || !lista.Any())
                {
                    RecarregarListaDeProdutos();
                    lista = App.ProdutosRepository.GetAll();
                }

                if (tabelaPreco == null || !tabelaPreco.Any())
                {
                    RecarregarTabelaDePrecos();
                    tabelaPreco = App.TabelaPrecoRepository.GetAll();
                }


                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                var userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
                var saler = App.VendedorRepository.GetByCodigo(userDetailStr.Codigo);

                var tipoTabela = saler.TabPreco;

                foreach (var item in lista)
                {
                    var precos = tabelaPreco.Where(x => x.CdProduto ==item.CdProduto).ToList();
                    var tbPrecos = App.TabelaPrecoRepository.Get(item.CdProduto, Pedido.FilialMinerthal, tipoTabela, Pedido.TipoVenda);

                    if (precos!=null && precos.Any())
                    {
                        var valorProduto = tbPrecos!=null && tbPrecos.Any() ? tbPrecos.Max(x => x.VlVvenda) : precos.Max(s => s.VlVvenda);
                        item.ValorCombinado= valorProduto;
                        item.VlPrectab= valorProduto;

                        var index = produtos.FindIndex(x => x.Id == item.Id);
                        if (index<0)
                            produtos.Add(item);
                    }
                }
                ListaProdutos = produtos.Distinct().OrderBy(x => x.DsProduto).ToList();
                return produtos;
            }
            catch (Exception ex)
            {
                _alertService.ShowAlertAsync("Produtos", ex.Message, "OK");
            }
            return produtos;

        }

        private void RecarregarTabelaDePrecos()
        {
            throw new NotImplementedException();
        }

        private void RecarregarListaDeProdutos()
        {
            var loadingProducts = _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Produtos);
            loadingProducts.GetAwaiter().GetResult();

            var totalProdutos = App.ProdutosRepository.GetTotal();
            if (totalProdutos<=0)
                RecarregarListaDeProdutos();
        }

        public void CarregarListagens()
        {
            try
            {
                ListaPlanoPagamento = _ListaPlanos();
                ListaFiliais = _ListaFiliais();
                ListaTipoPedido = _ListaTipoPedido();
                ListaTipoVenda = _ListaTipoVenda();
                ListaTipoCobranca = _ListaTipoCobranca();
                ListaProdutos= _ListaProdutos();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CarregarCliente(string codCliente)
        {
            Cliente = App.ClienteRepository.GetByCodigo(codCliente);
        }

        private int _PlanoPadraoCliente()
        {

            return App.ClientePlanoPagamentoRepository.RecuperarPlanoPadrao(Cliente.Codigo, Cliente.Loja);

        }

        private Plano RecuperarPercentualEncargos()
        {
            return App.PlanosRepository.GetByCode(Pedido.PlanoPagamento);
        }





    }
}