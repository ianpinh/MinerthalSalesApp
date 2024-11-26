using MinerthalSalesApp.Controls;
using MinerthalSalesApp.ViewModels.Shared;
using MinerthalSalesApp.Views.Clients;
using MinerthalSalesApp.Views.DadosEquipe;
using MinerthalSalesApp.Views.Dashboard;
using MinerthalSalesApp.Views.Orders;
using MinerthalSalesApp.Views.Pesquisa;
using MinerthalSalesApp.Views.Products;
using MinerthalSalesApp.Views.Ranking;

namespace MinerthalSalesApp.Models
{
    public class AppConstant
    {
        public async static void AddFlyoutMenusDetails(string route = default)
        {

            var model = new FlyoutHeaderControlViewModel();
            Shell.Current.FlyoutHeader = new FlyoutHeaderControl(model);

            //var adminDashboardInfo = Shell.Current.Items.Where(f => f.Route == nameof(AdminDashboardPage)).FirstOrDefault();
            //if (adminDashboardInfo != null) Shell.Current.Items.Remove(adminDashboardInfo);

            if (string.IsNullOrWhiteSpace(route))
            {
                if (App.UserDetails.RoleID == (int)RoleDetails.Admin)
                {


                    var flyoutItem = new FlyoutItem()
                    {
                        Title = "Dashboard Page",
                        Route = nameof(AdminDashboardPage),
                        FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                        Items =
                    {
                            new ShellContent
                                {
                                    Icon = Icons.Home,
                                    Title = "Home",
                                    ContentTemplate = new DataTemplate(typeof(AdminDashboardPage)),
                                },
                                  new ShellContent
                                {
                                    Icon = Icons.Clients,
                                    Title = "Clientes",
                                    ContentTemplate = new DataTemplate(typeof(ClientsPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Products,
                                    Title = "Produtos",
                                    ContentTemplate = new DataTemplate(typeof(ProdutosPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Orders,
                                    Title = "Pedidos",
                                    ContentTemplate = new DataTemplate(typeof(MeusPedidosPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Search,
                                    Title = "Busca",
                                    ContentTemplate = new DataTemplate(typeof(PesquisaPage)),
                                },
                                new ShellContent
                                {
                                    Icon = Icons.Ranking,
                                    Title = "Ranking",
                                    ContentTemplate = new DataTemplate(typeof(RankingPage)),
                                },
                                 new ShellContent
                                {
                                    Icon = Icons.DadosEquipe,
                                    Title = "Dados Equipe",
                                    ContentTemplate = new DataTemplate(typeof(DadosEquipePage)),
                                }

                   }
                    };

                    var item = Shell.Current.Items.Where(x => x.Title == flyoutItem.Title).FirstOrDefault();


                    //if (!Shell.Current.Items.Contains(flyoutItem))
                    if (item == null)
                    {
                        Shell.Current.Items.Add(flyoutItem);
                    }
                    if (DeviceInfo.Platform == DevicePlatform.WinUI)
                    {
                        Shell.Current.Dispatcher.Dispatch(async () =>
                        {
                            await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                        });
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                    }
                }
            }
            else if (route == "ranking")
            {
                //var flyoutItem = new FlyoutItem()
                //{
                //	Title = "Ranking Page",
                //	Route = nameof(RankingPage),
                //	FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                //	Items =
                //	{
                //				new ShellContent
                //				{
                //					Icon = Icons.Clients,
                //					Title = "Clientes",
                //					ContentTemplate = new DataTemplate(typeof(RankingPage)),
                //				},
                //				new ShellContent
                //				{
                //					Icon = Icons.Products,
                //					Title = "Produtos",
                //					ContentTemplate = new DataTemplate(typeof(RankingPage)),
                //				},
                //				new ShellContent
                //				{
                //					Icon = Icons.Orders,
                //					Title = "Pedidos",
                //					ContentTemplate = new DataTemplate(typeof(RankingPage)),
                //				},
                //				new ShellContent
                //				{
                //					Icon = Icons.Search,
                //					Title = "Busca",
                //					ContentTemplate = new DataTemplate(typeof(RankingPage)),
                //				},
                //				new ShellContent
                //				{
                //					Icon = Icons.Ranking,
                //					Title = "Ranking",
                //					ContentTemplate = new DataTemplate(typeof(RankingPage)),
                //				},
                //   }
                //};

                //if (!Shell.Current.Items.Contains(flyoutItem))
                //{
                //	Shell.Current.Items.Add(flyoutItem);
                //	if (DeviceInfo.Platform == DevicePlatform.WinUI)
                //	{
                //		Shell.Current.Dispatcher.Dispatch(async () =>
                //		{
                //			await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
                //		});
                //	}
                //	else
                //	{
                //		await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
                //	}
                //}

                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
                        FecharPaginasAbertas();
                    });
                }
                else
                {
                    //await Shell.Current.GoToAsync($"//{nameof(SharedLoadingPage)}",true,new Dictionary<string, object> { { "pagina", "RankingPage" } });
                    await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
                }

            }
            else if (route == "clients")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
                }
            }
            else if (route == "produts")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(ProdutosPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(ProdutosPage)}");
                }
            }
            else if (route == "meusPedidos")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
                }
            }
            else if (route == "Pedidos")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(PedidoPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(PedidoPage)}");
                }
            }
            else if (route == "Pesquisa")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(PesquisaPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(PesquisaPage)}");
                }
            }
            else if (route == "DadosEquipe")
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Shell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(DadosEquipePage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(DadosEquipePage)}");
                }
            }

        }

        private static void FecharPaginasAbertas()
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
    }
}
