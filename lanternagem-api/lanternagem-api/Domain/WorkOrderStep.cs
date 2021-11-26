using System.Text.Json.Serialization;

namespace lanternagem_api.Models
{
    public class WorkOrderStep : ServiceStep
    {
        [JsonIgnore]
        public bool Completed { get; set; }

        public WorkOrderStep()
        {

        }
        public WorkOrderStep(ServiceStep serviceStep)
        {
            Name = serviceStep.Name;
            AverageTime = serviceStep.AverageTime;
            Description = serviceStep.Description;
            Order = serviceStep.Order;
            Completed = false;
        }

        public void Reopen()
        {
            Completed = false;
        }

        public void Finish()
        {
            Completed = true;
        }

        public bool IsCompleted()
        {
            return Completed;
        }
    }
}