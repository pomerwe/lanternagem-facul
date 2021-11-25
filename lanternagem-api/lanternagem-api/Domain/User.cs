using lanternagem_api.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Domain
{
  public abstract class User : IUser, IEntity
  {
    [Key]
    public long Id { get; set; }

    public abstract string GetCPF();
    public abstract string GetName();

    public object GetPrimaryKey()
    {
      return Id;
    }
  }
}
