using System.Collections.Generic;

namespace lanternagem_api.Models
{
  public class InsuranceBranch
  {
    public InsuranceCompany Mother { get; set; }
    public List<Customer> Customers { get; set; }
    public string CNPJ { get; set; }
    public string Name { get; set; }
  }
}