using lanternagem_api.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace lanternagem_api.Models
{
    public class Accident : IEntity
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<WorkOrder> WorkOrders { get; set; }

        public object GetPrimaryKey()
        {
            return Id;
        }
    }
}