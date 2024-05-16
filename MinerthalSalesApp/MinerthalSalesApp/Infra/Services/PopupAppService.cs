using CommunityToolkit.Maui.Views;

namespace MinerthalSalesApp.Infra.Services
{
    public class PopupAppService : IPopupAppService
    {
        Page page { get; set; }

        public void ClosePopup(Popup popup)
        {
            if (page == null)
                page = Application.Current?.MainPage ?? throw new NullReferenceException();
            popup.Close();
        }

        public void Init(Page page)
        {
            this.page = page;
        }

        public void ShowPopup(Popup popup)
        {
            if (page == null)
                page = Application.Current?.MainPage ?? throw new NullReferenceException();
            page.ShowPopup(popup);
        }
    }
}
