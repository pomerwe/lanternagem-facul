using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Database
{
  public class InsuranceDbContext : DbContext
  {
    public DbSet<Accident> Accidents { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<InsuranceBranch> InsuranceBranches { get; set; }
    public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceStep> ServiceSteps { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }
    public DbSet<WorkOrderStep> WorkOrderSteps { get; set; }
    public InsuranceDbContext(DbContextOptions options) : base(options)
    {

    }

    public async Task<(bool IsSuccess, T Entity, string ErrorMessage)> AddOrUpdate<T>(T entity) where T : class
    {
      try
      {
        var entitySet = Set<T>();
        var dbEntity = entitySet.Find(entity);
        if (dbEntity != null)
        {
          entitySet.Update(entity);
        }
        else
        {
          await entitySet.AddAsync(entity);
        }
        await SaveChangesAsync();
        Entry(entity).State = EntityState.Detached;
        return (true, entity, null);
      }
      catch (Exception ex)
      {
        return (false, null, ex.ToString());
      }
    }

    public async Task<(bool IsSuccess, string ErrorMessage)> Delete<T>(T entity) where T : class
    {
      try
      {
        var entitySet = Set<T>();
        entitySet.Remove(entity);
        await SaveChangesAsync();
        return (true, null);
      }
      catch (Exception ex)
      {
        return (false, ex.ToString());
      }
    }
  }
}
