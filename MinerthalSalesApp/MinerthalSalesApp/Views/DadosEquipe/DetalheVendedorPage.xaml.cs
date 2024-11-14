using MinerthalSalesApp.Controls;
using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.Dashboard;
using MinerthalSalesApp.Views.Startup;
using Newtonsoft.Json;
namespace MinerthalSalesApp.Views.DadosEquipe;

public partial class DetalheVendedorPage : ContentPage
{
	public DetalheVendedorPage(Vendedor vendedor)
	{
		InitializeComponent();
		BindingContext = vendedor;
		
	}

	private async void BtnNovoLogin_Clicked(object sender, EventArgs e)
	{
		var btn = (Button)sender;
		var _vendedorStr = btn.CommandParameter.ToString();

		if (!string.IsNullOrWhiteSpace(_vendedorStr))
		{
			var dadosVendedor = JsonConvert.DeserializeObject<Vendedor>(_vendedorStr);
			var user = App.UserRepository.GetByCodigo(dadosVendedor.CdRca);
			if (dadosVendedor != null)
			{
				var lstClinetes = await App.ServicoDeCarregamentoDasBases.PesquisarClienteAsync(dadosVendedor.CdRca);
				var vendedorSelecionado = new Models.Dtos.VendedorSelecionadoDto
				{
					Clientes = lstClinetes,
					CodigoVendedor = dadosVendedor.CdRca,
					VendedorId = dadosVendedor.Id,
					NomeVendedor = user.SellerName
				};

				App.VendedorSelecionado = vendedorSelecionado;
                AppConstant.AddFlyoutMenusDetails();
                //await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
			}


			//if (dadosVendedor != null)
			//{
			//	var userDetails = new Models.UserBasicInfo();
			//	userDetails.Codigo = dadosVendedor.CdRca;
			//	userDetails.FullName = dadosVendedor.NmRca;

			//	userDetails.RoleID = (int)Models.RoleDetails.Admin;
			//	userDetails.RoleText = "Admin Role";

			//	var gerente = App.UserDetails;
			//	userDetails.UserInfoManager = JsonConvert.SerializeObject(gerente);

			//	//if (Preferences.ContainsKey(nameof(App.UserDetails)))
			//	//    Preferences.Remove(nameof(App.UserDetails));


			//	//string userDetailStr = JsonConvert.SerializeObject(userDetails);
			//	//Preferences.Set(nameof(App.UserDetails), userDetailStr);
			//	//App.UserDetails = userDetails;

			//	await SignOut(userDetails);

		//}
		}
    }

	async Task SignOut(Models.UserBasicInfo userDetails)
	{
		if (Preferences.ContainsKey(nameof(App.UserDetails)))
		{
			Preferences.Remove(nameof(App.UserDetails));
		}

		App.AtualizacaoRepository.ClearAllTables();
		await RedirectToMain(userDetails);


	}

	private async Task RedirectToMain(Models.UserBasicInfo userDetails)
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

            var model = new FlyoutHeaderControlViewModel();
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl(model);
		}

		//var totalAtualizacoes = App.AtualizacaoRepository.GetTotal();
		//if (totalAtualizacoes > 1)
		//	Models.AppConstant.AddFlyoutMenusDetails();
		//else
		await Shell.Current.GoToAsync($"//{nameof(AtualizacaoPage)}");
	}

	async Task<Usuario> LoginUsuario(Models.UserBasicInfo userDetails)
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
