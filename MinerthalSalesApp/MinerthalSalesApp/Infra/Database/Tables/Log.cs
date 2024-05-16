using System.Collections.Specialized;

namespace MinerthalSalesApp.Infra.Database.Tables
{
    //[Table("Log")]
    public class Log
    {
        //[PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Pagina { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string ErrorDetail { get; set; }

        public string Command { get; set; }


    }
}
