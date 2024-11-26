using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models.Enums;
using MinerthalSalesApp.ViewModels.Startup;
using Newtonsoft.Json;

namespace MinerthalSalesApp.Infra.Services
{
    public partial class ServicoDeCarregamentoDasBasesBackUP
    {
        private readonly IMinerthalApiServices _minerthal;
        public ServicoDeCarregamentoDasBasesBackUP(IMinerthalApiServices minerthalApiServices)
        {
            _minerthal = minerthalApiServices ?? throw new ArgumentNullException(nameof(minerthalApiServices));
        }

        bool IsLoading { get; set; } = false;

        public async Task AtualizarBaseDeDados(ApiMinertalTypes tipo)
        {
            try
            {
                switch (tipo)
                {
                    case ApiMinertalTypes.BaseDeDados: await AtualizarBaseDeDados(); break;
                    case ApiMinertalTypes.ClienteFaturamento: await CarregarClienteFaturamento(); break;
                    case ApiMinertalTypes.Produtos: await CarregarProdutosAsync(); break;
                    case ApiMinertalTypes.Ranking: await CarregarRankingAsync(); break;
                    case ApiMinertalTypes.Filiais: await CarregarFiliaisAsync(); break;
                    case ApiMinertalTypes.MeusPedidos: await CarregarMeusPedidosAsync(); break;
                    case ApiMinertalTypes.TabelaDePrecos: await CarregarTabelaDePrecosAsync(); break;
                    case ApiMinertalTypes.Planos: await CarregarPlanosAsync(); break;
                    case ApiMinertalTypes.Bancos: await CarregarBancosAsync(); break;
                    case ApiMinertalTypes.Usuarios: await CarregarUsuariosAsync(); break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AtualizarBaseDeDadosPrimeiraCarga(AtualizacaoViewModel model)
        {
            await AtualizarBaseDeDadosPrimeiraCargaEfetiva(model);
        }

        private async Task AtualizarBaseDeDadosPrimeiraCargaEfetivaOld(AtualizacaoViewModel model)
        {
            var errorsList = new List<string>();
            var totalCalls = 13;
            var totalFinalised = 1;
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;
                    //await CarregarUsuariosAsync();
                    model.TotalUsuarios = 2;// App.UserRepository.GetTotal();
                    try
                    {
                        _ = CarregarRankingAsync();
                        var totalRanking = 2;
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalRanking = totalRanking;// App.RankingRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalRanking = 2;
                    }

                    try
                    {
                        var totalClientes = await CarregarClientesAsync();
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalClientes = totalClientes;// App.ClienteRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalClientes = 2;
                    }

                    try
                    {
                        var totalTabela = await CarregarTabelaDePrecosAsync();
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalTbPrecos = totalTabela;//; App.TabelaPrecoRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalTbPrecos = 2;
                    }

                    try
                    {
                        _ = CarregarFaturamentoAsync();
                        var totalFaturamentos = 2;
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFaturamento = totalFaturamentos;
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFaturamento = 2;
                    }

                    try
                    {
                        var totalProdutos = await CarregarProdutosAsync();
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalProdutos = totalProdutos;//App.ProdutosRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalProdutos = 2;
                    }

                    try
                    {
                        var totalFiliais = await CarregarFiliaisAsync();
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFiliais = totalFiliais;// App.FilialRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFiliais = 2;
                    }

                    try
                    {

                        _ = CarregarMeusPedidosAsync();
                        var totalPedidos = 2;
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPedidos = totalPedidos;// App.PedidoRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPedidos = 2;
                    }

                    try
                    {
                        var totalVendedores = await CarregarVendedoresAsync();
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalVendedores = totalVendedores;// App.VendedorRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalVendedores = 2;
                    }

                    try
                    {

                        var totalPlanos = await CarregarPlanosAsync();
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPlanos = totalPlanos;// App.PlanosRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPlanos = 2;
                    }

                    try
                    {
                        var totalBancos = await CarregarBancosAsync();
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalBancos = totalBancos;// App.BancoRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalBancos = 2;
                    }

                    try
                    {
                        _ = CarregarHistoricoPedidoAsync();
                        var totalHistoricoPedido = 2;
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalHistoricoPedidos = totalHistoricoPedido;// App.HistoricoPedidoReposity.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalHistoricoPedidos = 2;
                    }

                    try
                    {
                        _ = CarregarResumoPedidoAsync();
                        var totalResumoPedido = 2;
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalResumoPedidos = totalResumoPedido;// App.ResumoPedidoRepository.GetTotal();
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalResumoPedidos = 2;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                {
                    var exc = (CustomExceptions)ex;

                    if (exc.ErroLancadoPor == ApiMinertalTypes.Bancos)
                        model.ImageSourceLoadingBanco = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Filiais)
                        model.ImageSourceLoadingFilial = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Usuarios)
                        model.ImageSourceLoadingUsuario = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.TabelaDePrecos)
                        model.ImageSourceLoadingPreco = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Planos)
                        model.ImageSourceLoadingPlano = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Cliente)
                        model.ImageSourceLoadingCliente = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Vendedor)
                        model.ImageSourceLoadingVendedor = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Ranking)
                        model.ImageSourceLoadingRanking = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.Produtos)
                        model.ImageSourceLoadingProduto = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.HistoricoPedido)
                        model.ImageSourceLoadingHistoricoPedido = model.imagError;
                    else if (exc.ErroLancadoPor == ApiMinertalTypes.ResumoPedido)
                        model.ImageSourceLoadingResumoPedido = model.imagError;

                    model.TotalResumoPedidos += 1;
                }

            }
        }

        public async Task<List<string>> AtualizarBaseDeDados()
        {
            var errorsList = new List<string>();
            var totalCalls = 11;
            var totalFinalised = 0;
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;

                    await CarregarUsuariosAsync();


                    var clientes = CarregarClientesAsync().GetAwaiter();
                    var faturamentos = CarregarFaturamentoAsync().GetAwaiter();
                    var produtos = CarregarProdutosAsync().GetAwaiter();
                    var filiais = CarregarFiliaisAsync().GetAwaiter();
                    var ranking = CarregarRankingAsync().GetAwaiter();
                    var meusPedidos = CarregarMeusPedidosAsync().GetAwaiter();
                    var vendedores = CarregarVendedoresAsync().GetAwaiter();
                    var planos = CarregarPlanosAsync().GetAwaiter();
                    var bancos = CarregarBancosAsync().GetAwaiter();
                    var tabPrecos = _ = CarregarTabelaDePrecosAsync().GetAwaiter();


                    clientes.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    tabPrecos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    faturamentos.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                    });
                    produtos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    filiais.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    ranking.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    meusPedidos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    vendedores.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    planos.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    bancos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                }

                return errorsList;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task AtualizarBaseDeDadosPrimeiraCargaEfetiva(AtualizacaoViewModel model)
        {
            var errorsList = new List<string>();
            var totalCalls = 12;
            var totalFinalised = 0;
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;

                    //await CarregarUsuariosAsync();
                    model.TotalUsuarios = 1; App.UserRepository.GetTotal();

                    var tabPrecos = CarregarTabelaDePrecosAsync();
                    var clientes = CarregarClientesAsync();
                    var faturamentos = CarregarFaturamentoAsync();
                    var produtos = CarregarProdutosAsync();
                    var filiais = CarregarFiliaisAsync();
                    var ranking = CarregarRankingAsync();
                    var meusPedidos = CarregarMeusPedidosAsync();
                    var vendedores = CarregarVendedoresAsync();
                    var planos = CarregarPlanosAsync();
                    var bancos = CarregarBancosAsync();
                    var historicoPedidos = CarregarHistoricoPedidoAsync();
                    var resumoPedido = CarregarResumoPedidoAsync();

                    var EndPoints = new List<Task<int>> {
                      tabPrecos,
                    clientes,
                    faturamentos,
                    produtos,
                    filiais,
                    ranking,
                    meusPedidos,
                    vendedores,
                    planos,
                    bancos,
                    historicoPedidos,
                    resumoPedido
                    };

                    await Task.WhenAll(EndPoints);



                    try
                    {
                        if (tabPrecos.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalTbPrecos = tabPrecos.Result;// App.TabelaPrecoRepository.GetTotal();
                        }
                        //tabPrecos.OnCompleted(() =>
                        //{
                        //    if (tabPrecos.IsCompleted)
                        //    {
                        //        totalFinalised+=1;
                        //        IsLoadingFinised(totalCalls, totalFinalised);
                        //        model.TotalTbPrecos =tabPrecos.GetResult();// App.TabelaPrecoRepository.GetTotal();
                        //    }

                        //});
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalTbPrecos = 1;
                    }

                    try
                    {
                        if (clientes.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalClientes = clientes.Result;// App.ClienteRepository.GetTotal();
                        }
                        //clientes.OnCompleted(() =>{});

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalClientes = 1;
                    }

                    try
                    {

                        //faturamentos.OnCompleted(() =>  {});

                        if (faturamentos.IsCompleted)
                        {
                            totalFinalised += 1; //IsLoading.Finised(totalCalls, totalFinalised);
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalFaturamento = faturamentos.Result;
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFaturamento = 1;
                    }

                    try
                    {
                        //produtos.OnCompleted(() => { });
                        if (produtos.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalProdutos = produtos.Result;// App.ProdutosRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalProdutos = 1;
                    }

                    try
                    {
                        //filiais.OnCompleted(() =>{ });
                        if (filiais.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalFiliais = filiais.Result;// App.FilialRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalFiliais = 1;
                    }

                    try
                    {
                        //ranking.OnCompleted(() =>{  });
                        if (produtos.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalRanking = ranking.Result;// App.RankingRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalRanking = 1;
                    }

                    try
                    {
                        //meusPedidos.OnCompleted(() =>{ });
                        if (produtos.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalPedidos = meusPedidos.Result;// App.PedidoRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPedidos = 1;
                    }

                    try
                    {
                        //vendedores.OnCompleted(() =>{ });
                        if (vendedores.IsCompleted)
                        {
                            totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalVendedores = vendedores.Result;// App.VendedorRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalVendedores = 1;
                    }

                    try
                    {
                        //planos.OnCompleted(() =>{ });
                        if (planos.IsCompleted)
                        {
                            totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalPlanos = planos.Result;// App.PlanosRepository.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalPlanos = 1;
                    }

                    try
                    {
                        if (bancos.IsCompleted)
                        {
                            //bancos.OnCompleted(() =>{ });
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalBancos = bancos.Result;// App.BancoRepository.GetTotal();

                        }
                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalBancos = 1;
                    }

                    try
                    {
                        //historicoPedidos.OnCompleted(() =>{ });
                        if (historicoPedidos.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalHistoricoPedidos = historicoPedidos.Result;// App.HistoricoPedidoReposity.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalHistoricoPedidos = 1;
                    }

                    try
                    {
                        //resumoPedido.OnCompleted(() =>{ });
                        if (resumoPedido.IsCompleted)
                        {
                            totalFinalised += 1;
                            IsLoadingFinised(totalCalls, totalFinalised);
                            model.TotalResumoPedidos = resumoPedido.Result;// App.HistoricoPedidoReposity.GetTotal();
                        }

                    }
                    catch (Exception)
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                        model.TotalResumoPedidos = 1;
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex is CustomExceptions)
                {
                    var exc = (CustomExceptions)ex;

                    switch (exc.ErroLancadoPor)
                    {
                        case ApiMinertalTypes.Bancos:
                            model.ImageSourceLoadingBanco = model.imagError;
                            break;
                        case ApiMinertalTypes.Filiais:
                            model.ImageSourceLoadingFilial = model.imagError;
                            break;
                        case ApiMinertalTypes.Usuarios:
                            model.ImageSourceLoadingUsuario = model.imagError;
                            break;
                        case ApiMinertalTypes.TabelaDePrecos:
                            model.ImageSourceLoadingPreco = model.imagError;
                            break;
                        case ApiMinertalTypes.Planos:
                            model.ImageSourceLoadingPlano = model.imagError;
                            break;
                        case ApiMinertalTypes.Cliente:
                            model.ImageSourceLoadingCliente = model.imagError;
                            break;
                        case ApiMinertalTypes.Vendedor:
                            model.ImageSourceLoadingVendedor = model.imagError;
                            break;
                        case ApiMinertalTypes.Ranking:
                            model.ImageSourceLoadingRanking = model.imagError;
                            break;
                        case ApiMinertalTypes.Produtos:
                            model.ImageSourceLoadingProduto = model.imagError;
                            break;
                        case ApiMinertalTypes.HistoricoPedido:
                            model.ImageSourceLoadingHistoricoPedido = model.imagError;
                            break;
                    }

                }
            }
            //return Task.CompletedTask;
        }
        public async Task<List<string>> AtualizarBaseDeDadosOld2()
        {
            var errorsList = new List<string>();
            var totalCalls = 11;
            var totalFinalised = 0;
            try
            {
                if (!IsLoading)
                {
                    IsLoading = true;

                    // await CarregarUsuariosAsync();

                    var clientes = CarregarClientesAsync().GetAwaiter();
                    var faturamentos = CarregarFaturamentoAsync().GetAwaiter();
                    var produtos = CarregarProdutosAsync().GetAwaiter();
                    var filiais = CarregarFiliaisAsync().GetAwaiter();
                    var ranking = CarregarRankingAsync().GetAwaiter();
                    var meusPedidos = CarregarMeusPedidosAsync().GetAwaiter();
                    var vendedores = CarregarVendedoresAsync().GetAwaiter();
                    var planos = CarregarPlanosAsync().GetAwaiter();
                    var bancos = CarregarBancosAsync().GetAwaiter();
                    var tabPrecos = _ = CarregarTabelaDePrecosAsync().GetAwaiter();

                    clientes.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    tabPrecos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    faturamentos.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);
                    });
                    produtos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    filiais.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    ranking.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    meusPedidos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    vendedores.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    planos.OnCompleted(() =>
                    {
                        totalFinalised += 1; IsLoadingFinised(totalCalls, totalFinalised);

                    });
                    bancos.OnCompleted(() =>
                    {
                        totalFinalised += 1;
                        IsLoadingFinised(totalCalls, totalFinalised);

                    });
                }

                return errorsList;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task<int> CarregarClientesAsync()
        {
            try
            {
                var cliente = await PesquisarCliente();
                App.ClienteRepository.SaveClientes(cliente);
                return cliente.Count;
            }
            catch (Exception ex)
            {
                throw new CustomExceptions($"Erro ao carregar os clientes. Erro: {ex.Message}", ApiMinertalTypes.Cliente);
            }
        }

        public async Task<int> CarregarUsuariosAsync()
        {
            try
            {
                var lista = await PesquisarMeusUsuarios();
                if (lista != null && lista.Any())
                    App.UserRepository.SaveUsuers(lista);

                return lista != null ? lista.Count : 0;
            }
            catch (Exception ex)
            {
                throw new CustomExceptions($"Erro ao carregar os Usuarios. Erro: {ex.Message}", ApiMinertalTypes.Usuarios);
            }
        }

        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos()
        {
            return await _minerthal.TransmitirPedidos();
        }

        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos(Guid pedidoId)
        {
            return await _minerthal.TransmitirPedidos(pedidoId);
        }

        #region ::.MÉTODOS PRIVADOS .::
        private async Task<int> CarregarFaturamentoAsync()
        {
            var total = 0;
            try
            {
                var fatura = await PesquisarFaturamento();
                App.FaturamentoRepository.SaveFaturamento(fatura);
                return fatura != null ? fatura.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Faturamento.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar o faturamento. Erro: {ex.Message}", ApiMinertalTypes.Faturamento);
            }
            return total;
        }

        private async Task<int> CarregarProdutosAsync()
        {
            var total = 0;
            try
            {
                var produtos = await PesquisarProduto();
                App.ProdutosRepository.SaveProduto(produtos);
                total = produtos != null ? produtos.Count : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Produtos.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar os produtos. Erro: {ex.Message}", ApiMinertalTypes.Produtos);
            }
            return total;
        }

        private async Task<int> CarregarFiliaisAsync()
        {
            var total = 0;
            try
            {
                var filiais = await PesquisarFilial();
                App.FilialRepository.SaveFilial(filiais);
                total = filiais != null ? filiais.Count : 0;
            }
            catch (Exception ex)

            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Ranking.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar as filiais. Erro: {ex.Message}", ApiMinertalTypes.Filiais);
            }
            return total;
        }

        private async Task<int> CarregarRankingAsync()
        {
            var total = 0;
            try
            {
                var ranking = await PesquisarRanking();
                App.RankingRepository.SaveRanking(ranking);
                total = ranking != null ? ranking.Count : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Ranking.ToString()
                });
                throw new CustomExceptions($"Erro ao carregar o ranking. Erro: {ex.Message}", ApiMinertalTypes.Ranking);
            }
            return total;
        }

        private async Task<int> CarregarMeusPedidosAsync()
        {
            var total = 0;
            try
            {
                var pedidos = await PesquisarMeusPedidos();
                App.MeusPedidosRepository.SaveMeusPedidos(pedidos);
                return pedidos != null ? pedidos.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.MeusPedidos.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar os meus pedidos. Erro: {ex.Message}", ApiMinertalTypes.MeusPedidos);
            }
            return total;
        }

        private async Task<int> CarregarTabelaDePrecosAsync()
        {
            var total = 0;
            try
            {
                var lista = await PesquisarTabelaPreco();
                App.TabelaPrecoRepository.SaveTabelaPreco(lista);
                total = lista != null ? lista.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Vendedor.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar a tabela de preços. Erro: {ex.Message}", ApiMinertalTypes.TabelaDePrecos);
            }
            return total;
        }

        private async Task<int> CarregarVendedoresAsync()
        {
            var total = 0;
            try
            {
                var lista = await PesquisarVendedores();
                App.VendedorRepository.SaveVendedor(lista);
                total = lista != null ? lista.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Vendedor.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar os vendedores. Erro: {ex.Message}", ApiMinertalTypes.Vendedor);
            }
            return total;
        }

        private async Task<int> CarregarPlanosAsync()
        {
            var total = 0;
            try
            {
                var lista = await PesquisarPlanos();
                App.PlanosRepository.Save(lista);
                total = lista != null ? lista.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Planos.ToString()
                });
                //throw new CustomExceptions($"Erro ao carregar os planos. Erro: {ex.Message}", ApiMinertalTypes.Bancos);
            }
            return total;
        }

        private async Task<int> CarregarBancosAsync()
        {
            var total = 0;
            try
            {
                var lista = await PesquisarBanco();
                App.BancoRepository.SaveProduto(lista);
                total = lista != null ? lista.Count() : 0;
            }
            catch (Exception ex)
            {
                App.LogRepository.Add(new Log
                {
                    Data = DateTime.Now,
                    Descricao = ex.Message,
                    Pagina = ApiMinertalTypes.Bancos.ToString()
                });
                //   throw new CustomExceptions($"Erro ao carregar o banco. Erro: {ex.Message}", ApiMinertalTypes.Bancos);
            }
            return total;
        }

        private async Task<int> CarregarClienteFaturamento()
        {
            var total = 0;

            try
            {
                var clientes = CarregarClientesAsync();
                var faturamento = CarregarFaturamentoAsync();
                await Task.WhenAll(clientes, faturamento);

                total = clientes.Result + faturamento.Result;
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

        private async Task<int> CarregarHistoricoPedidoAsync()
        {
            var lista = new List<HistoricoDePedidos>();
            try
            {
                lista = await PesquisarHistoricoPedidoAsync();
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
                //throw new CustomExceptions($"Erro ao carregar a listagem de pedidos. Erro: {ex.Message}", ApiMinertalTypes.HistoricoPedido);
            }
            return lista != null ? lista.Count() : 0;
        }

        private async Task<int> CarregarResumoPedidoAsync()
        {
            var lista = new List<ResumoPedido>();
            try
            {
                lista = await PesquisarResumoPedidoAsync();
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
                //throw new CustomExceptions($"Erro ao carregar a listagem de resumo dos pedidos. Erro: {ex.Message}", ApiMinertalTypes.ResumoPedido);
            }
            return lista != null ? lista.Count() : 0;
        }

        private async Task<List<Plano>> PesquisarPlanos()
        {
            try
            {
                var obj = new List<Plano>();
                string queryId = "000003";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Cliente>> PesquisarCliente()
        {
            try
            {
                var obj = new List<Cliente>();
                string queryId = "000001";
                bool filter = true;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Produto>> PesquisarProduto()
        {
            try
            {
                var obj = new List<Produto>();

                string queryId = "000006";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Filial>> PesquisarFilial()
        {
            try
            {
                var obj = new List<Filial>();

                string queryId = "000013";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<TabelaPreco>> PesquisarTabelaPreco()
        {
            try
            {
                var obj = new List<TabelaPreco>();

                string queryId = "000002";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Vendedor>> PesquisarVendedores()
        {
            try
            {
                var obj = new List<Vendedor>();

                string queryId = "000008";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Faturamento>> PesquisarFaturamento()
        {
            try
            {
                var obj = new List<Faturamento>();

                string queryId = "000004";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<MeusPedidos>> PesquisarMeusPedidos()
        {
            try
            {
                var obj = new List<MeusPedidos>();

                string queryId = "000006";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);

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

        private async Task<List<Ranking>> PesquisarRanking()
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

        private async Task<List<Banco>> PesquisarBanco()
        {
            try
            {
                var obj = new List<Banco>();

                string queryId = "000021";
                bool filter = false;
                string model = RecuperarDadosDaApi(queryId, filter);
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

        private async Task<List<HistoricoDePedidos>> PesquisarHistoricoPedidoAsync()
        {
            try
            {
                var obj = new List<HistoricoDePedidos>();

                string queryId = "000007";
                bool filter = true;
                string model = RecuperarDadosDaApi(queryId, filter);
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

                string model = RecuperarDadosDaApi(queryId, filter);
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

        private string RecuperarDadosDaApi(string queryId, bool filter)
        {
            try
            {
                return _minerthal.ApiRequestServiceAsync(queryId, filter);
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

        #endregion
    }
}

