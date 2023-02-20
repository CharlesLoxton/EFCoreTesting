using IntegrationLibrary.Interfaces;
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
            IGateway gateway = new Gateway(userID);
            return _integration.CreateConnection(provider, gateway);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //string code = Request.QueryString["code"] ?? "none";
            //string realmId = Request.QueryString["companyID"] ?? "none";
            //await GetAuthTokensAsync(code, realmId);
            return Redirect("");
        }

        private async Task GetAuthTokensAsync(string code, string realmId)
        {

            //var tokenResponse = await auth2Client.GetBearerTokenAsync(code);
            //var accessToken = tokenResponse.AccessToken;
            //var refreshToken = tokenResponse.RefreshToken;
        }
    }
}
