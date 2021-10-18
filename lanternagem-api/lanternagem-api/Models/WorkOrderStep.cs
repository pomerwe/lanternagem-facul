namespace lanternagem_api.Models
{
  public class WorkOrderStep : ServiceStep
  {
    private bool completed;

    public WorkOrderStep(ServiceStep serviceStep)
    {
      Name = serviceStep.Name;
      AverageTime = serviceStep.AverageTime;
      Description = serviceStep.Description;
      Order = serviceStep.Order;
      completed = false;
    }

    public void Cancel()
    {
      completed = false;
    }

    public void Finish()
    {
      completed = true;
    }

    public bool IsCompleted()
    {
      return completed;
    }
  }
}