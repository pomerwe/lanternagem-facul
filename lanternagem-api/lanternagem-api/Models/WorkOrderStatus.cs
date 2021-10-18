using System;

namespace lanternagem_api.Models
{
  public class WorkOrderStatus
  {
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