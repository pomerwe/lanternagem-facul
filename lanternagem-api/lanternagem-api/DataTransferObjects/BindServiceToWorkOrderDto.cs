using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class BindServiceToWorkOrderDto
    {
        public int ServiceId { get; set; }
        public long WorkOrderId { get; set; }
    }
}
