using CommunityToolkit.Mvvm.Input;
using MinerthalSalesApp.Controls;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Views.Startup;
using Newtonsoft.Json;
using System.Text;
namespace MinerthalSalesApp.ViewModels
{
	public partial class AppShellViewModel : BaseViewModel
	{
		public AppShellViewModel()
		{
			IsManagerButtonVisible = false;

		}

		[RelayCommand]
		async Task SignOut()
		{

			//var sb = new StringBuilder();
			//sb.AppendLine("DROP TABLE Atualizacoes;");
			//sb.AppendLine("DROP TABLE HistoricoDePedidos;");
			//sb.AppendLine("DROP TABLE ResumoPedido;");
			//sb.AppendLine("DROP TABLE Banco;");
			//sb.AppendLine("DROP TABLE Log;");
			//sb.AppendLine("DROP TABLE TabelaPreco;");
			//sb.AppendLine("DROP TABLE Carrinho;");
			//sb.AppendLine("DROP TABLE MeusPedidos;");
			//sb.AppendLine("DROP TABLE Titulo;");
			//sb.AppendLine("DROP TABLE Cliente;");
			//sb.AppendLine("DROP TABLE Pedido;");
			//sb.AppendLine("DROP TABLE Usuario;");
			//sb.AppendLine("DROP TABLE ClientePlanoPagamento;");
			//sb.AppendLine("DROP TABLE Plano;");
			//sb.AppendLine("DROP TABLE Vendedor;");
			//sb.AppendLine("DROP TABLE Faturamento;");
			//sb.AppendLine("DROP TABLE Produto;");
			//sb.AppendLine("DROP TABLE Visita;");
			//sb.AppendLine("DROP TABLE Filial;");
			//sb.AppendLine("DROP TABLE Ranking;");
			//App.AtualizacaoRepository.DeleteAllTables(sb.ToString());
			//Reset();

			if (!string.IsNullOrEmpty(App.UserDetails.UserInfoManager))
				App.AtualizacaoRepository.ClearAllTables();

			if (Preferences.ContainsKey(nameof(App.UserDetails)))
				Preferences.Remove(nameof(App.UserDetails));

			await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
		}

		private bool isManagerButtonVisible;
		public bool IsManagerButtonVisible
		{
			get => isManagerButtonVisible;
			set
			{
				isManagerButtonVisible = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(IsManagerButtonVisible));
			}
		}

		[RelayCommand]
		async Task SignInManager()
		{
			if (!string.IsNullOrWhiteSpace(App.UserDetails.UserInfoManager))
			{
				var userDetails = JsonConvert.DeserializeObject<UserBasicInfo>(App.UserDetails.UserInfoManager);

				if (userDetails != null)
					await SignOutUser(userDetails);
			}
		}

		async Task SignOutUser(UserBasicInfo userDetails)
		{
			if (Preferences.ContainsKey(nameof(App.UserDetails)))
			{
				Preferences.Remove(nameof(App.UserDetails));
			}
			//var sb = new StringBuilder();
			//sb.AppendLine("DROP TABLE Atualizacoes;");
			//sb.AppendLine("DROP TABLE HistoricoDePedidos;");
			//sb.AppendLine("DROP TABLE ResumoPedido;");
			//sb.AppendLine("DROP TABLE Banco;");
			//sb.AppendLine("DROP TABLE Log;");
			//sb.AppendLine("DROP TABLE TabelaPreco;");
			//sb.AppendLine("DROP TABLE Carrinho;");
			//sb.AppendLine("DROP TABLE MeusPedidos;");
			//sb.AppendLine("DROP TABLE Titulo;");
			//sb.AppendLine("DROP TABLE Cliente;");
			//sb.AppendLine("DROP TABLE Pedido;");
			//sb.AppendLine("DROP TABLE ClientePlanoPagamento;");
			//sb.AppendLine("DROP TABLE Plano;");
			//sb.AppendLine("DROP TABLE Vendedor;");
			//sb.AppendLine("DROP TABLE Faturamento;");
			//sb.AppendLine("DROP TABLE Produto;");
			//sb.AppendLine("DROP TABLE Visita;");
			//sb.AppendLine("DROP TABLE Filial;");
			//sb.AppendLine("DROP TABLE Ranking;");
			//App.AtualizacaoRepository.DeleteAllTables(sb.ToString());

			App.AtualizacaoRepository.ClearAllTables();
			await RedirectToMain(userDetails);
		}

		private async Task RedirectToMain(UserBasicInfo userDetails)
		{
			var usuario = await LoginUsuario(userDetails);

			if (usuario != null)
			{
				if (Preferences.ContainsKey(nameof(App.UserDetails)))
					Preferences.Remove(nameof(App.UserDetails));

				userDetails.QtdVendedoresNaEquipe = usuario.QtdVendedoresNaEquipe;
				string userDetailStr = JsonConvert.SerializeObject(userDetails);
				Preferences.Set(nameof(App.UserDetails), userDetailStr);
				App.UserDetails = userDetails;
				AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
			}


			//var totalAtualizacoes = App.AtualizacaoRepository.GetTotal();
			//if (totalAtualizacoes > 1)
			//	AppConstant.AddFlyoutMenusDetails();
			//else
			await Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
		}

		async Task<Usuario> LoginUsuario(UserBasicInfo userDetails)
		{
			try
			{
				var numUsers = App.UserRepository.GetTotal();
				if (numUsers == 0)
					await CarregarUsuarios();

				var usuario = App.UserRepository.GetByCodigo(userDetails.Codigo);
				if (usuario is null)
					throw new Exception("Usuário não encontrado");

				return usuario;
			}
			catch (Exception ex)
			{
				//await _alertService.ShowAlertAsync("Login", $"Não foi possível realizar o login. {ex.Message}", "OK");
				return null;
			}
		}

		async Task CarregarUsuarios()
		{
			try
			{
				await App.ServicoDeCarregamentoDasBases.CarregarUsuariosAsync();
				var numUsers = App.UserRepository.GetTotal();

				if (numUsers < 0)
					throw new CustomExceptions("Erro ao carregar os dados do usuário");
			}
			catch (Exception ex)
			{
				//_alertService.ShowAlert("Planos", $"Erro ao fazer a atualização dos planos. {ex.Message}", "OK");
			}
		}


	}
}
