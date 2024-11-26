using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class PlanosRepository : IPlanosRepository
    {
        private readonly IAppthalContext _context;
        public PlanosRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }

        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS Plano(
                                                   Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdPlano VARCHAR(20)
                                                  ,DsPlano VARCHAR(100)
                                                  ,QtDiaPrz VARCHAR(20)
                                                  ,NrColPrec VARCHAR(20)
                                                  ,NvPlano VARCHAR(20)
                                                  ,QtMaxParc VARCHAR(20)
                                                  ,TpPrazo VARCHAR(20)
                                                  ,InEspec VARCHAR(20)
                                                  ,TxObs VARCHAR(300)
                                                  ,NrItmin VARCHAR(20)
                                                  ,TpVenda VARCHAR(20)
                                                  ,CdRcaxxx INT
                                                  ,TxPerFin DECIMAL(7,2)
                                                  ,VlMinped DECIMAL(7,2)
                                                  ,VlDescpl DECIMAL(7,2)
                                                  ,TxPerfinProd DECIMAL(7, 2));";
            _context.ExcecutarComandoCrud(command);
        }

        public List<Plano> GetAll()
        {
            var command = $@"SELECT * FROM Plano;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Plano>();

            var lstPlanos = new List<Plano>();

            foreach (var item in retorno)
            {

                try
                {
                    var plano = new Plano();
                    plano.Id = item.Id != null ? Convert.ToInt32(item.Id) : 0;
                    plano.CdPlano = item.CdPlano != null ? item.CdPlano.ToString() : "";
                    plano.DsPlano = item.DsPlano != null ? item.DsPlano.ToString() : "";
                    plano.QtDiaPrz = item.QtDiaPrz != null ? item.QtDiaPrz.ToString() : "";
                    plano.NrColPrec = item.NrColPrec != null ? item.NrColPrec.ToString() : "";
                    plano.NvPlano = item.NvPlano != null ? item.NvPlano.ToString() : "";
                    plano.QtMaxParc = item.QtMaxParc != null ? item.QtMaxParc.ToString() : "";
                    plano.TpPrazo = item.TpPrazo != null ? item.TpPrazo.ToString() : "";
                    plano.InEspec = item.InEspec != null ? item.InEspec.ToString() : "";
                    plano.TxObs = item.TxObs != null ? item.TxObs.ToString() : "";
                    plano.NrItmin = item.NrItmin != null ? item.NrItmin.ToString() : "";
                    plano.TpVenda = item.TpVenda != null ? item.TpVenda.ToString() : "";
                    plano.CdRcaxxx = item.CdRcaxxx != null ? Convert.ToInt32(item.CdRcaxxx) : 0;
                    plano.TxPerFin = item.TxPerFin != null ? (decimal)item.TxPerFin : 0M;
                    plano.VlMinped = item.VlMinped != null ? Convert.ToInt32(item.VlMinped) : 0M;
                    plano.VlDescpl = item.VlDescpl != null ? Convert.ToInt32(item.VlDescpl) : 0M;
                    plano.TxPerfinProd = item.TxPerfinProd != null ? Convert.ToInt32(item.TxPerfinProd) : 0M;
                    lstPlanos.Add(plano);
                }
                catch (Exception)
                {
                    throw;
                }

            }

            return lstPlanos;
        }

        public void Add(Plano plano)
        {
            if (plano != null)
            {
                var scriptCommand = new StringBuilder();


                var commandInsert = $@"INSERT INTO [Plano](
                                                         CdPlano
                                                        ,DsPlano
                                                        ,QtDiaPrz
                                                        ,NrColPrec
                                                        ,NvPlano
                                                        ,QtMaxParc
                                                        ,TpPrazo
                                                        ,InEspec
                                                        ,TxObs
                                                        ,NrItmin
                                                        ,TpVenda
                                                        ,CdRcaxxx
                                                        ,TxPerFin
                                                        ,VlMinped
                                                        ,VlDescpl
                                                        ,TxPerfinProd)
                                                            VALUES (
                                                         '{plano.CdPlano}'
                                                        ,'{plano.DsPlano}'
                                                        ,'{plano.QtDiaPrz}'
                                                        ,'{plano.NrColPrec}'
                                                        ,'{plano.NvPlano}'
                                                        ,'{plano.QtMaxParc}'
                                                        ,'{plano.TpPrazo}'
                                                        ,'{plano.InEspec}'
                                                        ,'{plano.TxObs}'
                                                        ,'{plano.NrItmin}'
                                                        ,'{plano.TpVenda}'
                                                        , {plano.CdRcaxxx}
                                                        , {plano.TxPerFin.ToStringInvariant("0.00")}
                                                        , {plano.VlMinped.ToStringInvariant("0.00")}
                                                        , {plano.VlDescpl.ToStringInvariant("0.00")}
                                                        ,'{plano.TxPerfinProd.ToStringInvariant("0.00")}');";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void AddRange(List<Plano> planos)
        {
            if (planos != null && planos.Any())
            {
                var scriptCommand = new StringBuilder();
                foreach (var item in planos)
                {
                    var commandInsert = $@"INSERT INTO [Plano](
                                                         CdPlano
                                                        ,DsPlano
                                                        ,QtDiaPrz
                                                        ,NrColPrec
                                                        ,NvPlano
                                                        ,QtMaxParc
                                                        ,TpPrazo
                                                        ,InEspec
                                                        ,TxObs
                                                        ,NrItmin
                                                        ,TpVenda
                                                        ,CdRcaxxx
                                                        ,TxPerFin
                                                        ,VlMinped
                                                        ,VlDescpl
                                                        ,TxPerfinProd)
                                                            VALUES (
                                                         '{item.CdPlano}'
                                                        ,'{item.DsPlano}'
                                                        ,'{item.QtDiaPrz}'
                                                        ,'{item.NrColPrec}'
                                                        ,'{item.NvPlano}'
                                                        ,'{item.QtMaxParc}'
                                                        ,'{item.TpPrazo}'
                                                        ,'{item.InEspec}'
                                                        ,'{item.TxObs}'
                                                        ,'{item.NrItmin}'
                                                        ,'{item.TpVenda}'
                                                        , {item.CdRcaxxx}
                                                        , {item.TxPerFin.ToStringInvariant("0.00")}
                                                        , {item.VlMinped.ToStringInvariant("0.00")}
                                                        , {item.VlDescpl.ToStringInvariant("0.00")}
                                                        ,'{item.TxPerfinProd.ToStringInvariant("0.00")}');";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void DeleteById(int id)
        {
            var command = $"Delete from Plano WHERE Id={id};";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteAll()
        {
            var command = $"Delete from Plano;";
            _context.ExcecutarComandoCrud(command);
        }

        public void Save(List<Plano> planos)
        {
            if (planos != null && planos.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM Plano;");
                foreach (var item in planos)
                {
                    var commandInsert = $@"INSERT INTO [Plano](
                                                         CdPlano
                                                        ,DsPlano
                                                        ,QtDiaPrz
                                                        ,NrColPrec
                                                        ,NvPlano
                                                        ,QtMaxParc
                                                        ,TpPrazo
                                                        ,InEspec
                                                        ,TxObs
                                                        ,NrItmin
                                                        ,TpVenda
                                                        ,CdRcaxxx
                                                        ,TxPerFin
                                                        ,VlMinped
                                                        ,VlDescpl
                                                        ,TxPerfinProd)
                                                            VALUES (
                                                         '{item.CdPlano}'
                                                        ,'{item.DsPlano}'
                                                        ,'{item.QtDiaPrz}'
                                                        ,'{item.NrColPrec}'
                                                        ,'{item.NvPlano}'
                                                        ,'{item.QtMaxParc}'
                                                        ,'{item.TpPrazo}'
                                                        ,'{item.InEspec}'
                                                        ,'{item.TxObs}'
                                                        ,'{item.NrItmin}'
                                                        ,'{item.TpVenda}'
                                                        , {item.CdRcaxxx}
                                                        , {item.TxPerFin.ToStringInvariant("0.00")}
                                                        , {item.VlMinped.ToStringInvariant("0.00")}
                                                        , {item.VlDescpl.ToStringInvariant("0.00")}
                                                        ,'{item.TxPerfinProd.ToStringInvariant("0.00")}');";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public Plano GetByCode(string planoPagamento)
        {
            var command = $@"SELECT * FROM Plano WHERE CdPlano = '{planoPagamento}';";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Plano();

            return new Plano
            {
                Id = Convert.ToInt32(retorno.Id),
                CdPlano = retorno.CdPlano.ToString(),
                DsPlano = retorno.DsPlano.ToString(),
                QtDiaPrz = retorno.QtDiaPrz.ToString(),
                NrColPrec = retorno.NrColPrec.ToString(),
                NvPlano = retorno.NvPlano.ToString(),
                QtMaxParc = retorno.QtMaxParc.ToString(),
                TpPrazo = retorno.TpPrazo.ToString(),
                InEspec = retorno.InEspec.ToString(),
                TxObs = retorno.TxObs.ToString(),
                NrItmin = retorno.NrItmin.ToString(),
                TpVenda = retorno.TpVenda.ToString(),
                CdRcaxxx = retorno.CdRcaxxx != null ? Convert.ToInt32(retorno.CdRcaxxx) : 0,
                TxPerFin = retorno.TxPerFin != null ? (decimal)retorno.TxPerFin : 0M,
                VlMinped = retorno.VlMinped != null ? Convert.ToInt32(retorno.VlMinped) : 0M,
                VlDescpl = retorno.VlDescpl != null ? Convert.ToInt32(retorno.VlDescpl) : 0M,
                TxPerfinProd = retorno.TxPerfinProd != null ? Convert.ToInt32(retorno.TxPerfinProd) : 0M,
            };
        }

        public int GetTotal()
        {
            var command = $@"SELECT COUNT(*) FROM Plano;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _ = int.TryParse(_total.ToString(), out int total);
            return total;
        }

        public void CriarTabela()
        {
            Init();
        }
    }
}