namespace MinerthalSalesApp.ViewModels.Shared
{
    public class FlyoutHeaderControlViewModel : BaseViewModel
    {
        public FlyoutHeaderControlViewModel()
        {

            if (App.VendedorSelecionado != null)
            {
                LblUserName = App.VendedorSelecionado.NomeVendedor;
                LblUserCodigo = App.VendedorSelecionado.CodigoVendedor;
            }
            else if (App.UserDetails != null)
            {
                LblUserName = App.UserDetails.FullName;
                LblUserCodigo = App.UserDetails.Codigo;
            }
        }
        private string lblUserName;
        public string LblUserName
        {
            get => lblUserName;
            set
            {
                lblUserName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LblUserName));

            }
        }

        private string lblUserCodigo;
        public string LblUserCodigo
        {
            get => lblUserCodigo;
            set
            {
                lblUserCodigo = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(LblUserCodigo));

            }
        }

        public void UpdadteLoginUser()
        {
            if (App.VendedorSelecionado != null)
            {
                LblUserName = App.VendedorSelecionado.NomeVendedor;
                LblUserCodigo = App.VendedorSelecionado.CodigoVendedor;
            }
            else if (App.UserDetails != null)
            {
                LblUserName = App.UserDetails.FullName;
                LblUserCodigo = App.UserDetails.Codigo;
            }
        }
    }
}
