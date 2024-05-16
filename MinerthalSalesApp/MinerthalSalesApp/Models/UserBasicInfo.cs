namespace MinerthalSalesApp.Models
{
	public class UserBasicInfo
	{
		public string FullName { get; set; }
		public string Codigo { get; set; }
		public int RoleID { get; set; }
		public string RoleText { get; set; }
		public string TabPreco { get; set; }
	}

	public enum RoleDetails
	{
		Saler = 1,
		Financial,
		Admin
	}
}
