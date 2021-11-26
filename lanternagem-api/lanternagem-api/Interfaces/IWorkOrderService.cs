using lanternagem_api.DataTransferObjects;
using lanternagem_api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
    public interface IWorkOrderService 
    {
        Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerCPF(string CNPJ);
        Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerId(long customerId);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> GetWorkOrderById(long workOrderId);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> RegisterWorkOrder(RegisterWorkOrderDto registerWorkOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> BindServiceToWorkOrder(BindServiceToWorkOrderDto bindServiceToWorkOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> BindAccidentToWorkOrder(BindAccidentToWorkOrderDto bindAccidentToWorkOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CompleteStep(WorkOrderDto workOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CancelStep(WorkOrderDto workOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> FinishWorkOrder(WorkOrderDto workOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CancelWorkOrder(WorkOrderDto workOrderDto);
        Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> UpdateWorkOrder(UpdateWorkOrderDto workOrder);
    }
}
