using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreTesting.Models
{
    [Table("APLink")]
    public class APLink
    {
        [Key]
        public int Id { get; set; }
        public string GUID { get; set; } = string.Empty;
        public string AccountingProviderEntityId { get; set; } = string.Empty;
        public string CompanyId { get; set; } = string.Empty;

    }
}
