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
    public class CustomerProvider : ICustomerProvider
    {
        private readonly InsuranceDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;

        public CustomerProvider(InsuranceDbContext dbContext, ILogger<CustomerProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<(bool IsSuccess, Customer customer, string ErrorMessage)> AddCostumer(Customer customer)
        {
            try
            {
                var result = await dbContext.AddEntity(customer);

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

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingCPF(string CPF)
        {
            try
            {
                var result = await GetCustomerByCPF(CPF);
                if (result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.Customer);
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

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingId(long customerId)
        {
            try
            {
                var result = await GetCustomerById(customerId); 
                if (result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.Customer);
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

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerByCPF(string CPF)
        {
            try
            {
                Customer customer = await dbContext.Customers
                                              .Include(c => c.Vehicles)
                                              .Where(c => c.CPF == CPF)
                                              .FirstOrDefaultAsync();
                if (customer != null)
                {
                    return (true, customer, null);
                }
                else
                {
                    return (false, null, "Customer with this CPF not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerById(long customerId)
        {
            try
            {
                Customer customer = await dbContext.Customers
                                              .Include(c => c.Vehicles)
                                              .Where(c => c.Id == customerId)
                                              .FirstOrDefaultAsync();
                if (customer != null)
                {
                    return (true, customer, null);
                }
                else
                {
                    return (false, null, "Customer with this Id not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<Customer> Customers, string ErrorMessage)> GetCustomersByBranch(int insuranceBranchId)
        {
            try
            {
                List<Customer> customers = await dbContext.Customers
                                              .Include(c => c.Vehicles)
                                              .Include(c => c.LinkedBranch)
                                              .Where(c => c.LinkedBranch.Id == insuranceBranchId)
                                              .ToListAsync();
                if (customers.Any())
                {
                    return (true, customers, null);
                }
                else
                {
                    return (false, null, "No customers found for this Insurance Branch!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Customer customer, string ErrorMessage)> UpdateCostumer(Customer customer)
        {
            try
            {
                var result = await dbContext.UpdateEntity(customer);

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
    }
}
