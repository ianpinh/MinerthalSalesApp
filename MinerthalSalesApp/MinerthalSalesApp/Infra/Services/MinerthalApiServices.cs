using MinerthalSalesApp.Customs.Exceptions;
using MinerthalSalesApp.Infra.Database.Tables;
using MinerthalSalesApp.Models;
using MinerthalSalesApp.Models.Dtos;
using MinerthalSalesApp.ViewModels;
using MinerthalSalesApp.ViewModels.Orders;
using Newtonsoft.Json;
using ServiceMinerthal;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MinerthalSalesApp.Infra.Services
{
    public class MinerthalApiServices : BaseViewModel, IMinerthalApiServices
    {
        public const string urlWebService = "http://remoto.minerthal.com.br:3019/WS/WSVeti.u_CoreFunction.apw";

        private const int registrosPorPagina = 5000;

        public MinerthalApiServices()
        {

        }

        public string ApiRequestServiceAsync(string queryId, bool filter)
        {
            try
            {
                ServicoDeRede.IsInternectConnected();
                var userDetailStr = AppUser();
                var paramsToSend = new
                {
                    cEmp = "01",
                    cFil = "01",
                    lChkPrepEnv = true,
                    ErrorType = "ErrorFullMessage",
                    ClassName = "uVSNT001",
                    FunctionName = "uGetInfoErp",
                    FunctionParameters = new
                    {
                        idQuery = queryId,
                        pagina = 1,
                        RegistrosPag = registrosPorPagina,
                        Filter = filter,
                        MV_PAR01 = UserDetailStr.Codigo
                    }
                };
                var _requestJson = JsonConvert.SerializeObject(paramsToSend);
                int attempts = 1;
tryAgain:

                try
                {

                    if (urlWebService.Contains("https://"))
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(urlWebService),
                        Content = new StringContent(
                                        _requestJson,
                                        Encoding.UTF8,
                                        MediaTypeNames.Application.Json),


                    };
                    using var client = new HttpClient();

                    var task = Task.Run(() => client.SendAsync(request));
                    task.Wait();
                    var response = task.Result;


                    //var response = await client.SendAsync(request).ConfigureAwait(false);
                    //response.EnsureSuccessStatusCode();
                    //var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();


                    var responseBody = Task.Run(() => response.Content.ReadAsStringAsync());
                    responseBody.Wait();

                    return responseBody.Result;
                }
                catch (Exception ex)
                {
                    if (attempts <= 12)
                    {
                        attempts++;
                        //Thread.Sleep(2000);
                        goto tryAgain;
                    }
                    else
                    {
                        throw;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RecuperarUsuarioAsync()
        {
            try
            {
                ServicoDeRede.IsInternectConnected();
                var paramsToSend = new
                {
                    cEmp = "01",
                    cFil = "01",
                    lChkPrepEnv = true,
                    ErrorType = "ErrorFullMessage",
                    ClassName = "MinerthalAPP",
                    FunctionName = "GetSellers",
                    FunctionParameters = new
                    {
                        cOperator = ""
                    }
                };
                var _requestJson = JsonConvert.SerializeObject(paramsToSend);
                int attempts = 1;
tryAgain:
                try
                {
                    if (urlWebService.Contains("https://"))
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    using var client = new HttpClient();
                    var content = new StringContent(_requestJson, Encoding.UTF8, "application/json");
                    //HttpResponseMessage response = await client.PostAsync(urlWebService, content);
                    //CustomExceptions.LancarExcecaoQuando(!response.IsSuccessStatusCode, response.StatusCode.ToString());
                    //return await response.Content.ReadAsStringAsync();


                    var task = Task.Run(() => client.PostAsync(urlWebService, content));
                    task.Wait();
                    var response = task.Result;


                    //var response = await client.SendAsync(request).ConfigureAwait(false);
                    //response.EnsureSuccessStatusCode();
                    //var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    response.EnsureSuccessStatusCode();


                    var responseBody = Task.Run(() => response.Content.ReadAsStringAsync());
                    responseBody.Wait();

                    return responseBody.Result;
                }
                catch (Exception)
                {
                    if (attempts <= 15)
                    {
                        attempts++;
                        //Thread.Sleep(2000);
                        goto tryAgain;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string RecuperarRankingAsync()
        {
            try
            {
                ServicoDeRede.IsInternectConnected();
                var initalDate = $"{DateTime.Today.Year}0101";
                var finalDate = $"{DateTime.Today.Year}1231";


                var paramsToSend = new
                {
                    cEmp = "01",
                    cFil = "01",
                    lChkPrepEnv = true,
                    ErrorType = "ErrorFullMessage",
                    ClassName = "MinerthalAPP",
                    FunctionName = "GetSalesRanking",
                    FunctionParameters = new
                    {
                        StartDate = initalDate,
                        FinishDate = finalDate
                    }
                };
                var _requestJson = JsonConvert.SerializeObject(paramsToSend);
                int attempts = 1;
tryAgain:
                try
                {
                    if (urlWebService.Contains("https://"))
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    //using var client = new HttpClient();
                    //var content = new StringContent(_requestJson, Encoding.UTF8, "application/json");
                    //HttpResponseMessage response = await client.PostAsync(urlWebService, content);
                    //CustomExceptions.LancarExcecaoQuando(!response.IsSuccessStatusCode, response.StatusCode.ToString());
                    //return await response.Content.ReadAsStringAsync();


                    using var client = new HttpClient();
                    var content = new StringContent(_requestJson, Encoding.UTF8, "application/json");
                    var task = Task.Run(() => client.PostAsync(urlWebService, content));
                    task.Wait();
                    var response = task.Result;
                    response.EnsureSuccessStatusCode();

                    var responseBody = Task.Run(() => response.Content.ReadAsStringAsync());
                    responseBody.Wait();

                    return responseBody.Result;

                }
                catch (Exception)
                {
                    if (attempts <= 12)
                    {
                        attempts++;
                        //Thread.Sleep(2000);
                        goto tryAgain;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //TRANSMITIR PEDIDO
        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos()
        {
            return await CallWebService();
        }
        public async Task<(string sucesso, string Mensagem)> TransmitirPedidos(Guid pedidoId)
        {
            return await CallWebService(pedidoId);
        }

        private async Task<(string sucesso, string Mensagem)> CallWebService()
        {
            try
            {
                var totalPedidos = new List<string>();
                var totalEnviados = 0;
                var retorno = ("", "");

                var listaPedidos = App.PedidoRepository.GetAll();
                if (listaPedidos!=null && listaPedidos.Any())
                {
                    foreach (var item in listaPedidos)
                    {
                        var carrinho = App.CartRepository.GetByOrderId(item.Id);
                        var cliente = App.ClienteRepository.GetByCodigo(item.CodigoCliente);
                        var totalFrete = carrinho.Sum(x => x.Frete * x.Quantidade);

                        var pesoTotal = 0M;
                        foreach (var cart in carrinho)
                        {
                            var produto = App.ProdutosRepository.GetById(cart.ProdutoId);
                            pesoTotal +=produto.VlPeso * cart.Quantidade;
                        }
                        var precFreteLiquido = totalFrete/pesoTotal;


                        var _parcelas = !string.IsNullOrWhiteSpace(item.Parcelas)
                            ? JsonConvert.DeserializeObject<List<DictionaryDto>>(item.Parcelas)
                            : new List<DictionaryDto>();

                        var valorPedido = carrinho.Sum(x =>
                            (x.Quantidade * x.ValorCombinado) +
                            ((x.Quantidade * x.ValorCombinado) * (x.TaxaEncargos == 0 ? 0 : x.TaxaEncargos / 100)) +
                            totalFrete);

                        var sbParcelas = new StringBuilder();
                        var SbValores = new StringBuilder();
                        if (_parcelas.Any())
                        {
                            foreach (var p in _parcelas)
                            {
                                sbParcelas.Append($"{p.Value};");
                            }

                            var valorParcelas = valorPedido/_parcelas.Count;

                            for (var i = 0; i< _parcelas.Count; i++)
                            {
                                SbValores.Append($"{valorParcelas};");// 80.01; 80.01;
                            }
                        }
                        else
                        {
                            SbValores.Append($"{valorPedido};");
                        }

                        TADDPEDIDODET[] itensPedido = default;
                        if (carrinho.Any())
                        {
                            itensPedido = carrinho.Select(x => new TADDPEDIDODET
                            {
                                PERCCOMISSAO = x.Comissao.ToString(),
                                PRECOUNITARIO = x.ValorCombinado.ToString().Replace(',', '.'),
                                QUANTIDADEPRODUTO=x.Quantidade.ToString(),
                                PRODUTOPEDIDO =x.CodProduto
                            }).ToArray();

                        }
                        var _totalFrete = (float)precFreteLiquido;
                        var _order = new TADDPEDIDOCAB
                        {
                            BANCOPEDIDO=item.TipoCobranca,
                            CONDICAOPAGAMENTO=item.PlanoPagamento,
                            PRECOFRETELIQ=_totalFrete,
                            OBSPEDIDO =!string.IsNullOrWhiteSpace(item.Observacao) ? item.Observacao : " ",
                            TIPOFRETE="C",
                            TRANSPORTADORACLIENTE=string.Empty,
                            DATASPARCELA=sbParcelas.ToString(),
                            VLRPARCELA = SbValores.ToString(),
                            TZITENSDOPEDIDO = itensPedido.ToArray()

                        };

                        using WSPEDIDOSERVICEPECASOAPClient client = new WSPEDIDOSERVICEPECASOAPClient();
                        var codCliente = $"{cliente.A1Cod}{cliente.A1Loja}";
                        var xml = SerializeToXml(_order);
                        try
                        {
                            totalEnviados+=1;
                            var _retorno = await client.ADDPEDIDOPECAAsync(codCliente, item.FilialMinerthal, Guid.NewGuid().ToString(), _order);
                            totalPedidos.Add(_retorno.ADDPEDIDOPECARESULT);

                            if (_retorno.ADDPEDIDOPECARESULT=="Sucesso")
                            {
                                App.CartRepository.DeleteByPedido(item.Id);
                                App.PedidoRepository.DeleteById(item.Id);
                            }
                        }
                        catch (Exception ex)
                        {
                            if (ex is System.ServiceModel.CommunicationException)
                            {
                                if (totalEnviados< listaPedidos.Count)
                                    continue;
                                else
                                    return ("Sucesso", $"Pedidos enviados com sucesso.");
                            }
                            else
                            {
                                throw;
                            }
                        }
                    }

                    var sb = new StringBuilder($"foram enviados {totalEnviados} com seucesso de {listaPedidos.Count}");
                    //sb.AppendLine($"Códigos retornos :");
                    //foreach (var item in totalPedidos)
                    //    sb.AppendLine($" {item}");


                    retorno=("Sucesso", sb.ToString());
                }
                else
                {
                    retorno=("Sucesso", "Não há pedidos para serem enviados");
                }
                return retorno;
            }
            catch (Exception ex)
            {
                return ("Falha", ex.Message);
            }
        }
        private async Task<(string sucesso, string Mensagem)> CallWebService(Guid pedidoId)
        {
            try
            {
                var retorno = ("", "");

                var pedido = App.PedidoRepository.GetById(pedidoId);
                if (pedido!=null)
                {

                    var carrinho = App.CartRepository.GetByOrderId(pedido.Id);
                    var cliente = App.ClienteRepository.GetByCodigo(pedido.CodigoCliente);
                    var totalFrete = carrinho.Sum(x => x.Frete * x.Quantidade);

                    var pesoTotal = 0M;
                    foreach (var cart in carrinho)
                    {
                        var produto = App.ProdutosRepository.GetById(cart.ProdutoId);
                        pesoTotal +=produto.VlPeso * cart.Quantidade;
                    }
                    var precFreteLiquido = totalFrete/pesoTotal;


                    var _parcelas = !string.IsNullOrWhiteSpace(pedido.Parcelas)
                        ? JsonConvert.DeserializeObject<List<DictionaryDto>>(pedido.Parcelas)
                        : new List<DictionaryDto>();

                    var valorPedido = carrinho.Sum(x => (x.Quantidade * x.ValorCombinado) + totalFrete);
                    var sbParcelas = new StringBuilder();
                    var SbValores = new StringBuilder();
                    if (_parcelas.Any())
                    {
                        foreach (var p in _parcelas)
                        {
                            sbParcelas.Append($"{p.Value};");
                        }

                        var valorParcelas = valorPedido/_parcelas.Count;

                        for (var i = 0; i< _parcelas.Count; i++)
                        {
                            SbValores.Append($"{valorParcelas};");// 80.01; 80.01;
                        }
                    }
                    else
                    {
                        SbValores.Append($"{valorPedido};");
                    }

                    var itensPedido = new List<TADDPEDIDODET>();
                    if (carrinho.Any())
                    {
                        itensPedido = carrinho.Select(x => new TADDPEDIDODET
                        {
                            PERCCOMISSAO = x.Comissao.ToString(),
                            PRECOUNITARIO=x.ValorCombinado.ToString(),
                            QUANTIDADEPRODUTO=x.Quantidade.ToString(),
                            PRODUTOPEDIDO =x.CodProduto
                        }).ToList();

                    }

                    
                    var _totalFrete = (float)precFreteLiquido;
                    var _order = new TADDPEDIDOCAB
                    {
                        
                        BANCOPEDIDO=pedido.TipoCobranca,
                        CONDICAOPAGAMENTO=pedido.PlanoPagamento,
                        PRECOFRETELIQ=_totalFrete,
                        OBSPEDIDO =!string.IsNullOrWhiteSpace(pedido.Observacao) ? pedido.Observacao : " ",
                        TIPOFRETE="C",
                        TRANSPORTADORACLIENTE=string.Empty,
                        DATASPARCELA=sbParcelas.ToString(),
                        VLRPARCELA = SbValores.ToString(),
                        TZITENSDOPEDIDO = itensPedido.ToArray()

                    };

                    using WSPEDIDOSERVICEPECASOAPClient client = new WSPEDIDOSERVICEPECASOAPClient();
                    var codCliente = $"{cliente.A1Cod}{cliente.A1Loja}";
                    var xml = SerializeToXml(_order);

                    var _retorno = await client.ADDPEDIDOPECAAsync(codCliente, pedido.FilialMinerthal, Guid.NewGuid().ToString(), _order).ConfigureAwait(true);

                    retorno=("Sucesso", $"Pedidos enviados com sucesso. código : {_retorno.ADDPEDIDOPECARESULT}");
                }
                else
                {
                    retorno=("Sucesso", "Não há pedidos para serem enviados");
                }

                return retorno;
            }
            catch (Exception ex)
            {
                if (ex is System.ServiceModel.CommunicationException)
                    return ("Sucesso", $"Pedidos enviados com sucesso.");
                else
                    return ("Falha", ex.Message);
            }
        }


        private static XmlDocument CreateSoapEnvelope(Pedido pedido)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            var order = new OrderXmlViewModel();
            var xml = order.MontarXmlPedido(pedido);
            soapEnvelopeDocument.LoadXml(xml);
            return soapEnvelopeDocument;
        }
        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        private string SerializeToXml(TADDPEDIDOCAB pedido)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TADDPEDIDOCAB));

            // Serialize the object to XML
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, pedido);
                string xmlString = writer.ToString();
                return xmlString;
            }
        }
        void ExisteConexao()
        {
            try
            {
                NetworkAccess accessType = Connectivity.Current.NetworkAccess;
                CustomExceptions.LancarExcecaoQuando(accessType != NetworkAccess.Internet, "Sem conexão de internet.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        UserBasicInfo UserDetailStr => AppUser();

        private static UserBasicInfo AppUser()
        {
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            return userDetailStr;
        }

        /*

        public async Task<UsuarioDto> RecuperarDadosUsuariosAsync()
        {
            ExisteConexao();
            var users = new UsuarioDto();
            int attempts = 1;
tryAgain:
            try
            {
                using (var client = new HttpClient(new HttpClientHandler()))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, urlWebService);

                    var functionParams = new { cOperator = "" };
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "MinerthalAPP",
                        FunctionName = "GetSellers",
                        FunctionParameters = functionParams
                    };

                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);


                    var content = new StringContent(_requestJson, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var errorMsg = new List<string>();
                    var retorno = response.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<UsuarioDto>(retorno);
                }
            }
            catch (Exception ex)
            {
                if (attempts <= 5)
                {
                    attempts++;
                    Thread.Sleep(1000);
                    MessageUsers = $"Sincronizando usuários...{attempts}ª tentativa";
                    goto tryAgain;
                }
                else
                {
                    throw new CustomExceptions($"Erro ao fazer o upload da listagem de usuários. {ex.Message}");
                }
            }
            return users;
        }

        public async Task<RankingSalers> RecuperarRankingUsuariosAsync()
        {
            ExisteConexao();
            var ranking = new RankingSalers();
            int attempts = 1;
tryAgain:
            try
            {
                using (var client = new HttpClient(new HttpClientHandler()))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, urlWebService);

                    var functionParams = new
                    {
                        StartDate = "20230101",
                        FinishDate = "20231231"
                    };
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "MinerthalAPP",
                        FunctionName = "GetSalesRanking",
                        FunctionParameters = functionParams
                    };

                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);


                    var content = new StringContent(_requestJson, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var errorMsg = new List<string>();
                    var retorno = response.Content.ReadAsStringAsync().Result;
                    ranking = JsonConvert.DeserializeObject<RankingSalers>(retorno);
                }
            }
            catch (Exception ex)
            {
                if (attempts <= 5)
                {
                    attempts++;
                    Thread.Sleep(1000);
                    MessageRanking = $"Sincronizando ranking...{attempts}ª tentativa";
                    goto tryAgain;
                }
                else
                {
                    _alertService.ShowAlert("Login", $"Erro ao fazer upolad do ranking. {ex.Message}", "OK");
                }
            }
            return ranking;
        }

        public async Task<FaturamentosModel> RecuperarRecuperarFaturamentoAsync()
        {
            return await RecuperarFaturamentoDaApiAsync();
        }

        public UsuarioDto RecuperarDadosUsuarios()
        {
            ExisteConexao();

            var users = new UsuarioDto();
            int attempts = 1;
tryAgain:
            try
            {
                using (var client = new HttpClient(new HttpClientHandler()))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, urlWebService);

                    var functionParams = new { cOperator = "" };
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "MinerthalAPP",
                        FunctionName = "GetSellers",
                        FunctionParameters = functionParams
                    };

                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);


                    var content = new StringContent(_requestJson, null, "application/json");
                    request.Content = content;
                    var response = client.Send(request);
                    response.EnsureSuccessStatusCode();

                    var errorMsg = new List<string>();
                    var retorno = response.Content.ToString();
                    users = JsonConvert.DeserializeObject<UsuarioDto>(retorno);
                }
            }
            catch (Exception ex)
            {
                if (attempts <= 5)
                {
                    attempts++;
                    Thread.Sleep(1000);
                    MessageUsers = $"Sincronizando usuários...{attempts}ª tentativa";
                    goto tryAgain;
                }
                else
                {
                    _alertService.ShowAlert("Login", $"Erro ao fazer o upload da listagem de usuários. {ex.Message}", "OK");
                }
            }
            return users;

        }

        public RankingSalers RecuperarRankingUsuarios()
        {
            ExisteConexao();

            var ranking = new RankingSalers();
            int attempts = 1;
tryAgain:
            try
            {
                using (var client = new HttpClient(new HttpClientHandler()))
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, urlWebService);

                    var functionParams = new
                    {
                        StartDate = "20230101",
                        FinishDate = "20231231"
                    };
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "MinerthalAPP",
                        FunctionName = "GetSalesRanking",
                        FunctionParameters = functionParams
                    };

                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);


                    var content = new StringContent(_requestJson, null, "application/json");
                    request.Content = content;
                    var response = client.Send(request);
                    response.EnsureSuccessStatusCode();

                    var errorMsg = new List<string>();
                    var retorno = response.Content.ToString();
                    ranking = JsonConvert.DeserializeObject<RankingSalers>(retorno);
                }
            }
            catch (Exception ex)
            {
                if (attempts <= 5)
                {
                    attempts++;
                    Thread.Sleep(1000);
                    MessageRanking = $"Sincronizando ranking...{attempts}ª tentativa";
                    goto tryAgain;
                }
                else
                {
                    throw new CustomExceptions($"Erro ao fazer upolad do ranking. {ex.Message}");
                }
            }
            return ranking;

        }

        public async Task<FaturamentosModel> RecuperarRecuperarFaturamento()
        {
            ExisteConexao();
            var fatura = RecuperarFaturamentoDaApi();
            if (fatura.Details.Any())
                await App.FaturamentoRepository.SaveInvoicingAsync(fatura);

            return fatura;
        }

        public async Task<FiliaisMinerthalModel> RecuperarFiliaisMinherthal()
        {
            ExisteConexao();
            var filiais = RecuperarFiliaisDaApi();
            if (filiais.Details.Any())
                await App.FilialRepository.Save(filiais);

            return filiais;
        }

        public MeusPedidosDto RecuperarMeusPedidos()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var filiais = new MeusPedidosDto();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000006",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    filiais = JsonConvert.DeserializeObject<MeusPedidosDto>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao fazer upolad dos meus pedidos. {ex.Message}", "OK");
                    }
                }
            }
            return filiais;

        }

        public TabelaPrecoDto RecuperarTabelaDePrecosDto()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var tb_precos = new TabelaPrecoDto();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000002",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    tb_precos = JsonConvert.DeserializeObject<TabelaPrecoDto>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao fazer upolad da tabela de preços. {ex.Message}", "OK");
                    }
                }
            }
            return tb_precos;
        }

        public VendedoresDto RecuperarVendedores()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var salers = new VendedoresDto();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000008",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    salers = JsonConvert.DeserializeObject<VendedoresDto>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao fazer a atualização dos vendedores. {ex.Message}", "OK");
                    }
                }
            }
            return salers;
        }

        public PlanosDto RecuperarPlanos()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var planos = new PlanosDto();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000003",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    planos = JsonConvert.DeserializeObject<PlanosDto>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao tentar carregar os planos. {ex.Message}", "OK");
                    }
                }
            }
            return planos;

        }

        public async Task<List<Banco>> RecuperarBancos()
        {
            try
            {
                var obj = new List<Banco>();
                string queryId = "000021";
                bool filter = true;
                var model = await ApiRequestService(queryId, filter);

                if (!string.IsNullOrWhiteSpace(model))
                {
                    var _model = JsonConvert.DeserializeObject<ResponseApiBanco>(model);
                    if (_model!=null && _model.Details.Any())
                        obj= _model.Details;
                }
                return obj;
            }
            catch (Exception)
            {

                throw;
            }
        }


        private ProdutoModel RecuperarProdutosDaApi()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var faturamento = new ProdutoModel();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000006",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    faturamento = JsonConvert.DeserializeObject<ProdutoModel>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao fazer upolad dos produtos. {ex.Message}", "OK");
                    }
                }
            }
            return faturamento;
        }

        private async Task<ProdutoModel> RecuperarProdutosDaApiAsync()
        {
            ExisteConexao();
            var registrosPorPagina = 5000;
            var faturamento = new ProdutoModel();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000006",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    faturamento = JsonConvert.DeserializeObject<ProdutoModel>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        await _alertService.ShowAlertAsync("Login", $"Erro ao fazer upolad dos produtos. {ex.Message}", "OK");
                    }
                }
            }
            return faturamento;

        }

        private FiliaisMinerthalModel RecuperarFiliaisDaApi()
        {
            ExisteConexao();

            var registrosPorPagina = 5000;
            var filiais = new FiliaisMinerthalModel();
            var userDetailStr = new UserBasicInfo();
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                string userDetails = Preferences.Get(nameof(App.UserDetails), "");
                userDetailStr = JsonConvert.DeserializeObject<UserBasicInfo>(userDetails);
            }

            if (userDetailStr.Codigo.Length > 0)
            {

                int attempts = 1;
tryAgain:

                try
                {
                    var paramsToSend = new
                    {
                        cEmp = "01",
                        cFil = "01",
                        lChkPrepEnv = true,
                        ErrorType = "ErrorFullMessage",
                        ClassName = "uVSNT001",
                        FunctionName = "uGetInfoErp",
                        FunctionParameters = new
                        {
                            idQuery = "000013",
                            pagina = 1,
                            RegistrosPag = registrosPorPagina,
                            Filter = false,
                            MV_PAR01 = userDetailStr.Codigo
                        }
                    };
                    var _requestJson = JsonConvert.SerializeObject(paramsToSend);

                    using var client = new RestClient(urlWebService);
                    var request = new RestRequest();

                    request.Method = Method.Post;
                    request.AddHeader("Accept", "application/json");
                    request.AddParameter("application/json", _requestJson, ParameterType.RequestBody);

                    var response = client.Execute(request);
                    var content = response.Content;

                    filiais = JsonConvert.DeserializeObject<FiliaisMinerthalModel>(content);


                    client.Dispose();
                }
                catch (Exception ex)
                {
                    if (attempts <= 5)
                    {
                        attempts++;
                        Thread.Sleep(200);
                        goto tryAgain;
                    }
                    else
                    {
                        _alertService.ShowAlert("Login", $"Erro ao fazer upolad das filiais. {ex.Message}", "OK");
                    }
                }
            }
            return filiais;

        }
        */
    }

    public class AppRequest
    {
        public string Data { get; set; }
    }

    public class MyResponse
    {
        // Define properties of the response
    }
}


