﻿using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lanternagem_api.Models
{
  public class Customer : IEntity
  {
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string CPF { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    public List<WorkOrder> WorkOrders { get; set; }

    [JsonIgnore]
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

    public void AddVehicle(Vehicle vehicle)
    {
      if (vehicle == null)
        throw new Exception("Error, vehicle is not set!");

      if (Vehicles.Contains(vehicle))
        throw new Exception("This costumer already has this vehicle registered!");

      Vehicles.Add(vehicle);
    }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}
