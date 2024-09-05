using MinerthalSalesApp.Customs;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.ViewModels.Startup;
using Newtonsoft.Json;
namespace MinerthalSalesApp.Infra.Services
{
    public partial class ServicoDeCarregamentoDasBases : IServicoDeCarregamentoDasBases
    {
        private readonly IMinerthalApiServices _minerthal;
        public ServicoDeCarregamentoDasBases(IMinerthalApiServices minerthalApiServices)
        {
            _minerthal = minerthalApiServices ?? throw new ArgumentNullException(nameof(minerthalApiServices));
        }


        bool IsLoading { get; set; } = false;
        int totalCalls = 1;
        int totalFinished;
        int TotalFinished
        {
            get => totalFinished;
            set
            {
                totalFinished = value;
                IsLoadingFinised(totalCalls, value);
            }
        }



        public async Task AtualizarBaseDeDados(ApiMinertalTypes tipo)
        {
            try
            {
                switch (tipo)
                {
                    case ApiMinertalTypes.BaseDeDados: await AtualizarBaseDeDados(); break;
                    case ApiMinertalTypes.ClienteFaturamento: await CarregarClienteFaturamento(); break;
                    case ApiMinertalTypes.Produtos: await CarregarProdutos(); break;
                    case ApiMinertalTypes.Ranking: CarregarRankingAsync(); break;
                    case ApiMinertalTypes.Filiais: await CarregarFiliaisAsync(); break;
                    case ApiMinertalTypes.MeusPedidos: await CarregarMeusPedidosAsync(); break;
                    case ApiMinertalTypes.TabelaDePrecos: await CarregarTabelaDePrecosAsync(); break;
                    case ApiMinertalTypes.Planos: await CarregarPlanosAsync(); break;
                    case ApiMinertalTypes.Bancos: await CarregarBancosAsync(); break;
                    case ApiMinertalTypes.Usuarios: await CarregarUsuariosAsync(); break;
                    case ApiMinertalTypes.MetaMensal: await CarregarMetaMensalAsync(); break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum tipo)
        {
            var success = false;
            try
            {
                if (tipo.Equals(ApiQueriesIdsEnum.FiliaisMinerthal))
                {
                    var _query = $"{(byte)ApiQueriesIdsEnum.FiliaisMinerthal:#000000}";
                    var dados = _minerthal.ApiRequestServiceAsync(_query, false);
                    var _model = JsonConvert.DeserializeObject<ResponseApiFilial>(dados);
                    if (_model.Details != null)
                    {
                        App.FilialRepository.SaveFilial(_model.Details);
                        success = true;
                    }

                }

                if (tipo.Equals(ApiQueriesIdsEnum.Bancos))
                {
                    var _query = $"{(byte)ApiQueriesIdsEnum.Bancos:#000000}";
                    var dados = _minerthal.ApiRequestServiceAsync(_query, false);
                    var _model = JsonConvert.DeserializeObject<ResponseApiBanco>(dados);
                    if (_model.Details != null)
                    {
                        App.BancoRepository.SaveProduto(_model.Details);
                        success = true;
                    }
                }
                if (tipo.Equals(ApiQueriesIdsEnum.FvPlano))
                {
                    var _query = $"{(byte)ApiQueriesIdsEnum.FvPlano:#000000}";
                    var dados = _minerthal.ApiRequestServiceAsync(_query, false);
                    var _model = JsonConvert.DeserializeObject<ResponseApiPlano>(dados);
                    if (_model.Details != null)
                    {
                        App.PlanosRepository.Save(_model.Details);
                        success = true;
                    }
                }
            }
            catch (Exception)
            {

            }
            return success;
        }


        public int AtualizarBaseDeDadosPrimeiraCarga(AtualizacaoViewModel model)
        {
            totalCalls = 1;
            totalFinished = 0;
            model.TotalClientes = 0;
            model.TotalTbPrecos = 0;
            model.TotalPlanos = 0; 
            model.TotalFaturamento = 0;
            model.TotalPedidos = 0;
            model.TotalProdutos = 0;
            model.TotalHistoricoPedidos = 0;
            model.TotalVendedores = 0;
            model.TotalFiliais = 0;
            model.TotalVisitas = 0;
            model.TotalTitulos = 0;
            model.TotalMetaMensal = 0;
            return CarregarDadosDoApp(model);
        }

        private async Task AtualizarBaseDeDadosPrimeiraCargaEfetiva(AtualizacaoViewModel model)
        {
            try
            {
                var totalCalls = 13;
                var totalFinalised = 1;

                if (!IsLoading)
                {
                    IsLoading = true;
                    model.TotalUsuarios = 2; //#1

                    model.TotalHistoricoPedidos = await CarregarHistoricoPedidoAsync(); //#2
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalResumoPedidos = await CarregarResumoPedidoAsync(); //#3
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalRanking = CarregarRankingAsync();//#4
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalClientes = await CarregarClientesAsync(); //#5
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalPlanos = await CarregarPlanosAsync(); //#6
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalBancos = await CarregarBancosAsync(); //#7
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalTbPrecos = await CarregarTabelaDePrecosAsync(); //#8
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalProdutos = await CarregarProdutos(); //#9
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalFiliais = await CarregarFiliaisAsync(); //#10
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalVendedores = await CarregarVendedoresAsync(); //#11
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalFaturamento = await CarregarFaturamentoAsync(); //#12
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);

                    model.TotalMetaMensal = await CarregarMetaMensalAsync(); //#17
                    totalFinalised += 1;
                    IsLoadingFinised(totalCalls, totalFinalised);



                    //await Task.Delay(1500);
                    //model.TotalPedidos= await CarregarMeusPedidosAsync();//#12
                    //totalFinalised+=1;
                    //IsLoadingFinised(totalCalls, totalFinalised);

                    //var planos = CarregarPlanosAsync().GetAwaiter();
                    //var bancos = CarregarBancosAsync().GetAwaiter();
                    //var produtos = CarregarProdutos().GetAwaiter();
                    //var filiais = CarregarFiliaisAsync().GetAwaiter();
                    //var vendedores = CarregarVendedoresAsync().GetAwaiter();
                    //var faturamento = CarregarFaturamentoAsync().GetAwaiter();
                    //var historicoPedidos = CarregarHistoricoPedidoAsync().GetAwaiter();
                    //var resumoPedidos = CarregarResumoPedidoAsync().GetAwaiter();
                    //var meusPedidos = CarregarMeusPedidosAsync().GetAwaiter();
                    //var precos = CarregarTabelaDePrecos().GetAwaiter();
                    //var ranking = CarregarRankingAsync().GetAwaiter();
                    //var clientes = CarregarClientes().GetAwaiter();

                    //precos.OnCompleted(() => { model.TotalTbPrecos = precos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //produtos.OnCompleted(() => { model.TotalProdutos = produtos.GetResult(); totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //faturamento.OnCompleted(() => { model.TotalFaturamento= faturamento.GetResult(); totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //filiais.OnCompleted(() => { model.TotalFiliais = filiais.GetResult(); totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //meusPedidos.OnCompleted(() => { model.TotalPedidos = meusPedidos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //vendedores.OnCompleted(() => { model.TotalVendedores = vendedores.GetResult(); totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //planos.OnCompleted(() => { model.TotalPlanos = planos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //bancos.OnCompleted(() => { model.TotalBancos = bancos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //historicoPedidos.OnCompleted(() => { model.TotalHistoricoPedidos = historicoPedidos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //resumoPedidos.OnCompleted(() => { model.TotalResumoPedidos = resumoPedidos.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //ranking.OnCompleted(() => { model.TotalRanking = ranking.GetResult();  totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                    //clientes.OnCompleted(() => { model.TotalClientes = clientes.GetResult();totalFinalised+=1; IsLoadingFinised(totalCalls, totalFinalised); });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<int> CarregarTabelaDePrecosAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarTabelaPrecoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.TabelaPrecoRepository.SaveTabelaPreco(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Vendedor.ToString()
                });
            }
            return total;

        }

