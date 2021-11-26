using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Controllers
{
    [ApiController]
    [Route("api/work-order")]
    public class WorkOrderController : Controller
    {
        private readonly IWorkOrderService workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            this.workOrderService = workOrderService;
        }

        [HttpGet("get-work-orders-by-customer-cpf/{CPF}")]
        [Authorize]
        public async Task<IActionResult> GetWorkOrdersByCustomerCPF(string CPF)
        {
            var result = await workOrderService.GetWorkOrdersByCustomerCPF(CPF);

            if(result.IsSuccess)
            {
                return Ok(result.WorkOrders);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet("get-work-orders-by-customerId/{customerId}")]
        [Authorize]
        public async Task<IActionResult> GetWorkOrdersByCustomerId(long customerId)
        {
            var result = await workOrderService.GetWorkOrdersByCustomerId(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrders);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet("get-work-order-by-id/{workOrderId}")]
        [Authorize]
        public async Task<IActionResult> GetWorkOrderById(long workOrderId)
        {
            var result = await workOrderService.GetWorkOrderById(workOrderId);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("register-work-order")]
        [Authorize]
        public async Task<IActionResult> RegisterWorkOrder(RegisterWorkOrderDto registerWorkOrderDto)
        {
            var result = await workOrderService.RegisterWorkOrder(registerWorkOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("bind-service-to-work-order")]
        [Authorize]
        public async Task<IActionResult> BindServiceToWorkOrder(BindServiceToWorkOrderDto bindServiceToWorkOrderDto)
        {
            var result = await workOrderService.BindServiceToWorkOrder(bindServiceToWorkOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("bind-accident-to-work-order")]
        [Authorize]
        public async Task<IActionResult> BindAccidentToWorkOrder(BindAccidentToWorkOrderDto bindAccidentToWorkOrderDto)
        {
            var result = await workOrderService.BindAccidentToWorkOrder(bindAccidentToWorkOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("complete-step")]
        [Authorize]
        public async Task<IActionResult> CompleteStep(WorkOrderDto workOrderDto)
        {
            var result = await workOrderService.CompleteStep(workOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("cancel-step")]
        [Authorize]
        public async Task<IActionResult> CancelStep(WorkOrderDto workOrderDto)
        {
            var result = await workOrderService.CancelStep(workOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost("finish-work-order")]
        [Authorize]
        public async Task<IActionResult> FinishWorkOrder(WorkOrderDto workOrderDto)
        {
            var result = await workOrderService.FinishWorkOrder(workOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }


        [HttpPost("cancel-work-order")]
        [Authorize]
        public async Task<IActionResult> CancelWorkOrder(WorkOrderDto workOrderDto)
        {
            var result = await workOrderService.CancelWorkOrder(workOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPut("update-work-order")]
        [Authorize]
        public async Task<IActionResult> UpdateWorkOrder(UpdateWorkOrderDto updateWorkOrderDto)
        {
            var result = await workOrderService.UpdateWorkOrder(updateWorkOrderDto);

            if (result.IsSuccess)
            {
                return Ok(result.WorkOrder);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
