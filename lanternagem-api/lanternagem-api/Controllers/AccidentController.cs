using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
    [ApiController]
    [Route("api/accident")]
    public class AccidentController : Controller
    {
        private readonly IAccidentProvider accidentProvider;

        public AccidentController(IAccidentProvider accidentProvider)
        {
            this.accidentProvider = accidentProvider;
        }

        [HttpPost("add-accident")]
        [Authorize]
        public async Task<IActionResult> AddAccident([FromBody] Accident accident)
        {
            var result = await accidentProvider.AddAccident(accident);

            if (result.IsSuccess)
            {
                return Created("add-accident", result.Accident);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("get-accident-by-id/{accidentId}")]
        [Authorize]
        public async Task<IActionResult> GetSAccidentById(int accidentId)
        {
            var result = await accidentProvider.GetAccidentById(accidentId);

            if (result.IsSuccess)
            {
                return Ok(result.Accident);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("get-accidents")]
        [Authorize]
        public async Task<IActionResult> GetAccidents()
        {
            var result = await accidentProvider.GetAccidents();

            if (result.IsSuccess)
            {
                return Ok(result.Accidents);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("update-accident")]
        [Authorize]
        public async Task<IActionResult> UpdateAccident([FromBody] Accident accident)
        {
            var result = await accidentProvider.UpdateAccident(accident);

            if (result.IsSuccess)
            {
                return Ok(result.Accident);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("delete-accident/{accidentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAccident(int accidentId)
        {
            var result = await accidentProvider.DeleteAccident(accidentId);

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
