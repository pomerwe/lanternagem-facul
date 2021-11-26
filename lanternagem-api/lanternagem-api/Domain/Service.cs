using lanternagem_api.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lanternagem_api.Models
{
    public class Service : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ServiceStep> Steps { get; set; }

        [JsonIgnore]
        public List<WorkOrder> WorkOrders { get; set; }

        public Service()
        {
            Steps = new List<ServiceStep>();
        }

        public object GetPrimaryKey()
        {
            return Id;
        }
    }
}