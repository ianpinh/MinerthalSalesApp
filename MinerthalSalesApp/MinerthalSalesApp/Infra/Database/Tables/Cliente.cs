using Microsoft.Maui.Graphics.Converters;
using Newtonsoft.Json;
using SQLite;
using System.Globalization;
using System.Text;

namespace MinerthalSalesApp.Infra.Database.Tables
{

    [Table("Cliente")]
    public class Cliente
    {
        [PrimaryKey, AutoIncrement, JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("A1_CGC")]
        public string A1Cgc { get; set; } = string.Empty;

        [JsonProperty("A1_COD")]
        public string A1Cod { get; set; } = string.Empty;

        [JsonProperty("A1_LOJA")]
        public string A1Loja { get; set; } = string.Empty;

        [JsonProperty("A1_NOME")]
        public string A1Nome { get; set; } = string.Empty;

        [JsonProperty("A1_NREDUZ")]
        public string A1Nreduz { get; set; } = string.Empty;

        [JsonProperty("A1_NOMPRP1")]
        public string A1Nomprp1 { get; set; } = string.Empty;

        [JsonProperty("A1_NOMPRP2")]
        public string A1Nomprp2 { get; set; } = string.Empty;

        [JsonProperty("A1_TIPO")]
        public string A1Tipo { get; set; } = string.Empty;

        [JsonProperty("A1_PESSOA")]
        public string A1Pessoa { get; set; } = string.Empty;

        [JsonProperty("A1_MSBLQL")]
        public string A1Msblql { get; set; } = string.Empty;

        [JsonProperty("A1_CONDPAG")]
        public string A1Condpag { get; set; } = string.Empty;

        [JsonProperty("A1_INSCR")]
        public string? A1Inscr { get; set; } = string.Empty;

        [JsonProperty("A1_OBSERV")]
        public string? A1Observ { get; set; } = string.Empty;

        [JsonProperty("A1_DDD")]
        public string? A1Ddd { get; set; } = string.Empty;

        [JsonProperty("A1_TELEX")]
        public string? A1Telex { get; set; } = string.Empty;

        [JsonProperty("A1_EMAIL")]
        public string? A1Email { get; set; } = string.Empty;

        [JsonProperty("A1_ESTE")]
        public string? A1Este { get; set; } = string.Empty;

        [JsonProperty("A1_MUNE")]
        public string? A1Mune { get; set; } = string.Empty;

        [JsonProperty("A1_BAIRROE")]
        public string? A1Bairroe { get; set; } = string.Empty;

        [JsonProperty("A1_ENDENT")]
        public string? A1Endent { get; set; } = string.Empty;

        [JsonProperty("A1_ULTCOM")]
        public string? A1Ultcom { get; set; } = string.Empty;

        [JsonProperty("A1_LC")]
        public decimal A1Lc { get; set; }

        [JsonProperty("LC_DISPONIVEL")]
        public decimal LcDisponivel { get; set; }

        [JsonProperty("A_VENCER")]
        public decimal AVencer { get; set; }

        [JsonProperty("A1_ATR")]
        public decimal? A1Atr { get; set; }

        [Ignore]
        public string Tipo => DescricaoTipoPessoa();

        [Ignore]
        public string TipoPessoa => TipoDocumentoPessoa();

        [Ignore]
        public Color CorCliente => RecuperarCorCliente();

        [Ignore]
        public string CodName => $"{A1Cod} - {A1Nome}";

        [Ignore]
        public string NumCodLoja => $"{A1Cod}{A1Loja}";

        [Ignore]
        public string CodLoja => $"{A1Cod} - {A1Loja}";

        [Ignore]
        public string Codigo => $"CÓDIGO : {A1Cod}";

        [Ignore]
        public string Loja => $"LOJA : {A1Loja}-{A1Nreduz}";

        [Ignore]
        public string CNPJ => $"CNPJ : {FormatCNPJ()}";

        [Ignore]
        public string LojaCnpj => $"{TipoPessoa} : {A1Cgc}";

        [Ignore]
        public string? CidadeEstado => $"{A1Mune} - {A1Este}";

