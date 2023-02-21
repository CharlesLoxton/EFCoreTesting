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
            throw new NotImplementedException();
        }

        public IEnumerable<IEntity> RetrieveAllEntities(DbContext context, string entityName)
        {
            throw new NotImplementedException();
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
    }
}
