using lanternagem_api.Domain;
using lanternagem_api.Models;
using lanternagem_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface ISystemManagementService
  {
    Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForCostumer(Customer costumer);
    Task<(bool IsSuccess, SystemUser NewUser, string ErrorMessage)> InsertNewUserForManager(Manager manager);
  }
}
