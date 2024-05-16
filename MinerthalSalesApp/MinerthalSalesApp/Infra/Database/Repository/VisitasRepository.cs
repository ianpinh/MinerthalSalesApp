using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class VisitasRepository : IVisitasRepository
    {
        private readonly IAppthalContext _context;
        public VisitasRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS Visita(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdCliente VARCHAR(20)
                                                 ,DtReg VARCHAR(150)
                                                 ,IsClienteNovo VARCHAR(20)
                                                 ,ProximaVisita VARCHAR(20)
                                                 ,NmCliente VARCHAR(20)
                                                 ,Ocorrencia VARCHAR(300)
                                                 ,Cidade VARCHAR(150)
                                                 ,Uf VARCHAR(20)
                                                 ,CdRcaxxx VARCHAR(20));";
            _context.ExcecutarComandoCrud(command);
        }
        public void SaveVisitasAsync(List<Visita> visitas)
        {
            if (visitas!=null && visitas.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM Visita;");

                foreach (var item in visitas)
                {
                    var nmCliente = item.NmCliente.Contains("'") ? item.NmCliente.Replace("'", "''") : item.NmCliente;
                    var ocorrencia = item.Ocorrencia.Contains("'") ? item.Ocorrencia.Replace("'", "''") : item.Ocorrencia;
                    var cidade = item.Cidade.Contains("'") ? item.Cidade.Replace("'", "''") : item.Cidade;
                    var commandInsert = $@"INSERT INTO [Visita](
                                                         CdCliente
                                                        ,DtReg
                                                        ,IsClienteNovo
                                                        ,ProximaVisita
                                                        ,NmCliente
                                                        ,Ocorrencia
                                                        ,Cidade
                                                        ,Uf        
                                                        ,CdRcaxxx)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.DtReg}'
                                                        ,'{item.IsClienteNovo}'
                                                        ,'{item.ProximaVisita}'
                                                        ,'{nmCliente}' 
                                                        ,'{ocorrencia}'
                                                        ,'{cidade}'
                                                        ,'{item.Uf}'
                                                        ,'{item.CdRcaxxx}');";

                    scriptCommand.AppendLine(commandInsert);
                }



                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }
        public IEnumerable<Visita> RecuperarTodasVisitas()
        {

            var command = $@"SELECT * FROM Visita";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new List<Visita>();

            var lst = new List<Visita>();

            foreach (var item in retorno)
            {
                lst.Add(new Visita
                {
                    CdCliente=item.CdClient.ToString(),
                    DtReg=item.DtReg.ToString(),
                    IsClienteNovo=item.IsClient.ToString(),
                    ProximaVisita=item.ProximaV.ToString(),
                    NmCliente=item.NmClient.ToString(),
                    Ocorrencia=item.Ocorrenc.ToString(),
                    Cidade=item.Cidade.ToString(),
                    Uf=item.Uf.ToString(),
                    CdRcaxxx=item.CdRcaxxx.ToString()
                });
            }

            return lst;
        }
        public IEnumerable<Visita> RecuperarTodasVisitasDoCliente(string codCliente)
        {
            var command = $@"SELECT * FROM Visita WHERE CdCliente='{codCliente}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Visita>();

            var lst = new List<Visita>();

            foreach (var item in retorno)
            {
                try
                {
                    var visita = new Visita();
                    visita.Id=item.Id!=null ? Convert.ToInt32(item.Id) : 0;
                    visita.CdCliente=item.CdCliente!=null ? item.CdCliente.ToString() : "";
                    visita.DtReg=item.DtReg!=null ? item.DtReg.ToString() : "";
                    visita.IsClienteNovo=item.IsClienteNovo!=null ? item.IsClienteNovo.ToString() : "";
                    visita.ProximaVisita=item.ProximaVisita!=null ? item.ProximaVisita.ToString() : "";
                    visita.NmCliente=item.NmCliente!=null ? item.NmCliente.ToString() : "";
                    visita.Ocorrencia=item.Ocorrencia!=null ? item.Ocorrencia.ToString() : "";
                    visita.Cidade=item.Cidade!=null ? item.Cidade.ToString() : "";
                    visita.Uf=item.Uf!=null ? item.Uf.ToString() : "";
                    visita.CdRcaxxx=item.CdRcaxxx!=null ? item.CdRcaxxx.ToString() : "";
                    lst.Add(visita);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return lst;

        }

        public void CriarTabela()
        {
            Init();
        }
    }
}