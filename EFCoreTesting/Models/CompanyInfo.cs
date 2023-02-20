
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTesting.Models
{
    [Table("APLink")]
    public class CompanyInfo
    {
        [Key]
        public int ComapnyID { get; set; }
        public string? ComapnyName { get; set; }
    }
}
