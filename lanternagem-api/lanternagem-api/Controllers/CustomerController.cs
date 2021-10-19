using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
  [ApiController]
  [Route("api/customer")]
  public class CustomerController : Controller
  {
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
      return Ok();
    }
  }
}
