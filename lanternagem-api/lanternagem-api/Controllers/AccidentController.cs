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
        private const string ADD_ACCIDENT_URI = "add-accident";
        private const string GET_ACCIDENT_BY_ID_URI = "add-accident";
        private const string GET_ACCIDENTS = "add-accidents";
        private const string UPDATE_ACCIDENT = "update-accident";
        private const string DELETE_ACCIDENT = "delete-accident/{accidentId}";

        public AccidentController(IAccidentProvider accidentProvider)
        {
            this.accidentProvider = accidentProvider;
        }

        [HttpPost(ADD_ACCIDENT_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddAccident([FromBody] Accident accident)
        {
            var result = await accidentProvider.AddAccident(accident);

            if (result.IsSuccess)
            {
                return Created(ADD_ACCIDENT_URI, result.Accident);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet(GET_ACCIDENT_BY_ID_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetAccidentById(int accidentId)
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

        [HttpGet(GET_ACCIDENTS)]
        [Authorize(Roles = "Admin,Manager")]
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

        [HttpPut(UPDATE_ACCIDENT)]
        [Authorize(Roles = "Admin,Manager")]
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

        [HttpDelete(DELETE_ACCIDENT)]
        [Authorize(Roles = "Admin,Manager")]
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
