using lanternagem_api.DataTransferObjects;
using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;

namespace lanternagem_api.Models
{
    public class WorkOrder : IEntity
    {
        [Key]
        public long Id { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }

        [NotMapped]
        public List<Blob> AccidentImages { get; set; }

        public Accident Accident { get; set; }
        public Service Service { get; set; }
        public List<WorkOrderStep> Steps { get; set; }
        [NotMapped]
        public WorkOrderStep CurrentStep
        {
            get
            {
                return Steps.Any() ? GetCurrentStep() : null;
            }
        }
        public string Description { get; set; }
        public List<WorkOrderStatus> Status { get; set; }
       

        public WorkOrder()
        {
            AccidentImages = new List<Blob>();
            Steps = new List<WorkOrderStep>();
            Status = new List<WorkOrderStatus>();
        }
        public WorkOrder(Customer customer, Vehicle vehicle, string description)
        {
            AccidentImages = new List<Blob>();
            Steps = new List<WorkOrderStep>();
            Status = new List<WorkOrderStatus>();

            Customer = customer;
            Vehicle = vehicle;
            Description = description;
            Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Opened));
        }

        public void BindAccident(Accident accident)
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            if (accident == null)
            {
                throw new Exception("accident can not be null!");
            }

            Accident = accident;
        }

        public void BindService(Service service)
        {
            if(IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            if (service == null)
            {
                throw new Exception("Service can not be null!");
            }

            Steps.Clear();
            Service = service;
            Service.Steps.ForEach(s =>
            {
                Steps.Add(new WorkOrderStep(s));
            });

            Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.InProgress));
        }

        public void CancelWorkOrder()
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Canceled));
        }

        public void FinishWorkOrder()
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            if (Service == null)
            {
                throw new Exception("A Service must be bound to WorkOrder!");
            }

            if (Steps.Any(s => !s.IsCompleted()))
            {
                throw new Exception("All Steps must be finished to close WorkOrder!");
            }

            Status.Add(new WorkOrderStatus(WorkOrderStatusEnum.Finished));
        }

        public void CompleteStep()
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            if (Service == null)
            {
                throw new Exception("No service bound to Work Order!");
            }

            var currentStep = GetCurrentStep();

            if (currentStep == null)
            {
                throw new Exception("All steps already finished!");
            }

            currentStep.Finish();
        }

        public void CancelStep()
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            if (Service == null)
            {
                throw new Exception("No service bound to Work Order!");
            }

            if(!Steps.Any(s => s.IsCompleted()))
            {
                throw new Exception("There is no steps to cancel!");
            }

            var lastFinishedStep = Steps.Where(s => s.IsCompleted()).OrderByDescending(s => s.Order).FirstOrDefault();
            lastFinishedStep.Reopen();
        }

        public WorkOrderStep GetCurrentStep()
        {
            if (IsClosed())
            {
                return null;
            }

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

        public object GetPrimaryKey()
        {
            return Id;
        }

        public void Update(UpdateWorkOrderDto updateDto)
        {
            if (IsClosed())
            {
                throw new Exception("WorkOrder closed, cannot be modified!");
            }

            Description = updateDto.Description;
        }
    }
}