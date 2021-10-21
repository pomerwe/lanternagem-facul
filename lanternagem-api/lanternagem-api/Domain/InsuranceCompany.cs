using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class InsuranceCompany
  {
    [Key]
    public int Id { get; set; }
    public List<InsuranceBranch> Children { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
  }
}