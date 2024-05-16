using System.ComponentModel.DataAnnotations;

namespace MinerthalSalesApp.Models.Usuario
{
	public class UsuarioViewModel
	{
		protected UsuarioViewModel()
		{

		}

		public UsuarioViewModel(string codigo, string senha)
		{
			Codigo = codigo;
			Password = senha;
		}

		public static UsuarioViewModel Criar(string codigo, string senha) => new UsuarioViewModel(codigo, senha);


		
		[Display(Name = "Código")]
		public string Codigo { get; private set; } = string.Empty;


		[DataType(DataType.Password)]
		[Display(Name = "Senha")]
		public string Password { get; private set; } = string.Empty;


	}
}
