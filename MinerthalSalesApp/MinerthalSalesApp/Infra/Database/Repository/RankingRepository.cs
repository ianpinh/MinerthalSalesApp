using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Globalization;
using System.Text;
using MinerthalSalesApp.Customs.CustomHelpers;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class RankingRepository : IRankingRepository
    {
        CultureInfo _culture = new CultureInfo("pt-BR");
        private readonly IAppthalContext _context;
        public RankingRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }

        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Ranking(
                                                 Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,Codigo  VARCHAR(20)
                                                 ,NomeRC  VARCHAR(150)
                                                 ,PesoTon  DECIMAL(7,2)
                                                 ,PontosVolume  DECIMAL(7,2)
                                                 ,PontosProd  DECIMAL(7,2)
                                                 ,PontsCliAtendido  DECIMAL(7,2)
                                                 ,PontosCliNovo  DECIMAL(7,2)
                                                 ,PontosCliRecuperados  DECIMAL(7,2)
                                                 ,PosicaoRanking  DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public List<Ranking> GetAll()
        {
            try
            {
                var command = $@"SELECT * FROM Ranking";
                var retorno = _context.ExcecutarSelect(command);

                if (retorno == null)
                    return new List<Ranking>();

                var lstuser = new List<Ranking>();
                foreach (var item in retorno)
                {
                    lstuser.Add(new Ranking
                    {
                        Codigo =item.Codigo.ToString(),
                        NomeRC =item.NomeRC.ToString(),
                        PontosVolume =item.PontosVolume!=null ? (decimal)item.PontosVolume : 0M,
                        PontosProd =item.PontosProd!=null ? (decimal)item.PontosProd : 0M,
                        PontsCliAtendido =item.PontsCliAtendido!=null ? (decimal)item.PontsCliAtendido : 0M,
                        PontosCliNovo =item.PontosCliNovo!=null ? (decimal)item.PontosCliNovo : 0M,
                        PontosCliRecuperados =item.PontosCliRecuperados!=null ? (decimal)item.PontosCliRecuperados : 0M,
                        PosicaoRanking =item.PosicaoRanking!=null ? (decimal)item.PosicaoRanking : 0M
                    });
                }
                return lstuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ranking GetByCodigo(string codigo)
        {
            try
            {
                var command = $@"SELECT * FROM Ranking WHERE SellerCode = {codigo}";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return default;

                return new Ranking
                {
                    Codigo =retorno.Codigo.ToString(),
                    NomeRC =retorno.NomeRC.ToString(),
                    PontosVolume =retorno.PontosVolume!=null ? (decimal)retorno.PontosVolume : 0M,
                    PontosProd =retorno.PontosProd!=null ? (decimal)retorno.PontosProd : 0M,
                    PontsCliAtendido =retorno.PontsCliAtendido!=null ? (decimal)retorno.PontsCliAtendido : 0M,
                    PontosCliNovo =retorno.PontosCliNovo!=null ? (decimal)retorno.PontosCliNovo : 0M,
                    PontosCliRecuperados =retorno.PontosCliRecuperados!=null ? (decimal)retorno.PontosCliRecuperados : 0M,
                    PosicaoRanking =retorno.PosicaoRanking!=null ? (decimal)retorno.PosicaoRanking : 0M
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveRanking(List<Ranking> ranking)
        {
            try
            {
                if (ranking!=null && ranking.Any())
                {
                    var scriptCommand = new StringBuilder();
                    scriptCommand.AppendLine("DELETE FROM Ranking;");

                    foreach (var item in ranking)
                    {
                        var nomeRC = item.NomeRC.Contains("'") ? item.NomeRC.Replace("'", "''") : item.NomeRC;

                        var commandInsert = @$"INSERT INTO Ranking(
                                           Codigo
                                          ,NomeRC 
                                          ,PesoTon 
                                          ,PontosVolume 
                                          ,PontosProd 
                                          ,PontsCliAtendido 
                                          ,PontosCliNovo 
                                          ,PontosCliRecuperados 
                                          ,PosicaoRanking)
                                           VALUES(
                                           '{item.Codigo}'
                                          ,'{nomeRC}'
                                          , {item.PesoTon.ToStringInvariant("0.00")}
                                          , {item.PontosVolume.ToStringInvariant("0.00")}
                                          , {item.PontosProd.ToStringInvariant("0.00")}
                                          , {item.PontsCliAtendido.ToStringInvariant("0.00")}
                                          , {item.PontosCliNovo.ToStringInvariant("0.00")}
                                          , {item.PontosCliRecuperados.ToStringInvariant("0.00")}
                                          , {item.PosicaoRanking.ToStringInvariant("0.00")});";

                        scriptCommand.AppendLine(commandInsert);
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

        public void Add(Ranking ranking)
        {
            var command = @$"INSERT INTO Ranking(
                                           Codigo
                                          ,NomeRC 
                                          ,PesoTon 
                                          ,PontosVolume 
                                          ,PontosProd 
                                          ,PontsCliAtendido 
                                          ,PontosCliNovo 
                                          ,PontosCliRecuperados 
                                          ,PosicaoRanking)
                                           VALUES(
                                           '{ranking.Codigo}'
                                          ,'{ranking.NomeRC}'
                                          , {ranking.PesoTon}
                                          , {ranking.PontosVolume}
                                          , {ranking.PontosProd}
                                          , {ranking.PontsCliAtendido}
                                          , {ranking.PontosCliNovo}
                                          , {ranking.PontosCliRecuperados}
                                          , {ranking.PosicaoRanking});";

            _context.ExcecutarComandoCrud(command);
        }

        public void AddRange(List<Ranking> ranking)
        {
            if (ranking !=null && ranking.Count>0)
                foreach (var item in ranking)
                    Add(item);
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM Ranking WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public int GetTotal()
        {
            try
            {
                var command = $@"SELECT COUNT(*) FROM Ranking;";
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

        public void CriarTabela()
        {
            Init();
        }
    }
}