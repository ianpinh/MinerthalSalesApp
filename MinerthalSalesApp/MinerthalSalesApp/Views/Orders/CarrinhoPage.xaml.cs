using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.ViewModels.Orders;
using MinerthalSalesApp.ViewModels.Shared;
using Newtonsoft.Json;
using System.Globalization;

namespace MinerthalSalesApp.Views.Orders;

public partial class CarrinhoPage : ContentPage
{
    PedidoViewModel model;
    private UserBasicInfo userDetailStr;
    CultureInfo cultureInfo = new CultureInfo("pt-BR");
    bool isInitial = true;

    public CarrinhoPage(PedidoViewModel viewModel)
    {
        InitializeComponent();
        model = viewModel;
        var index = model.Pedido.ItensPedido.FindIndex(x => x.Ordem == model.Pedido.ItensPedido.Max(x => x.Ordem));
        //model.ItemDeCalculoCarrinho = model.Pedido.ItensPedido[index];

        Quantidade.Text=model.ItemDeCalculoCarrinho.Quantidade.ToString();
        ValorCombinado.Text= model.ItemDeCalculoCarrinho.ValorBrutoProduto.ToString("N2");
        Frete.Text= model.ItemDeCalculoCarrinho.FreteUnidade.ToString("N2");
        model.ItemDeCalculoCarrinho.CalcularComissaoTotal();

        BindingContext = model;
        CarregarDadosVendedor();
        CalcularItem();
        isInitial=false;
        App.AtualizarModel(model);
    }

    protected override bool OnBackButtonPressed()
    {
        return base.OnBackButtonPressed();
    }


    private void EntryQuantidade_TextChanged(object sender, TextChangedEventArgs e)
    {

        var valor = ConvertValueToDecimal(ValorCombinado.Text);

        int _valueA = 0, _valueB = 0;
        if (!string.IsNullOrWhiteSpace(e.OldTextValue))
            int.TryParse(e.OldTextValue.Replace(",", "").Replace(".", ""), out _valueA);
        if (!string.IsNullOrWhiteSpace(e.NewTextValue))
            int.TryParse(e.NewTextValue.Replace(",", "").Replace(".", ""), out _valueB);

        var textbox = (Entry)sender;

        if (_valueA != _valueB)
        {
            textbox.Text = _valueB.ToString();
            textbox.CursorPosition = 10;
            textbox.CursorPosition = textbox.Text.Length;
            SalvarItemCarrinho();
            CalcularItem();
        }
    }

    private void AddQuantidade_Clicked(object sender, EventArgs e)
    {
        var valorCombinado = ConvertValueToDecimal(ValorCombinado.Text);
        _ = int.TryParse(Quantidade.Text, out int quantidade);

        quantidade+=1;
        Quantidade.Text=quantidade.ToString();
        SalvarItemCarrinho();
        CalcularItem();
        ValidarQuantidadeValor();

    }

    private void RemoveQuantidade_Clicked(object sender, EventArgs e)
    {
        var valorCombinado = ConvertValueToDecimal(ValorCombinado.Text);
        _ = int.TryParse(Quantidade.Text, out int quantidade);

        quantidade-=1;
        Quantidade.Text=quantidade.ToString();
        SalvarItemCarrinho();
        CalcularItem();
        ValidarQuantidadeValor();
    }

