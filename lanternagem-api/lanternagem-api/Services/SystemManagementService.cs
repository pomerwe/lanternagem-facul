using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Services
{
  public class SystemManagementService : ISystemManagementService
  {
    private readonly ILogger<SystemManagementService> logger;
    private readonly ISystemUserProvider userProvider;

    public SystemManagementService(ILogger<SystemManagementService> logger, ISystemUserProvider userProvider)
    {
      this.logger = logger;
      this.userProvider = userProvider;
    }

    public async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForCostumer(Customer customer)
    {
      try
      {
        var result = await InsertNewUser(customer, Role.Customer);

        if (result.IsSuccess)
        {
          return (true, result.NewUser, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }

    public async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForManager(Manager manager)
    {
      try
      {
        var result = await InsertNewUser(manager, Role.Manager);

        if (result.IsSuccess)
        {
          return (true, result.NewUser, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message);
      }
    }

    private async Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUser(IUser user, Role role)
    {
        var newUser = CreateNewUser(user, role);

        var result = await userProvider.AddUser(newUser);

        if (result.IsSuccess)
        {
          return (true, result.User, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
    }

    private SystemUser CreateNewUser(IUser user, Role role)
    {
      SystemUser newUser = new SystemUser();
      newUser.Login = user.GetName().Substring(user.GetName().Length/2) + user.GetCPF().Substring(user.GetName().Length / 4, user.GetName().Length/3) + user.GetCPF().Substring(0);
      var guid =  Guid.NewGuid();
      newUser.Password = guid.ToString();
      newUser.Role = role;
      return newUser;
    }
  }
}
