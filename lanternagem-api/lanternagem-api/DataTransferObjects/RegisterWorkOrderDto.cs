using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class RegisterWorkOrderDto
    {
        public long CustomerId { get; set; }
        public string LicensePlate { get; set; }
        public string Description { get; set; }
    }
}
