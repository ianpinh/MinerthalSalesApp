using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using Newtonsoft.Json;
using System.Text;


namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly IAppthalContext _context;
        public ProdutosRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS [Produto](
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,CdProduto VARCHAR(20)
                                                 ,CdPraux VARCHAR(20)
                                                 ,DsEmabala VARCHAR(20)
                                                 ,DsProduto VARCHAR(80)
                                                 ,NmUnidade VARCHAR(20)
                                                 ,NmEmbalag VARCHAR(20)
                                                 ,QtUnidemb VARCHAR(20)
                                                 ,InBloqven VARCHAR(20)
                                                 ,QtEmbmast VARCHAR(20)
                                                 ,Txobs VARCHAR(300)
                                                 ,QtEstoque VARCHAR(20)
                                                 ,CdDepto VARCHAR(20)
                                                 ,CdSecao VARCHAR(20)
                                                 ,CdFornec VARCHAR(20)
                                                 ,CdBarra VARCHAR(20)
                                                 ,NrCor VARCHAR(20)
                                                 ,InFraci VARCHAR(20)
                                                 ,QtMultip VARCHAR(20)
                                                 ,InMix VARCHAR(20)
                                                 ,CdFilial VARCHAR(20)
                                                 ,CdPrinc VARCHAR(20)
                                                 ,DtUltalt VARCHAR(12)
                                                 ,DtULTentr VARCHAR(12)
                                                 ,CdFabric VARCHAR(20)
                                                 ,CdCategoria VARCHAR(20)
                                                 ,CdSubcategoria VARCHAR(20)
                                                 ,CdRcaxxX INT
                                                 ,VlPeso DECIMAL(7,2)
                                                 ,VlPrectab DECIMAL(7,2)
                                                 ,VlPercom DECIMAL(7,2)
                                                 ,TxPercom DECIMAL(7, 2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Produto GetById(int id)
        {
            var command = $@"SELECT * FROM Produto WHERE Id = {id}";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Produto();

            return new Produto
            {
                Id=retorno.Id!=null ? Convert.ToInt32(retorno.Id) : 0,
                CdProduto=retorno.CdProduto.ToString(),
                CdPraux=retorno.CdPraux.ToString(),
                DsEmabala=retorno.DsEmabala.ToString(),
                DsProduto=retorno.DsProduto.ToString(),
                NmUnidade=retorno.NmUnidade.ToString(),
                NmEmbalag=retorno.NmEmbalag.ToString(),
                QtUnidemb=retorno.QtUnidemb.ToString(),
                InBloqven=retorno.InBloqven.ToString(),
                QtEmbmast=retorno.QtEmbmast.ToString(),
                Txobs=retorno.Txobs.ToString(),
                QtEstoque=retorno.QtEstoque.ToString(),
                CdDepto=retorno.CdDepto.ToString(),
                CdSecao=retorno.CdSecao.ToString(),
                CdFornec=retorno.CdFornec.ToString(),
                CdBarra=retorno.CdBarra.ToString(),
                NrCor=retorno.NrCor.ToString(),
                InFraci=retorno.InFraci.ToString(),
                QtMultip=retorno.QtMultip.ToString(),
                InMix=retorno.InMix.ToString(),
                CdFilial=retorno.CdFilial.ToString(),
                CdPrinc=retorno.CdPrinc.ToString(),
                DtUltalt=retorno.DtUltalt.ToString(),
                DtULTentr=retorno.DtULTentr.ToString(),
                CdFabric=retorno.CdFabric.ToString(),
                CdCategoria=retorno.CdCategoria.ToString(),
                CdSubcategoria=retorno.CdSubcategoria.ToString(),
                CdRcaxxX=retorno.CdRcaxxX!=null ? Convert.ToInt32(retorno.CdRcaxxX) : 0,
                VlPeso=retorno.VlPeso!=null ? Convert.ToDecimal(retorno.VlPeso) : 0M,
                VlPrectab=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                VlPercom=retorno.VlPercom!=null ? Convert.ToDecimal(retorno.VlPercom) : 0M,
                TxPercom=retorno.TxPercom!=null ? Convert.ToDecimal(retorno.TxPercom) : 0M,
            };
        }
        public IEnumerable<Produto> GetAll()
        {
            var command = $@"SELECT * FROM Produto";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Produto>();

            var lstuser = new List<Produto>();
            foreach (var item in retorno)
            {
                lstuser.Add(new Produto
                {
                    Id=item.Id!=null ? Convert.ToInt32(item.Id) : 0,
                    CdProduto=item.CdProduto.ToString(),
                    CdPraux=item.CdPraux.ToString(),
                    DsEmabala=item.DsEmabala.ToString(),
                    DsProduto=item.DsProduto.ToString(),
                    NmUnidade=item.NmUnidade.ToString(),
                    NmEmbalag=item.NmEmbalag.ToString(),
                    QtUnidemb=item.QtUnidemb.ToString(),
                    InBloqven=item.InBloqven.ToString(),
                    QtEmbmast=item.QtEmbmast.ToString(),
                    Txobs=item.Txobs.ToString(),
                    QtEstoque=item.QtEstoque.ToString(),
                    CdDepto=item.CdDepto.ToString(),
                    CdSecao=item.CdSecao.ToString(),
                    CdFornec=item.CdFornec.ToString(),
                    CdBarra=item.CdBarra.ToString(),
                    NrCor=item.NrCor.ToString(),
                    InFraci=item.InFraci.ToString(),
                    QtMultip=item.QtMultip.ToString(),
                    InMix=item.InMix.ToString(),
                    CdFilial=item.CdFilial.ToString(),
                    CdPrinc=item.CdPrinc.ToString(),
                    DtUltalt=item.DtUltalt.ToString(),
                    DtULTentr=item.DtULTentr.ToString(),
                    CdFabric=item.CdFabric.ToString(),
                    CdCategoria=item.CdCategoria.ToString(),
                    CdSubcategoria=item.CdSubcategoria.ToString(),
                    CdRcaxxX=item.CdRcaxxX!=null ? Convert.ToInt32(item.CdRcaxxX) : 0,
                    VlPeso=item.VlPeso!=null ? Convert.ToDecimal(item.VlPeso) : 0M,
                    VlPrectab=item.VlPrectab!=null ? Convert.ToDecimal(item.VlPrectab) : 0M,
                    VlPercom=item.VlPercom!=null ? Convert.ToDecimal(item.VlPercom) : 0M,
                    TxPercom=item.TxPercom!=null ? Convert.ToDecimal(item.TxPercom) : 0M,
                });
            }
            return lstuser;

        }
        public IEnumerable<Produto> GetByCodigo(string codCliente)
        {
            var command = $@"SELECT * FROM Produto WHERE A1Cod  = {codCliente}";
            var retorno = _context.ExcecutarSelect(command);

            if (retorno == null)
                return new List<Produto>();

            var lstuser = new List<Produto>();
            foreach (var item in retorno)
            {
                lstuser.Add(new Produto
                {
                    Id=item.Id!=null ? Convert.ToInt32(item.Id) : 0,
                    CdProduto=item.CdProduto.ToString(),
                    CdPraux=item.CdPraux.ToString(),
                    DsEmabala=item.DsEmabala.ToString(),
                    DsProduto=item.DsProduto.ToString(),
                    NmUnidade=item.NmUnidade.ToString(),
                    NmEmbalag=item.NmEmbalag.ToString(),
                    QtUnidemb=item.QtUnidemb.ToString(),
                    InBloqven=item.InBloqven.ToString(),
                    QtEmbmast=item.QtEmbmast.ToString(),
                    Txobs=item.Txobs.ToString(),
                    QtEstoque=item.QtEstoque.ToString(),
                    CdDepto=item.CdDepto.ToString(),
                    CdSecao=item.CdSecao.ToString(),
                    CdFornec=item.CdFornec.ToString(),
                    CdBarra=item.CdBarra.ToString(),
                    NrCor=item.NrCor.ToString(),
                    InFraci=item.InFraci.ToString(),
                    QtMultip=item.QtMultip.ToString(),
                    InMix=item.InMix.ToString(),
                    CdFilial=item.CdFilial.ToString(),
                    CdPrinc=item.CdPrinc.ToString(),
                    DtUltalt=item.DtUltalt.ToString(),
                    DtULTentr=item.DtULTentr.ToString(),
                    CdFabric=item.CdFabric.ToString(),
                    CdCategoria=item.CdCategoria.ToString(),
                    CdSubcategoria=item.CdSubcategoria.ToString(),
                    CdRcaxxX=item.CdRcaxxX!=null ? Convert.ToInt32(item.CdRcaxxX) : 0,
                    VlPeso=item.VlPeso!=null ? Convert.ToDecimal(item.VlPeso) : 0M,
                    VlPrectab=item.VlPrectab!=null ? Convert.ToDecimal(item.VlPrectab) : 0M,
                    VlPercom=item.VlPercom!=null ? Convert.ToDecimal(item.VlPercom) : 0M,
                    TxPercom=item.TxPercom!=null ? Convert.ToDecimal(item.TxPercom) : 0M,
                });
            }
            return lstuser;
        }
        public IEnumerable<Produto> GetProdutoPrecoDefault()
        {
            try
            {
                var produtos = new List<Produto>();
                var lista = GetAll();
                var tabelaPreco = App.TabelaPrecoRepository.GetAll();

                if (lista == null || !lista.Any())
                    lista = GetAll();

                if (tabelaPreco == null || !tabelaPreco.Any())
                    tabelaPreco = App.TabelaPrecoRepository.GetAll();


                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                var userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
                var saler = App.VendedorRepository.GetByCodigo(userDetailStr.Codigo);

                var tipoTabela = saler.TabPreco;
                var filialPadrao = "02"; // GOIÂNIA
                var tipoVendaPadrao = "2";
                foreach (var item in lista)
                {
                    var precos = tabelaPreco.Where(x => x.CdProduto ==item.CdProduto).ToList();
                    var tbPrecos = App.TabelaPrecoRepository.Get(item.CdProduto, filialPadrao, tipoTabela, tipoVendaPadrao);

                    if (precos!=null && precos.Any())
                    {
                        var valorProduto = tbPrecos!=null && tbPrecos.Any() ? tbPrecos.Max(x => x.VlVvenda) : precos.Max(s => s.VlVvenda);
                        item.ValorCombinado= valorProduto;
                        item.VlPrectab= valorProduto;

                        var index = produtos.FindIndex(x => x.Id == item.Id);
                        if (index<0)
                            produtos.Add(item);
                    }
                }
                return produtos.Distinct().OrderBy(x => x.DsProduto).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Produto GetByCodProduto(string codigo)
        {
            var command = $@"SELECT * FROM Produto WHERE CdProduto = {codigo}";
            var retorno = _context.ExcecutarSelectFirstOrDefault(command);

            if (retorno == null)
                return new Produto();

            return new Produto
            {
                Id=retorno.Id!=null ? Convert.ToInt32(retorno.Id) : 0,
                CdProduto=retorno.CdProduto.ToString(),
                CdPraux=retorno.CdPraux.ToString(),
                DsEmabala=retorno.DsEmabala.ToString(),
                DsProduto=retorno.DsProduto.ToString(),
                NmUnidade=retorno.NmUnidade.ToString(),
                NmEmbalag=retorno.NmEmbalag.ToString(),
                QtUnidemb=retorno.QtUnidemb.ToString(),
                InBloqven=retorno.InBloqven.ToString(),
                QtEmbmast=retorno.QtEmbmast.ToString(),
                Txobs=retorno.Txobs.ToString(),
                QtEstoque=retorno.QtEstoque.ToString(),
                CdDepto=retorno.CdDepto.ToString(),
                CdSecao=retorno.CdSecao.ToString(),
                CdFornec=retorno.CdFornec.ToString(),
                CdBarra=retorno.CdBarra.ToString(),
                NrCor=retorno.NrCor.ToString(),
                InFraci=retorno.InFraci.ToString(),
                QtMultip=retorno.QtMultip.ToString(),
                InMix=retorno.InMix.ToString(),
                CdFilial=retorno.CdFilial.ToString(),
                CdPrinc=retorno.CdPrinc.ToString(),
                DtUltalt=retorno.DtUltalt.ToString(),
                DtULTentr=retorno.DtULTentr.ToString(),
                CdFabric=retorno.CdFabric.ToString(),
                CdCategoria=retorno.CdCategoria.ToString(),
                CdSubcategoria=retorno.CdSubcategoria.ToString(),
                CdRcaxxX=retorno.CdRcaxxX!=null ? Convert.ToInt32(retorno.CdRcaxxX) : 0,
                VlPeso=retorno.VlPeso!=null ? Convert.ToDecimal(retorno.VlPeso) : 0M,
                VlPrectab=retorno.VlPrectab!=null ? Convert.ToDecimal(retorno.VlPrectab) : 0M,
                VlPercom=retorno.VlPercom!=null ? Convert.ToDecimal(retorno.VlPercom) : 0M,
                TxPercom=retorno.TxPercom!=null ? Convert.ToDecimal(retorno.TxPercom) : 0M,
            };
        }

        public void SaveProduto(List<Produto> produto)
        {
            if (produto!=null && produto.Any())
            {
                var scriptCommand = new StringBuilder();
                scriptCommand.AppendLine("DELETE FROM Produto;");

                foreach (var item in produto)
                {
                    var commandInsert = $@"INSERT INTO [Produto](
                                                     CdProduto
                                                    ,CdPraux
                                                    ,DsEmabala
                                                    ,DsProduto
                                                    ,NmUnidade
                                                    ,NmEmbalag
                                                    ,QtUnidemb
                                                    ,InBloqven
                                                    ,QtEmbmast
                                                    ,Txobs
                                                    ,QtEstoque
                                                    ,CdDepto
                                                    ,CdSecao
                                                    ,CdFornec
                                                    ,CdBarra
                                                    ,NrCor
                                                    ,InFraci
                                                    ,QtMultip
                                                    ,InMix
                                                    ,CdFilial
                                                    ,CdPrinc
                                                    ,DtUltalt
                                                    ,DtULTentr
                                                    ,CdFabric
                                                    ,CdCategoria
                                                    ,CdSubcategoria
                                                    ,CdRcaxxX
                                                    ,VlPeso
                                                    ,VlPrectab
                                                    ,VlPercom
                                                    ,TxPercom)
                                                            VALUES (
                                                     '{item.CdProduto}'
                                                    ,'{item.CdPraux}'
                                                    ,'{item.DsEmabala}'
                                                    ,'{item.DsProduto}'
                                                    ,'{item.NmUnidade}'
                                                    ,'{item.NmEmbalag}'
                                                    ,'{item.QtUnidemb}'
                                                    ,'{item.InBloqven}'
                                                    ,'{item.QtEmbmast}'
                                                    ,'{item.Txobs}'
                                                    ,'{item.QtEstoque}'
                                                    ,'{item.CdDepto}'
                                                    ,'{item.CdSecao}'
                                                    ,'{item.CdFornec}'
                                                    ,'{item.CdBarra}'
                                                    ,'{item.NrCor}'
                                                    ,'{item.InFraci}'
                                                    ,'{item.QtMultip}'
                                                    ,'{item.InMix}'
                                                    ,'{item.CdFilial}'
                                                    ,'{item.CdPrinc}'
                                                    ,'{item.DtUltalt}'
                                                    ,'{item.DtULTentr}'
                                                    ,'{item.CdFabric}'
                                                    ,'{item.CdCategoria}'
                                                    ,'{item.CdSubcategoria}'
                                                    , {item.CdRcaxxX}
                                                    , {item.VlPeso.ToStringInvariant("0.00")}
                                                    , {item.VlPrectab.ToStringInvariant("0.00")}
                                                    , {item.VlPercom.ToStringInvariant("0.00")}
                                                    , {item.TxPercom.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }

               
                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Add(Produto produto)
        {
            var commandInsert = $@"INSERT INTO [Produto](
                                                     CdProduto
                                                    ,CdPraux
                                                    ,DsEmabala
                                                    ,DsProduto
                                                    ,NmUnidade
                                                    ,NmEmbalag
                                                    ,QtUnidemb
                                                    ,InBloqven
                                                    ,QtEmbmast
                                                    ,Txobs
                                                    ,QtEstoque
                                                    ,CdDepto
                                                    ,CdSecao
                                                    ,CdFornec
                                                    ,CdBarra
                                                    ,NrCor
                                                    ,InFraci
                                                    ,QtMultip
                                                    ,InMix
                                                    ,CdFilial
                                                    ,CdPrinc
                                                    ,DtUltalt
                                                    ,DtULTentr
                                                    ,CdFabric
                                                    ,CdCategoria
                                                    ,CdSubcategoria
                                                    ,CdRcaxxX
                                                    ,VlPeso
                                                    ,VlPrectab
                                                    ,VlPercom
                                                    ,TxPercom)
                                                            VALUES (
                                                     '{produto.CdProduto}'
                                                    ,'{produto.CdPraux}'
                                                    ,'{produto.DsEmabala}'
                                                    ,'{produto.DsProduto}'
                                                    ,'{produto.NmUnidade}'
                                                    ,'{produto.NmEmbalag}'
                                                    ,'{produto.QtUnidemb}'
                                                    ,'{produto.InBloqven}'
                                                    ,'{produto.QtEmbmast}'
                                                    ,'{produto.Txobs}'
                                                    ,'{produto.QtEstoque}'
                                                    ,'{produto.CdDepto}'
                                                    ,'{produto.CdSecao}'
                                                    ,'{produto.CdFornec}'
                                                    ,'{produto.CdBarra}'
                                                    ,'{produto.NrCor}'
                                                    ,'{produto.InFraci}'
                                                    ,'{produto.QtMultip}'
                                                    ,'{produto.InMix}'
                                                    ,'{produto.CdFilial}'
                                                    ,'{produto.CdPrinc}'
                                                    ,'{produto.DtUltalt}'
                                                    ,'{produto.DtULTentr}'
                                                    ,'{produto.CdFabric}'
                                                    ,'{produto.CdCategoria}'
                                                    ,'{produto.CdSubcategoria}'
                                                    , {produto.CdRcaxxX}
                                                    , {produto.VlPeso.ToStringInvariant("0.00")}
                                                    , {produto.VlPrectab.ToStringInvariant("0.00")}
                                                    , {produto.VlPercom.ToStringInvariant("0.00")}
                                                    , {produto.TxPercom.ToStringInvariant("0.00")});";
            _context.ExcecutarComandoCrud(commandInsert);
        }

        public void AddRange(List<Produto> produto)
        {
            if (produto!=null && produto.Any())
            {
                var scriptCommand = new StringBuilder();
                foreach (var item in produto)
                {
                    var commandInsert = $@"INSERT INTO [Produto](
                                                     CdProduto
                                                    ,CdPraux
                                                    ,DsEmabala
                                                    ,DsProduto
                                                    ,NmUnidade
                                                    ,NmEmbalag
                                                    ,QtUnidemb
                                                    ,InBloqven
                                                    ,QtEmbmast
                                                    ,Txobs
                                                    ,QtEstoque
                                                    ,CdDepto
                                                    ,CdSecao
                                                    ,CdFornec
                                                    ,CdBarra
                                                    ,NrCor
                                                    ,InFraci
                                                    ,QtMultip
                                                    ,InMix
                                                    ,CdFilial
                                                    ,CdPrinc
                                                    ,DtUltalt
                                                    ,DtULTentr
                                                    ,CdFabric
                                                    ,CdCategoria
                                                    ,CdSubcategoria
                                                    ,CdRcaxxX
                                                    ,VlPeso
                                                    ,VlPrectab
                                                    ,VlPercom
                                                    ,TxPercom)
                                                            VALUES (
                                                     '{item.CdProduto}'
                                                    ,'{item.CdPraux}'
                                                    ,'{item.DsEmabala}'
                                                    ,'{item.DsProduto}'
                                                    ,'{item.NmUnidade}'
                                                    ,'{item.NmEmbalag}'
                                                    ,'{item.QtUnidemb}'
                                                    ,'{item.InBloqven}'
                                                    ,'{item.QtEmbmast}'
                                                    ,'{item.Txobs}'
                                                    ,'{item.QtEstoque}'
                                                    ,'{item.CdDepto}'
                                                    ,'{item.CdSecao}'
                                                    ,'{item.CdFornec}'
                                                    ,'{item.CdBarra}'
                                                    ,'{item.NrCor}'
                                                    ,'{item.InFraci}'
                                                    ,'{item.QtMultip}'
                                                    ,'{item.InMix}'
                                                    ,'{item.CdFilial}'
                                                    ,'{item.CdPrinc}'
                                                    ,'{item.DtUltalt}'
                                                    ,'{item.DtULTentr}'
                                                    ,'{item.CdFabric}'
                                                    ,'{item.CdCategoria}'
                                                    ,'{item.CdSubcategoria}'
                                                    , {item.CdRcaxxX}
                                                    , {item.VlPeso.ToStringInvariant("0.00")}
                                                    , {item.VlPrectab.ToStringInvariant("0.00")}
                                                    , {item.VlPercom.ToStringInvariant("0.00")}
                                                    , {item.TxPercom.ToStringInvariant("0.00")});";
                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM Produto WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public int GetTotal()
        {

            var command = $@"SELECT COUNT(*) FROM Produto;";
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
    }
}

