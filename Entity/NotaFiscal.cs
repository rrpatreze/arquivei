using Dapper.Contrib.Extensions;

namespace Entity
{
    [Table("nota_fiscal")]
    public class NotaFiscal
    {
        [Key]
        public string access_key { get; set; }
        public double total_nota { get; set; }
    }

}