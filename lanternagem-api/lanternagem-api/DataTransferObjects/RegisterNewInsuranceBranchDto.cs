using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
  public class RegisterNewInsuranceBranchDto
  {
    public int MotherCompanyId { get; set; }
    public string CNPJ { get; set; }
    public string Name { get; set; }
  }
}
