using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Views.Startup;
using System.Text;


namespace MinerthalSalesApp.ViewModels
{
    public partial class AppShellViewModel : BaseViewModel
    {

        [RelayCommand]
        async Task SignOut()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            var sb = new StringBuilder();
            sb.AppendLine("DROP TABLE Atualizacoes;");
            sb.AppendLine("DROP TABLE HistoricoDePedidos;");
            sb.AppendLine("DROP TABLE ResumoPedido;");
            sb.AppendLine("DROP TABLE Banco;");
            sb.AppendLine("DROP TABLE Log;");
            sb.AppendLine("DROP TABLE TabelaPreco;");
            sb.AppendLine("DROP TABLE Carrinho;");
            sb.AppendLine("DROP TABLE MeusPedidos;");
            sb.AppendLine("DROP TABLE Titulo;");
            sb.AppendLine("DROP TABLE Cliente;");
            sb.AppendLine("DROP TABLE Pedido;");
            sb.AppendLine("DROP TABLE Usuario;");
            sb.AppendLine("DROP TABLE ClientePlanoPagamento;");
            sb.AppendLine("DROP TABLE Plano;");
            sb.AppendLine("DROP TABLE Vendedor;");
            sb.AppendLine("DROP TABLE Faturamento;");
            sb.AppendLine("DROP TABLE Produto;");
            sb.AppendLine("DROP TABLE Visita;");
            sb.AppendLine("DROP TABLE Filial;");
            sb.AppendLine("DROP TABLE Ranking;");
            App.AtualizacaoRepository.DeleteAllTables(sb.ToString());
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
