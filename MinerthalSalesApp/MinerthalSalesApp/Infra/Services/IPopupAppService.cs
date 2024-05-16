using CommunityToolkit.Maui.Views;

namespace MinerthalSalesApp.Infra.Services
{
    public interface IPopupAppService
    {
        void Init(Page page);
        void ClosePopup(Popup popup);
        void ShowPopup(Popup popup);
    }
}
