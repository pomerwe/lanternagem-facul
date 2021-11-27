using lanternagem_api.DataTransferObjects;
using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
    public interface IInsuranceService
    {
        Task<(bool IsSuccess, Vehicle AddedVehicle, string ErrorMessage)> AddVehicleToInsured(AddVehicleToInsuredDto addVehicleToInsuredDto);
        Task<(bool IsSuccess, Customer CreatedCustomer, string ErrorMessage)> RegisterNewInsured(RegisterNewInsuredDto registerNewInsuredDto);
        Task<(bool IsSuccess, InsuranceCompany CreatedInsuranceCompany, string ErrorMessage)> RegisterNewInsuranceCompany(RegisterNewInsuranceCompanyDto registerNewInsuranceCompanyDto);
        Task<(bool IsSuccess, InsuranceBranch CreatedInsuranceBranch, string ErrorMessage)> RegisterNewInsuranceBranch(RegisterNewInsuranceBranchDto registerNewInsuranceBranchDto);
    }
}
