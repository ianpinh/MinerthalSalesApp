using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models.Enums;
using System.Globalization;

namespace MinerthalSalesApp.ViewModels.Products
{
    public partial class ProdutosPageViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;

        public ProdutosPageViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases;
            _ = ListarProdutos();
        }


        private List<Produto> items;
        public List<Produto> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Items));
            }
        }


        public decimal Total => Items.Count;

        public string TotalFormatado => Total.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));

        private List<Produto> ListagemDeProdutos()
        {
            try
            {
                var lista = App.ProdutosRepository.GetProdutoPrecoDefault();
                if (!lista.Any())
                    _alertService.ShowAlertAsync("Produtos", $"Não foi possível carregar a listagem de produtos.", "OK");

                var produtos = lista.OrderBy(x => x.DsProduto).ToList();
                Items = produtos;// new ObservableCollection<Produto>(produtos);
                return produtos;
            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
                return new List<Produto>();
            }
        }

        public List<Produto> FiltrarProdutos(string textoBusca)
        {

            var lista = App.ProdutosRepository.GetAll();
            if (!string.IsNullOrWhiteSpace(textoBusca) && lista.Any())
            {
                var produtos = lista.Where(x =>
                             x.CdProduto.ToLower().Contains(textoBusca.ToLower())
                             || x.DsProduto.ToLower().Contains(textoBusca.ToLower())
                             || x.CdCategoria.ToLower().Contains(textoBusca.ToLower())
                             ).OrderBy(x => x.DsProduto).ToList();
                Items = produtos;// new ObservableCollection<Produto>(produtos);
                return produtos;
            }
            return lista.ToList();
        }

        private void CadastrarLogErro(string descErro)
        {
            App.LogRepository.Add(new Log
            {
                Data = DateTime.Now,
                Descricao = descErro,
                Pagina = "Produto",
                Tipo = "Error"
            });
        }

        private async Task ListarProdutos()
        {
            var numProdutos = App.ProdutosRepository.GetTotal();

            if (numProdutos <= 0)
                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Produtos);

            ListagemDeProdutos();
        }

        [RelayCommand]
        private void UpdateListagemDeProdutos()
        {
            ListagemDeProdutos();
        }


    }
}
