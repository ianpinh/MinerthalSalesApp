using MinerthalSalesApp.ViewModels;

namespace MinerthalSalesApp;

public partial class AppShell : Shell
{
    private AppShellViewModel model;
    public AppShell()
    {
        InitializeComponent();
        model = new AppShellViewModel();
        this.BindingContext = model;
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
        var _current = args.Current;
        if (model != null)
            model.IsManagerButtonVisible = App.VendedorSelecionado != null;

        if (args.Current != null)
        {
            //if (args.Source == ShellNavigationSource.ShellItemChanged)
            //{
            //    if (args.Target.Location.OriginalString == _current)
            //    {
            //        // Cancel the original route.
            //        args.Cancel();
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            // Used by the next OnAppearing.
            //            HelpPage.Entering = true;
            //            // Go there by a route that isn't a child of Shell.
            //            // Doing so, pushes our previous location on to Navigation stack.
            //            Shell.Current.GoToAsync("Help2");
            //        });
            //    }
            //}
        }
        else
        {

        }
    }

}

