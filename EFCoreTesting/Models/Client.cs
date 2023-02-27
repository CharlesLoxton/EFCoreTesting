using IntegrationLibrary.Interfaces;

namespace EFCoreTesting.Models
{
    public class Client : IClient
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool isCompany { get; set; } = false;
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CCEmails { get; set; } = string.Empty;
        public string VATNumber { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public string Guid { get; set; } = string.Empty;
    }
}
