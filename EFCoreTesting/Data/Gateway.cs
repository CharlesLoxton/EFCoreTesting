using IntegrationLibrary.Interfaces;

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
            throw new NotImplementedException();
        }

        public void SaveGUID(DbContext context, IEntity entity, string guid)
        {
            throw new NotImplementedException();
        }

        public void SaveIntegrationID(DbContext kDBcontext, string integrationID)
        {
            Console.WriteLine("Saving Integration ID: " + integrationID);
        }
    }
}
