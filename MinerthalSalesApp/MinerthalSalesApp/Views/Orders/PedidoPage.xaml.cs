using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Infra.Services;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Dashboard;
using MinerthalSalesApp.Views.Ranking;
using Newtonsoft.Json;
using System.Globalization;


namespace MinerthalSalesApp.Views.Orders;

public partial class PedidoPage : ContentPage, IAsyncInitialization
{
    public Task Initialization { get; private set; }
    PedidoViewModel _model;
    CultureInfo cultureInfo = new CultureInfo("pt-BR");

    public PedidoPage(PedidoViewModel viewmodel)
    {
        if (viewmodel == null)
        {
            if (App.PedidoModel != null)
                viewmodel = App.PedidoModel;

        }
        else
        {
            App.PedidoModel = viewmodel;
        }

        _model = viewmodel;
        BindingContext = _model;
        InitializeComponent();
        Cliente.Text = $"{_model.Cliente.A1Cod}-{_model.Cliente.A1Loja}";


        Initialization = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        CarregarDadosSelect();
        ModifyEntry();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (_model.ListaFiliais == null || _model.ListaFiliais.Count <= 0)
            if (_model.AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum.FiliaisMinerthal))
                _model.CarregarListagens();

        if (_model.ListaTipoCobranca == null || _model.ListaTipoCobranca.Count <= 0)
            if (_model.AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum.Bancos))
                _model.CarregarListagens();

        if (_model.ListaPlanoPagamento == null || _model.ListaPlanoPagamento.Count <= 0)
            if (_model.AtualizarBaseDeDadosPedido(ApiQueriesIdsEnum.FvPlano))
                _model.CarregarListagens();


    }

    private bool _backPressed;

    protected override bool OnBackButtonPressed()
    {
        _backPressed = true;
        return true; //true or false does not make any difference.
    }

    public bool CanNavigate()
    {
        if (!_backPressed)
        {
            return true;
        }

        _backPressed = false;
        return false;
    }


    private void CarregarDadosSelect()
    {
        try
        {
            if (!Guid.Empty.Equals(_model.Pedido.Id))
            {
                if (!string.IsNullOrWhiteSpace(_model.Pedido.FilialMinerthal))
                    Filial.SelectedItem = ((List<DictionaryDto>)Filial.ItemsSource).FirstOrDefault(c => c.Key == _model.Pedido.FilialMinerthal.ToString());
                else
                    Filial.SelectedItem = ((List<DictionaryDto>)Filial.ItemsSource).FirstOrDefault(c => c.Key == "02");

                if (!string.IsNullOrWhiteSpace(_model.Pedido.TipoPedido))
                    TipoPedido.SelectedItem = ((List<DictionaryDto>)TipoPedido.ItemsSource).FirstOrDefault(c => c.Key == _model.Pedido.TipoPedido);
                else
                    TipoPedido.SelectedItem = ((List<DictionaryDto>)TipoPedido.ItemsSource).FirstOrDefault(c => c.Key == "1");

                if (!string.IsNullOrWhiteSpace(_model.Pedido.TipoVenda))
                    TipoVenda.SelectedItem = ((List<DictionaryDto>)TipoVenda.ItemsSource).FirstOrDefault(c => c.Key == _model.Pedido.TipoVenda);
                else
                    TipoVenda.SelectedItem = ((List<DictionaryDto>)TipoVenda.ItemsSource).FirstOrDefault(c => c.Key == "2");

                if (!string.IsNullOrWhiteSpace(_model.Pedido.PlanoPagamento))
                    PlanoPagamento.SelectedItem = ((List<DictionaryDto>)PlanoPagamento.ItemsSource).FirstOrDefault(c => c.Key == _model.Pedido.PlanoPagamento);
                else
                    PlanoPagamento.SelectedItem = ((List<DictionaryDto>)PlanoPagamento.ItemsSource).FirstOrDefault(c => c.Key == "20C");

                if (!string.IsNullOrWhiteSpace(_model.Pedido.TipoCobranca))
                    TipoCobranca.SelectedItem = ((List<DictionaryDto>)TipoCobranca.ItemsSource).FirstOrDefault(c => c.Key == _model.Pedido.TipoCobranca);
                else
                    TipoCobranca.SelectedItem = ((List<DictionaryDto>)TipoCobranca.ItemsSource).FirstOrDefault(c => c.Key == "04");




                if (_model.PlanoPadraoCliente != null)
                {
                    _model.Pedido.PercentualTaxaPlano = _model.PlanoPadraoCliente.TxPerFin;
                    _model.Pedido.PercentualDesconto = _model.PlanoPadraoCliente.VlDescpl;
                    if (_model.Pedido.ItensPedido.Any())
                    {
                        _model.Pedido.ItensPedido.Select(c =>
                        {
                            c.TaxaPlano = _model.PlanoPadraoCliente.TxPerFin;
                            c.Desconto = _model.PlanoPadraoCliente.VlDescpl;
                            return c;
                        }).ToList();
                    }
                }


                if (!string.IsNullOrWhiteSpace(_model.Pedido.Observacao))
                    TxtObservacao.Text = _model.Pedido.Observacao;


                if (_model.Pedido.ValorFrete25 > 0)
                    VlFreteSc25k.Text = _model.Pedido.ValorFrete25.ToString(cultureInfo);
                else
                    VlFreteSc25k.Text = "0,00";

                if (_model.Pedido.ValorFrete30 > 0)
                    VlFreteSc30k.Text = _model.Pedido.ValorFrete30.ToString(cultureInfo);
                else
                    VlFreteSc30k.Text = "0,00";

                if (_model.Pedido.ItensPedido.Any())
                {
                    QtdProdutos.Text = _model.Pedido.ItensPedido.Sum(x => (x.Quantidade)).ToString();
                    var subtotal = _model.Pedido.ItensPedido.Sum(x => (x.Quantidade * x.ValorCombinado));
                    var subtotalFrete = _model.Pedido.ItensPedido.Sum(x => (x.Quantidade * x.FreteUnidade));
                    var totalEncargos = 0M;
                    var totalDescontos = 0M;
                    if (_model.PlanoPadraoCliente != null)
                    {
                        if (_model.PlanoPadraoCliente.TxPerFin > 0)
                        {
                            if (_model.PlanoPadraoCliente.CdPlano.Equals("88"))
                            {
                                var parcelas = _model.Pedido.Parcelas; // Supondo que seja sua lista de parcelas

                                DateTime dataReferencia = DateTime.Today;

                                // Variável para somar os dias de diferença
                                decimal somaDias = 0;

                                // Para cada parcela, calcular a diferença de dias
                                foreach (var item in parcelas)
                                {
                                    DateTime dataConvertida = DateTime.ParseExact(item.Value.Substring(0, 10), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                    // Calcular a diferença de dias entre a data da parcela e a data de referência (primeira parcela)
                                    decimal diasDiferenca = (decimal)(dataConvertida - dataReferencia).TotalDays;

                                    // Adicionar à soma dos dias
                                    somaDias += diasDiferenca;
                                }

                                // Calcular o prazo médio
                                decimal prazoMedio = somaDias / parcelas.Count;
                                totalEncargos = (subtotal * (1 + (_model.PlanoPadraoCliente.TxPerFin * prazoMedio) / 100)) - subtotal;
                            }
                            else
                            {
                                totalEncargos = ((subtotal + subtotalFrete) * (_model.PlanoPadraoCliente.TxPerFin / 100));
                            }
                        }

                        if (_model.PlanoPadraoCliente.VlDescpl > 0)
                            totalDescontos = (subtotal) - ((subtotal) / (1 + (_model.PlanoPadraoCliente.VlDescpl / 100)));
                    }


                    Subtotal.Text = subtotal.ToString("c", cultureInfo);
                    TotalFrete.Text = subtotalFrete.ToString("c", cultureInfo);
                    TotalEncargos.Text = totalEncargos.ToString("c", cultureInfo);
                    TotalDescontos.Text = totalDescontos.ToString("c", cultureInfo);
                    TotalPedido.Text = (subtotal + subtotalFrete + totalEncargos - totalDescontos).ToString("c", cultureInfo);

                    if (Filial.SelectedIndex >= 0)
                    {
                        var _filial = (DictionaryDto)Filial.ItemsSource[Filial.SelectedIndex];
                        FilialEntry.Text = _filial.Value;
                        FilialEntry.IsVisible = true;
                        Filial.IsVisible = false;
                    }

                    if (TipoPedido.SelectedIndex >= 0)
                    {
                        var _tipo = (DictionaryDto)TipoPedido.ItemsSource[TipoPedido.SelectedIndex];
                        TipoPedidoEntry.Text = _tipo.Value;
                        TipoPedidoEntry.IsVisible = true;
                        TipoPedido.IsVisible = false;
                    }


                    if (TipoVenda.SelectedIndex >= 0)
                    {
                        var _tipo = (DictionaryDto)TipoVenda.ItemsSource[TipoVenda.SelectedIndex];
                        TipoVendaEntry.Text = _tipo.Value;
                        TipoVendaEntry.IsVisible = true;
                        TipoVenda.IsVisible = false;
                    }

                    if (TipoCobranca.SelectedIndex >= 0)
                    {
                        var _tipo = (DictionaryDto)TipoCobranca.ItemsSource[TipoCobranca.SelectedIndex];
                        TipoCobrancaEntry.Text = _tipo.Value;
                        TipoCobrancaEntry.IsVisible = true;
                        TipoCobranca.IsVisible = false;
                    }

                    if (PlanoPagamento.SelectedIndex >= 0)
                    {
                        var _tipo = (DictionaryDto)PlanoPagamento.ItemsSource[PlanoPagamento.SelectedIndex];
                        PlanoPagamentoEntry.Text = _tipo.Value;
                        PlanoPagamentoEntry.IsVisible = true;
                        PlanoPagamento.IsVisible = false;
                    }


                    VlFreteSc25k.IsReadOnly = true;
                    VlFreteSc30k.IsReadOnly = true;
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("MyCustomization", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetSelectAllOnFocus(true);
#elif IOS || MACCATALYST
            handler.PlatformView.EditingDidBegin += (s, e) =>
            {
                handler.PlatformView.PerformSelector(new ObjCRuntime.Selector("selectAll"), null, 0.0f);
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
            };
#elif WINDOWS
            handler.PlatformView.GotFocus += (s, e) =>
            {
                handler.PlatformView.SelectAll();
            };
#endif
        });
    }

    private void BtnPrincipal_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        ResetVisible();
        geralview.IsVisible = true;

        BtnPrincipal.BorderColor = Color.FromArgb("#5d5d5d");
        BtnPrincipal.BorderWidth = 4;
    }

    private void BtnObservacoes_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        ResetVisible();
        observacaoview.IsVisible = true;
        BtnObservacoes.BorderColor = Color.FromArgb("#5d5d5d");
        BtnObservacoes.BorderWidth = 4;
    }

    private void BtnAcompanhamento_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        ResetVisible();
        acompanhamentoview.IsVisible = true;
        BtnAcompanhamento.BorderColor = Color.FromArgb("#5d5d5d");
        BtnAcompanhamento.BorderWidth = 4;
    }

    private void Button_AddProduto_Clicked(object sender, EventArgs e)
    {
        SetSelectedBorder();
        ResetVisible();
    }

    private void BtnVisualizarCarrinho_Clicked(object sender, EventArgs e)
    {


        SetSelectedBorder();
        ResetVisible();
        ItensCarrinho.IsVisible = true;

        BtnVisualizarCarrinho.BorderColor = Color.FromArgb("#5d5d5d");
        BtnVisualizarCarrinho.BorderWidth = 4;
    }

    //FINALIZAR PEDIDO
    private async void BtnFinalizarPedido_Clicked(object sender, EventArgs e)
    {
        var _popmodel = new PopupViewModel
        {
            PopupMessage = "Finalizando pedido, aguarde..."
        };
        var pop = new PopupPage(_popmodel);
        this.ShowPopup(pop);

        try
        {
            if (!_model.Pedido.ItensPedido.Any())
                throw new CustomExceptions("Não há produtos no carrinho");

            RecalcularCarrinho();
            var clId = _model.Cliente.A1Cod;

            var _pedido = new Pedido
            {
                CodigoCliente = _model.Cliente.A1Cod,
                CodigoLoja = _model.Cliente.A1Loja,
                FilialMinerthal = _model.Pedido.FilialMinerthal,
                PlanoPagamento = _model.Pedido.PlanoPagamento,
                TipoCobranca = _model.Pedido.TipoCobranca,
                TipoPedido = _model.Pedido.TipoPedido,
                TipoVenda = _model.Pedido.TipoVenda,
                ValorFrete25 = _model.Pedido.ValorFrete25,
                ValorFrete30 = _model.Pedido.ValorFrete30,
                Parcelas = _model.Pedido.Parcelas.Count() > 0
                            ? JsonConvert.SerializeObject(_model.Pedido.Parcelas)
                            : default,
                PercentualDesconto = _model.PlanoPadraoCliente.VlDescpl,
                PercentualJuros = _model.PlanoPadraoCliente.TxPerFin,
                Observacao = TxtObservacao.Text,
                NomeFilial = _model.Pedido.NomeFilial,
                NomeTipo = _model.Pedido.NomeTipo,
                NomeTipoVenda = _model.Pedido.NomeTipoVenda,
                NomeTipoCobranca = _model.Pedido.NomeTipoCobranca,
                NomePlanoPagamento = _model.Pedido.NomePlanoPagamento
            };
            foreach (var iten in _model.Pedido.ItensPedido)
            {
                var totalEncargos = 0M;
                if (_model.PlanoPadraoCliente.CdPlano.Equals("88"))
                {
                    var parcelas = _model.Pedido.Parcelas; // Supondo que seja sua lista de parcelas

                    // Extrair a data da primeira parcela para ser a data de referência
                    string primeiraDataIngles = parcelas.First().Value.Length == 9
                        ? parcelas.First().Value.Substring(0, 9)
                        : parcelas.First().Value.Substring(0, 10);

                    DateTime dataReferencia = DateTime.ParseExact(primeiraDataIngles, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    // Variável para somar os dias de diferença
                    decimal somaDias = 0;

                    // Para cada parcela, calcular a diferença de dias
                    foreach (var item in parcelas)
                    {
                        // Extrair e converter a data de cada parcela
                        string dataIngles = item.Value.Length == 9 ? item.Value.Substring(0, 9) : item.Value.Substring(0, 10);
                        DateTime dataConvertida = DateTime.ParseExact(dataIngles, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        // Calcular a diferença de dias entre a data da parcela e a data de referência (primeira parcela)
                        decimal diasDiferenca = (decimal)(dataConvertida - dataReferencia).TotalDays;

                        // Adicionar à soma dos dias
                        somaDias += diasDiferenca;
                    }

                    // Calcular o prazo médio
                    decimal prazoMedio = somaDias / parcelas.Count;
                    totalEncargos = (iten.ValorCombinado * (1 + (_model.PlanoPadraoCliente.TxPerFin * prazoMedio) / 100)) - iten.ValorCombinado;
                }
                else
                {
                    totalEncargos = ((iten.ValorCombinado + iten.FreteUnidade) * (_model.PlanoPadraoCliente.TxPerFin / 100));
                }
                var totalDescontos = _model.PlanoPadraoCliente != null && _model.PlanoPadraoCliente.VlDescpl > 0 ? _model.PlanoPadraoCliente.VlDescpl : 0M;

                var _itensPedido = new Carrinho
                {
                    Id = iten.Id,
                    CodigoNomeProduto = iten.CodigoNomeProduto,
                    Desconto = totalDescontos,
                    Comissao = iten.Comissao,
                    Frete = iten.FreteUnidade,
                    ImagemProduto = iten.ImagemProduto,

                    ProdutoId = iten.Produto.Id,
                    Quantidade = iten.Quantidade,
                    ValorProduto = iten.ValorBrutoProduto,
                    ValorCombinado = iten.ValorCombinado,
                    CodProduto = iten.CodProduto,
                    TaxaEncargos = _model.PlanoPadraoCliente.TxPerFin,
                    Encargos = totalEncargos
                };
                //App.CartRepository.Add(_itensPedido);
                _pedido.ItensDoPedido.Add(_itensPedido);
            }

            var pedidoId = App.PedidoRepository.Add(_pedido);
            await _model.SendAlert("Pedido", "Pedido salvo com sucesso. Pendente de envio para a central");
            Navigation.RemovePage(this);
            await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
        }
        catch (Exception ex)
        {
            await _model.SendAlert("Pedido", ex.Message);
        }
        finally
        {
            pop.Close();
        }
    }

    private void FecharPaginasAbertas()
    {
        try
        {
            var openedPages = Shell.Current.Navigation.NavigationStack.ToList();
            foreach (var item in openedPages)
            {
                if (item != null)
                {
                    if (item.GetType().Equals(typeof(ClientsPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(ProdutosPedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(CarrinhoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(RankingPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(PedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(MeusPedidosPage)))
                        Shell.Current.Navigation.RemovePage(item);

                    if (item.GetType().Equals(typeof(DetalhePedidoPage)))
                        Shell.Current.Navigation.RemovePage(item);
                }
            }
        }
        catch (Exception)
        {


        }
    }

    private async void BtnCancelarPedido_Clicked(object sender, EventArgs e)
    {
        if (_model != null && !Guid.Empty.Equals(_model.Pedido.Id))
        {
            _model.CancelarPedido(_model.Pedido.Id);
        }

        await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
    }

    private void SetSelectedBorder()
    {
        BtnPrincipal.BorderColor = Color.FromArgb("#eee");
        BtnPrincipal.BorderWidth = 1;

        BtnObservacoes.BorderColor = Color.FromArgb("#eee");
        BtnObservacoes.BorderWidth = 1;

        BtnAcompanhamento.BorderColor = Color.FromArgb("#eee");
        BtnAcompanhamento.BorderWidth = 1;
    }

    private void ResetVisible()
    {
        geralview.IsVisible = false;
        observacaoview.IsVisible = false;
        acompanhamentoview.IsVisible = false;
        ItensCarrinho.IsVisible = false;

        lblGerais.IsVisible = true;
        BtnPrincipal.IsVisible = true;
    }


    //INCLUIR PRODUTOS
    private async void BtnIncluirProdutos_Clicked(object sender, EventArgs e)
    {
        var _popmodel = new PopupViewModel
        {
            PopupMessage = "Carregando produtos..."
        };
        var pop = new PopupPage(_popmodel);
        await Task.Delay(1000);
        try
        {

            this.ShowPopup(pop);
            AtualizarPedido();
            await Navigation.PushAsync(new ProdutosPedidoPage(_model));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Pedido", ex.Message, "Ok");
        }
        finally
        {
            pop.Close();
        }
    }

    private void AtualizarPedido()
    {
        try
        {
            RecalcularCarrinho();
            _model.AtualizarListaDeProdutos();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void RecalcularCarrinho()
    {
        var filial = Filial.SelectedIndex >= 0 ? (DictionaryDto)Filial.ItemsSource[Filial.SelectedIndex] : throw new Exception("Nenhum filial selecionada");
        var tipoPedido = TipoPedido.SelectedIndex >= 0 ? (DictionaryDto)TipoPedido.ItemsSource[TipoPedido.SelectedIndex] : throw new Exception("Nenhum tipo de pedido selecionado");
        var tipoVenda = TipoVenda.SelectedIndex >= 0 ? (DictionaryDto)TipoVenda.ItemsSource[TipoVenda.SelectedIndex] : throw new Exception("Nenhum tipo de venda selecionado");
        var planoPagamento = PlanoPagamento.SelectedIndex >= 0 ? (DictionaryDto)PlanoPagamento.ItemsSource[PlanoPagamento.SelectedIndex] : throw new Exception("Nenhum plano de pagamento selecionado");
        var tipoCobranca = TipoCobranca.SelectedIndex >= 0 ? (DictionaryDto)TipoCobranca.ItemsSource[TipoCobranca.SelectedIndex] : throw new Exception("Nenhum tipo de cobrança selecionado");

        decimal.TryParse(VlFreteSc25k.Text, cultureInfo, out decimal _valorFrete25);
        decimal.TryParse(VlFreteSc30k.Text, cultureInfo, out decimal _valorFrete30);

        _model.Pedido.FilialMinerthal = filial.Key;
        _model.Pedido.PlanoPagamento = planoPagamento.Key;
        _model.Pedido.TipoCobranca = tipoCobranca.Key;
        _model.Pedido.TipoPedido = tipoPedido.Key;
        _model.Pedido.TipoVenda = tipoVenda.Key;
        _model.Pedido.ValorFrete25 = _valorFrete25;
        _model.Pedido.ValorFrete30 = _valorFrete30;
        _model.Pedido.Observacao = TxtObservacao.Text;

        _model.Pedido.NomeFilial = filial.Value;
        _model.Pedido.NomeTipo = tipoPedido.Value;
        _model.Pedido.NomeTipoVenda = tipoVenda.Value;
        _model.Pedido.NomeTipoCobranca = tipoCobranca.Value;
        _model.Pedido.NomePlanoPagamento = planoPagamento.Value;


    }

    private void PlanoPagamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dictionary = (DictionaryDto)PlanoPagamento.SelectedItem;

        if (dictionary != null && dictionary.Key == "88")
        {
            //LblParcelas.IsVisible = true;
            BtnParcelamento.IsVisible = true;
        }
        else
        {
            //LblParcelas.IsVisible = false;
            BtnParcelamento.IsVisible = false;
        }
    }

    private void BtnAdicionarParcela_Clicked(object sender, EventArgs e)
    {
        //var _popmodel = new PopupViewModel
        //{
        //    PopupMessage = "Aguarde..."
        //};
        //var pop = new PopupPage(_popmodel);

        //var totalRows = GridParcelas.RowDefinitions.Count;

        var maxParcela = _model.Pedido.Parcelas != null && _model.Pedido.Parcelas.Any() ? _model.Pedido.Parcelas.Max(x => x.OptionalInteger) : 0;
        maxParcela += 1;


        var guid = Guid.NewGuid();

        _model.Pedido.Parcelas.Add(new DictionaryDto
        {
            Key = guid.ToString(),
            Value = DateTime.Today.ToShortDateString(),
            Optional = $"Parcela {maxParcela}",
            OptionalInteger = maxParcela

        });


        AdicionarParcelasNoGrid(maxParcela, guid, DateTime.Today);

        //GridPedido.RowDefinitions[6].Height = GridLength.Auto;

        //pop.Close();
    }

    private void BtnRemoverParcela_Clicked(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = Guid.Parse(btn.CommandParameter.ToString());

        var parcelas = new List<DateTime>();
        var copyParcelas = new DictionaryDto[_model.Pedido.Parcelas.Count];
        _model.Pedido.Parcelas.CopyTo(copyParcelas, 0);

        if (copyParcelas.Count() > 0)
        {
            _model.Pedido.Parcelas.Clear();
            foreach (var item in copyParcelas)
            {
                DateTime.TryParse(item.Value, out DateTime _data);
                Guid.TryParse(item.Key, out Guid _id);
                if (!id.Equals(_id))
                    parcelas.Add(_data);
            }


            for (var i = 0; i < parcelas.Count; i++)
            {
                var _dicitionary = new DictionaryDto
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = parcelas[i].ToShortDateString(),
                    Optional = $"Parcela {i + 1}"
                };
                _model.Pedido.Parcelas.Add(_dicitionary);
            }
        }

        //grid.Children.Remove(picker);
    }

    private void BtnRemoverParcela_Clicked_old(object sender, EventArgs e)
    {
        var btn = (Button)sender;
        var id = Guid.Parse(btn.ClassId.ToString());

        var parcelas = new List<DateTime>();
        var copyParcelas = new DictionaryDto[_model.Pedido.Parcelas.Count];
        _model.Pedido.Parcelas.CopyTo(copyParcelas, 0);

        if (copyParcelas.Count() > 0)
        {
            _model.Pedido.Parcelas.Clear();
            foreach (var item in copyParcelas)
            {
                DateTime.TryParse(item.Value, out DateTime _data);
                Guid.TryParse(item.Key, out Guid _id);
                if (!id.Equals(_id))
                    parcelas.Add(_data);
            }


            for (var i = 0; i < parcelas.Count; i++)
            {
                var _dicitionary = new DictionaryDto
                {
                    Key = Guid.NewGuid().ToString(),
                    Value = parcelas[i].ToShortDateString(),
                    Optional = $"Parcela {i + 1}",
                    OptionalInteger = i + 1
                };
                _model.Pedido.Parcelas.Add(_dicitionary);
            }
        }

        int rowNumber = Grid.GetRow(btn);
        var childs = GridParcelas.Children.Count;
        for (var i = childs - 1; i >= 0; i--)
        {
            GridParcelas.Children.RemoveAt(i);
        }


        //int callingButtonIndex = GridParcelas.Children.IndexOf(btn);
        //var totalRows = GridParcelas.RowDefinitions.Count;
        //GridParcelas.Children.RemoveAt(callingButtonIndex);
        //GridParcelas.Children.RemoveAt(callingButtonIndex - 1);
        //GridParcelas.Children.RemoveAt(callingButtonIndex - 2);
        //GridParcelas.RowDefinitions.RemoveAt(rowNumber);

        if (_model.Pedido.Parcelas != null && _model.Pedido.Parcelas.Count > 0)
        {
            foreach (var item in _model.Pedido.Parcelas)
            {
                var guid = Guid.Parse(item.Key);
                DateTime.TryParse(item.Value, out DateTime dateTime);
                // AdicionarParcelasNoGrid(item.OptionalInteger, guid, dateTime);
            }
        }

        //grid.Children.Remove(picker);
    }
    private void AdicionarParcelasNoGrid(int totalRows, Guid guidControle, DateTime data)
    {
        //var labelParcela = new Label
        //{
        //    Text = $"Parcela {totalRows}",
        //    VerticalTextAlignment = TextAlignment.Center,
        //    Margin = new Thickness(0, 2, 0, 0)
        //};
        //var entryParcela = new DatePicker
        //{
        //    MinimumDate = data
        //};


        //var btn = new Button
        //{
        //    ClassId = guidControle.ToString(),
        //    ImageSource = "close_btn_mini.png",
        //    HeightRequest = 30,
        //    WidthRequest = 30,
        //    CommandParameter = GridParcelas,
        //};

        //btn.Clicked += BtnRemoverParcela_Clicked;

        //GridParcelas.RowDefinitions.Add(new RowDefinition
        //{
        //    Height = GridLength.Auto,
        //});

        //var numRow = totalRows -= 1;
        //GridParcelas.SetRow(labelParcela, numRow);
        //GridParcelas.SetColumn(labelParcela, 0);
        //GridParcelas.Children.Add(labelParcela);


        //GridParcelas.SetRow(entryParcela, numRow);
        //GridParcelas.SetColumn(entryParcela, 1);
        ////GridParcelas.SetColumnSpan(entryParcela, 2);
        //GridParcelas.Children.Add(entryParcela);

        //GridParcelas.SetRow(btn, numRow);
        //GridParcelas.SetColumn(btn, 2);
        //GridParcelas.Children.Add(btn);
        //GridPedido.RowDefinitions[13].Height = GridLength.Auto;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

        int _valueA = 0, _valueB = 0;
        if (!string.IsNullOrWhiteSpace(e.OldTextValue))
            int.TryParse(e.OldTextValue.Replace(",", "").Replace(".", ""), out _valueA);
        if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            int.TryParse(e.NewTextValue.Replace(",", "").Replace(".", ""), out _valueB);

        var textbox = (Entry)sender;
        if (_valueA != _valueB)
        {

            var tempValue = decimal.Parse(_valueB.ToString(), cultureInfo);
            var newValue = 0M;
            if (tempValue > 0)
                newValue = tempValue / 100;

            var newFormat = newValue.ToString("N2", cultureInfo);
            textbox.Text = newFormat;
            textbox.CursorPosition = 10;
        }

        textbox.CursorPosition = textbox.Text.Length;
    }
}

