using Android.Media.TV;
using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class TitulosRepositoy : ITitulosRepositoy
    {
        private string NomeTabelaTitulo => RecuperarNomeDaTabela();
        private readonly IAppthalContext _context;
        public TitulosRepositoy(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init(NomeTabelaTitulo);
        }

        private void Init(string tableName)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS {tableName}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdRca VARCHAR(15)
                                                  ,CdRcaxxx VARCHAR(15)
                                                  ,QtFatSc DECIMAL(7,2)
                                                  ,QtFatCabsupl DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        public void SaveTitulos(List<Titulo> titulos)
        {
            if (titulos != null && titulos.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM {NomeTabelaTitulo};");
                foreach (var item in titulos)
                {
                    var commandInsert = $@"INSERT INTO [{NomeTabelaTitulo}](
                                                         CdRca
                                                        ,CdRcaxxx
                                                        ,QtFatSc
                                                        ,QtFatCabsupl)
                                                            VALUES (
                                                         '{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtFatSc.ToStringInvariant("0.00")}
                                                        , {item.QtFatCabsupl.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public List<Titulo> RecuperarTodosTitulos()
        {
            var command = $@"SELECT * FROM {NomeTabelaTitulo};";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Titulo>();

            var lstPlanos = new List<Titulo>();

            foreach (var item in retorno)
            {

                lstPlanos.Add(new Titulo
                {
                    Id = Convert.ToInt32(item.Id),
                    CdRca = item.CdRca.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    QtFatSc = item.QtFatSc != null ? Convert.ToDecimal(item.QtFatSc) : 0M,
                    QtFatCabsupl = item.QtFatCabsupl != null ? Convert.ToDecimal(item.QtFatCabsupl) : 0M,
                });
            }

            return lstPlanos;
        }

        public List<Titulo> RecuperarTodosTitulosDoCliente(string codCliente)
        {
            var command = $@"SELECT * FROM {NomeTabelaTitulo} WHERE CdCliente='{codCliente}'";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Titulo>();

            var lstPlanos = new List<Titulo>();

            foreach (var item in retorno)
            {

                lstPlanos.Add(new Titulo
                {
                    Id = Convert.ToInt32(item.Id),
                    CdRca = item.CdRca.ToString(),
                    CdRcaxxx = item.CdRcaxxx.ToString(),
                    QtFatSc = item.QtFatSc != null ? Convert.ToDecimal(item.QtFatSc) : 0M,
                    QtFatCabsupl = item.QtFatCabsupl != null ? Convert.ToDecimal(item.QtFatCabsupl) : 0M,
                });
            }

            return lstPlanos;
        }

        public void CriarTabela()
        {
            Init(NomeTabelaTitulo);
        }

        public void SaveTitulosVendedor(List<Titulo> titulos, string codigoVendedor)
        {
            if (titulos != null && titulos.Any())
            {
                CriarTabelaTituloVendedor(codigoVendedor);
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine($"DELETE FROM Titulo_{codigoVendedor};");
                foreach (var item in titulos)
                {
                    var commandInsert = $@"INSERT INTO Titulo_{codigoVendedor}(
                                                         CdRca
                                                        ,CdRcaxxx
                                                        ,QtFatSc
                                                        ,QtFatCabsupl)
                                                            VALUES (
                                                         '{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtFatSc.ToStringInvariant("0.00")}
                                                        , {item.QtFatCabsupl.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        private void CriarTabelaTituloVendedor(string codigoVendedor)
        {
            var command = $@"CREATE TABLE IF NOT EXISTS Titulo_{codigoVendedor}(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdRca VARCHAR(15)
                                                  ,CdRcaxxx VARCHAR(15)
                                                  ,QtFatSc DECIMAL(7,2)
                                                  ,QtFatCabsupl DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        private string RecuperarNomeDaTabela()
        {
            try
            {
                if (App.VendedorSelecionado != null)
                {
                    var tableName = $"Titulo_{App.VendedorSelecionado.CodigoVendedor}";
                    Init(tableName);
                    return tableName;
                }

                return "Titulo";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
