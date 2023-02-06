using Azure.Core;
using EFCoreTesting.DTO;
using EFCoreTesting.Models;
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
        private int userID = 123;
        public ClientController(KDBContext context)
        {
            _context = context;
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
            var client = new Client()
            {
                Name = request.Name,
                UserId = userID,
                GUID = request.GUID,
            };
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(await _context.Clients.ToListAsync());
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

            dbClient.Name= request.Name;
            dbClient.GUID= request.GUID;

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
