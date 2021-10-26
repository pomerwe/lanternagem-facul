using lanternagem_api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface ISystemUserProvider
  {
    Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> GetUserByLogin(string login);
    Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> AddUser(SystemUser user);
    Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> UpdateUser(SystemUser user);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteUserUsingLogin(string login);
  }
}
