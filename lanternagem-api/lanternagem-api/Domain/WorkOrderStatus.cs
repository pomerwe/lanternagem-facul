using System;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class WorkOrderStatus
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
  }
  public enum WorkOrderStatusEnum
  {
    Opened,
    InProgress,
    Finished,
    Canceled
  }
}