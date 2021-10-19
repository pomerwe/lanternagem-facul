using lanternagem_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
  public interface IServiceProvider
  {
    Task<(bool IsSuccess, Service Service, string ErrorMessage)> GetServiceById(int id);
    Task<(bool IsSuccess, List<Service> Services , string ErrorMessage)> GetServices();
    Task<(bool IsSuccess, string ErrorMessage)> AddService(Service service);
    Task<(bool IsSuccess, string ErrorMessage)> UpdateService(Service service);
    Task<(bool IsSuccess, string ErrorMessage)> DeleteService(Service service);
  }
}
