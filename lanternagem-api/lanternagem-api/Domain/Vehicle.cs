using lanternagem_api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class Vehicle : IEntity
  {
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}