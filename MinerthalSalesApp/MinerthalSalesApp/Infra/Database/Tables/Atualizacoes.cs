namespace MinerthalSalesApp.Infra.Database.Tables
{
    //[Table("Atualizacoes")]
    public class Atualizacoes
    {
        //[PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public string NomeTabela { get; set; } = string.Empty;
    }
}