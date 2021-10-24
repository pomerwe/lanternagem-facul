using lanternagem_api.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class WorkOrderStatus : IEntity
  {
    [Key]
    public long Id { get; set; }
    public DateTime StatusMoment { get; set; }
    public WorkOrderStatusEnum Status { get; set; }

    public WorkOrderStatus(WorkOrderStatusEnum status)
    {
      Status = status;
      StatusMoment = DateTime.Now;
    }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
  public enum WorkOrderStatusEnum
  {
    Opened,
    InProgress,
    Finished,
    Canceled
  }
}