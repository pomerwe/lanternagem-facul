using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
  public class RegisterNewInsuranceCompanyDto
  {
    public string Name { get; set; }
    public string CNPJ { get; set; }
  }
}
