using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Services
{
  public class InsuranceService : IInsuranceService
  {
    private readonly ILogger<InsuranceService> logger;
    private readonly IInsuranceCompanyProvider insuranceCompanyProvider;
    private readonly ICustomerProvider costumerProvider;
    private readonly ISystemManagementService systemManagementService;

    public InsuranceService(
      ILogger<InsuranceService> logger, 
      IInsuranceCompanyProvider insuranceCompanyProvider, 
      ICustomerProvider costumerProvider,
      ISystemManagementService systemManagementService
      )
    {
      this.logger = logger;
      this.insuranceCompanyProvider = insuranceCompanyProvider;
      this.costumerProvider = costumerProvider;
      this.systemManagementService = systemManagementService;
    }

    public async Task<(bool IsSuccess, InsuranceCompany CreatedInsuranceCompany, string ErrorMessage)> RegisterNewInsuranceCompany(RegisterNewInsuranceCompanyDto registerNewInsuranceCompanyDto)
    {
      try
      {
        InsuranceCompany insuranceCompany = new InsuranceCompany();
        insuranceCompany.CNPJ = registerNewInsuranceCompanyDto.CNPJ;
        insuranceCompany.Name = registerNewInsuranceCompanyDto.Name;
        var result = await insuranceCompanyProvider.AddInsuranceCompany(insuranceCompany);

        if(result.IsSuccess)
        {
          return (true, result.InsuranceCompany, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }        
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }

    public async Task<(bool IsSuccess, InsuranceBranch CreatedInsuranceBranch, string ErrorMessage)> RegisterNewInsuranceBranch(RegisterNewInsuranceBranchDto registerNewInsuranceBranchDto)
    {
      try
      {
        var resultGetMother = await insuranceCompanyProvider.GetInsuranceCompanyById(registerNewInsuranceBranchDto.MotherCompanyId);

        if (!resultGetMother.IsSuccess)
          return (false, null, resultGetMother.ErrorMessage);

        InsuranceCompany mother = resultGetMother.InsuranceCompany;
        InsuranceBranch newBranch = new InsuranceBranch();
        newBranch.CNPJ = registerNewInsuranceBranchDto.CNPJ;
        newBranch.Name = registerNewInsuranceBranchDto.Name;

        mother.AddChildBranch(newBranch);

        var result = await insuranceCompanyProvider.UpdateInsuranceCompany(mother);
        if(result.IsSuccess)
        {
          return (true, result.InsuranceCompany.Children.Find(c => c.CNPJ == newBranch.CNPJ), null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }

    public async Task<(bool IsSuccess, Customer CreatedCustomer, string ErrorMessage)> RegisterNewInsured(RegisterNewInsuredDto registerNewInsuredDto)
    {
      try
      {
        var getBranchResult = await insuranceCompanyProvider.GetInsuranceBranchById(registerNewInsuredDto.LinkedBranchId);

        if (!getBranchResult.IsSuccess)
          return (false, null, getBranchResult.ErrorMessage);

        InsuranceBranch linkedBranch = getBranchResult.InsuranceBranch;

        Customer customer = new Customer();
        customer.CPF = registerNewInsuredDto.NewCustomer.CPF;
        customer.Name = registerNewInsuredDto.NewCustomer.Name;
        customer.Vehicles = new List<Vehicle>();
        customer.AddVehicle(registerNewInsuredDto.CustomerVehile);

        linkedBranch.AddNewCustomer(customer);

        var createdUser = await systemManagementService.InsertNewUserForCostumer(customer);

        if (!createdUser.IsSuccess)
          return (false, null, createdUser.ErrorMessage);

        var result = await insuranceCompanyProvider.UpdateInsuranceBranch(linkedBranch);

        if(result.IsSuccess)
        {
          return (true, result.InsuranceBranch.Customers.Find(c => c.CPF == customer.CPF), null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }
  }
}
