using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface ICustomerProvider
  {
    Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerByCNPJ(string CNPJ);
    Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerById(long customerId);
    Task<(bool IsSuccess, List<Customer> Customers, string ErrorMessage)> GetCustomersByBranch(InsuranceBranch insuranceBranch);
    Task<(bool IsSuccess, string ErrorMessage)> AddCostumer(Customer customer);
    Task<(bool IsSuccess, string ErrorMessage)> UpdateCostumer(Customer customer);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingId(long customerId);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteCostumerUsingCNPJ(string CNPJ);
  }
}
