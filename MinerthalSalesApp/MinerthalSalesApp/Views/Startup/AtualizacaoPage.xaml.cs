using MinerthalSalesApp.ViewModels.Startup;
using MinerthalSalesApp.Views.Dashboard;

namespace MinerthalSalesApp.Views.Startup;

public partial class AtualizacaoPage : ContentPage
{
    AtualizacaoViewModel _model;
    bool _isBusy = false;

    public AtualizacaoPage(AtualizacaoViewModel model)
    {
        InitializeComponent();
        _model = model;
        BindingContext =_model;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        int total = 0;
        if (!_isBusy)
        {
            _isBusy=true;
            Loaded+=async (s, e) =>
            {
                await Task.Run(() =>
              {
                  total = _model.InitializeAsync(this);
              });

                if (total > 0)
                {
                   
                    //var dashview = new DashboardPageViewModel(App.PopupAppService, App.AlertService);
                    //await Navigation.PushAsync(new AdminDashboardPage(dashview, App.PopupAppService));
                    await Shell.Current.GoToAsync($"//{nameof(AdminDashboardPage)}");
                    _isBusy=false;
                }
            };

        }

        //Thread.Sleep(1000);
        //_model.InitializeAsync();
        //await Task.Delay(120);
        //loadingImage.IsAnimationPlaying = false;

        //await Task.Delay(120);
        //loadingImage.IsAnimationPlaying = true;
    }

    protected override void OnDisappearing()
    {
        try
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
            Shell.Current.Navigation.RemovePage(this);
            //Navigation.RemovePage(this);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}