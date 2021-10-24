using lanternagem_api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Models
{
  public class Accident : IEntity
  {
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}