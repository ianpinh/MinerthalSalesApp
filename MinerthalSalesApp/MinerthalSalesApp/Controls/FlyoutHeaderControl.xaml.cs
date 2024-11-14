using MinerthalSalesApp.ViewModels.Shared;

namespace MinerthalSalesApp.Controls;
public partial class FlyoutHeaderControl : StackLayout
{
    private FlyoutHeaderControlViewModel _model;
    public FlyoutHeaderControl(FlyoutHeaderControlViewModel model)
    {
        InitializeComponent();
        _model = model;
        BindingContext = _model;
    }

    public void UpdateBindindContext()
    {
        _model.UpdadteLoginUser();
    }
}