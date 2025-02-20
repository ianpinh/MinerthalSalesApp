using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models.Dtos;
using Newtonsoft.Json;
using System.Globalization;


namespace MinerthalSalesApp.ViewModels.Pesquisa
{
    public class PesquisaViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        CultureInfo cultureInfo = new CultureInfo("pt-BR");

        public PesquisaViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            Initialize();
        }

        private void Initialize()
        {
            CarregarPesquisa();
            ExpandedImageInadimplentes = "chevron_down.png";
            ExpandedImageTitulosaVencer = "chevron_down.png";
            ExpandedImageTitulosVencidos = "chevron_down.png";
            ExpandedImagePedidosEmAberto = "chevron_down.png";
            ExpandedImageCarregamentos = "chevron_down.png";
            ExpandedImageMetaMensal = "chevron_down.png";

            IsExpandedTitulosAVencer = false;
            IsExpandedTitulosVencidos = false;
            IsExpandedInadimplentes = false;
            IsExpandedPedidosAberto = false;
            IsExpandedCarregamentos = false;
            IsExpandedMetaMensal = false;
        }

        private void CarregarPesquisa()
        {
            var inadimplentes = App.ClienteRepository.RecuperarClientesInadimplentes();
            var titulosVencidos = App.FaturamentoRepository.RecuperarTitulosVencidos();
            var titulosAvencer = App.FaturamentoRepository.RecuperarTitulosAVencer();
            var pedidosEmAberto = App.HistoricoPedidoReposity.PedidosEmAberto();
            var carregamentos = App.HistoricoPedidoReposity.CarregamentoDePedidos();
            var metasMensais = App.MetaMensalRepository.GetAll();
            var historicoPedidos = App.HistoricoPedidoReposity.GetAll();

            PesquisaDto.TitulosaVencer = titulosAvencer;
            PesquisaDto.ClientesInadinplentes = inadimplentes;
            PesquisaDto.TitulosVencidos = titulosVencidos;

            if (pedidosEmAberto.Count > 0)
                PesquisaDto.PedidosEmAberto = pedidosEmAberto.OrderByDescending(x => x.DataPedido).Take(20).ToList();

            if (carregamentos.Count > 0)
                PesquisaDto.Carregamentos = carregamentos.OrderByDescending(x => x.DataPedido).Take(20).ToList();

            if (metasMensais.Count() > 0)
                PesquisaDto.MetaMensal = metasMensais.OrderByDescending(x => x.Ano).ToList();

            if (historicoPedidos != null && historicoPedidos.Any())
            {
                foreach (var item in PesquisaDto.MetaMensal)
                    item.Realizado = historicoPedidos.Count(x => x.DataPedido.Value.Year == item.Ano && x.DataPedido.Value.Month == item.NumeroMes);
            }
            var _metas = JsonConvert.SerializeObject(PesquisaDto.MetaMensal);
        }

        public PesquisaDto PesquisaDto { get; set; } = new PesquisaDto();

        private string expandedImageInadimplentes;
        public string ExpandedImageInadimplentes
        {
            get => expandedImageInadimplentes;
            set
            {
                expandedImageInadimplentes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImageInadimplentes));
            }
        }

        private string expandedImagePedidosEmAberto;
        public string ExpandedImagePedidosEmAberto
        {
            get => expandedImagePedidosEmAberto;
            set
            {
                expandedImagePedidosEmAberto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImagePedidosEmAberto));
            }
        }

        private string expandedImageTitulosaVencer;
        public string ExpandedImageTitulosaVencer
        {
            get => expandedImageTitulosaVencer;
            set
            {
                expandedImageTitulosaVencer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImageTitulosaVencer));
            }
        }

        private string expandedImageTitulosVencidos;
        public string ExpandedImageTitulosVencidos
        {
            get => expandedImageTitulosVencidos;
            set
            {
                expandedImageTitulosVencidos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImageTitulosVencidos));
            }
        }

        private string expandedImageCarregamentos;
        public string ExpandedImageCarregamentos
        {
            get => expandedImageCarregamentos;
            set
            {
                expandedImageCarregamentos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImageCarregamentos));
            }
        }

        private string expandedImageMetaMensal;
        public string ExpandedImageMetaMensal
        {
            get => expandedImageMetaMensal;
            set
            {
                expandedImageMetaMensal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ExpandedImageMetaMensal));
            }
        }


        private bool isExpandedTitulosVencidos;
        private bool isExpandedTitulosAVencer;
        private bool isExpandedInadimplentes;
        private bool isExpandedPedidosAberto;
        private bool isExpandedCarregamentos;
        private bool isExpandedMetaMensal;

        public bool IsExpandedTitulosVencidos
        {
            get => isExpandedTitulosVencidos;
            set
            {
                isExpandedTitulosVencidos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedTitulosVencidos));
            }
        }
        public bool IsExpandedTitulosAVencer
        {
            get => isExpandedTitulosAVencer;
            set
            {
                isExpandedTitulosAVencer = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedTitulosAVencer));
            }
        }
        public bool IsExpandedInadimplentes
        {
            get => isExpandedInadimplentes;
            set
            {
                isExpandedInadimplentes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedInadimplentes));
            }
        }
        public bool IsExpandedPedidosAberto
        {
            get => isExpandedPedidosAberto;
            set
            {
                isExpandedPedidosAberto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedPedidosAberto));
            }
        }
        public bool IsExpandedCarregamentos
        {
            get => isExpandedCarregamentos;
            set
            {
                isExpandedCarregamentos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedCarregamentos));
            }
        }
        public bool IsExpandedMetaMensal
        {
            get => isExpandedMetaMensal;
            set
            {
                isExpandedMetaMensal = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsExpandedMetaMensal));
            }
        }


    }
}
