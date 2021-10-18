using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Models
{
  public class Customer
  {
    public string Name { get; set; }
    public string CPF { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    public List<WorkOrder> WorkOrders { get; set; }
    public InsuranceBranch LinkedBranch { get; set; }

    public Vehicle PickVehicle(string LicensePlate)
    {
      var requestedVehicle = Vehicles?.FirstOrDefault(v => v.LicensePlate.Equals(LicensePlate));

      if(requestedVehicle == null)
      {
        throw new Exception("Customer does not have this vehicle registered in the system");
      }

      return requestedVehicle;
    }
  }
}
