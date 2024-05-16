namespace MinerthalSalesApp.Models.Dtos
{
	public class UsuarioDto: BaseDtoResponse
	{
		public List<UsuarioDetalhesDto> Details { get; set; } = new List<UsuarioDetalhesDto>();
	}

	public class UsuarioDetalhesDto
	{
		public string SellerCode { get; set; }
		public string SellerName { get; set; }
		public string CNPJorCPF { get; set; }
		public string SellerNickName { get; set; }
		public string SellerPassword { get; set; }
	}
}
