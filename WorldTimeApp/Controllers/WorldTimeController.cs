using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WorldTimeApp.Module.Model;
using WorldTimeApp.Module.Service;

namespace WorldTimeApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorldTimeController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        public WorldTimeController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
        }

        [HttpGet("GetTime")]
        public async Task<IActionResult> GetTime()
        {

            GetWorldTime time = new GetWorldTime(_clientFactory, _config);

            var response = await time.Get();

            if (response.StatusCode is HttpStatusCode.InternalServerError || response.StatusCode is HttpStatusCode.NotFound)
            {
                return StatusCode(response.StatusCode.GetHashCode(), response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
