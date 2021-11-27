using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
    [ApiController]
    [Route("api/insurance-processes")]
    public class InsuranceProcessesController : Controller
    {
        private readonly IInsuranceService insuranceService;
        private const string NEW_INSURANCE_COMPANY_URI = "new-insurance-company";
        private const string NEW_INSURED_URI = "new-insured";
        private const string ADD_VEHICLE_INSURED_URI = "add-vehicle-insured";
        private const string NEW_INSURANCE_BRANCH_URI = "new-insurance-branch";

        public InsuranceProcessesController(IInsuranceService insuranceService)
        {
            this.insuranceService = insuranceService;
        }

        [HttpPost(NEW_INSURANCE_COMPANY_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> NewInsuranceCompany(RegisterNewInsuranceCompanyDto dto)
        {
            var result = await insuranceService.RegisterNewInsuranceCompany(dto);

            if (result.IsSuccess)
            {
                return Created(NEW_INSURANCE_COMPANY_URI, result.CreatedInsuranceCompany);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost(NEW_INSURANCE_BRANCH_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> NewInsuranceBranch(RegisterNewInsuranceBranchDto dto)
        {
            var result = await insuranceService.RegisterNewInsuranceBranch(dto);

            if (result.IsSuccess)
            {
                return Created(NEW_INSURANCE_BRANCH_URI, result.CreatedInsuranceBranch);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost(NEW_INSURED_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> NewInsuranceCompany(RegisterNewInsuredDto dto)
        {
            var result = await insuranceService.RegisterNewInsured(dto);

            if (result.IsSuccess)
            {
                return Created(NEW_INSURED_URI, result.CreatedCustomer);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost(ADD_VEHICLE_INSURED_URI)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AddVehicleToInsured(AddVehicleToInsuredDto dto)
        {
            var result = await insuranceService.AddVehicleToInsured(dto);

            if (result.IsSuccess)
            {
                var payload = new
                {
                    customerId = dto.CustomerId,
                    vehicle = result.AddedVehicle
                };

                return Created(ADD_VEHICLE_INSURED_URI, payload);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}
