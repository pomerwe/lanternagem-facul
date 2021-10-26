using lanternagem_api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace lanternagem_api.Domain
{
  public class SystemUser : IEntity
  {
    [Key]
    public long Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public User User { get; set; }

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}
