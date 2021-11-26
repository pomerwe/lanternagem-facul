using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class UpdateWorkOrderDto
    {
        public long WorkOrderId { get; set; }
        public string Description { get; set; }
    }
}
