using CommunityToolkit.Maui.Views;
using MinerthalSalesApp.ViewModels.Shared;

namespace MinerthalSalesApp;

public partial class PopupPage : Popup
{
    public PopupPage(PopupViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}