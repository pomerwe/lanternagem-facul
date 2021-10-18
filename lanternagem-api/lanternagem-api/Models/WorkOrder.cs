using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace lanternagem_api.Models
{
  public class WorkOrder
  {
    public Customer Customer { get;set; }
    public Vehicle Vehicle { get; set; }
    public List<Blob> AccidentImages { get; set; }
    public Accident Accident { get; set; }
    public Service Service { get; set; }
    public List<WorkOrderStep> Steps { get; set; }
    public string Description { get; set; }

    public List<WorkOrderStatus> Status { get; set; }

    public WorkOrder(Customer customer, Vehicle vehicle, string description)
    {
      Customer = customer;
      Vehicle = vehicle;
      Description = description;
      Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Opened));
    }

    public void BindService(Service service)
    {
      Service = service;
      Service.Steps.ForEach(s => 
      {
        Steps.Add(new WorkOrderStep(s));
      });

      Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.InProgress));
    }

    public void CancelWorkOrder()
    {
      Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Canceled));
    }

    public void FinishWorkOrder()
    {
      Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Finished));
    }

    public void CompleteStep()
    {
      if(Service == null)
      {
        throw new Exception("No service bound to Work Order!");
      }

      var currentStep = GetCurrentStep();

      if(currentStep == null)
      {
        throw new Exception("All steps already finished!")
      }

      currentStep.Finish();
    }

    public void CancelStep()
    {
      if (Service == null)
      {
        throw new Exception("No service bound to Work Order!");
      }

      var lastFinishedStep = Steps.Where(s => s.IsCompleted()).OrderByDescending(s => s.Order).FirstOrDefault();
      lastFinishedStep.Cancel();
    }
    
    public WorkOrderStep GetCurrentStep()
    {
      if (Service == null)
      {
        throw new Exception("No service bound to Work Order!");
      }

      return Steps.Where(s => !s.IsCompleted()).OrderBy(s => s.Order).FirstOrDefault();
    }

    public bool IsClosed()
    {
      return Status.Any(s => s.Status == WorkOrderStatusEnum.Finished || s.Status == WorkOrderStatusEnum.Canceled);
    }
  }
}