using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lanternagem_api.Models
{
    public class Customer : User
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public List<Vehicle> Vehicles { get; set; }

        [JsonIgnore]
        public List<WorkOrder> WorkOrders { get; set; }

        [JsonIgnore]
        public SystemUser User { get; set; }

        public Customer()
        {
            Vehicles = new List<Vehicle>();
            WorkOrders = new List<WorkOrder>();
        }

        [JsonIgnore]
        public InsuranceBranch LinkedBranch { get; set; }
        public DateTime SignatureExpirationDate { get; set; }
        public bool IsSignatureExpired() => DateTime.Now > SignatureExpirationDate;

        public Vehicle PickVehicle(string LicensePlate)
        {
            var requestedVehicle = Vehicles?.FirstOrDefault(v => v.LicensePlate.Equals(LicensePlate));

            if (requestedVehicle == null)
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

        public override string GetName()
        {
            return Name;
        }

        public override string GetCPF()
        {
            return CPF;
        }
    }
}
