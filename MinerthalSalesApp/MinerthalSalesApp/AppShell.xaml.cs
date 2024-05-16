using Microsoft.Maui.Controls;
using MinerthalSalesApp.ViewModels;

namespace MinerthalSalesApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        this.BindingContext = new AppShellViewModel();
    }

    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);
        var _current = args.Current;
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
        else { 
        
        }
    }

}

