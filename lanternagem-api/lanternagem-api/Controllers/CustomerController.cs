using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet("get-customer-by-id/{customerId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetCustomerById(long customerId)
        {
            var result = await customerProvider.GetCustomerById(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Customer);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpGet("get-customers-by-branch/{insuranceBranchId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetCustomers(int insuranceBranchId)
        {
            var result = await customerProvider.GetCustomersByBranch(insuranceBranchId);

            if (result.IsSuccess)
            {
                return Ok(result.Customers);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPut("update-costumer")]
        [Authorize(Roles = "Admin,Manager,Customer")]
        public async Task<IActionResult> UpdateCostumer([FromBody] Customer customer)
        {
            var result = await customerProvider.UpdateCostumer(customer);

            if (result.IsSuccess)
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("delete-customer/{customerId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> DeleteCustomer(long customerId)
        {
            var result = await customerProvider.DeleteCostumerUsingId(customerId);

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
