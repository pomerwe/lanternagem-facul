using System.Collections.Generic;

namespace lanternagem_api.Models
{
  public class InsuranceCompany
  {
    public List<InsuranceBranch> Children { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
  }
}