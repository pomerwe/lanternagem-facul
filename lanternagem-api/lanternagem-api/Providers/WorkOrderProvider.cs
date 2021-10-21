using lanternagem_api.Database;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Providers
{
  public class WorkOrderProvider : IWorkOrderProvider
  {
    private readonly InsuranceDbContext dbContext;
    private readonly ILogger<WorkOrderProvider> logger;

    public WorkOrderProvider(InsuranceDbContext dbContext, ILogger<WorkOrderProvider> logger)
    {
      this.dbContext = dbContext;
      this.logger = logger;
    }
    public async Task<(bool IsSuccess, string ErrorMessage)> AddWorkOrder(WorkOrder workOrder)
    {
      try
      {
        var result = await dbContext.AddOrUpdate(workOrder);

        if (result.IsSuccess)
        {
          return (true, null);
        }
        else
        {
          return (false, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteWorkOrder(long workOrderId)
    {
      try
      {
        var result = await GetWorkOrderById(workOrderId);
        return await dbContext.Delete(result.WorkOrder);
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, WorkOrder WorkOrder, string ErrorMessage)> GetWorkOrderById(long workOrderId)
    {
      try
      {
        WorkOrder workOrder = await dbContext.WorkOrders
                                      .Include(w => w.Customer)
                                      .Include(w => w.Vehicle)
                                      .Include(w => w.AccidentImages)
                                      .Include(w => w.Service)
                                      .Include(w => w.Steps)
                                      .Include(w => w.Status)
                                      .Where(w => w.Id == workOrderId)
                                      .FirstOrDefaultAsync();
        if (workOrder != null)
        {
          return (true, workOrder, null);
        }
        else
        {
          return (false, null, "Work Order with this id does not exists!");
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerCPF(string CPF)
    {
      try
      {
        List<WorkOrder> workOrders = await dbContext.WorkOrders
                                        .Include(w => w.Customer)
                                        .Include(w => w.Vehicle)
                                        .Include(w => w.AccidentImages)
                                        .Include(w => w.Service)
                                        .Include(w => w.Steps)
                                        .Include(w => w.Status)
                                        .Where(w => w.Customer.CPF == CPF)
                                        .ToListAsync();
        if (workOrders.Any())
        {
          return (true, workOrders, null);
        }
        else
        {
          return (false, null, "Customer does not have any work order!");
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, List<WorkOrder> WorkOrders, string ErrorMessage)> GetWorkOrdersByCustomerId(long customerId)
    {
      try
      {
        List<WorkOrder> workOrders = await dbContext.WorkOrders
                                        .Include(w => w.Customer)
                                        .Include(w => w.Vehicle)
                                        .Include(w => w.AccidentImages)
                                        .Include(w => w.Service)
                                        .Include(w => w.Steps)
                                        .Include(w => w.Status)
                                        .Where(w => w.Customer.Id == customerId)
                                        .ToListAsync();
        if (workOrders.Any())
        {
          return (true, workOrders, null);
        }
        else
        {
          return (false, null, "Customer does not have any work order!");
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> UpdateWorkOrder(WorkOrder workOrder)
    {
      return await AddWorkOrder(workOrder);
    }
  }
}
