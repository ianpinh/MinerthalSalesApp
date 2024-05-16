using MinerthalSalesApp.Infra.Database.Tables;

namespace MinerthalSalesApp.Infra.Database.Repository.Interface
{
    public interface ITabelaPrecoRepository
    {
        TabelaPreco GetById(int id);

        List<TabelaPreco> GetAll();

        void Add(TabelaPreco tbpreco);

        void AddRange(List<TabelaPreco> tbpreco);

        void DeleteById(int id);

        void DeleteAll();

        /// <summary>
        /// Recuperar validade da tabela de preços
        /// </summary>
        /// <param name="cdProduto">Código do produto</param>
        /// <param name="codigoTabela">CODIGO DA TABELA, está fixo em 2 hoje.</param>
        /// <param name="tipoTabela">A ou C = Tipo de tabela. (A3_MITPTAB = A / C OU X = INVATIVO)</param>
        /// <param name="faixaDesconto">01 até 07 são as faixas de desconto e comissão </param>
        /// <param name="codFilial ">Código da filial</param>
        /// <returns></returns>
        List<TabelaPreco> Get(string cdProduto, string filialMinerthal, string tipoTabela, string codigoTabela = "2");

        int GetTotal();

        void SaveTabelaPreco(List<TabelaPreco> tbpreco);
        void CriarTabela();
    }
}
