using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class MeusPedidosRepository : IMeusPedidosRepository
    {
        private readonly IAppthalContext _context;
        public MeusPedidosRepository(IAppthalContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            var command = $@"CREATE TABLE IF NOT EXISTS MeusPedidos(
                                                   Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                  ,CdProduto VARCHAR(15)
                                                  ,CdPraux VARCHAR(100)
                                                  ,DsEmabala VARCHAR(100)
                                                  ,DsProduto VARCHAR(150)
                                                  ,NmUnidade VARCHAR(15)
                                                  ,NmEmbalag VARCHAR(15)
                                                  ,QtUnidemb VARCHAR(15)
                                                  ,InBloqVen VARCHAR(15)
                                                  ,QtEmbMast VARCHAR(15)
                                                  ,TxObs VARCHAR(300)
                                                  ,QtEstoque VARCHAR(15)
                                                  ,CdDepto VARCHAR(15)
                                                  ,CdSecao VARCHAR(15)
                                                  ,CdBarra VARCHAR(15)
                                                  ,InFraci VARCHAR(15)
                                                  ,QtMultip  VARCHAR(15)
                                                  ,InMix VARCHAR(15)
                                                  ,CdFilial  VARCHAR(15)
                                                  ,CdPrinc VARCHAR(15)
                                                  ,CdFornec  VARCHAR(15)
                                                  ,NrCor VARCHAR(15)
                                                  ,DtUltalt  VARCHAR(14)
                                                  ,DtUltentr VARCHAR(14)
                                                  ,CdFabric  VARCHAR(50)
                                                  ,CdCategoria VARCHAR(80)
                                                  ,CdSubcategoria VARCHAR(80)
                                                  ,VlPeso INT
                                                  ,CdRcaxxx INT
                                                  ,TxPercom DECIMAL(7,2)
                                                  ,VlPrecTab DECIMAL(7,2)
                                                  ,VlPercom DECIMAL(7,2));";
            _context.ExcecutarComandoCrud(command);
        }

        public MeusPedidos Get(int id)
        {
            var command = $@"SELECT * FROM MeusPedidos WHERE Id = {id};";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new MeusPedidos();

            var lst = new MeusPedidos();


            return new MeusPedidos
            {
                Id = Convert.ToInt32(retorno.Id),
                CdProduto = retorno.CdProduto.ToString(),
                CdPraux = retorno.CdPraux.ToString(),
                DsEmabala = retorno.DsEmabala.ToString(),
                DsProduto = retorno.DsProduto.ToString(),
                NmUnidade = retorno.NmUnidade.ToString(),
                NmEmbalag = retorno.NmEmbalag.ToString(),
                QtUnidemb = retorno.QtUnidemb.ToString(),
                InBloqVen = retorno.InBloqVen.ToString(),
                QtEmbMast = retorno.QtEmbMast.ToString(),
                TxObs = retorno.TxObs.ToString(),
                QtEstoque = retorno.QtEstoque.ToString(),
                CdDepto = retorno.CdDepto.ToString(),
                CdSecao = retorno.CdSecao.ToString(),
                CdBarra = retorno.CdBarra.ToString(),
                InFraci = retorno.InFraci.ToString(),
                QtMultip = retorno.QtMultip.ToString(),
                InMix = retorno.InMix.ToString(),
                CdFilial = retorno.CdFilial.ToString(),
                CdPrinc = retorno.CdPrinc.ToString(),
                CdFornec = retorno.CdFornec.ToString(),
                NrCor = retorno.NrCor.ToString(),
                DtUltalt = retorno.DtUltalt.ToString(),
                DtUltentr = retorno.DtUltentr.ToString(),
                CdFabric = retorno.CdFabric.ToString(),
                CdCategoria = retorno.CdCategoria.ToString(),
                CdSubcategoria = retorno.CdSubcategoria.ToString(),
                VlPeso = retorno.VlPeso != null ? Convert.ToDecimal(retorno.VlPeso) : 0M,
                CdRcaxxx = retorno.CdRcaxxx != null ? Convert.ToDecimal(retorno.CdRcaxxx) : 0M,
                TxPercom = retorno.TxPercom != null ? Convert.ToDecimal(retorno.TxPercom) : 0M,
                VlPrecTab = retorno.VlPrecTab != null ? Convert.ToDecimal(retorno.VlPrecTab) : 0M,
                VlPercom = retorno.VlPercom != null ? Convert.ToDecimal(retorno.VlPercom) : 0M
            };
        }

        public List<MeusPedidos> GetAll()
        {
            var command = $@"SELECT * FROM MeusPedidos;";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<MeusPedidos>();

            var lst = new List<MeusPedidos>();

            foreach (var item in retorno)
            {
                try
                {
                    var _pedido = new MeusPedidos();

                    _pedido.Id = Convert.ToInt32(item.Id);
                    _pedido.CdProduto = item.CdProduto.ToString();
                    _pedido.CdPraux = item.CdPraux.ToString();
                    _pedido.DsEmabala = item.DsEmabala.ToString();
                    _pedido.DsProduto = item.DsProduto.ToString();
                    _pedido.NmUnidade = item.NmUnidade.ToString();
                    _pedido.NmEmbalag = item.NmEmbalag.ToString();
                    _pedido.QtUnidemb = item.QtUnidemb.ToString();
                    _pedido.InBloqVen = item.InBloqVen.ToString();
                    _pedido.QtEmbMast = item.QtEmbMast.ToString();
                    _pedido.TxObs = item.TxObs.ToString();
                    _pedido.QtEstoque = item.QtEstoque.ToString();
                    _pedido.CdDepto = item.CdDepto.ToString();
                    _pedido.CdSecao = item.CdSecao.ToString();
                    _pedido.CdBarra = item.CdBarra.ToString();
                    _pedido.InFraci = item.InFraci.ToString();
                    _pedido.QtMultip = item.QtMultip.ToString();
                    _pedido.InMix = item.InMix.ToString();
                    _pedido.CdFilial = item.CdFilial.ToString();
                    _pedido.CdPrinc = item.CdPrinc.ToString();
                    _pedido.CdFornec = item.CdFornec.ToString();
                    _pedido.NrCor = item.NrCor.ToString();
                    _pedido.DtUltalt = item.DtUltalt.ToString();
                    _pedido.DtUltentr = item.DtUltentr.ToString();
                    _pedido.CdFabric = item.CdFabric.ToString();
                    _pedido.CdCategoria = item.CdCategoria.ToString();
                    _pedido.CdSubcategoria = item.CdSubcategoria.ToString();
                    _pedido.CdRcaxxx = item.CdRcaxxx != null ? Convert.ToInt32(item.CdRcaxxx) : 0;
                    _pedido.VlPeso = item.VlPeso != null ? Convert.ToInt32(item.VlPeso) : 0M;
                    _pedido.TxPercom = item.TxPercom != null ? Convert.ToDecimal(item.TxPercom) : 0M;
                    _pedido.VlPrecTab = item.VlPrecTab != null ? Convert.ToDecimal(item.VlPrecTab) : 0M;
                    _pedido.VlPercom = item.VlPercom != null ? Convert.ToDecimal(item.VlPercom) : 0;

                    lst.Add(_pedido);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return lst;
        }

        public void DeleteAll()
        {
            var command = $"Delete from MeusPedidos;";
            _context.ExcecutarComandoCrud(command);
        }

        public void DeleteById(int id)
        {
            var command = $"Delete from MeusPedidos WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public void Add(MeusPedidos pedido)
        {
            if (pedido != null)
            {
                var scriptCommand = new StringBuilder();

                var commandInsert = $@"INSERT INTO [MeusPedidos](
                                                   CdProduto
                                                  ,CdPraux
                                                  ,DsEmabala
                                                  ,DsProduto
                                                  ,NmUnidade
                                                  ,NmEmbalag
                                                  ,QtUnidemb
                                                  ,InBloqVen
                                                  ,QtEmbMast
                                                  ,TxObs
                                                  ,QtEstoque
                                                  ,CdDepto
                                                  ,CdSecao
                                                  ,CdBarra
                                                  ,InFraci
                                                  ,QtMultip 
                                                  ,InMix
                                                  ,CdFilial 
                                                  ,CdPrinc
                                                  ,CdFornec 
                                                  ,NrCor
                                                  ,DtUltalt 
                                                  ,DtUltentr
                                                  ,CdFabric 
                                                  ,CdCategoria
                                                  ,CdSubcategoria
                                                  ,VlPeso
                                                  ,CdRcaxxx
                                                  ,TxPercom
                                                  ,VlPrecTab
                                                  ,VlPercom)
                                                            VALUES (
                                                   '{pedido.CdProduto}'
                                                  ,'{pedido.CdPraux}'
                                                  ,'{pedido.DsEmabala}'
                                                  ,'{pedido.DsProduto}'
                                                  ,'{pedido.NmUnidade}'
                                                  ,'{pedido.NmEmbalag}'
                                                  ,'{pedido.QtUnidemb}'
                                                  ,'{pedido.InBloqVen}'
                                                  ,'{pedido.QtEmbMast}'
                                                  ,'{pedido.TxObs}'
                                                  ,'{pedido.QtEstoque}'
                                                  ,'{pedido.CdDepto}'
                                                  ,'{pedido.CdSecao}'
                                                  ,'{pedido.CdBarra}'
                                                  ,'{pedido.InFraci}'
                                                  ,'{pedido.QtMultip}'
                                                  ,'{pedido.InMix}'
                                                  ,'{pedido.CdFilial}'
                                                  ,'{pedido.CdPrinc}'
                                                  ,'{pedido.CdFornec}'
                                                  ,'{pedido.NrCor}'
                                                  ,'{pedido.DtUltalt}'
                                                  ,'{pedido.DtUltentr}'
                                                  ,'{pedido.CdFabric}'
                                                  ,'{pedido.CdCategoria}'
                                                  ,'{pedido.CdSubcategoria}'
                                                  , {pedido.VlPeso}
                                                  , {pedido.CdRcaxxx}
                                                  , {pedido.TxPercom.ToStringInvariant("0.00")}
                                                  , {pedido.VlPrecTab.ToStringInvariant("0.00")}
                                                  , {pedido.VlPercom.ToStringInvariant("0.00")});";
                scriptCommand.AppendLine(commandInsert);


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void AddRange(List<MeusPedidos> meusPedidos)
        {

            if (meusPedidos != null && meusPedidos.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in meusPedidos)
                {
                    var commandInsert = $@"INSERT INTO [MeusPedidos](
                                                   CdProduto
                                                  ,CdPraux
                                                  ,DsEmabala
                                                  ,DsProduto
                                                  ,NmUnidade
                                                  ,NmEmbalag
                                                  ,QtUnidemb
                                                  ,InBloqVen
                                                  ,QtEmbMast
                                                  ,TxObs
                                                  ,QtEstoque
                                                  ,CdDepto
                                                  ,CdSecao
                                                  ,CdBarra
                                                  ,InFraci
                                                  ,QtMultip 
                                                  ,InMix
                                                  ,CdFilial 
                                                  ,CdPrinc
                                                  ,CdFornec 
                                                  ,NrCor
                                                  ,DtUltalt 
                                                  ,DtUltentr
                                                  ,CdFabric 
                                                  ,CdCategoria
                                                  ,CdSubcategoria
                                                  ,VlPeso
                                                  ,CdRcaxxx
                                                  ,TxPercom
                                                  ,VlPrecTab
                                                  ,VlPercom)
                                                            VALUES (
                                                   '{item.CdProduto}'
                                                  ,'{item.CdPraux}'
                                                  ,'{item.DsEmabala}'
                                                  ,'{item.DsProduto}'
                                                  ,'{item.NmUnidade}'
                                                  ,'{item.NmEmbalag}'
                                                  ,'{item.QtUnidemb}'
                                                  ,'{item.InBloqVen}'
                                                  ,'{item.QtEmbMast}'
                                                  ,'{item.TxObs}'
                                                  ,'{item.QtEstoque}'
                                                  ,'{item.CdDepto}'
                                                  ,'{item.CdSecao}'
                                                  ,'{item.CdBarra}'
                                                  ,'{item.InFraci}'
                                                  ,'{item.QtMultip}'
                                                  ,'{item.InMix}'
                                                  ,'{item.CdFilial}'
                                                  ,'{item.CdPrinc}'
                                                  ,'{item.CdFornec}'
                                                  ,'{item.NrCor}'
                                                  ,'{item.DtUltalt}'
                                                  ,'{item.DtUltentr}'
                                                  ,'{item.CdFabric}'
                                                  ,'{item.CdCategoria}'
                                                  ,'{item.CdSubcategoria}'
                                                  , {item.VlPeso}
                                                  , {item.CdRcaxxx}
                                                  , {item.TxPercom.ToStringInvariant("0.00")}
                                                  , {item.VlPrecTab.ToStringInvariant("0.00")}
                                                  , {item.VlPercom.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public int GetTotal()
        {
            var command = $@"SELECT COUNT(*) FROM MeusPedidos;";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);
            if (retorno == null)
                return 0;

            var fields = retorno as IDictionary<string, object>;
            var _total = fields["COUNT(*)"];

            _ = int.TryParse(_total.ToString(), out int total);
            return total;
        }

        public void SaveMeusPedidos(List<MeusPedidos> pedidos)
        {
            if (pedidos != null && pedidos.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM MeusPedidos;");

                foreach (var item in pedidos)
                {
                    var commandInsert = $@"INSERT INTO [MeusPedidos](
                                                             CdProduto
                                                            ,CdPraux
                                                            ,DsEmabala
                                                            ,DsProduto
                                                            ,NmUnidade
                                                            ,NmEmbalag
                                                            ,QtUnidemb
                                                            ,InBloqVen
                                                            ,QtEmbMast
                                                            ,TxObs
                                                            ,QtEstoque
                                                            ,CdDepto
                                                            ,CdSecao
                                                            ,CdBarra
                                                            ,InFraci
                                                            ,QtMultip 
                                                            ,InMix
                                                            ,CdFilial 
                                                            ,CdPrinc
                                                            ,CdFornec 
                                                            ,NrCor
                                                            ,DtUltalt 
                                                            ,DtUltentr
                                                            ,CdFabric 
                                                            ,CdCategoria
                                                            ,CdSubcategoria
                                                            ,VlPeso
                                                            ,CdRcaxxx
                                                            ,TxPercom
                                                            ,VlPrecTab
                                                            ,VlPercom)
                                                   VALUES(
                                                             '{item.CdProduto}'
                                                            ,'{item.CdPraux}'
                                                            ,'{item.DsEmabala}'
                                                            ,'{item.DsProduto}'
                                                            ,'{item.NmUnidade}'
                                                            ,'{item.NmEmbalag}'
                                                            ,'{item.QtUnidemb}'
                                                            ,'{item.InBloqVen}'
                                                            ,'{item.QtEmbMast}'
                                                            ,'{item.TxObs}'
                                                            ,'{item.QtEstoque}'
                                                            ,'{item.CdDepto}'
                                                            ,'{item.CdSecao}'
                                                            ,'{item.CdBarra}'
                                                            ,'{item.InFraci}'
                                                            ,'{item.QtMultip}'
                                                            ,'{item.InMix}'
                                                            ,'{item.CdFilial}'
                                                            ,'{item.CdPrinc}'
                                                            ,'{item.CdFornec}'
                                                            ,'{item.NrCor}'
                                                            ,'{item.DtUltalt}'
                                                            ,'{item.DtUltentr}'
                                                            ,'{item.CdFabric}'
                                                            ,'{item.CdCategoria}'
                                                            ,'{item.CdSubcategoria}'
                                                            ,'{item.VlPeso}'
                                                            , {item.CdRcaxxx}
                                                            , {item.TxPercom.ToStringInvariant("0.00")}
                                                            , {item.VlPrecTab.ToStringInvariant("0.00")}
                                                            , {item.VlPercom.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }

                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void CriarTabela()
        {
            Init();
        }
    }
}



