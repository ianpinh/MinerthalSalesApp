using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models.Enums;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly IAppthalContext _context;
		public UserRepository(IAppthalContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			Init();
		}

		private void Init()
		{
			try
			{
				var command = $@"CREATE TABLE IF NOT EXISTS Usuario(
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                ,SellerCode VARCHAR(15) null
                                                ,SellerName VARCHAR(120) null
                                                ,CNPJorCPF VARCHAR(25) null                                               
                                                ,SellerPassword VARCHAR(150) null
                                                ,SellerNickName VARCHAR(20) null );";
				_context.ExcecutarComandoCrud(command);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<Usuario> GetAll()
		{
			try
			{
				var command = $@"SELECT * FROM Usuario";
				var retorno = _context.ExcecutarSelect(command);

				if (retorno == null)
					return new List<Usuario>();

				var lstuser = new List<Usuario>();
				foreach (var item in retorno)
				{
					lstuser.Add(new Usuario
					{
						SellerCode = item.SellerCode.ToString(),
						SellerName = item.SellerName.ToString(),
						CNPJorCPF = item.CNPJorCPF.ToString(),
						SellerPassword = item.SellerPassword.ToString(),
						SellerNickName = item.SellerNickName.ToString(),
					});
				}
				return lstuser;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Usuario GetByCpf(string cpf)
		{
			try
			{
				var command = $@"SELECT * FROM Usuario WHERE CNPJorCPF = {cpf}";
				var retorno = _context.ExcecutarSelectFirstOrDefault(command);

				if (retorno == null)
					return default;

				return new Usuario
				{
					SellerCode = retorno.SellerCode.ToString(),
					SellerName = retorno.SellerName.ToString(),
					CNPJorCPF = retorno.CNPJorCPF.ToString(),
					SellerPassword = retorno.SellerPassword.ToString(),
					SellerNickName = retorno.SellerNickName.ToString(),
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Usuario GetByCodigo(string codigo)
		{
			try
			{
				//var users = App.UserRepository.GetAll();
				//if (users == null || users.Count == 0) return null;
				//var retorno = users.FirstOrDefault(x => x.SellerCode.Equals(codigo));//  $@"SELECT * FROM Usuario WHERE SellerCode = '{codigo}'";
				var command = $@"SELECT A.*,Count(B.Id) AS QtdVendedoresNaEquipe FROM USUARIO A LEFT JOIN Vendedor B on B.CdRcaxxx =A.SellerCode WHERE A.SellerCode='{codigo}';";
				var data = _context.ExcecutarSelectFirstOrDefault(command);
				
				if (data== null)
					return default;

				return new Usuario
				{
					SellerCode = data.SellerCode.ToString(),
					SellerName = data.SellerName.ToString(),
					CNPJorCPF = data.CNPJorCPF.ToString(),
					SellerPassword = data.SellerPassword.ToString(),
					SellerNickName = data.SellerNickName.ToString(),
					QtdVendedoresNaEquipe = Convert.ToInt32(data.QtdVendedoresNaEquipe)
				};
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void SaveUsuers(List<Usuario> users)
		{
			try
			{
				if (users != null && users.Any())
				{
					var usersCadastrados = App.UserRepository.GetAll();
					var scriptCommand = new StringBuilder();

					foreach (var item in users)
					{
						var existingUser = usersCadastrados.FirstOrDefault(x => x.SellerCode.Equals(item.SellerCode));
						var exists = existingUser != null;

						if (!exists)
						{
							var sellerName = item.SellerName.Contains("'") ? item.SellerName.Replace("'", "''") : item.SellerName;
							var commandInsert = $@"INSERT INTO [Usuario](
                                                          SellerCode 
                                                         ,SellerName 
                                                         ,CNPJorCPF 
                                                         ,SellerPassword 
                                                         ,SellerNickName)
                                                            VALUES (
                                                         '{item.SellerCode}'
                                                        ,'{sellerName}'
                                                        ,'{item.CNPJorCPF}'
                                                        ,'{item.SellerPassword}'
                                                        ,'{item.SellerNickName}');";
							scriptCommand.AppendLine(commandInsert);
						}
						else
						{
							if (existingUser.SellerPassword != item.SellerPassword)
							{
								var commandUpdate = $@"UPDATE [Usuario] 
                                   SET SellerPassword = '{item.SellerPassword}' 
                                   WHERE SellerCode = '{item.SellerCode}';";
								scriptCommand.AppendLine(commandUpdate);
							}
						}
					}
					var command = scriptCommand.ToString();
					_context.ExcecutarComandoCrud(command);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public void Add(Usuario user)
		{
			var command = @$"INSERT INTO Usuario(
                                           SellerCode 
                                          ,SellerName 
                                          ,CNPJorCPF 
                                          ,SellerPassword 
                                          ,SellerNickName)
                                           VALUES(
                                           '{user.SellerCode}'
                                          ,'{user.SellerName}'
                                          ,'{user.CNPJorCPF}'
                                          ,'{user.SellerPassword}'
                                          ,'{user.SellerNickName}');";

			_context.ExcecutarComandoCrud(command);
		}

		public void AddRange(List<Usuario> users)
		{
			if (users != null && users.Count > 0)
			{
				var usersCadastrados = App.UserRepository.GetAll();
				var scriptCommand = new StringBuilder();
				foreach (var item in users)
				{
					var exists = users != null && usersCadastrados.FirstOrDefault(x => x.SellerCode.Equals(item.SellerCode)) != null;

					if (!exists)
					{
						var sellerName = item.SellerName.Contains("'") ? item.SellerName.Replace("'", "''") : item.SellerName;
						var commandInsert = $@"INSERT INTO [Usuario](
                                                          SellerCode 
                                                         ,SellerName 
                                                         ,CNPJorCPF 
                                                         ,SellerPassword 
                                                         ,SellerNickName)
                                                            VALUES (
                                                         '{item.SellerCode}'
                                                        ,'{sellerName}'
                                                        ,'{item.CNPJorCPF}'
                                                        ,'{item.SellerPassword}'
                                                        ,'{item.SellerNickName}');";
						scriptCommand.AppendLine(commandInsert);
					}
				}
				var command = scriptCommand.ToString();
				_context.ExcecutarComandoCrud(command);
			}
		}

		public void Delete(int id)
		{
			var command = @$"DELETE FROM Usuario WHERE Id = {id};";
			_context.ExcecutarComandoCrud(command);
		}

		public int GetTotal()
		{
			try
			{
				var command = $@"SELECT COUNT(*) FROM Usuario AS Total;";
				var retorno = _context.ExcecutarSelectFirstOrDefault(command);

				if (retorno == null)
					return 0;


				var fields = retorno as IDictionary<string, object>;
				var _total = fields["COUNT(*)"];


				int.TryParse(_total.ToString(), out int total);
				return total;
			}
			catch (Exception ex)
			{
				App.LogRepository.Add(new Log
				{
					Data = DateTime.Now,
					ErrorDetail = ex.Message,
					Pagina = ApiMinertalTypes.Vendedor.ToString()
				});
				return 0;
			}
		}

		public void CriarTabela()
		{
			Init();
		}
	}
}