        [Ignore]
        public string Telefone => FormatarTelefone();

        [Ignore]
        public string CNPJFormatado => FormatCNPJ();

        [Ignore]
        public string UltimaAtualizacao => FormatarDataVencimento();

        [Ignore]
        public string LimiteCredito => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:c}", A1Lc);

        [Ignore]
        public string LimiteDisponivel => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:c}", LcDisponivel);

        [Ignore]
        public string ValorAVencer => string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:c}", AVencer);

        [Ignore]
        public string ValorVencido => A1Atr != null ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:c}", A1Atr) : string.Format("{0:c}", 0);

        [Ignore]
        public IEnumerable<Titulo> Titulos { get; set; } = new List<Titulo>();


        private Color RecuperarCorCliente()
        {
            var theme = Application.Current.RequestedTheme;
            var converterColor = new ColorTypeConverter();
            var cor = A1Atr < 0 ?
                (Color)converterColor.ConvertFromInvariantString("red") :
                    theme == AppTheme.Light ? (Color)converterColor.ConvertFromInvariantString("black") :
                    (Color)converterColor.ConvertFromInvariantString("white");
            return cor;
        }

        private string FormatarTelefone()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(A1Ddd))
            {
                sb.Append($"({A1Ddd}) ");
            }

            if (!string.IsNullOrWhiteSpace(A1Telex))
            {
                var _phone = "";
                string _tel = LimpaCepCpfCnpj(A1Telex);
                if (_tel.Length == 9)
                    _phone = Convert.ToUInt64(_tel).ToString(@"00000\-0000");
                else
                    _phone = Convert.ToUInt64(_tel).ToString(@"0000\-0000");

                sb.Append(_phone);
            }
            string? telefone = sb.ToString();
            return telefone;

        }

        private string LimpaCepCpfCnpj(string doc)
        {
            if (!string.IsNullOrEmpty(doc))
                return doc.Where(char.IsNumber).Aggregate(string.Empty, (current, item) => current + item);

            return string.Empty;
        }


        public string FormatCNPJ()
        {
            if (string.IsNullOrEmpty(A1Cgc))
                return string.Empty;


            string _cnpj = LimpaCepCpfCnpj(A1Cgc);

            if (_cnpj.Length == 11)
                return Convert.ToUInt64(_cnpj).ToString(@"000\.000\.000\-00");
            else
                return Convert.ToUInt64(_cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public string FormatCPF()
        {
            if (string.IsNullOrEmpty(A1Cgc))
                return string.Empty;


            string _cnpj = LimpaCepCpfCnpj(A1Cgc);

            return Convert.ToUInt64(_cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        private string FormatarDataVencimento()
        {
            var vencimento = "";
            if (A1Ultcom != null)
            {
                try
                {
                    var ano = Convert.ToInt32(A1Ultcom.Substring(0, 4));
                    var mes = Convert.ToInt32(A1Ultcom.Substring(4, 2));
                    var dia = Convert.ToInt32(A1Ultcom.Substring(6, 2));
                    var data = new DateTime(ano, mes, dia);
                    vencimento = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:d}", data);
                }
                catch (Exception)
                {
                    vencimento = A1Ultcom;
                }

            }
            return vencimento;
        }

        private string CnpjcomPontos()
        {
            if (string.IsNullOrEmpty(A1Cgc))
                return string.Empty;


            A1Cgc = LimpaCepCpfCnpj(A1Cgc);

            string novodoc = string.Empty;
            int cont = 1;
            foreach (var item in A1Cgc)
            {
                novodoc += (item);
                if (cont == 2) { novodoc += "."; }
                if (cont == 6) { novodoc += "."; }
                if (cont == 10) { novodoc += "/"; }
                if (cont == 15) { novodoc += "-"; }
                cont++;
            }
            return novodoc;
        }

        private string TipoDocumentoPessoa()
        {
            return A1Pessoa == "F" ? "CPF" : "CNPJ";
        }

        private string DescricaoTipoPessoa()
        {
            return A1Pessoa == "F" ? "Física" : "Jurídica";
        }

    }
}



