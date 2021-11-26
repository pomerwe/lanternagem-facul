using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IWorkOrderProvider workOrderProvider;
        private readonly Interfaces.IServiceProvider serviceProvider;
        private readonly IAccidentProvider accidentProvider;
        private readonly ICustomerProvider customerProvider;

        public WorkOrderService(
            IWorkOrderProvider workOrderProvider,
            Interfaces.IServiceProvider serviceProvider,
            IAccidentProvider accidentProvider,
            ICustomerProvider customerProvider)
        {
            this.workOrderProvider = workOrderProvider;
            this.serviceProvider = serviceProvider;
            this.accidentProvider = accidentProvider;
            this.customerProvider = customerProvider;
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> BindAccidentToWorkOrder(BindAccidentToWorkOrderDto bindAccidentToWorkOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(bindAccidentToWorkOrderDto.WorkOrderId);
                var accidentResult = await accidentProvider.GetAccidentById(bindAccidentToWorkOrderDto.AccidentId);

                if (workOrderResult.IsSuccess && accidentResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    var accident = accidentResult.Accident;

                    workOrder.BindAccident(accident);

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    if (!workOrderResult.IsSuccess)
                    {
                        return (false, null, workOrderResult.ErrorMessage);
                    }
                    else
                    {
                        return (false, null, accidentResult.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> BindServiceToWorkOrder(BindServiceToWorkOrderDto bindServiceToWorkOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(bindServiceToWorkOrderDto.WorkOrderId);
                var serviceResult = await serviceProvider.GetServiceById(bindServiceToWorkOrderDto.ServiceId);

                if (workOrderResult.IsSuccess && serviceResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    var service = serviceResult.Service;

                    workOrder.BindService(service);

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    if (!workOrderResult.IsSuccess)
                    {
                        return (false, null, workOrderResult.ErrorMessage);
                    }
                    else
                    {
                        return (false, null, serviceResult.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CancelStep(WorkOrderDto workOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderDto.WorkOrderId);

                if (workOrderResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    workOrder.CancelStep();

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    return (false, null, workOrderResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CancelWorkOrder(WorkOrderDto workOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderDto.WorkOrderId);

                if (workOrderResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    workOrder.CancelWorkOrder();

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    return (false, null, workOrderResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> CompleteStep(WorkOrderDto workOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderDto.WorkOrderId);

                if (workOrderResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    workOrder.CompleteStep();

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    return (false, null, workOrderResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> FinishWorkOrder(WorkOrderDto workOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderDto.WorkOrderId);

                if (workOrderResult.IsSuccess)
                {
                    var workOrder = workOrderResult.WorkOrder;
                    workOrder.FinishWorkOrder();

                    var result = await workOrderProvider.UpdateWorkOrder(workOrder);

                    if (result.IsSuccess)
                    {
                        return (true, workOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    return (false, null, workOrderResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> GetWorkOrderById(long workOrderId)
        {
            var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderId);

            if (workOrderResult.IsSuccess)
            {
                var workOrder = workOrderResult.WorkOrder;
                return (true, workOrder, null);
            }
            else
            {
                return (false, null, workOrderResult.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerCPF(string CNPJ)
        {
            var workOrderResult = await workOrderProvider.GetWorkOrdersByCustomerCPF(CNPJ);

            if (workOrderResult.IsSuccess)
            {
                var workOrders = workOrderResult.WorkOrders;
                return (true, workOrders, null);
            }
            else
            {
                return (false, null, workOrderResult.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerId(long customerId)
        {
            var workOrderResult = await workOrderProvider.GetWorkOrdersByCustomerId(customerId);

            if (workOrderResult.IsSuccess)
            {
                var workOrders = workOrderResult.WorkOrders;
                return (true, workOrders, null);
            }
            else
            {
                return (false, null, workOrderResult.ErrorMessage);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> RegisterWorkOrder(RegisterWorkOrderDto registerWorkOrderDto)
        {
            try
            {
                var customerResult = await customerProvider.GetCustomerById(registerWorkOrderDto.CustomerId);
                
                if(customerResult.IsSuccess)
                {
                    var customer = customerResult.Customer;
                    var vehicle = customer.PickVehicle(registerWorkOrderDto.LicensePlate);

                    var workOrder = new WorkOrder(customer, vehicle, registerWorkOrderDto.Description);

                    var result = await workOrderProvider.AddWorkOrder(workOrder);

                    if(result.IsSuccess)
                    {
                        return (true, result.WorkOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }
                }
                else
                {
                    return (false, null, customerResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> UpdateWorkOrder(UpdateWorkOrderDto workOrderDto)
        {
            try
            {
                var workOrderResult = await workOrderProvider.GetWorkOrderById(workOrderDto.WorkOrderId); if (workOrderResult.IsSuccess)
                {
                    var dbWorkOrder = workOrderResult.WorkOrder;
                    dbWorkOrder.Update(workOrderDto);

                    var result = await workOrderProvider.UpdateWorkOrder(dbWorkOrder);

                    if(result.IsSuccess)
                    {
                        return (true, result.WorkOrder, null);
                    }
                    else
                    {
                        return (false, null, result.ErrorMessage);
                    }                    
                }
                else
                {
                    return (false, null, workOrderResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }
    }
}