        public async Task<int> CarregarClientesAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarClienteAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.ClienteRepository.SaveClientes(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Cliente.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarProdutos()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarProdutoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.ProdutosRepository.SaveProduto(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Produtos.ToString()
                });
            }
            return total;

        }

        private async Task<int> CarregarFaturamentoAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarFaturamentoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.FaturamentoRepository.SaveFaturamento(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Faturamento.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarVendedoresAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarVendedoresAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.VendedorRepository.SaveVendedor(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Vendedor.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarPlanosAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarPlanosAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.PlanosRepository.Save(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Planos.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarHistoricoPedidoAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarHistoricoPedidoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.HistoricoPedidoReposity.SaveHistorico(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.HistoricoPedido.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarResumoPedidoAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarResumoPedidoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.ResumoPedidoRepository.SavePedido(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.ResumoPedido.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarFiliaisAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarFilialAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.FilialRepository.SaveFilial(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Ranking.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarBancosAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarBancoAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.BancoRepository.SaveProduto(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Bancos.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarMeusPedidosAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarMeusPedidosAsync();
                App.MeusPedidosRepository.SaveMeusPedidos(lista);
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.MeusPedidos.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarClienteFaturamento()
        {
            var total = 2;

            try
            {
                var clientes = CarregarClientesAsync();
                var faturamento = CarregarFaturamentoAsync();
                await Task.WhenAll(clientes, faturamento);

                total = clientes.Result + faturamento.Result;

                total = total > 0 ? total : 1;
            }
            catch (Exception ex)
            {

                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.ClienteFaturamento.ToString()
                });
            }
            return total;
        }

        private async Task<int> CarregarMetaMensalAsync()
        {
            var total = 2;
            try
            {
                var lista = await PesquisarMetaMensalAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.MetaMensalRepository.SaveMeta(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.MetaMensal.ToString()
                });
            }
            return total;
        }

        private async Task<List<TabelaPreco>> PesquisarTabelaPrecoAsync()
        {
            try
            {
                var obj = new List<TabelaPreco>();

                string queryId = "000002";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiTabelaPreco>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Faturamento>> PesquisarFaturamentoAsync()
        {
            try
            {
                var obj = new List<Faturamento>();

                string queryId = "000004";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiFaturamento>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Plano>> PesquisarPlanosAsync()
        {
            try
            {
                var obj = new List<Plano>();
                string queryId = "000003";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiPlano>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Cliente>> PesquisarClienteAsync()
        {
            try
            {
                var obj = new List<Cliente>();
                string queryId = "000001";
                bool filter = true;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiCliente>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Produto>> PesquisarProdutoAsync()
        {
            try
            {
                var obj = new List<Produto>();

                string queryId = "000006";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiProduto>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Filial>> PesquisarFilialAsync()
        {
            try
            {
                var obj = new List<Filial>();

                string queryId = "000013";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiFilial>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Vendedor>> PesquisarVendedoresAsync()
        {
            try
            {
                var obj = new List<Vendedor>();

                string queryId = "000008";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiVendedor>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<MeusPedidos>> PesquisarMeusPedidosAsync()
        {
            try
            {
                var obj = new List<MeusPedidos>();

                string queryId = "000006";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiMeusPedidos>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }

                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Usuario>> PesquisarMeusUsuariosAsync()
        {
            try
            {
                var obj = new List<Usuario>();

                var model = _minerthal.RecuperarUsuarioAsync();

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiUsuario>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<Ranking> PesquisarRankingAsync()
        {
            try
            {
                var obj = new List<Ranking>();

                var model = _minerthal.RecuperarRankingAsync();
                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiRanking>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<Banco>> PesquisarBancoAsync()
        {
            try
            {
                var obj = new List<Banco>();

                string queryId = "000021";
                bool filter = false;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);
                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiBanco>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<MetaMensal>> PesquisarMetaMensalAsync()
        {
            try
            {
                var obj = new List<MetaMensal>();

                string queryId = "000017";
                bool filter = true;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);
                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiMetaMensal>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<HistoricoDePedidos>> PesquisarHistoricoPedidoAsync()
        {
            try
            {
                var obj = new List<HistoricoDePedidos>();

                string queryId = "000007";
                bool filter = true;
                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);
                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiHistoricoPedido>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<ResumoPedido>> PesquisarResumoPedidoAsync()
        {
            try
            {
                var obj = new List<ResumoPedido>();
                string queryId = "000005";
                bool filter = true;

                string model = _minerthal.ApiRequestServiceAsync(queryId, filter);
                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiResumoPedido>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;

            }
            catch (Exception)
            {
                throw;
            }
        }



        private async Task<List<Usuario>> PesquisarMeusUsuarios()
        {
            try
            {
                var obj = new List<Usuario>();

                var model = _minerthal.RecuperarUsuarioAsync();

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiUsuario>(model);
                    if (_model != null && _model.Details.Any())
                        obj = _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void IsLoadingFinised(int totalCall, int totalFinalized)
        {
            if (totalCall == totalFinalized)
                IsLoading = false;
        }


        public Task AtualizarBaseDeDados()
        {
            return CarregarDadosDoAppAsync();
        }

        bool isFinding = false;
        public async Task<int> CarregarUsuariosAsync()
        {

            var total = 2;
            try
            {
                if (!isFinding)
                {
                    isFinding = true;
                    var lista = await PesquisarMeusUsuarios();
                    if (lista != null && lista.Any())
                        App.UserRepository.SaveUsuers(lista);

                    total = lista != null && lista.Count > 0 ? lista.Count : 2;
                    isFinding = false;
                }
            }
            catch (Exception ex)
            {
                isFinding = false;
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Usuarios.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar os Usuarios. Erro: {ex.Message}", ApiMinertalTypes.Usuarios);
            }

            return total;
        }

        private Task CarregarDadosDoAppAsync()
        {
            int total = 0;
            if (!IsLoading)
            {
                try
                {
                    IsLoading = true;
                    total += 1;

                    var lista = new List<CustomDictionary>();
                    Task.Run(() =>
                    {
                        CarregarRankingAsync();
                    });

                    foreach (ApiQueriesIdsEnum query in (ApiQueriesIdsEnum[])Enum.GetValues(typeof(ApiQueriesIdsEnum)))
                    {
                        var _filter = (byte)query switch
                        {
                            1 => true,
                            7 => true,
                            4 => true,
                            5 => true,
                            _ => false
                        };

                        var _query = $"{(byte)query:#000000}";
                        lista.Add(new CustomDictionary
                        {
                            Key = query.ToName(),
                            StringValue = _query,
                            ByteValue = (byte)query,
                            Filter = _filter
                        });
                    }

                    Task.Run(() =>
                    {
                        CarregarDadosDaApiAsync(lista);
                    });

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    IsLoading = false;
                }
            }
            return Task.CompletedTask;
        }
        private int CarregarDadosDoApp(AtualizacaoViewModel model)
        {
            int total = 0;
            if (!IsLoading)
            {
                var _query = "";
                try
                {
                    IsLoading = true;
                    model.TotalUsuarios = 2;
                    total += 1;

                    var lista = new List<CustomDictionary>();
                    model.TotalRanking = CarregarRankingAsync();
                    total += 1;

                    foreach (ApiQueriesIdsEnum query in (ApiQueriesIdsEnum[])Enum.GetValues(typeof(ApiQueriesIdsEnum)))
                    {
                        var _filter = (byte)query switch
                        {
                            1 => true,
                            7 => true,
                            4 => true,
                            5 => true,
                           17 => true,
                            _ => false
                        };

                        _query = $"{(byte)query:#000000}";
                        lista.Add(new CustomDictionary
                        {
                            Key = query.ToName(),
                            StringValue = _query,
                            ByteValue = (byte)query,
                            Filter = _filter
                        });
                    }
                    total += CarregarDadosDaApi(lista, model);

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    IsLoading = false;
                }
            }
            return total;
        }


        private int CarregarDadosDaApi(List<CustomDictionary> lista, AtualizacaoViewModel viewmodel)
        {
            var cont = 0;
            var model = string.Empty;
            foreach (var item in lista)
            {
                try
                {
                    model = _minerthal.ApiRequestServiceAsync(item.StringValue, item.Filter);

                    if (!string.IsNullOrWhiteSpace(model))
                    {

                        if (item.ByteValue == (byte)1)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiCliente>(model);
                            if (_model.Details != null)
                            {
                                App.ClienteRepository.SaveClientes(_model.Details);
                                viewmodel.TotalClientes = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)2)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiTabelaPreco>(model);
                            if (_model.Details != null)
                            {
                                App.TabelaPrecoRepository.SaveTabelaPreco(_model.Details);
                                viewmodel.TotalTbPrecos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)3)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiPlano>(model);
                            if (_model.Details != null)
                            {
                                App.PlanosRepository.Save(_model.Details);
                                viewmodel.TotalPlanos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)4)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiFaturamento>(model);
                            if (_model.Details != null)
                            {
                                App.FaturamentoRepository.SaveFaturamento(_model.Details);
                                viewmodel.TotalFaturamento = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)5)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiResumoPedido>(model);
                            if (_model.Details != null)
                            {
                                App.ResumoPedidoRepository.SavePedido(_model.Details);
                                viewmodel.TotalPedidos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)6)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiProduto>(model);
                            if (_model.Details != null)
                            {
                                App.ProdutosRepository.SaveProduto(_model.Details);
                                viewmodel.TotalProdutos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)7)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiHistoricoPedido>(model);
                            if (_model.Details != null)
                            {
                                App.HistoricoPedidoReposity.SaveHistorico(_model.Details);
                                viewmodel.TotalHistoricoPedidos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)8)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiVendedor>(model);
                            if (_model.Details != null)
                            {
                                App.VendedorRepository.SaveVendedor(_model.Details);
                                viewmodel.TotalVendedores = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }

                        }
                        else if (item.ByteValue == (byte)9) { }
                        else if (item.ByteValue == (byte)10) { }
                        else if (item.ByteValue == (byte)11) { }
                        else if (item.ByteValue == (byte)12) { }
                        else if (item.ByteValue == (byte)13)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiFilial>(model);
                            if (_model.Details != null)
                            {
                                App.FilialRepository.SaveFilial(_model.Details);
                                viewmodel.TotalFiliais = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)14)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiVisitas>(model);
                            if (_model.Details != null)
                            {
                                App.VisitasRepository.SaveVisitasAsync(_model.Details);
                                viewmodel.TotalVisitas = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)15) { }
                        else if (item.ByteValue == (byte)16) { }
                        else if (item.ByteValue == (byte)17)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiMetaMensal>(model);
                            if (_model.Details != null)
                            {
                                App.MetaMensalRepository.SaveMeta(_model.Details);
                                viewmodel.TotalMetaMensal = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)18) { }
                        else if (item.ByteValue == (byte)19)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiTitulos>(model);
                            if (_model.Details != null)
                            {
                                App.TitulosRepositoy.SaveTitulos(_model.Details);
                                viewmodel.TotalTitulos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                        else if (item.ByteValue == (byte)20) { }
                        else if (item.ByteValue == (byte)21)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiBanco>(model);
                            if (_model.Details != null)
                            {
                                App.BancoRepository.SaveProduto(_model.Details);
                                viewmodel.TotalBancos = _model.Details.Count > 0 ? _model.Details.Count : 1;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Pagina = item.ByteValue.ToString(),
                        Descricao = model
                    });
                    switch (item.ByteValue)
                    {
                        case 1: viewmodel.TotalClientes = 1; break;
                        case 2: viewmodel.TotalTbPrecos = 1; break;
                        case 3: viewmodel.TotalPlanos = 1; break;
                        case 4: viewmodel.TotalFaturamento = 1; break;
                        case 5: viewmodel.TotalPedidos = 1; break;
                        case 6: viewmodel.TotalProdutos = 1; break;
                        case 7: viewmodel.TotalHistoricoPedidos = 1; break;
                        case 8: viewmodel.TotalVendedores = 1; break;
                        case 13: viewmodel.TotalFiliais = 1; break;
                        case 14: viewmodel.TotalVisitas = 1; break;
                        case 17: viewmodel.TotalMetaMensal = 1; break;
                        case 19: viewmodel.TotalTitulos = 1; break;
                        case 21: viewmodel.TotalBancos = 1; break;
                    }
                    continue;
                }
                cont++;
            }
            return cont;
        }
        private Task CarregarDadosDaApiAsync(List<CustomDictionary> lista)
        {
            var model = string.Empty;
            foreach (var item in lista)
            {
                try
                {
                    model = _minerthal.ApiRequestServiceAsync(item.StringValue, item.Filter);

                    if (!string.IsNullOrWhiteSpace(model))
                    {

                        if (item.ByteValue == (byte)1)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiCliente>(model);
                            if (_model.Details != null)
                                App.ClienteRepository.SaveClientes(_model.Details);
                        }
                        else if (item.ByteValue == (byte)2)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiTabelaPreco>(model);
                            if (_model.Details != null)
                                App.TabelaPrecoRepository.SaveTabelaPreco(_model.Details);
                        }
                        else if (item.ByteValue == (byte)3)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiPlano>(model);
                            if (_model.Details != null)
                                App.PlanosRepository.Save(_model.Details);
                        }
                        else if (item.ByteValue == (byte)4)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiFaturamento>(model);
                            if (_model.Details != null)
                                App.FaturamentoRepository.SaveFaturamento(_model.Details);
                        }
                        else if (item.ByteValue == (byte)5)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiResumoPedido>(model);
                            if (_model.Details != null)
                                App.ResumoPedidoRepository.SavePedido(_model.Details);
                        }
                        else if (item.ByteValue == (byte)6)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiProduto>(model);
                            if (_model.Details != null)
                                App.ProdutosRepository.SaveProduto(_model.Details);
                        }
                        else if (item.ByteValue == (byte)7)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiHistoricoPedido>(model);
                            if (_model.Details != null)
                                App.HistoricoPedidoReposity.SaveHistorico(_model.Details);
                        }
                        else if (item.ByteValue == (byte)8)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiVendedor>(model);
                            if (_model.Details != null)
                                App.VendedorRepository.SaveVendedor(_model.Details);

                        }
                        else if (item.ByteValue == (byte)9) { }
                        else if (item.ByteValue == (byte)10) { }
                        else if (item.ByteValue == (byte)11) { }
                        else if (item.ByteValue == (byte)12) { }
                        else if (item.ByteValue == (byte)13)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiFilial>(model);
                            if (_model.Details != null)
                                App.FilialRepository.SaveFilial(_model.Details);
                        }
                        else if (item.ByteValue == (byte)14)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiVisitas>(model);
                            if (_model.Details != null)
                                App.VisitasRepository.SaveVisitasAsync(_model.Details);
                        }
                        else if (item.ByteValue == (byte)15) { }
                        else if (item.ByteValue == (byte)16) { }
                        else if (item.ByteValue == (byte)17)
                        {

                        }
                        else if (item.ByteValue == (byte)18) { }
                        else if (item.ByteValue == (byte)19)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiTitulos>(model);
                            if (_model.Details != null)
                                App.TitulosRepositoy.SaveTitulos(_model.Details);
                        }
                        else if (item.ByteValue == (byte)20) { }
                        else if (item.ByteValue == (byte)21)
                        {
                            var _model = JsonConvert.DeserializeObject<ResponseApiBanco>(model);
                            if (_model.Details != null)
                                App.BancoRepository.SaveProduto(_model.Details);
                        }
                    }
                }
                catch (Exception ex)
                {
                    App.LogRepository.Add(new Log
                    {
                        Data = DateTime.Now,
                        ErrorDetail = ex.Message,
                        Pagina = item.ByteValue.ToString(),
                        Descricao = model
                    });
                    continue;
                }
            }
            return Task.CompletedTask;
        }

        private int CarregarRankingAsync()
        {
            var total = 2;
            try
            {
                var lista = PesquisarRankingAsync();
                total = lista != null && lista.Count > 0 ? lista.Count : 2;
                App.RankingRepository.SaveRanking(lista);
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Ranking.ToString()
                });
            }
            return total;
        }

        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos()
        {
            return await _minerthal.TransmitirPedidos();
        }
        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos(Guid pedidoId)
        {
            return await _minerthal.TransmitirPedidos(pedidoId);
        }
        public void DeleteAllTables(string dropCommand)
        {
            var tableName = "APPTHAL_BASE_V53";
            var exists = App.AtualizacaoRepository.GetByTableName(tableName);

            if (!exists)
            {
                App.AtualizacaoRepository.DeleteAllTables(dropCommand);

                App.AtualizacaoRepository.CriarTabela();
                App.AtualizacaoRepository.Add(new Atualizacoes
                {
                    DataAtualizacao = DateTime.Now,
                    NomeTabela = tableName
                });

                App.LogRepository.CriarTabela();
                App.UserRepository.CriarTabela();
                App.RankingRepository.CriarTabela();
                App.ClienteRepository.CriarTabela();
                App.FaturamentoRepository.CriarTabela();
                App.CartRepository.CriarTabela();
                App.ProdutosRepository.CriarTabela();
                App.PedidoRepository.CriarTabela();
                App.FilialRepository.CriarTabela();
                App.MeusPedidosRepository.CriarTabela();
                App.TabelaPrecoRepository.CriarTabela();
                App.VendedorRepository.CriarTabela();
                App.PlanosRepository.CriarTabela();
                App.ClientePlanoPagamentoRepository.CriarTabela();
                App.BancoRepository.CriarTabela();
                App.HistoricoPedidoReposity.CriarTabela();
                App.ResumoPedidoRepository.CriarTabela();
                App.TitulosRepositoy.CriarTabela();
                App.VisitasRepository.CriarTabela();
                App.MetaMensalRepository.CriarTabela();
            }
        }
    }
}
