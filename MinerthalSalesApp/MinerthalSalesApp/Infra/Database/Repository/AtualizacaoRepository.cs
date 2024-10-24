using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
	public class AtualizacaoRepository : IAtualizacaoRepository
    {
        private readonly IAppthalContext _context;

        public AtualizacaoRepository(IAppthalContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
            Init();

        }

        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Atualizacoes(
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT
                                               ,DataAtualizacao DateTime
                                               ,NomeTabela VARCHAR(50) null);";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Atualizacoes> GetAll()
        {
            var command = $@"SELECT * FROM Atualizacoes;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Atualizacoes>();

            var lst = new List<Atualizacoes>();

            foreach (var item in retorno)
            {
                DateTime.TryParse(item.DataAtualizacao.ToString(), out DateTime data);
                lst.Add(new Atualizacoes
                {
                    DataAtualizacao = data,
                    NomeTabela = item.NomeTabela.ToString(),
                });
            }

            return lst;

        }

        public void Add(Atualizacoes log)
        {
            var command = @$"INSERT INTO Atualizacoes(DataAtualizacao,NomeTabela )VALUES('{log.DataAtualizacao}','{log.NomeTabela}' );";
            _context.ExcecutarComandoCrud(command);
        }

        public void AddRange(List<Atualizacoes> logs)
        {
            if (logs !=null && logs.Count>0)
                foreach (var item in logs)
                    Add(item);
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM Atualizacoes WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public DateTime? GetLastLog(string tabela)
        {
            var command = @$"SELECT MAX(DataAtualizacao) FROM Atualizacoes WHERE NomeTabela ='{tabela}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return default;

            var valid = DateTime.TryParse(retorno.ToString(), out DateTime data);

            if (valid)
                return data;
            else
                return default;
        }

        public int GetTotal()
        {
            try
            {
                var command = $@"SELECT COUNT(*) FROM Atualizacoes;";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return 0;

                var fields = retorno as IDictionary<string, object>;
                var _total = fields["COUNT(*)"];

                _=int.TryParse(_total.ToString(), out int total);
                return total;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAllTables(string dropCommand)
        {
            _context.ExcecutarComandoCrud(dropCommand);
        }

        public bool GetByTableName(string tableName)
        {
            var command = $@"SELECT * FROM Atualizacoes WHERE NomeTabela='{tableName}';";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);
            return retorno != null;
        }

        public void CriarTabela()
        {
            Init();
        }

		public void ClearAllTables()
		{
            try
            {
				var sb = new StringBuilder();
				sb.AppendLine("delete from Atualizacoes; delete from sqlite_sequence where name = 'Atualizacoes'");
				sb.AppendLine("delete from HistoricoDePedidos; delete from sqlite_sequence where name = 'HistoricoDePedidos'");
				sb.AppendLine("delete from ResumoPedido; delete from sqlite_sequence where name = 'ResumoPedido'");
				sb.AppendLine("delete from Carrinho; delete from sqlite_sequence where name = 'Carrinho'");
				sb.AppendLine("delete from MeusPedidos; delete from sqlite_sequence where name = 'MeusPedidos'");
				sb.AppendLine("delete from Titulo; delete from sqlite_sequence where name = 'Titulo'");
				sb.AppendLine("delete from Cliente; delete from sqlite_sequence where name = 'Cliente'");
				sb.AppendLine("delete from Pedido; delete from sqlite_sequence where name = 'Pedido'");
				sb.AppendLine("delete from Faturamento; delete from sqlite_sequence where name = 'Faturamento'");
				sb.AppendLine("delete from Visita; delete from sqlite_sequence where name = 'Visita'");
                var clearCommand = sb.ToString();

				_context.ExcecutarComandoCrudNoCommit(clearCommand);
			}
            catch (Exception)
            {

                throw;
            }
		}
	}
}


