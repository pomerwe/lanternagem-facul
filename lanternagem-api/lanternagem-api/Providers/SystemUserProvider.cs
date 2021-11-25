using lanternagem_api.Database;
using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Providers
{
  public class SystemUserProvider : ISystemUserProvider
  {
    private readonly InsuranceDbContext dbContext;
    private readonly ILogger<SystemUserProvider> logger;

    public SystemUserProvider(InsuranceDbContext dbContext, ILogger<SystemUserProvider> logger)
    {
      this.dbContext = dbContext;
      this.logger = logger;
    }
    public async Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> AddUser(SystemUser user)
    {
      try
      {
        var result = await dbContext.AddEntity(user);

        if (result.IsSuccess)
        {
          return (true, result.Entity, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> DeleteUserUsingLogin(string login)
    {
      try
      {
        var result = await GetUserByUsername(login);
        return await dbContext.DeleteEntity(result.User);
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> GetUserByUsername(string login)
    {
      try
      {
        SystemUser User = await dbContext.Users
                                           .Include(u => u.User)
                                           .FirstOrDefaultAsync(u => u.Username == login);
        if (User != null)
        {
          return (true, User, null);
        }
        else
        {
          return (false, null, "User not found in system!");
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }

    public async Task<(bool IsSuccess, SystemUser User, string ErrorMessage)> UpdateUser(SystemUser user)
    {
      try
      {
        var result = await dbContext.UpdateEntity(user);

        if (result.IsSuccess)
        {
          return (true, result.Entity, null);
        }
        else
        {
          return (false, null, result.ErrorMessage);
        }
      }
      catch (Exception ex)
      {
        logger.LogError(ex.ToString());
        return (false, null, ex.Message.ToString());
      }
    }
  }
}
