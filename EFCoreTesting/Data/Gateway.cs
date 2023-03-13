using Azure.Core;
using EFCoreTesting.Models;
using IntegrationLibrary;
using IntegrationLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTesting.Data
{
    public class Gateway : IGateway
    {
        int userID;
        public Gateway(int _userID) 
        {
            userID = _userID;
        }

        public void AddClient(DbContext context, IClient client)
        {
            Console.WriteLine("Adding clients from Accounting Provider");
            var dbClient = new Client
            {
                CompanyName = client.CompanyName,
                isCompany = true,
                Name = client.Name,
                Number = client.Number,
                Email = client.Email,
                VATNumber = client.VATNumber,
                UserId = userID,
                Guid = client.Guid,
            };

            KDBContext _context = (KDBContext)context;
            _context.Clients.Add(dbClient);
            _context.SaveChanges();
        }

        public void AddClientByIntegrationId(DbContext context, IClient client, string integrationId)
        {
            KDBContext _context = (KDBContext)context;
            var users = _context.Users.Where(x => x.IntegrationId == integrationId).ToList();

            foreach(var user in users)
            {
                var dbClient = new Client
                {
                    CompanyName = client.CompanyName,
                    isCompany = true,
                    Name = client.Name,
                    Number = client.Number,
                    Email = client.Email,
                    VATNumber = client.VATNumber,
                    UserId = user.UserId,
                    Guid = client.Guid,
                };

                _context.Clients.Add(dbClient);
            }
            _context.SaveChanges();
        }

        public IEnumerable<IEntity> RetrieveAllEntities(DbContext context, string entityName)
        {
            Console.WriteLine("Retrieving All Clients to Sync");
            if (entityName == "Client")
            {
                KDBContext _context = (KDBContext)context;
                var clientList = _context.Clients.Where(x => x.UserId == userID).ToList();
                Console.WriteLine("Found this many clients for user: " + clientList.Count);
                foreach (var client in clientList)
                {
                    yield return client;
                }
            }
        }

        public string RetrieveIntegrationID(DbContext context)
        {
            KDBContext _context = (KDBContext)context;
            var dbUser = _context.Users.Find(userID);

            return dbUser.IntegrationId;
        }

        public void SaveGUID(DbContext context, IEntity entity, string guid)
        {
            throw new NotImplementedException();
        }

        public void SaveIntegrationID(DbContext context, string integrationID)
        {
         
            Console.WriteLine("Saving Integration ID: " + integrationID);
            KDBContext _context = (KDBContext)context;
            var dbUser = _context.Users.Find(userID);

            if(dbUser != null)
            {
                dbUser.IntegrationId = integrationID;

                _context.SaveChanges();
            }
        }

        public void UpdateEntityByGuid(DbContext context, string guid, IEntity entity)
        {
            KDBContext _context = (KDBContext)context;

            if (entity is IClient)
            {
                var client = (IClient)entity;

                var dbClients = _context.Clients.Where(x => x.Guid == guid).ToList();

                foreach(var item in dbClients)
                {
                    item.CompanyName = client.CompanyName;
                    item.Name = client.Name;
                    item.Email = client.Email;
                    item.Number= client.Number;
                    item.VATNumber = client.VATNumber;
                    item.Guid = client.Guid;
                }
            }

            context.SaveChanges();
        }
    }
}
