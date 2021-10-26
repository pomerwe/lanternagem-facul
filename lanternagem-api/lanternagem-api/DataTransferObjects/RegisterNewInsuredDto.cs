using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
  public class RegisterNewInsuredDto
  {
    public int LinkedBranchId { get; set; }
    public NewCustomerDto NewCustomer { get; set; }
    public Vehicle CustomerVehile { get; set; }
    public DateTime SignatureExpirationDate { get; set; }
  }

  public class NewCustomerDto
  {
    public string Name { get; set; }
    public string CPF { get; set; }
  }
}
