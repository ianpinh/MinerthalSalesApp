using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using Newtonsoft.Json;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
	public class VendedorRepository : IVendedorRepository
	{
		private readonly IAppthalContext _context;
		public VendedorRepository(IAppthalContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			Init();
		}
		private void Init()
		{
			var command = $@"CREATE TABLE IF NOT EXISTS Vendedor(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdRca VARCHAR(150)
                                                  ,NmRca VARCHAR(255)
                                                  ,NmCidade VARCHAR(255)
                                                  ,CdUf VARCHAR(20)
                                                  ,NrCpf VARCHAR(14)
                                                  ,NrFone VARCHAR(14)
                                                  ,NrCelular VARCHAR(14)
                                                  ,NmEmail VARCHAR(100)
                                                  ,TabPreco VARCHAR(20)
                                                  ,CdRcaxxx VARCHAR(20));";
			_context.ExcecutarComandoCrud(command);
		}

		public List<Vendedor> GetAll()
		{
			var command = $@"SELECT * FROM Vendedor;";
			var retorno = _context.ExcecutarSelect(command);

			if (retorno == null)
				return new List<Vendedor>();

			var lst = new List<Vendedor>();

			foreach (var item in retorno)
			{
				lst.Add(new Vendedor
				{
					Id = Convert.ToInt32(item.Id),
					CdRca = item.CdRca.ToString(),
					NmRca = item.NmRca.ToString(),
					NmCidade = item.NmCidade.ToString(),
					CdUf = item.CdUf.ToString(),
					NrCpf = item.NrCpf.ToString(),
					NrFone = item.NrFone.ToString(),
					NrCelular = item.NrCelular.ToString(),
					NmEmail = item.NmEmail.ToString(),
					TabPreco = item.TabPreco.ToString(),
					CdRcaxxx = item.CdRcaxxx.ToString(),
				});
			}

			return lst;
		}

		public Vendedor GetById(int id)
		{
			var command = $@"SELECT * FROM Vendedor WHERE Id = {id};";
			var retorno = _context.ExcecutarSelectFirstOrDefault(command);

			if (retorno == null)
				return new Vendedor();

			return new Vendedor
			{
				Id = Convert.ToInt32(retorno.Id),
				CdRca = retorno.CdRca.ToString(),
				NmRca = retorno.NmRca.ToString(),
				NmCidade = retorno.NmCidade.ToString(),
				CdUf = retorno.CdUf.ToString(),
				NrCpf = retorno.NrCpf.ToString(),
				NrFone = retorno.NrFone.ToString(),
				NrCelular = retorno.NrCelular.ToString(),
				NmEmail = retorno.NmEmail.ToString(),
				TabPreco = retorno.TabPreco.ToString(),
				CdRcaxxx = retorno.CdRcaxxx.ToString(),
			};
		}

		public Vendedor GetByCodigo(string rca)
		{
			var command = $@"SELECT * FROM Vendedor WHERE CdRca = '{rca}';";
			var retorno = _context.ExcecutarSelectFirstOrDefault(command);

			if (retorno == null)
				return new Vendedor();

			return new Vendedor
			{
				Id = Convert.ToInt32(retorno.Id),
				CdRca = retorno.CdRca.ToString(),
				NmRca = retorno.NmRca.ToString(),
				NmCidade = retorno.NmCidade.ToString(),
				CdUf = retorno.CdUf.ToString(),
				NrCpf = retorno.NrCpf.ToString(),
				NrFone = retorno.NrFone.ToString(),
				NrCelular = retorno.NrCelular.ToString(),
				NmEmail = retorno.NmEmail.ToString(),
				TabPreco = retorno.TabPreco.ToString(),
				CdRcaxxx = retorno.CdRcaxxx.ToString(),
			};
		}

		public IEnumerable<Vendedor> GetByCodigoSupervisor(string rcaxxx)
		{
			var command = $@"SELECT * FROM Vendedor WHERE CdRcaxxx = '{rcaxxx}';";
			var retorno = _context.ExcecutarSelect(command);

			if (retorno == null)
				return Enumerable.Empty<Vendedor>();

			var listaVendedores = new List<Vendedor>();

			if (retorno != null && retorno.Any())
			{
				foreach (var item in retorno)
				{

					var seller = new Vendedor
					{
						Id = Convert.ToInt32(item.Id),
						CdRca = item.CdRca.ToString(),
						NmRca = item.NmRca.ToString(),
						NmCidade = item.NmCidade.ToString(),
						CdUf = item.CdUf.ToString(),
						NrCpf = item.NrCpf.ToString(),
						NrFone = item.NrFone.ToString(),
						NrCelular = item.NrCelular.ToString(),
						NmEmail = item.NmEmail.ToString(),
						TabPreco = item.TabPreco.ToString(),
						CdRcaxxx = item.CdRcaxxx.ToString(),
					};
					listaVendedores.Add(seller);
				}
			}

			return listaVendedores;
		}
		public void Add(Vendedor vendedor)
		{
			try
			{
				var nmCidade = vendedor.NmCidade.Contains("'") ? vendedor.NmCidade.Replace("'", "''") : vendedor.NmCidade;
				var nmRca = vendedor.NmRca.Contains("'") ? vendedor.NmRca.Replace("'", "''") : vendedor.NmRca;
				var command = $@"INSERT INTO [Vendedor](
                                                                   CdRca
                                                                  ,NmRca
                                                                  ,NmCidade
                                                                  ,CdUf
                                                                  ,NrCpf
                                                                  ,NrFone
                                                                  ,NrCelular
                                                                  ,NmEmail
                                                                  ,TabPreco
                                                                  ,CdRcaxxx)
                                                                      VALUES (
                                                                    '{vendedor.CdRca}'
                                                                   ,'{nmRca}'
                                                                   ,'{nmCidade}'
                                                                   ,'{vendedor.CdUf}'
                                                                   ,'{vendedor.NrCpf}'
                                                                   ,'{vendedor.NrFone}'
                                                                   ,'{vendedor.NrCelular}'
                                                                   ,'{vendedor.NmEmail}'
                                                                   ,'{vendedor.TabPreco}'
                                                                   ,'{vendedor.CdRcaxxx}');";

				_context.ExcecutarComandoCrud(command);
			}
			catch (Exception)
			{
				throw;
			}

		}

		public void AddRange(List<Vendedor> salers)
		{
			if (salers != null && salers.Any())
			{
				var scriptCommand = new StringBuilder();

				foreach (var item in salers)
				{
					var nmCidade = item.NmCidade.Contains("'") ? item.NmCidade.Replace("'", "''") : item.NmCidade;
					var nmRca = item.NmRca.Contains("'") ? item.NmRca.Replace("'", "''") : item.NmRca;
					var commandInsert = $@"INSERT INTO [Vendedor](
                                                                   CdRca
                                                                  ,NmRca
                                                                  ,NmCidade
                                                                  ,CdUf
                                                                  ,NrCpf
                                                                  ,NrFone
                                                                  ,NrCelular
                                                                  ,NmEmail
                                                                  ,TabPreco
                                                                  ,CdRcaxxx)
                                                                      VALUES (
                                                                    '{item.CdRca}'
                                                                   ,'{nmRca}'
                                                                   ,'{nmCidade}'
                                                                   ,'{item.CdUf}'
                                                                   ,'{item.NrCpf}'
                                                                   ,'{item.NrFone}'
                                                                   ,'{item.NrCelular}'
                                                                   ,'{item.NmEmail}'
                                                                   ,'{item.TabPreco}'
                                                                   ,'{item.CdRcaxxx}');";
					scriptCommand.AppendLine(commandInsert);
				}

				var command = scriptCommand.ToString();
				_context.ExcecutarComandoCrud(command);
			}
		}

		public void DeleteById(int id)
		{
			var command = $"Delete from Vendedor WHERE Id={id};";
			_context.ExcecutarComandoCrud(command);
		}

		public void DeleteAll()
		{
			var command = $"Delete from Vendedor;";
			_context.ExcecutarComandoCrud(command);
		}

		public int GetTotal()
		{
			var command = $@"SELECT COUNT(*) FROM Vendedor;";
			var retorno = _context.ExcecutarSelectFirstOrDefault(command);

			if (retorno == null)
				return 0;

			var fields = retorno as IDictionary<string, object>;
			var _total = fields["COUNT(*)"];

			_ = int.TryParse(_total.ToString(), out int total);
			return total;
		}

		public void SaveVendedor(List<Vendedor> salers)
		{
			if (salers != null && salers.Any())
			{
				var vendedoresCadastrados = App.VendedorRepository.GetAll();

				var scriptCommand = new StringBuilder();
				scriptCommand.AppendLine("DELETE FROM Vendedor;");

				foreach (var item in salers)
				{
					var nmCidade = item.NmCidade.Contains("'") ? item.NmCidade.Replace("'", "''") : item.NmCidade;
					var nmRca = item.NmRca.Contains("'") ? item.NmRca.Replace("'", "''") : item.NmRca;
					var commandInsert = $@"INSERT INTO [Vendedor](
                                                                   CdRca
                                                                  ,NmRca
                                                                  ,NmCidade
                                                                  ,CdUf
                                                                  ,NrCpf
                                                                  ,NrFone
                                                                  ,NrCelular
                                                                  ,NmEmail
                                                                  ,TabPreco
                                                                  ,CdRcaxxx)
                                                                      VALUES (
                                                                    '{item.CdRca}'
                                                                   ,'{nmRca}'
                                                                   ,'{nmCidade}'
                                                                   ,'{item.CdUf}'
                                                                   ,'{item.NrCpf}'
                                                                   ,'{item.NrFone}'
                                                                   ,'{item.NrCelular}'
                                                                   ,'{item.NmEmail}'
                                                                   ,'{item.TabPreco}'
                                                                   ,'{item.CdRcaxxx}');";
					scriptCommand.AppendLine(commandInsert);

					//if (vendedoresCadastrados == null || !vendedoresCadastrados.Any() || vendedoresCadastrados.Contains(item))
					//{
					//	var nmCidade = item.NmCidade.Contains("'") ? item.NmCidade.Replace("'", "''") : item.NmCidade;
					//	var nmRca = item.NmRca.Contains("'") ? item.NmRca.Replace("'", "''") : item.NmRca;
					//	var commandInsert = $@"INSERT INTO [Vendedor](
     //                                                              CdRca
     //                                                             ,NmRca
     //                                                             ,NmCidade
     //                                                             ,CdUf
     //                                                             ,NrCpf
     //                                                             ,NrFone
     //                                                             ,NrCelular
     //                                                             ,NmEmail
     //                                                             ,TabPreco
     //                                                             ,CdRcaxxx)
     //                                                                 VALUES (
     //                                                               '{item.CdRca}'
     //                                                              ,'{nmRca}'
     //                                                              ,'{nmCidade}'
     //                                                              ,'{item.CdUf}'
     //                                                              ,'{item.NrCpf}'
     //                                                              ,'{item.NrFone}'
     //                                                              ,'{item.NrCelular}'
     //                                                              ,'{item.NmEmail}'
     //                                                              ,'{item.TabPreco}'
     //                                                              ,'{item.CdRcaxxx}');";
					//	scriptCommand.AppendLine(commandInsert);
					//}
				}

				var command = scriptCommand.ToString();

				if (!string.IsNullOrEmpty(command))
					_context.ExcecutarComandoCrud(command);
			}
		}

		public void CriarTabela()
		{
			Init();
		}
	}
}