using MinerthalSalesApp.Customs.CustomHelpers;
using MinerthalSalesApp.Infra.Database.Base;
using MinerthalSalesApp.Infra.Database.Repository.Interface;
using MinerthalSalesApp.Infra.Database.Tables;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IAppthalContext _context;
        public ClienteRepository(IAppthalContext context)
        {
            _context = context??throw new ArgumentNullException(nameof(context));
            Init();
        }
        private void Init()
        {
            try
            {
                var command = $@"CREATE TABLE IF NOT EXISTS Cliente(
                                                  Id INTEGER PRIMARY KEY AUTOINCREMENT
                                                 ,A1Cgc VARCHAR(20) NULL
                                                 ,A1Cod VARCHAR(20) NULL
                                                 ,A1Loja VARCHAR(30) NULL
                                                 ,A1Nome VARCHAR(150) NULL
                                                 ,A1Nreduz VARCHAR(150) NULL
                                                 ,A1Nomprp1 VARCHAR(100) NULL
                                                 ,A1Nomprp2 VARCHAR(100) NULL
                                                 ,A1Tipo VARCHAR(100) NULL
                                                 ,A1Pessoa VARCHAR(100) NULL
                                                 ,A1Msblql VARCHAR(100) NULL
                                                 ,A1Condpag VARCHAR(100) NULL
                                                 ,A1Inscr VARCHAR(30) NULL
                                                 ,A1Observ VARCHAR(500) NULL
                                                 ,A1Ddd VARCHAR(20) NULL
                                                 ,A1Telex VARCHAR(20) NULL
                                                 ,A1Email VARCHAR(100) NULL
                                                 ,A1Este VARCHAR(20) NULL
                                                 ,A1Mune VARCHAR(100) NULL
                                                 ,A1Bairroe VARCHAR(100) NULL
                                                 ,A1Endent VARCHAR(300) NULL
                                                 ,A1Ultcom VARCHAR(200) NULL
                                                 ,A1Lc DECIMAL(7,2)
                                                 ,LcDisponivel DECIMAL(7,2)
                                                 ,AVencer DECIMAL(7,2)
                                                 ,A1Atr DECIMAL(7,2));";
                _context.ExcecutarComandoCrud(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Cliente> GetAll()
        {
            try
            {
                var command = $@"SELECT * FROM Cliente ORDER BY A1Nome";
                var retorno = _context.ExcecutarSelect(command);

                if (retorno == null)
                    return new List<Cliente>();

                var lstuser = new List<Cliente>();
                foreach (var item in retorno)
                {
                    lstuser.Add(new Cliente
                    {
                        A1Cgc=item.A1Cgc.ToString(),
                        A1Cod=item.A1Cod.ToString(),
                        A1Loja=item.A1Loja.ToString(),
                        A1Nome=item.A1Nome.ToString(),
                        A1Nreduz=item.A1Nreduz.ToString(),
                        A1Nomprp1=item.A1Nomprp1.ToString(),
                        A1Nomprp2=item.A1Nomprp2.ToString(),
                        A1Tipo=item.A1Tipo.ToString(),
                        A1Pessoa=item.A1Pessoa.ToString(),
                        A1Msblql=item.A1Msblql.ToString(),
                        A1Condpag=item.A1Condpag.ToString(),
                        A1Inscr=item.A1Inscr.ToString(),
                        A1Observ=item.A1Observ.ToString(),
                        A1Ddd=item.A1Ddd.ToString(),
                        A1Telex=item.A1Telex.ToString(),
                        A1Email=item.A1Email.ToString(),
                        A1Este=item.A1Este.ToString(),
                        A1Mune=item.A1Mune.ToString(),
                        A1Bairroe=item.A1Bairroe.ToString(),
                        A1Endent=item.A1Endent.ToString(),
                        A1Ultcom=item.A1Ultcom.ToString(),
                        LcDisponivel=item.LcDisponivel!=null ? (decimal)item.LcDisponivel : 0M,
                        AVencer=item.AVencer!=null ? (decimal)item.AVencer : 0M,
                        A1Atr=item.A1Atr!=null ? (decimal)item.A1Atr : 0M
                    });
                }
                return lstuser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Cliente GetByCpf(string cpf)
        {
            try
            {
                var command = $@"SELECT * FROM Cliente WHERE CNPJorCPF = '{cpf}'";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return default;

                return new Cliente
                {
                    A1Cgc=retorno.A1Cgc.ToString(),
                    A1Cod=retorno.A1Cod.ToString(),
                    A1Loja=retorno.A1Loja.ToString(),
                    A1Nome=retorno.A1Nome.ToString(),
                    A1Nreduz=retorno.A1Nreduz.ToString(),
                    A1Nomprp1=retorno.A1Nomprp1.ToString(),
                    A1Nomprp2=retorno.A1Nomprp2.ToString(),
                    A1Tipo=retorno.A1Tipo.ToString(),
                    A1Pessoa=retorno.A1Pessoa.ToString(),
                    A1Msblql=retorno.A1Msblql.ToString(),
                    A1Condpag=retorno.A1Condpag.ToString(),
                    A1Inscr=retorno.A1Inscr.ToString(),
                    A1Observ=retorno.A1Observ.ToString(),
                    A1Ddd=retorno.A1Ddd.ToString(),
                    A1Telex=retorno.A1Telex.ToString(),
                    A1Email=retorno.A1Email.ToString(),
                    A1Este=retorno.A1Este.ToString(),
                    A1Mune=retorno.A1Mune.ToString(),
                    A1Bairroe=retorno.A1Bairroe.ToString(),
                    A1Endent=retorno.A1Endent.ToString(),
                    A1Ultcom=retorno.A1Ultcom.ToString(),
                    A1Lc=retorno.A1Lc!=null ? (decimal)retorno.A1Lc : 0M,
                    LcDisponivel=retorno.LcDisponivel!=null ? (decimal)retorno.LcDisponivel : 0M,
                    AVencer=retorno.AVencer!=null ? (decimal)retorno.AVencer : 0M,
                    A1Atr=retorno.A1Atr!=null ? (decimal)retorno.A1Atr : 0M
                };
            }
            catch (Exception)
            {
                throw;
            }
        }



        public Cliente GetByCodigo(string codCliente)
        {
            try
            {
                var command = $@"SELECT * FROM Cliente WHERE A1Cod || A1Loja  = '{codCliente}';";
                var retorno = _context.ExcecutarSelectFirstOrDefault(command);

                if (retorno == null)
                    return default;

                return new Cliente
                {
                    A1Cgc=retorno.A1Cgc.ToString(),
                    A1Cod=retorno.A1Cod.ToString(),
                    A1Loja=retorno.A1Loja.ToString(),
                    A1Nome=retorno.A1Nome.ToString(),
                    A1Nreduz=retorno.A1Nreduz.ToString(),
                    A1Nomprp1=retorno.A1Nomprp1.ToString(),
                    A1Nomprp2=retorno.A1Nomprp2.ToString(),
                    A1Tipo=retorno.A1Tipo.ToString(),
                    A1Pessoa=retorno.A1Pessoa.ToString(),
                    A1Msblql=retorno.A1Msblql.ToString(),
                    A1Condpag=retorno.A1Condpag.ToString(),
                    A1Inscr=retorno.A1Inscr.ToString(),
                    A1Observ=retorno.A1Observ.ToString(),
                    A1Ddd=retorno.A1Ddd.ToString(),
                    A1Telex=retorno.A1Telex.ToString(),
                    A1Email=retorno.A1Email.ToString(),
                    A1Este=retorno.A1Este.ToString(),
                    A1Mune=retorno.A1Mune.ToString(),
                    A1Bairroe=retorno.A1Bairroe.ToString(),
                    A1Endent=retorno.A1Endent.ToString(),
                    A1Ultcom=retorno.A1Ultcom.ToString(),
                    A1Lc=retorno.A1Lc!=null ? (decimal)retorno.A1Lc : 0M,
                    LcDisponivel=retorno.LcDisponivel!=null ? (decimal)retorno.LcDisponivel : 0M,
                    AVencer=retorno.AVencer!=null ? (decimal)retorno.AVencer : 0M,
                    A1Atr=retorno.A1Atr!=null ? (decimal)retorno.A1Atr : 0M
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveClientes(List<Cliente> clientes)
        {
            try
            {
                if (clientes!=null && clientes.Any())
                {
                    var scriptCommand = new StringBuilder();
                    scriptCommand.AppendLine("DELETE FROM Cliente;");

                    foreach (var item in clientes)
                    {
                        var a1Nreduz = item.A1Nreduz.Contains("'") ? item.A1Nreduz.Replace("'", "''") : item.A1Nreduz;
                        var a1Endent = item.A1Endent.Contains("'") ? item.A1Endent.Replace("'", "''") : item.A1Endent;
                        var a1Bairroe = item.A1Bairroe.Contains("'") ? item.A1Bairroe.Replace("'", "''") : item.A1Bairroe;
                        var a1Mune = item.A1Mune.Contains("'") ? item.A1Mune.Replace("'", "''") : item.A1Mune;

                        var commandInsert = $@"INSERT INTO [Cliente](
                                                         A1Cgc 
                                                        ,A1Cod 
                                                        ,A1Loja 
                                                        ,A1Nome 
                                                        ,A1Nreduz 
                                                        ,A1Nomprp1 
                                                        ,A1Nomprp2 
                                                        ,A1Tipo 
                                                        ,A1Pessoa 
                                                        ,A1Msblql 
                                                        ,A1Condpag 
                                                        ,A1Inscr 
                                                        ,A1Observ 
                                                        ,A1Ddd 
                                                        ,A1Telex 
                                                        ,A1Email 
                                                        ,A1Este 
                                                        ,A1Mune 
                                                        ,A1Bairroe 
                                                        ,A1Endent 
                                                        ,A1Ultcom 
                                                        ,LcDisponivel 
                                                        ,A1Lc
                                                        ,AVencer 
                                                        ,A1Atr)
                                                            VALUES (
                                                         '{item.A1Cgc}'
                                                        ,'{item.A1Cod}'
                                                        ,'{item.A1Loja}'
                                                        ,'{item.A1Nome}'
                                                        ,'{a1Nreduz}'
                                                        ,'{item.A1Nomprp1}'
                                                        ,'{item.A1Nomprp2}'
                                                        ,'{item.A1Tipo}'
                                                        ,'{item.A1Pessoa}'
                                                        ,'{item.A1Msblql}'
                                                        ,'{item.A1Condpag}'
                                                        ,'{item.A1Inscr}'
                                                        ,'{item.A1Observ}'
                                                        ,'{item.A1Ddd}'
                                                        ,'{item.A1Telex}'
                                                        ,'{item.A1Email}'
                                                        ,'{item.A1Este}'
                                                        ,'{a1Mune}'
                                                        ,'{a1Bairroe}'
                                                        ,'{a1Endent}'
                                                        ,'{item.A1Ultcom}'
                                                        , {item.A1Lc.ToStringInvariant("0.00")}
                                                        , {item.LcDisponivel.ToStringInvariant("0.00")}
                                                        , {item.AVencer.ToStringInvariant("0.00")}
                                                        , {item.A1Atr.ToStringInvariant("0.00")});";

                        scriptCommand.AppendLine(commandInsert);
                    }


                    var command = scriptCommand.ToString();
                    _context.ExcecutarComandoCrud(command);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Add(Cliente cliente)
        {
            var a1Nreduz = cliente.A1Nreduz.Contains("'") ? cliente.A1Nreduz.Replace("'", "''") : cliente.A1Nreduz;
            var a1Endent = cliente.A1Endent.Contains("'") ? cliente.A1Endent.Replace("'", "''") : cliente.A1Endent;

            var command = $@"INSERT INTO [Cliente](
                                                         A1Cgc 
                                                        ,A1Cod 
                                                        ,A1Loja 
                                                        ,A1Nome 
                                                        ,A1Nreduz 
                                                        ,A1Nomprp1 
                                                        ,A1Nomprp2 
                                                        ,A1Tipo 
                                                        ,A1Pessoa 
                                                        ,A1Msblql 
                                                        ,A1Condpag 
                                                        ,A1Inscr 
                                                        ,A1Observ 
                                                        ,A1Ddd 
                                                        ,A1Telex 
                                                        ,A1Email 
                                                        ,A1Este 
                                                        ,A1Mune 
                                                        ,A1Bairroe 
                                                        ,A1Endent 
                                                        ,A1Ultcom 
                                                        ,LcDisponivel 
                                                        ,A1Lc
                                                        ,AVencer 
                                                        ,A1Atr)
                                                            VALUES (
                                                         '{cliente.A1Cgc}'
                                                        ,'{cliente.A1Cod}'
                                                        ,'{cliente.A1Loja}'
                                                        ,'{cliente.A1Nome}'
                                                        ,'{a1Nreduz}'
                                                        ,'{cliente.A1Nomprp1}'
                                                        ,'{cliente.A1Nomprp2}'
                                                        ,'{cliente.A1Tipo}'
                                                        ,'{cliente.A1Pessoa}'
                                                        ,'{cliente.A1Msblql}'
                                                        ,'{cliente.A1Condpag}'
                                                        ,'{cliente.A1Inscr}'
                                                        ,'{cliente.A1Observ}'
                                                        ,'{cliente.A1Ddd}'
                                                        ,'{cliente.A1Telex}'
                                                        ,'{cliente.A1Email}'
                                                        ,'{cliente.A1Este}'
                                                        ,'{cliente.A1Mune}'
                                                        ,'{cliente.A1Bairroe}'
                                                        ,'{a1Endent}'
                                                        ,'{cliente.A1Ultcom}'
                                                        , {cliente.LcDisponivel}
                                                        , {cliente.A1Lc}
                                                        , {cliente.AVencer}
                                                        , {cliente.A1Atr});";
            _context.ExcecutarComandoCrud(command);
        }

        public void AddRange(List<Cliente> clientes)
        {
            if (clientes!=null && clientes.Any())
            {
                var scriptCommand = new StringBuilder();

                foreach (var item in clientes)
                {
                    var a1Nreduz = item.A1Nreduz.Contains("'") ? item.A1Nreduz.Replace("'", "''") : item.A1Nreduz;
                    var a1Endent = item.A1Endent.Contains("'") ? item.A1Endent.Replace("'", "''") : item.A1Endent;

                    var commandInsert = $@"INSERT INTO [Cliente](
                                                         A1Cgc 
                                                        ,A1Cod 
                                                        ,A1Loja 
                                                        ,A1Nome 
                                                        ,A1Nreduz 
                                                        ,A1Nomprp1 
                                                        ,A1Nomprp2 
                                                        ,A1Tipo 
                                                        ,A1Pessoa 
                                                        ,A1Msblql 
                                                        ,A1Condpag 
                                                        ,A1Inscr 
                                                        ,A1Observ 
                                                        ,A1Ddd 
                                                        ,A1Telex 
                                                        ,A1Email 
                                                        ,A1Este 
                                                        ,A1Mune 
                                                        ,A1Bairroe 
                                                        ,A1Endent 
                                                        ,A1Ultcom 
                                                        ,LcDisponivel 
                                                        ,AVencer 
                                                        ,A1Atr)
                                                            VALUES (
                                                         '{item.A1Cgc}'
                                                        ,'{item.A1Cod}'
                                                        ,'{item.A1Loja}'
                                                        ,'{item.A1Nome}'
                                                        ,'{a1Nreduz}'
                                                        ,'{item.A1Nomprp1}'
                                                        ,'{item.A1Nomprp2}'
                                                        ,'{item.A1Tipo}'
                                                        ,'{item.A1Pessoa}'
                                                        ,'{item.A1Msblql}'
                                                        ,'{item.A1Condpag}'
                                                        ,'{item.A1Inscr}'
                                                        ,'{item.A1Observ}'
                                                        ,'{item.A1Ddd}'
                                                        ,'{item.A1Telex}'
                                                        ,'{item.A1Email}'
                                                        ,'{item.A1Este}'
                                                        ,'{item.A1Mune}'
                                                        ,'{item.A1Bairroe}'
                                                        ,'{a1Endent}'
                                                        ,'{item.A1Ultcom}'
                                                        , {item.LcDisponivel.ToStringInvariant("0.00")}
                                                        , {item.AVencer.ToStringInvariant("0.00")}
                                                        , {item.A1Atr.ToStringInvariant("0.00")});";

                    scriptCommand.AppendLine(commandInsert);
                }


                var command = scriptCommand.ToString();
                _context.ExcecutarComandoCrud(command);
            }
        }

        public void Delete(int id)
        {
            var command = @$"DELETE FROM Cliente WHERE Id = {id};";
            _context.ExcecutarComandoCrud(command);
        }

        public int GetTotal()
        {
            try
            {
                var command = $@"SELECT COUNT(*) FROM Cliente;";
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

        public List<Cliente> RecuperarClientesInadimplentes()
        {
            try
            {
                var command = $@"SELECT * FROM Cliente WHERE A1Atr < 0";
                var dataCliente = _context.ExcecutarSelect(command);

                if (dataCliente == null)
                    return default;

                var lst = new List<Cliente>();
                foreach (var retorno in dataCliente)
                {
                    var _cliente = new Cliente
                    {
                        A1Cgc=retorno.A1Cgc.ToString(),
                        A1Cod=retorno.A1Cod.ToString(),
                        A1Loja=retorno.A1Loja.ToString(),
                        A1Nome=retorno.A1Nome.ToString(),
                        A1Nreduz=retorno.A1Nreduz.ToString(),
                        A1Nomprp1=retorno.A1Nomprp1.ToString(),
                        A1Nomprp2=retorno.A1Nomprp2.ToString(),
                        A1Tipo=retorno.A1Tipo.ToString(),
                        A1Pessoa=retorno.A1Pessoa.ToString(),
                        A1Msblql=retorno.A1Msblql.ToString(),
                        A1Condpag=retorno.A1Condpag.ToString(),
                        A1Inscr=retorno.A1Inscr.ToString(),
                        A1Observ=retorno.A1Observ.ToString(),
                        A1Ddd=retorno.A1Ddd.ToString(),
                        A1Telex=retorno.A1Telex.ToString(),
                        A1Email=retorno.A1Email.ToString(),
                        A1Este=retorno.A1Este.ToString(),
                        A1Mune=retorno.A1Mune.ToString(),
                        A1Bairroe=retorno.A1Bairroe.ToString(),
                        A1Endent=retorno.A1Endent.ToString(),
                        A1Ultcom=retorno.A1Ultcom.ToString(),
                        A1Lc=retorno.A1Lc!=null ? (decimal)retorno.A1Lc : 0M,
                        LcDisponivel=retorno.LcDisponivel!=null ? (decimal)retorno.LcDisponivel : 0M,
                        AVencer=retorno.AVencer!=null ? (decimal)retorno.AVencer : 0M,
                        A1Atr=retorno.A1Atr!=null ? (decimal)retorno.A1Atr : 0M
                    };
                    lst.Add(_cliente);
                }
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
