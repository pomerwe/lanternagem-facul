using lanternagem_api.Database;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Providers
{
    public class AccidentProvider : IAccidentProvider
    {
        private readonly InsuranceDbContext dbContext;
        private readonly ILogger<AccidentProvider> logger;

        public AccidentProvider(InsuranceDbContext dbContext, ILogger<AccidentProvider> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> AddAccident(Accident accident)
        {
            try
            {
                var result = await dbContext.AddEntity(accident);

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

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteAccident(int accidentId)
        {
            try
            {
                var result = await GetAccidentById(accidentId);
                if(result.IsSuccess)
                {
                    return await dbContext.DeleteEntity(result.Accident);
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

        public async Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> GetAccidentById(int id)
        {
            try
            {
                Accident accident = await dbContext.Accidents
                                                 .FirstOrDefaultAsync(a => a.Id == id);
                if (accident != null)
                {
                    return (true, accident, null);
                }
                else
                {
                    return (false, null, "Accident not found!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, List<Accident> Accidents, string ErrorMessage)> GetAccidents()
        {
            try
            {
                List<Accident> accidents = await dbContext.Accidents
                                                        .ToListAsync();
                if (accidents.Any())
                {
                    return (true, accidents, null);
                }
                else
                {
                    return (false, null, "No accidents recorded!");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message.ToString());
            }
        }

        public async Task<(bool IsSuccess, Accident Accident, string ErrorMessage)> UpdateAccident(Accident accident)
        {
            try
            {
                var result = await dbContext.UpdateEntity(accident);

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
