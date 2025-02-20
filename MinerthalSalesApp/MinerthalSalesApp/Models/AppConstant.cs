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

                    if (item == null)
                        Shell.Current.Items.Add(flyoutItem);

                    await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                }
            }
            else
            {
                switch (route)
                {
                    case "ranking":
                        await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
                        FecharPaginasAbertas();
                        break;
                    case "clients":
                        await Shell.Current.GoToAsync($"//{nameof(ClientsPage)}");
                        break;
                    case "produts":
                        await Shell.Current.GoToAsync($"//{nameof(ProdutosPage)}");
                        break;
                    case "meusPedidos":
                        await Shell.Current.GoToAsync($"//{nameof(MeusPedidosPage)}");
                        break;
                    case "Pedidos":
                        await Shell.Current.GoToAsync($"//{nameof(PedidoPage)}");
                        break;
                    case "Pesquisa":
                        await Shell.Current.GoToAsync($"//{nameof(PesquisaPage)}");
                        break;
                    case "DadosEquipe":
                        await Shell.Current.GoToAsync($"//{nameof(DadosEquipePage)}");
                        break;
                };
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
                        if (item.GetType().Equals(typeof(ClientsPage)) ||
                            item.GetType().Equals(typeof(ProdutosPedidoPage)) ||
                            item.GetType().Equals(typeof(CarrinhoPage)) ||
                            item.GetType().Equals(typeof(RankingPage)) ||
                            item.GetType().Equals(typeof(PedidoPage)) ||
                            item.GetType().Equals(typeof(MeusPedidosPage)) ||
                            item.GetType().Equals(typeof(DetalhePedidoPage)))
                        {

                            Shell.Current.Navigation.RemovePage(item);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}