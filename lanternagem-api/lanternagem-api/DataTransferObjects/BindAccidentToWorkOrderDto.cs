using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class BindAccidentToWorkOrderDto
    {
        public int AccidentId { get; set; }
        public long WorkOrderId { get; set; }
    }
}
