using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
namespace MinerthalSalesApp.ViewModels.Clients
{
    public  partial class HistoricoPageViewModel : BaseViewModel, IAsyncInitialization
    {
        public Task Initialization { get; private set; }
        public HistoricoPageViewModel(string clienteLoja)
        {
            Initialization = InitializeAsync(clienteLoja);
        }
      
        private async Task InitializeAsync(string clienteLoja)
        {
            await _listarHistoricoPedido(clienteLoja);
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
                }
            }
        }

        private List<HistoricoDePedidos> historicoPedido;

        private async Task _listarHistoricoPedido(string codigoCliente)
        {
            try
            {
                var pedidos = App.HistoricoPedidoReposity.GetAllFromClient(codigoCliente);
                if (pedidos.Any())
                {
                    foreach (var item in pedidos)
                    {
                        var cdCliente = item.CdCliente.Substring(0, item.CdCliente.Length-2);
                        
                        var cliente =  App.ClienteRepository.GetByCodigo(cdCliente);
                        var tpCobranca =  App.BancoRepository.RecuperarNomeTipoCobranca(item.CdTipocob);
                        var resumo =  App.ResumoPedidoRepository.GetByNumPedido(item.NrPedido);


                        if (cliente!=null)
                        {
                            item.NomeCliente = cliente.A1Nome;
                            item.Loja = cliente.A1Loja;
                            item.ClienteCodigo =cliente.A1Cod;
                        }

                        if (tpCobranca!=null && tpCobranca!=null)
                            item.NomeTipoCobranca = tpCobranca.DsTipocob;


                        if(resumo!=null && resumo.Count>0)
                        {
                            item.ResumoDoPedido = resumo;
                        }
                    }

                    HistoricoPedido = pedidos.ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
