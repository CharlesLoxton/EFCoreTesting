using EFCoreTesting.DTO;
using IntegrationLibrary.Interfaces;
using IntegrationLibrary.Responses;
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
        private int userID = 1;
        public IntegrationController(KDBContext context, Integration integration)
        {
            _context = context;
            _integration = integration;
        }

      
      
        [HttpGet("StartConnection/{provider}")]
        public IActionResult StartConnection(string provider) {
            try {
                IGateway gateway = new Gateway(userID);
                return _integration.CreateConnection(provider, gateway);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }

        [HttpGet("GetCompanyList/{code}")]
        public async Task<ActionResult> GetCompanyList(string code)
        {
            try
            {
                return Ok(await _integration.GetSageCompanyList(code));
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            
        }

        [HttpPost]
        [Route("SaveCode")]
        public async Task<ActionResult> SaveCode([FromBody] CodeDTO request)
        {
            try
            {
                IGateway gateway = new Gateway(userID);
                await _integration.Authenticate(request.code, request.ProviderName, gateway, request.CompanyId);

                return Ok("Success");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Disconnect")]
        public async Task<ActionResult> Disconnect()
        {
            try
            {
                IGateway gateway = new Gateway(userID);
                await _integration.Disconnect(gateway);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("RefreshToken")]
        public async Task<ActionResult> RefreshToken()
        {
            try
            {
                IGateway gateway = new Gateway(userID);
                await _integration.RefreshAccessCode(gateway);

                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
