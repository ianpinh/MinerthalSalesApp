using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using System.Globalization;
using System.Windows.Input;

namespace MinerthalSalesApp.ViewModels.Usuarios
{
    public partial class RankingViewModel : BaseViewModel, IAsyncInitialization
    {
        private readonly IAlertService _alertService;
        private readonly IServicoDeCarregamentoDasBases _servicoDeCarregamentoDasBases;
        public Task Initialization { get; private set; }

        public RankingViewModel(IAlertService alertService, IServicoDeCarregamentoDasBases servicoDeCarregamentoDasBases)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _servicoDeCarregamentoDasBases = servicoDeCarregamentoDasBases ?? throw new ArgumentNullException(nameof(servicoDeCarregamentoDasBases));
            Initialization = InitializeAsync();

        }

        private async Task InitializeAsync()
        {
            await RecuperarRankingVendedores();
        }

        private List<Ranking> items;
        public List<Ranking> Items
        {
            get => items;
            set
            {
                items = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Items));
                if (items != null)
                    RecuperarPosicaoAtual(items);
            }
        }

        public decimal Total => Items.Sum(x => x.PosicaoRanking);

        public string TotalFormatado => Total.ToString("N2", CultureInfo.CreateSpecificCulture("pt-BR"));

        private string posicaoAtual;// => RecuperarPosicaoAtual();
        public string PosicaoAtual
        {
            get => posicaoAtual;
            set
            {
                posicaoAtual = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PosicaoAtual));
            }
        }

        private void RecuperarPosicaoAtual(List<Ranking> _items)
        {
            try
            {
                var posicaoAtual = 0M;
                var index = _items.FindIndex(x => x.Codigo == App.UserDetails.Codigo);

                if (index >= 0)
                    posicaoAtual = _items[index].Rank;

                PosicaoAtual = posicaoAtual > 0 ? $"{posicaoAtual}º lugar" : "Você não tem pontuação suficiente";
            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
            }
        }

        public string DataAtual => RecuperarUltimoLogAtualizacao();

        public async Task<List<Ranking>> RecuperarRankingVendedores()
        {
            var ranking = new List<Ranking>();
            try
            {
                var lista = App.RankingRepository.GetAll();
                if (!lista.Any())
                    lista = await CarregarRanking();


                var maxWin = lista.Max(x => x.PosicaoRanking);
                ranking = lista.Select(c => { c.PercentRanking = CalcularPercentual(maxWin, c.PosicaoRanking); return c; }).OrderByDescending(x => x.PosicaoRanking).ToList();

                for (var i = 0; i < ranking.Count; i++)
                    ranking[i].Rank = i + 1;

            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
                Items = new List<Ranking>(ranking);
            }

            if (ranking.Any())
            {
                Items =  ranking.Where(x=>x.Codigo == App.UserDetails.Codigo).ToList();
            }
            return ranking;
        }

        //[RelayCommand]
        //private async Task UpdateRanking()
        //{
        //    await RecuperarRankingVendedores();

        //}

        private bool isRefreshing;
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
        public ICommand RefreshCommand => new Command(async () =>
        {
            var list = await RecuperarRankingVendedores();
            IsRefreshing = false;

        });



        private decimal CalcularPercentual(decimal maxWin, decimal ranking)
        {
            try
            {
                return Math.Round(ranking / maxWin, 2);
            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
                return 0;
            }
        }

        private string RecuperarUltimoLogAtualizacao()
        {
            try
            {
                var data = App.AtualizacaoRepository.GetLastLog("Ranking");
                if (!data.HasValue)
                    return string.Empty;

                var _data = (DateTime)data;
                return $"{_data.ToString("d", CultureInfo.CreateSpecificCulture("pt-BR"))} - {_data.ToString("t", CultureInfo.CreateSpecificCulture("pt-BR"))}";
            }
            catch (Exception ex)
            {
                CadastrarLogErro(ex.Message);
                _ = _alertService.ShowAlertAsync("Ranking", $"Erro:{ex.Message}", "OK");
                return string.Empty;
            }
        }

        private void CadastrarLogErro(string descErro)
        {
            App.LogRepository.Add(new Log
            {
                Data = DateTime.Now,
                Descricao = descErro,
                Pagina = "Ranking",
                Tipo = "Error"
            });
        }

        async Task<List<Ranking>> CarregarRanking()
        {
            try
            {

                await _servicoDeCarregamentoDasBases.AtualizarBaseDeDados(ApiMinertalTypes.Ranking);
                var ranking = App.RankingRepository.GetAll();
                CustomExceptions.LancarExcecaoQuando(ranking == null || !ranking.Any(), "Não foi possivel carregar o ranking da API.");
                return ranking;
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync("Ranking", $"Não foi possível carregar a listagem de ranking. Erro:{ex.Message}", "OK");
                return new List<Ranking>();
            }
        }
    }
}




