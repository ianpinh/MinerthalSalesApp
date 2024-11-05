using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Views.Clients;
using System.Windows.Input;

namespace MinerthalSalesApp.ViewModels.Clients
{
    public partial class ClientViewModel : BaseViewModel, IAsyncInitialization
    {
        const string routeClient = "clientDetail";
        private readonly IAlertService _alertService;
        public Task Initialization { get; private set; }
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        public ClientViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            Initialization = InitializeAsync();

        }
        public ClientViewModel()
        {

        }

        private async Task InitializeAsync()
        {
            await ListarClientes();
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText == null || !searchText.Equals(value))
                {
                    searchText = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SearchText));
                    SearchingClientes(SearchText);

                }
            }
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


        private List<Cliente> items;
        public List<Cliente> Items
        {
            get => items;
            set
            {
                if (!value.Equals(items))
                {
                    items = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Items));
                }
                GridLoadingVisible = false;
            }
        }

        public async Task RecuperarClientes()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();

                clientes = App.ClienteRepository.GetAll();
                if (!clientes.Any())
                    await _alertService.ShowAlertAsync("Clientes", $"Não foi possível carregar a listagem de clientes.", "OK");


                if (Items != null) Items.Clear();

                Items = clientes;//new ObservableCollection<Cliente>(clientes);

            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Clientes", $"Erro ao carregar os clientes.  Erro:{ex.Message}", "OK");
                CadastrarLogErro(ex.Message);
            }
        }

        public async Task RecuperarClientesAsync()
        {
            try
            {
                if (Items != null) Items.Clear();
                var clientes = App.ClienteRepository.GetAll();
                if (clientes.Any())
                    Items = clientes;// new ObservableCollection<Cliente>(clientes);
                else
                    await _alertService.ShowAlertAsync("Clientes", $"Não foi possível carregar a listagem de clientes.", "OK");
            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
            }

        }


        [RelayCommand]
        public void UpdateClientsCommand()
        {
            ListarClientes();
        }

        [RelayCommand]
        async Task CarregarDadosClienteCommand()
        {
            await Shell.Current.GoToAsync($"//{nameof(ClientsPageDetail)}");
        }



        public ICommand PerformSearchOld => new Command<string>((string query) =>
        {
            FiltrarClientesAsync(query);
        });

        [RelayCommand]
        public void PerformSearch(string query)
        {

        }


        public void SearchingClientes(string textoBusca)
        {
            var lst = App.ClienteRepository.GetAll();


            if (lst.Any())
            {
                if (string.IsNullOrWhiteSpace(textoBusca))
                {
                    Items = lst;
                }
                else
                {
                    var lsita = lst.Where(x =>
                                   x.A1Cod.ToLower().Contains(textoBusca.ToLower())
                                || x.A1Nome.ToLower().Contains(textoBusca.ToLower())
                                || x.A1Nreduz.ToLower().Contains(textoBusca.ToLower())
                                || x.A1Cgc.ToLower().Contains(textoBusca.ToLower())
                                || x.A1Loja.ToLower().Contains(textoBusca.ToLower())
                                ).OrderBy(x => x.A1Nome).ToList();

                    Items = lsita;//new ObservableCollection<Cliente>(lst);
                }
            }

        }


        public async Task<IEnumerable<Cliente>> FiltrarClientesAsync(string textoBusca)
        {
            var lista = Enumerable.Empty<Cliente>();
            var lst = App.ClienteRepository.GetAll();

            if (lst.Any())
            {
                if (string.IsNullOrWhiteSpace(textoBusca))
                {
                    lista = lst;
                }
                else
                {
                    lista = lst.Where(x =>
                                  x.A1Cod.ToLower().Contains(textoBusca.ToLower())
                               || x.A1Nome.ToLower().Contains(textoBusca.ToLower())
                               || x.A1Nreduz.ToLower().Contains(textoBusca.ToLower())
                               || x.A1Cgc.ToLower().Contains(textoBusca.ToLower())
                               || x.A1Loja.ToLower().Contains(textoBusca.ToLower())
                               ).OrderBy(x => x.A1Nome).ToList();
                }
            }
            return lista;
        }

        private async Task ListarClientes()
        {
            var total = App.ClienteRepository.GetTotal();
            if (total <= 1)
                await _servicoDeCarregamentoDasBases.CarregarClientesAsync();

            _ = RecuperarClientes();
        }

        private void CadastrarLogErro(string descErro)
        {
            try
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = descErro,
                    Pagina = "clientes",
                    Tipo = "Error"
                });
            }
            catch (Exception)
            {
            }
        }


    }
}