using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class InsuranceBranch
  {
    [Key]
    public int Id { get; set; }
    public InsuranceCompany Mother { get; set; }
    public string CNPJ { get; set; }
    public string Name { get; set; }
  }
}