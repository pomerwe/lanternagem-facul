using lanternagem_api.Database;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Providers
{
    public class InsuranceCompanyProvider : IInsuranceCompanyProvider
    {
        private readonly InsuranceDbContext dbContext;
        private readonly ILogger<InsuranceCompanyProvider> logger;

        public InsuranceCompanyProvider(InsuranceDbContext dbContext, ILogger<InsuranceCompanyProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, InsuranceCompany InsuranceCompany, string ErrorMessage)> AddInsuranceCompany(InsuranceCompany insuranceCompany)
        {
            try
            {
                var result = await dbContext.AddEntity(insuranceCompany);

                if (result.IsSuccess)
                {
                    return (true, result.Entity, null);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteInsuranceBranch(int insuranceBranchId)
        {
            try
            {
                var result = await GetInsuranceBranchById(insuranceBranchId);
                if (result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.InsuranceBranch);
                }
                else
                {
                    return (false, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteInsuranceCompany(int insuranceCompanyId)
        {
            try
            {
                var result = await GetInsuranceCompanyById(insuranceCompanyId);
                if (result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.InsuranceCompany);
                }
                else
                {
                    return (false, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, InsuranceBranch InsuranceBranch, string ErrorMessage)> GetInsuranceBranchById(int insuranceBranchId)
        {
            try
            {
                InsuranceBranch insuranceBranch = await dbContext.InsuranceBranches
                                              .Include(i => i.Mother)
                                              .Include(i => i.Customers)
                                              .Where(i => i.Id == insuranceBranchId)
                                              .FirstOrDefaultAsync();
                if (insuranceBranch != null)
                {
                    return (true, insuranceBranch, null);
                }
                else
                {
                    return (false, null, "Insurance Branch not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<InsuranceBranch> InsuranceBranches, string ErrorMessage)> GetInsuranceBranches()
        {
            try
            {
                List<InsuranceBranch> insuranceBranches = await dbContext.InsuranceBranches
                                                .Include(i => i.Mother)
                                                .ToListAsync();
                if (insuranceBranches.Any())
                {
                    return (true, insuranceBranches, null);
                }
                else
                {
                    return (false, null, "No Insurance Branches recorded!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<InsuranceBranch> InsuranceBranch, string ErrorMessage)> GetInsuranceBranchesByCompanyId(int insuranceCompanyId)
        {
            try
            {
                List<InsuranceBranch> insuranceBranches = await dbContext.InsuranceBranches
                                              .Include(i => i.Mother)
                                              .Where(i => i.Mother.Id == insuranceCompanyId)
                                              .ToListAsync();
                if (insuranceBranches.Any())
                {
                    return (true, insuranceBranches, null);
                }
                else
                {
                    return (false, null, "Insurance Branches not found in this company!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<InsuranceCompany> InsuranceCompanies, string ErrorMessage)> GetInsuranceCompanies()
        {
            try
            {
                List<InsuranceCompany> insuranceCompanies = await dbContext.InsuranceCompanies
                                                                           .Include(i => i.Children)
                                                                           .ToListAsync();
                if (insuranceCompanies.Any())
                {
                    return (true, insuranceCompanies, null);
                }
                else
                {
                    return (false, null, "No services recorded!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, InsuranceCompany InsuranceCompany, string ErrorMessage)> GetInsuranceCompanyById(int insuranceCompanyId)
        {
            try
            {
                InsuranceCompany insuranceCompany = await dbContext.InsuranceCompanies
                                              .Include(i => i.Children)
                                              .Where(i => i.Id == insuranceCompanyId)
                                              .FirstOrDefaultAsync();
                if (insuranceCompany != null)
                {
                    return (true, insuranceCompany, null);
                }
                else
                {
                    return (false, null, "Insurance Branch not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, InsuranceBranch InsuranceBranch, string ErrorMessage)> UpdateInsuranceBranch(InsuranceBranch insuranceBranch)
        {
            try
            {
                var dbCompany = await GetCompanyByBranch(insuranceBranch);
                var dbInsuranceBranch = dbCompany.Children.Where(c => c.Id == insuranceBranch.Id).FirstOrDefault();
                dbInsuranceBranch = insuranceBranch;

                var result = await dbContext.UpdateEntity(dbInsuranceBranch);

                if (result.IsSuccess)
                {
                    return (true, result.Entity, null);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, InsuranceCompany InsuranceCompany, string ErrorMessage)> UpdateInsuranceCompany(InsuranceCompany insuranceCompany)
        {
            try
            {
                var result = await dbContext.UpdateEntity(insuranceCompany);

                if (result.IsSuccess)
                {
                    return (true, result.Entity, null);
                }
                else
                {
                    return (false, null, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        private async Task<InsuranceCompany> GetCompanyByBranch(InsuranceBranch insuranceBranch)
        {
            var branchResult = await GetInsuranceBranchById(insuranceBranch.Id);
            var companyResult = await GetInsuranceCompanyById(branchResult.InsuranceBranch.Mother.Id);
            return companyResult.InsuranceCompany;
        }
    }
}
