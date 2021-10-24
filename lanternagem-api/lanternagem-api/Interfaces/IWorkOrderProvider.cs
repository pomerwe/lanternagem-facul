using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface IWorkOrderProvider
  {
    Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerCPF(string CNPJ);
    Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerId(long customerId);
    Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> GetWorkOrderById(long workOrderId);
    Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> AddWorkOrder(WorkOrder workOrder);
    Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> UpdateWorkOrder(WorkOrder workOrder);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteWorkOrder(long workOrderId);
  }
}
