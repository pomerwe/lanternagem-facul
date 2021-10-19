using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class Service
  {
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ServiceStep> Steps { get; set; }
  }
}