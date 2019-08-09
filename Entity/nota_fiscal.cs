using Dapper.Contrib.Extensions;
using System;

namespace Entity
{
    [Table("nota_fiscal")]
    public class nota_fiscal
    {
        [Key]
        public string access_key { get; set; }
        public double total_nota { get; set; }
    }

}