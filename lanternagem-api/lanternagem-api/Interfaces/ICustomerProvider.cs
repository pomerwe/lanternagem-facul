using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface ICustomerProvider
  {
    Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerByCPF(string CNPJ);
    Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerById(long customerId);
    Task<(bool IsSuccess, List<Customer> Customers, string ErrorMessage)> GetCustomersByBranch(int insuranceBranchId);
    Task<(bool IsSuccess, Customer customer, string ErrorMessage)> AddCostumer(Customer customer);
    Task<(bool IsSuccess, Customer customer, string ErrorMessage)> UpdateCostumer(Customer customer);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingId(long customerId);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingCPF(string CPF);
  }
}
