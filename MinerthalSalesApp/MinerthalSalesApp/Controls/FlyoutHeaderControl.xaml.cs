namespace MinerthalSalesApp.Controls;
public partial class FlyoutHeaderControl : StackLayout
{
	public FlyoutHeaderControl()
	{
		InitializeComponent();

		if (App.UserDetails != null)
		{
			lblUserName.Text = App.UserDetails.FullName;
			lblUserCodigo.Text = App.UserDetails.Codigo;
			//lblUserRole.Text = App.UserDetails.RoleText;

			
		}
	}

	
}