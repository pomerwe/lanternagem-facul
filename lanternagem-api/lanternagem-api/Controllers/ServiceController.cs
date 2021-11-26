using lanternagem_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
    [ApiController]
    [Route("api/service")]
    public class ServiceController : Controller
    {
        private readonly Interfaces.IServiceProvider serviceProvider;

        public ServiceController(Interfaces.IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        [HttpPost("add-service")]
        [Authorize]
        public async Task<IActionResult> AddService([FromBody] Service service)
        {
            var result = await serviceProvider.AddService(service);

            if (result.IsSuccess)
            {
                return Created("add-service", result.Service);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("get-service-by-id/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> GetServiceById(int serviceId)
        {
            var result = await serviceProvider.GetServiceById(serviceId);

            if (result.IsSuccess)
            {
                return Ok(result.Service);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("get-services")]
        [Authorize]
        public async Task<IActionResult> GetServices()
        {
            var result = await serviceProvider.GetServices();

            if (result.IsSuccess)
            {
                return Ok(result.Services);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("update-service")]
        [Authorize]
        public async Task<IActionResult> UpdateService([FromBody] Service service)
        {
            var result = await serviceProvider.UpdateService(service);

            if (result.IsSuccess)
            {
                return Ok(result.Service);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("delete-service/{serviceId}")]
        [Authorize]
        public async Task<IActionResult> DeleteService(int serviceId)
        {
            var result = await serviceProvider.DeleteService(serviceId);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
