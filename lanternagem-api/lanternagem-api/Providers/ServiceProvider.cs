using lanternagem_api.Database;
using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Providers
{
    public class ServiceProvider : Interfaces.IServiceProvider
    {
        private readonly InsuranceDbContext dbContext;
        private readonly ILogger<ServiceProvider> logger;

        public ServiceProvider(InsuranceDbContext dbContext, ILogger<ServiceProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, Service Service, string ErrorMessage)> AddService(Service service)
        {
            try
            {
                var result = await dbContext.AddEntity(service);

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

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteService(int serviceId)
        {
            try
            {
                var result = await GetServiceById(serviceId);
                if (result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.Service);
                }
                else
                {
                    return (false, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Service Service, string ErrorMessage)> GetServiceById(int id)
        {
            try
            {
                Service service = await dbContext.Services
                                                 .Include(s => s.Steps)
                                                 .FirstOrDefaultAsync(s => s.Id == id);
                if (service != null)
                {
                    return (true, service, null);
                }
                else
                {
                    return (false, null, "Service not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<Service> Services, string ErrorMessage)> GetServices()
        {
            try
            {
                List<Service> services = await dbContext.Services
                                                        .Include(s => s.Steps)
                                                        .ToListAsync();
                if (services.Any())
                {
                    return (true, services, null);
                }
                else
                {
                    return (false, null, "No services recorded!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Service Service, string ErrorMessage)> UpdateService(Service service)
        {
            try
            {
                var result = await dbContext.UpdateEntity(service);

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
