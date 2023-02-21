using System.ComponentModel.DataAnnotations;

namespace EFCoreTesting.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string IntegrationId { get; set; } = string.Empty;
    }
}
