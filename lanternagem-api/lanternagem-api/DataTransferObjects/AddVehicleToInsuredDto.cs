using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class AddVehicleToInsuredDto
    {
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public long CustomerId { get; set; }
    }
}
