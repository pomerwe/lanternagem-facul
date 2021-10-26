using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class InsuranceBranch : IEntity
  {
    [Key]
    public int Id { get; set; }
    public InsuranceCompany Mother { get; set; }
    public string CNPJ { get; set; }
    public string Name { get; set; }
    public List<Customer> Customers { get; set; }

    public InsuranceBranch()
    {
      Customers = new List<Customer>();
    }

    public void AddNewCustomer(Customer customer)
    {
      if (customer == null)
        throw new Exception("Customer is not set!");

      if (Customers.Contains(customer))
        throw new Exception("Customer is already linked to that branch!");

      Customers.Add(customer);
    }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}