    private void EntryValor_TextChanged(object sender, TextChangedEventArgs e)
    {
        _=int.TryParse(Quantidade.Text, out int quantidade);

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
            if (tempValue>0)
                newValue=tempValue/100;


            textbox.Text= newValue.ToString("N2", cultureInfo);
            textbox.CursorPosition = 10;
            SalvarItemCarrinho();
            CalcularItem();
        }
        textbox.CursorPosition = textbox.Text.Length;
    }

    private void AddValor_Clicked(object sender, EventArgs e)
    {
        var valorCombinado = ConvertValueToDecimal(ValorCombinado.Text);
        _ = int.TryParse(Quantidade.Text, out int quantidade);

        if (model.PlanoPadraoCliente.VlDescpl > 0)
        {
            valorCombinado = ConvertValueToDecimal(Total.Text);
        }

        valorCombinado +=0.01M;
        ValorCombinado.Text = valorCombinado.ToString("N2", cultureInfo);
        SalvarItemCarrinho();
        CalcularItem();
        ValidarQuantidadeValor();
    }

    private void RemoveValor_Clicked(object sender, EventArgs e)
    {
        var valorCombinado = ConvertValueToDecimal(ValorCombinado.Text);
        _=int.TryParse(Quantidade.Text, out int quantidade);

        valorCombinado-=0.01M;
        ValorCombinado.Text =valorCombinado.ToString("N2", cultureInfo);
        SalvarItemCarrinho();
        CalcularItem();
        ValidarQuantidadeValor();
    }

    private void CalcularItem()
    {

        var subtotalFrete = model.ItemDeCalculoCarrinho.Quantidade * model.ItemDeCalculoCarrinho.FreteUnidade;
        var valorCorrenteProduto = model.ItemDeCalculoCarrinho.ValorCombinado >0 ? model.ItemDeCalculoCarrinho.ValorCombinado : model.ItemDeCalculoCarrinho.ValorBrutoProduto;

        var subTotalProduto = model.ItemDeCalculoCarrinho.Quantidade * valorCorrenteProduto;
        var subtotal = subTotalProduto + subtotalFrete;

        var faixaComissao = model.ItemDeCalculoCarrinho.Comissao;
        var totalDescontos = 0M;
        var totalEncargos = 0M;

        if (model.PlanoPadraoCliente.TxPerFin>0)
        {
            if (model.PlanoPadraoCliente.CdPlano.Equals("88"))
            {
                var parcelas = model.Pedido.Parcelas; // Supondo que seja sua lista de parcelas

                DateTime dataReferencia = DateTime.Today;

                // Variável para somar os dias de diferença
                decimal somaDias = 0;

                // Para cada parcela, calcular a diferença de dias
                foreach (var item in parcelas)
                {
                    DateTime dataConvertida = DateTime.ParseExact(item.Value.Substring(0,10), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    // Calcular a diferença de dias entre a data da parcela e a data de referência (primeira parcela)
                    decimal diasDiferenca = (decimal)(dataConvertida - dataReferencia).TotalDays;

                    // Adicionar à soma dos dias
                    somaDias += diasDiferenca;
                }

                // Calcular o prazo médio
                decimal prazoMedio = somaDias / parcelas.Count;
                totalEncargos = (subtotal * (1 + (model.PlanoPadraoCliente.TxPerFin * prazoMedio)/100)) - subtotal;
            }
            else
            {
                totalEncargos = subtotal * (model.PlanoPadraoCliente.TxPerFin / 100);
            }
        }
        
        if (model.PlanoPadraoCliente.VlDescpl > 0)
            totalDescontos = subTotalProduto - (subTotalProduto / (1 + (model.PlanoPadraoCliente.VlDescpl / 100)));
                //subtotal * (model.PlanoPadraoCliente.VlDescpl/100);

        //var totalGeral = (subtotal+totalEncargos)-totalDescontos;
        var totalGeral = ((subTotalProduto / (1+(model.PlanoPadraoCliente.VlDescpl / 100))) + totalEncargos + subtotalFrete);
        var valorComissao = 0M;
        if (faixaComissao>0)
            valorComissao = (subTotalProduto / (1 + (model.PlanoPadraoCliente.VlDescpl / 100))) * (faixaComissao/100);

        Frete.Text = subtotalFrete.ToString("c", cultureInfo);
        SubTotal.Text = subtotal.ToString("c", cultureInfo);
        Encargos.Text=totalEncargos.ToString("c", cultureInfo);
        Descontos.Text = totalDescontos.ToString("c", cultureInfo);
        Total.Text = totalGeral.ToString("c", cultureInfo);
        Comissao.Text=string.Format("{0:N2}%", faixaComissao, cultureInfo);// faixaComissao.ToString("N2", cultureInfo);
        ValComissao.Text=valorComissao.ToString("c", cultureInfo);// faixaComissao.ToString("N2", cultureInfo);

    }

    private decimal ConvertValueToDecimal(string value)
    {
        int.TryParse(value.Replace(",", "").Replace(".", ""), out int _valorombinado);
        return _valorombinado/100M;
    }

    private void CarregarDadosVendedor()
    {
        if (Preferences.ContainsKey(nameof(App.UserDetails)))
        {
            string userDetails = Preferences.Get(nameof(App.UserDetails), "");
            userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
        }
    }

    private void CalcularValorDoProduto(object sender, TextChangedEventArgs e)
    {
        if (!isInitial)
            CalcularItem();
    }

    private void ConcluirItensDopedido(object sender, EventArgs e)
    {
        var _popmodel = new PopupViewModel
        {
            PopupMessage = "Calculando itens carrinho..."
        };
        var pop = new PopupPage(_popmodel);
        this.ShowPopup(pop);


        try
        {
            if (ValidarItem())
            {
                AdicionarItemDeCalculoCarrinhoAoPedido();
                Navigation.PushAsync(new PedidoPage(model));
                Navigation.RemovePage(this);
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            pop.Close();
        }
    }

    private void AdicionarItensDopedido(object sender, EventArgs e)
    {

        var _popmodel = new PopupViewModel
        {
            PopupMessage = "Carregando produtos..."
        };
        var pop = new PopupPage(_popmodel);
        try
        {
            this.ShowPopup(pop);

            if (ValidarItem())
            {
                AdicionarItemDeCalculoCarrinhoAoPedido();
                Navigation.PushAsync(new ProdutosPedidoPage(model));
                Navigation.RemovePage(this);
                model.ItemDeCalculoCarrinho=new Models.Dtos.ItensDto();
            }
        }
        catch (Exception)
        {

            throw;
        }
        finally
        {
            pop.Close();
        }

    }

    private void SalvarItemCarrinho()
    {
        var valorCombinado = ConvertValueToDecimal(ValorCombinado.Text);
        _ =int.TryParse(Quantidade.Text.Replace(",", "").Replace(".", ""), out int quantidade);


        var totalFrete = quantidade * model.ItemDeCalculoCarrinho.FreteUnidade;

        if (model.PlanoPadraoCliente.VlDescpl > 0)
        {
            model.ItemDeCalculoCarrinho.TaxaDesconto = model.PlanoPadraoCliente.VlDescpl;
        }

        model.ItemDeCalculoCarrinho.Quantidade=quantidade;
        model.ItemDeCalculoCarrinho.ValorCombinado= valorCombinado;
        model.ItemDeCalculoCarrinho.Frete = totalFrete;
        model.ItemDeCalculoCarrinho.CalcularComissaoTotal();


    }


    private void AdicionarItemDeCalculoCarrinhoAoPedido()
    {
        var index = model.Pedido.ItensPedido.FindIndex(x => x.CodProduto == model.ItemDeCalculoCarrinho.CodProduto);
        if (index>=0)
            model.Pedido.ItensPedido[index] = model.ItemDeCalculoCarrinho;
        else
            model.Pedido.ItensPedido.Add(model.ItemDeCalculoCarrinho);
    }

    private bool ValidarValorMaximo(decimal valorCombinado)
    {
        var valido = true;
        //VALOR
        var valorMaximo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMinimo / 100));
        var valorMinimo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMaximo / 100));

        if (valorCombinado>valorMaximo)
        {
            DisplayAlert("Carrinho", "O Valor do produto ultrapassa o limite máximo", "OK");
            valido = false;
        }
        else if (valorCombinado< valorMinimo)
        {
            DisplayAlert("Carrinho", "O Valor de desconto ultrapassa o limite mínimo", "OK");
            valido = false;
        }

        return valido;
    }
    private bool ValidarQuantidadeValor()
    {
        var valido = true;
        //QUANTIDADE
        var qdtMax = model.ItemDeCalculoCarrinho.QuantidadeMax;
        var qdtMin = model.ItemDeCalculoCarrinho.QuantidadeMin;

        var quantidade = model.ItemDeCalculoCarrinho.Quantidade;
        var valorombinado = model.ItemDeCalculoCarrinho.ValorCombinado;

        if (quantidade > qdtMax)
        {
            DisplayAlert("Carrinho", "A quantidade ultrapassa o limite máximo", "OK");
            Quantidade.Text= model.ItemDeCalculoCarrinho.QuantidadeMin.ToString();
            valido = false;
        }
        else if (quantidade < qdtMin)
        {
            DisplayAlert("Carrinho", "A quantidade ultrapassa o limite mínimo", "OK");
            Quantidade.Text= model.ItemDeCalculoCarrinho.QuantidadeMin.ToString();
            valido = false;
        }

        //VALOR
        var valorMaximo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMinimo / 100));
        var valorMinimo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMaximo / 100));



        if (valorombinado>valorMaximo)
        {
            DisplayAlert("Carrinho", "O Valor do produto ultrapassa o limite máximo", "OK");
            ValorCombinado.Text= model.ItemDeCalculoCarrinho.ValorBrutoProduto.ToString("N2", cultureInfo);
            valido = false;

        }
        else if (valorombinado< valorMinimo)
        {
            DisplayAlert("Carrinho", "O Valor de desconto ultrapassa o limite mínimo", "OK");
            ValorCombinado.Text= model.ItemDeCalculoCarrinho.ValorBrutoProduto.ToString("N2", cultureInfo);
            valido = false;
        }

        if (valido)
        {
            SalvarItemCarrinho();
            CalcularItem();
        }

        return valido;
    }

    private bool ValidarItem()
    {
        var valido = true;
        var quantidade = model.ItemDeCalculoCarrinho.Quantidade;
        var valorombinado = model.ItemDeCalculoCarrinho.ValorCombinado;
        


        //QUANTIDADE
        var qdtMax = model.ItemDeCalculoCarrinho.QuantidadeMax;
        var qdtMin = model.ItemDeCalculoCarrinho.QuantidadeMin;
        if (quantidade > qdtMax)
        {
            DisplayAlert("Carrinho", $"A quantidade ultrapassa o limite máximo: {qdtMax}", "OK");
            valido = false;
        }
        else if (quantidade < qdtMin)
        {
            DisplayAlert("Carrinho", $"A quantidade ultrapassa o limite mínimo: {qdtMin}", "OK");
            valido = false;
        }

        //VALOR
        var valorMaximo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMinimo / 100));
        var valorMinimo = model.ItemDeCalculoCarrinho.ValorBrutoProduto - (model.ItemDeCalculoCarrinho.ValorBrutoProduto * (model.ItemDeCalculoCarrinho.ValorDescontoMaximo / 100));



        if (valorombinado>valorMaximo)
        {
            DisplayAlert("Carrinho", $"O Valor do produto ultrapassa o limite máximo: {valorMaximo:N2}", "OK");
            valido = false;
        }
        else if (valorombinado< valorMinimo)
        {
            DisplayAlert("Carrinho", $"O Valor de desconto ultrapassa o limite mínimo: {valorMinimo:N2}", "OK");
            valido = false;
        }

        return valido;
    }

    private void ValorCombinado_Unfocused(object sender, FocusEventArgs e)
    {
        ValidarQuantidadeValor();
    }

    private void Quantidade_Unfocused(object sender, FocusEventArgs e)
    {
        ValidarQuantidadeValor();
    }

    private void ComissaoVisible_Clicked(object sender, EventArgs e)
    {
        var closeEye = "invisible_eye.png";
        var openEye = "visible_eye.png";
        var classeIdVisible = "visible";
        var classeIdInVisible = "invisible";

        var btn = (ImageButton)sender;
        var atualSource = btn.ClassId;


        if (atualSource.Equals(classeIdInVisible))
        {
            comVibleLabel1.IsVisible = true;
            comVibleLabel2.IsVisible = true;
            Comissao.IsVisible = true;
            ValComissao.IsVisible = true;
            btn.Source = openEye;
            btn.ClassId = classeIdVisible;
        }
        else
        {
            comVibleLabel1.IsVisible = false;
            comVibleLabel2.IsVisible = false;
            Comissao.IsVisible = false;
            ValComissao.IsVisible = false;
            btn.Source = closeEye;
            btn.ClassId = classeIdInVisible;

        }
    }

    private async void CancelCart_Clicked(object sender, EventArgs e)
    {
        await model.ExcluirPedido();
    }

    private async void BtnVoltar_Clicked(object sender, EventArgs e)
    {
        var _popmodel = new PopupViewModel { PopupMessage = "Carregando produtos..." };
        var pop = new PopupPage(_popmodel);
        this.ShowPopup(pop);
        try
        {
            if (ValidarItem())
            {
                await Navigation.PushAsync(new ProdutosPedidoPage(model));
                Navigation.RemovePage(this);
                model.ItemDeCalculoCarrinho=new Models.Dtos.ItensDto();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Carrinho", $"Erro ao retorna à pagina de produtos. Erro: {ex.Message}", "Ok");
        }
        finally
        {
            pop.Close();
        }
    }
}


