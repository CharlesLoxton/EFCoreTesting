using Azure.Core;
using EFCoreTesting.DTO;
using EFCoreTesting.Models;
using IntegrationLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly KDBContext _context;
        private readonly Integration _integration;
        private int userID = 1;
        public ClientController(KDBContext context, Integration integration)
        {
            _context = context;
            _integration = integration;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<Client>>> Get()
        {
            return Ok(await _context.Clients.Where(x => x.UserId == userID).ToListAsync());
        }

        [HttpGet("Get/{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if(client== null)
            {
                return BadRequest("Client not found");
            };

            return Ok(client);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<List<Client>>> AddClient(ClientDTO request)
        {
            IGateway gateway = new Gateway(userID);

            var client = new Client
            {
                CompanyName = request.CompanyName,
                isCompany = request.isCompany,
                Name = request.Name,
                Number = request.Number,
                Email = request.Email,
                CCEmails = request.CCEmails,
                VATNumber = request.VATNumber,
                UserId = userID,
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();

                    // Call transaction.Commit to persist the changes to the database
                    transaction.Commit();

                    // Query the database to retrieve the newly created client and upsert to Accounting Provider
                    var newClient = await _context.Clients.FindAsync(client.Id);
                    _integration.CreateAccountingProvider(gateway).Upsert(newClient);

                    // Return the newly created client
                    return Ok(newClient);
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ActionResult<List<Client>>> UpdateClient(Client request)
        {
            var dbClient = await _context.Clients.FindAsync(request.Id);

            if (dbClient == null)
            {
                return BadRequest("Client not found");
            };

            dbClient.CompanyName = request.CompanyName;
            dbClient.isCompany = request.isCompany;
            dbClient.Name = request.Name;
            dbClient.Number = request.Number;
            dbClient.Email = request.Email;
            dbClient.CCEmails = request.CCEmails;

            //This fixes the concurrency issues, we will have to tell Neil to do this.
            //It checks if the object is being updated in the database and if so the second concurrent request
            //will cancel and rollback
            _context.Clients.Attach(dbClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict();
            }

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<Client>> Delete(int id)
        {
            var dbClient = await _context.Clients.FindAsync(id);

            if (dbClient == null)
            {
                return BadRequest("Client not found");
            };

            _context.Clients.Remove(dbClient);
            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
        }
    }
}
