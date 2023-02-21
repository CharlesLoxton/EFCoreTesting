using EFCoreTesting.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EFCoreTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {

        private readonly KDBContext _context;
        private readonly Integration _integration;
        private int userID = 123;
        public IntegrationController(KDBContext context, Integration integration)
        {
            _context = context;
            _integration = integration;
        }

        [HttpGet("StartConnection/{provider}")]
        public IActionResult StartConnection(string provider)
        {
            return _integration.CreateConnection(provider);
        }

        [HttpPost]
        [Route("SaveCode")]
        public async Task<ActionResult> SaveCode([FromBody] CodeDTO request)
        {
            try
            {
                string res = await _integration.GetAccessCode(request.code, "QuickBooks");
                return Ok(res);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
