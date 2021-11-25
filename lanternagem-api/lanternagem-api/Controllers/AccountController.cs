using lanternagem_api.DataTransferObjects;
using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
    [Route("api/account-controller")]
    public class AccountController : Controller
    {
        private readonly ISystemManagementService systemManagementService;

        public AccountController(
            ISystemManagementService systemManagementService)
        {
            this.systemManagementService = systemManagementService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await systemManagementService.Login(loginDto);

            if(result.IsSuccess)
            {
                return Ok(result.AuthenticatedDto);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
