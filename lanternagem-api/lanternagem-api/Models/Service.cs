using System.Collections.Generic;

namespace lanternagem_api.Models
{
  public class Service
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<ServiceStep> Steps { get; set; }
  }
}