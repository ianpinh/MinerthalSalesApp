namespace MinerthalSalesApp.Infra.Database.Tables
{
    //[Table("Usuario")]
    public class Usuario
    {
        //[Ignore]
        //[PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; } 
        public string SellerCode { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public string CNPJorCPF { get; set; } = string.Empty;
        public string SellerNickName { get; set; } = string.Empty;
        public string SellerPassword { get; set; } = string.Empty;
        public int QtdVendedoresNaEquipe { get; set; } = 0;

	}
}
