using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface IInsuranceCompanyProvider
  {
    Task<(bool IsSuccess, List<InsuranceBranch> InsuranceBranch, string ErrorMessage)> GetInsuranceBranchesByCompanyId(int insuranceCompanyId);
    Task<(bool IsSuccess, InsuranceBranch InsuranceBranch, string ErrorMessage)> GetInsuranceBranchById(int insuranceBranchId);
    Task<(bool IsSuccess, List<InsuranceCompany> InsuranceCompanies, string ErrorMessage)> GetInsuranceCompanies();
    Task<(bool IsSuccess, List<InsuranceBranch> InsuranceBranches, string ErrorMessage)> GetInsuranceBranches();
    Task<(bool IsSuccess, InsuranceCompany InsuranceCompany, string ErrorMessage)> GetInsuranceCompanyById(long insuranceCompanyId);
    Task<(bool IsSuccess, string ErrorMessage)> AddInsuranceCompany(InsuranceCompany insuranceCompany);
    Task<(bool IsSuccess, string ErrorMessage)> UpdateInsuranceCompany(InsuranceCompany insuranceCompany);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteInsuranceCompany(long insuranceCompanyId);
  }
}
