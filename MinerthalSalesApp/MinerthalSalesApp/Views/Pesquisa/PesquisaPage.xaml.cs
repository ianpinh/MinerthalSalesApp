using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.ViewModels.Pesquisa;
using MinerthalSalesApp.ViewModels.Shared;

namespace MinerthalSalesApp.Views.Pesquisa;

public partial class PesquisaPage : ContentPage
{
    PesquisaViewModel model;
    public PesquisaPage(PesquisaViewModel pesquisaViewModel)
    {
        InitializeComponent();
        model=pesquisaViewModel;
        BindingContext = model;
        ImgUserLoading.IsAnimationPlaying = true;
    }

    private void Expander_ExpandedChangedTitulosaVencidos(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImageTitulosaVencer="chevron_up.png";
        }
        else
        {
            model.ExpandedImageTitulosaVencer = "chevron_down.png";
        }
    }
    
    private void Expander_ExpandedChangedTitulosaVencer(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImageTitulosaVencer="chevron_up.png";
        }
        else
        {
            model.ExpandedImageTitulosaVencer= "chevron_down.png";
        }
    }

    private void Expander_ExpandedChangedInadimplentes(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {
        
        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImageInadimplentes="chevron_up.png";
        }
        else
        {
            model.ExpandedImageInadimplentes= "chevron_down.png";
        }
    }
 
    private void Expander_ExpandedChangedPedidosEmAberto(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {

        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImagePedidosEmAberto="chevron_up.png";
        }
        else
        {
            model.ExpandedImagePedidosEmAberto= "chevron_down.png";
        }
    }

    private void Expander_ExpandedChangedCarregamentos(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {

        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImageCarregamentos="chevron_up.png";
        }
        else
        {
            model.ExpandedImageCarregamentos= "chevron_down.png";
        }
    }

    private void Expander_ExpandedChangedMetasMensais(object sender, CommunityToolkit.Maui.Core.ExpandedChangedEventArgs e)
    {

        var _expander = (Expander)sender;
        if (_expander.IsExpanded)
        {
            model.ExpandedImageMetaMensal = "chevron_up.png";
        }
        else
        {
            model.ExpandedImageMetaMensal= "chevron_down.png";
        }
    }

    private void Expander_Loaded(object sender, EventArgs e)
    {
        //ImgUserLoading.IsVisible=false;
    }
}