using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
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
        private const string NEW_INSURANCE_BRANCH_URI = "new-insurance-branch";

        public InsuranceProcessesController(IInsuranceService insuranceService)
        {
            this.insuranceService = insuranceService;
        }

        [HttpPost(NEW_INSURANCE_COMPANY_URI)]
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
    }
}
