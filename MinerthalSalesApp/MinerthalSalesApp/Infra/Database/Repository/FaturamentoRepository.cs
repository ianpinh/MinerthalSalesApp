using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class FaturamentoRepository : IFaturamentoRepository
    {
        private readonly IAppthalContext _context;
        public FaturamentoRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Faturamento(
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                ,CdCliente VARCHAR(20)
                                                ,NrDocum VARCHAR(20)
                                                ,NrParcel VARCHAR(20)
                                                ,DtEmissao VARCHAR(12)
                                                ,DtVenc VARCHAR(12)
                                                ,TpCobran VARCHAR(20)
                                                ,CdRca VARCHAR(20)
                                                ,CdRcaxxx VARCHAR(20)
                                                ,QtDiaatr INT
                                                ,Juros DECIMAL(7,2)
                                                ,VlDocum DECIMAL(7,2)
                                                ,VlJuro DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Faturamento> GetAll()
        {
            try
            {
                var command = $@"SELECT F.* ,(SELECT C.A1Nome FROM Cliente C WHERE C.A1Loja = (SELECT substr(F.CdCliente,7,2)) AND C.A1Cod = (SELECT substr(F.CdCliente,0,7))) AS NomeCliente FROM Faturamento F;";
                //var command = $@"SELECT * FROM Faturamento";
                var retorno = _context.ExcecutarSelect(command);

                if (retorno == null)
                    return new List<Faturamento>();

                var lstuser = new List<Faturamento>();
                foreach (var item in retorno)
                {
                    lstuser.Add(new Faturamento
                    {
                        NomeCliente = item.NomeCliente?.ToString(),
                        Id=item.Id!=null ? Convert.ToInt32(item.Id) : 0,
                        CdCliente=item.CdCliente.ToString(),
                        NrDocum=item.NrDocum.ToString(),
                        NrParcel=item.NrParcel.ToString(),
                        DtEmissao=item.DtEmissao.ToString(),
                        DtVenc=item.DtVenc.ToString(),
                        TpCobran=item.TpCobran.ToString(),
                        CdRca=item.CdRca.ToString(),
                        CdRcaxxx=item.CdRcaxxx.ToString(),
                        QtDiaatr=item.QtDiaatr!=null ? Convert.ToInt32(item.QtDiaatr) : 0,
                        Juros=item.Juros!=null ? Convert.ToDecimal(item.Juros) : 0M,
                        VlDocum=item.VlDocum!=null ? Convert.ToDecimal(item.VlDocum) : 0M,
                        VlJuro=item.VlJuro!=null ? Convert.ToDecimal(item.VlJuro) : 0M,
                    });
                }
                return lstuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Faturamento> GetByCodigo(string codCliente)
        {
            var command = $@"SELECT * FROM Faturamento WHERE CdCliente  = '{codCliente}';";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Faturamento>();

            var lstuser = new List<Faturamento>();
            foreach (var item in retorno)
            {

                try
                {
                    var teste = new Faturamento();

                    teste.Id=item.Id!=null ? Convert.ToInt32(item.Id) : 0;
                    teste.CdCliente=item.CdCliente!=null ? item.CdCliente.ToString() : "";
                    teste.NrDocum=item.NrDocum!=null ? item.NrDocum.ToString() : "";
                    teste.NrParcel=item.NrParcel!=null ? item.NrParcel.ToString() : "";
                    teste.DtEmissao=item.DtEmissao!=null ? item.DtEmissao.ToString() : "";
                    teste.DtVenc=item.DtVenc!=null ? item.DtVenc.ToString() : "";
                    teste.TpCobran=item.TpCobran!=null ? item.TpCobran.ToString() : "";
                    teste.CdRca=item.CdRca!=null ? item.CdRca.ToString() : "";
                    teste.CdRcaxxx=item.CdRcaxxx!=null ? item.CdRcaxxx.ToString() : "";
                    teste.QtDiaatr=item.QtDiaatr!=null ? Convert.ToInt32(item.QtDiaatr) : 0;
                    teste.Juros=item.Juros!=null ? Convert.ToDecimal(item.Juros) : 0M;
                    teste.VlDocum=item.VlDocum!=null ? Convert.ToDecimal(item.VlDocum) : 0M;
                    teste.VlJuro=item.VlJuro!=null ? Convert.ToDecimal(item.VlJuro) : 0M;

                    lstuser.Add(teste);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return lstuser;
        }

        public void SaveFaturamento(List<Faturamento> fatura)
        {
            if (fatura!=null && fatura.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM Faturamento;");

                foreach (var item in fatura)
                {
                    var commandInsert = $@"INSERT INTO [Faturamento](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.NrDocum}'
                                                        ,'{item.NrParcel}'
                                                        ,'{item.DtEmissao}'
                                                        ,'{item.DtVenc}'
                                                        ,'{item.TpCobran}'
                                                        ,'{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtDiaatr}
                                                        , {item.Juros.ToStringInvariant("0.00")}
                                                        , {item.VlDocum.ToStringInvariant("0.00")}
                                                        , {item.VlJuro.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Add(Faturamento fatura)
        {
            var commandInsert = $@"INSERT INTO [Faturamento](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{fatura.CdCliente}'
                                                        ,'{fatura.NrDocum}'
                                                        ,'{fatura.NrParcel}'
                                                        ,'{fatura.DtEmissao}'
                                                        ,'{fatura.DtVenc}'
                                                        ,'{fatura.TpCobran}'
                                                        ,'{fatura.CdRca}'
                                                        ,'{fatura.CdRcaxxx}'
                                                        , {fatura.QtDiaatr}
                                                        , {fatura.Juros.ToStringInvariant("0.00")}
                                                        , {fatura.VlDocum.ToStringInvariant("0.00")}
                                                        , {fatura.VlJuro.ToStringInvariant("0.00")});";
            _context.ExcecutarComandoCrud(commandInsert);
        }

        public void AddRange(List<Faturamento> fatura)
        {
            if (fatura!=null && fatura.Any())
            {
                var scriptCommand = new StringBuilder();
                foreach (var item in fatura)
                {
                    var commandInsert = $@"INSERT INTO [Faturamento](
                                                         CdCliente
                                                        ,NrDocum
                                                        ,NrParcel
                                                        ,DtEmissao
                                                        ,DtVenc
                                                        ,TpCobran
                                                        ,CdRca
                                                        ,CdRcaxxx
                                                        ,QtDiaatr
                                                        ,Juros
                                                        ,VlDocum
                                                        ,VlJuro)
                                                            VALUES (
                                                         '{item.CdCliente}'
                                                        ,'{item.NrDocum}'
                                                        ,'{item.NrParcel}'
                                                        ,'{item.DtEmissao}'
                                                        ,'{item.DtVenc}'
                                                        ,'{item.TpCobran}'
                                                        ,'{item.CdRca}'
                                                        ,'{item.CdRcaxxx}'
                                                        , {item.QtDiaatr}
                                                        , {item.Juros.ToStringInvariant("0.00")}
                                                        , {item.VlDocum.ToStringInvariant("0.00")}
                                                        , {item.VlJuro.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM Faturamento WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM Faturamento;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _=int.TryParse(_total.ToString(), out int total);
            return total;
        }

        public void CriarTabela()
        {
            Init();
        }

        public List<Faturamento> RecuperarTitulosAVencer()
        {
            var titulos = GetAll();
            if (titulos==null || titulos.Count==0)
                return new List<Faturamento>();

            var hoje = DateTime.Today;
            var titulosAvencer = titulos.Where(x => x.DataDeVencimento>=hoje/* && x.DataDeVencimento.Value.Month==hoje.Month*/).OrderByDescending(x => x.DataDeVencimento).ToList();

            return titulosAvencer;

        }
    }
}