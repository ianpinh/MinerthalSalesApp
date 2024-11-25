using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class ClientePlanoPagamentoRepository : IClientePlanoPagamentoRepository
    {
        private readonly IAppthalContext _context;
        public ClientePlanoPagamentoRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS ClientePlanoPagamento(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdCliente VARCHAR(15)
                                                 ,Loja VARCHAR(15)
                                                 ,CdPlpag VARCHAR(15)
                                                 ,CdRcaxxx INT);";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ClientePlanoPagamento GetById(int id)
        {
            var command = $@"SELECT * FROM ClientePlanoPagamento WHERE Id={id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new ClientePlanoPagamento();

            return new ClientePlanoPagamento
            {
                Id = retorno.Id != null ? Convert.ToInt32(retorno.Id) : 0,
                CdCliente = retorno.CdCliente.ToString(),
                Loja = retorno.Loja.ToString(),
                CdPlpag = retorno.CdPlpag.ToString(),
                CdRcaxxx = retorno.CdRcaxxx != null ? Convert.ToInt32(retorno.CdRcaxxx) : 0,
            };
        }
        public List<ClientePlanoPagamento> GetAll()
        {
            var command = $@"SELECT * FROM ClientePlanoPagamento";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<ClientePlanoPagamento>();

            var lstuser = new List<ClientePlanoPagamento>();
            foreach (var item in retorno)
            {
                lstuser.Add(new ClientePlanoPagamento
                {
                    Id = item.Id != null ? Convert.ToInt32(item.Id) : 0,
                    CdCliente = item.CdCliente.ToString(),
                    Loja = item.Loja.ToString(),
                    CdPlpag = item.CdPlpag.ToString(),
                    CdRcaxxx = item.CdRcaxxx != null ? Convert.ToInt32(item.CdRcaxxx) : 0,
                });
            }
            return lstuser;
        }
        public void Add(ClientePlanoPagamento plano)
        {
            if (plano != null)
            {
                var scriptCommand = new StringBuilder();

                var commandInsert = $@"INSERT INTO [ClientePlanoPagamento](
                                                            CdCliente
                                                            ,Loja
                                                            ,CdPlpag
                                                            ,CdRcaxxx)
                                                   VALUES(
                                                             '{plano.CdCliente}'
                                                            ,'{plano.Loja}'
                                                            ,'{plano.CdPlpag}'
                                                            ,'{plano.CdRcaxxx}');";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void AddRange(List<ClientePlanoPagamento> planos)
        {
            if (planos != null && planos.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in planos)
                {
                    var commandInsert = $@"INSERT INTO [ClientePlanoPagamento](
                                                             CdCliente
                                                            ,Loja
                                                            ,CdPlpag
                                                            ,CdRcaxxx)
                                                   VALUES(
                                                             '{item.CdCliente}'
                                                            ,'{item.Loja}'
                                                            ,'{item.CdPlpag}'
                                                            ,'{item.CdRcaxxx}');";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public void DeleteAll()
        {
            var command = @$"DELETE FROM ClientePlanoPagamento;";
            _context.ExcecutarComandoCrud(command);
        }
        public void Delete(int id)
        {
            var command = @$"DELETE FROM ClientePlanoPagamento WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }
        public void Save(List<ClientePlanoPagamento> plano)
        {
            if (plano != null && plano.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM ClientePlanoPagamento;");

                foreach (var item in plano)
                {
                    var commandInsert = $@"INSERT INTO [ClientePlanoPagamento](
                                                            CdCliente
                                                            ,Loja
                                                            ,CdPlpag
                                                            ,CdRcaxxx)
                                                   VALUES(
                                                             '{item.CdCliente}'
                                                            ,'{item.Loja}'
                                                            ,'{item.CdPlpag}'
                                                            ,'{item.CdRcaxxx}');";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public int RecuperarPlanoPadrao(string codigo, string loja)
        {
            var command = $@"SELECT * FROM ClientePlanoPagamento WHERE CdCliente='{codigo}' AND Loja='{loja}';";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null) return 0;

            return int.TryParse(retorno.CdPlpag, out int pl) ? pl : 0;

        }
        public void CriarTabela()
        {
            Init();
        }
    }
}
