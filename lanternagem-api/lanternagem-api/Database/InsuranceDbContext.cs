using lanternagem_api.Database.Configurations;
using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using lanternagem_api.Models;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<SystemUser> Users { get; set; }

        public InsuranceDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WorkOrderConfiguration());
        }

        public async Task<(bool IsSuccess, T Entity, string ErrorMessage)> AddEntity<T>(T entity) where T : class
        {
            try
            {
                var entitySet = Set<T>();

                var dbEntity = GetDbEntity(entity);

                if (dbEntity != null)
                {
                    throw new Exception("Entity already exists in database!");
                }

                await entitySet.AddAsync(entity);
                await SaveChangesAsync();
                Entry(entity).State = EntityState.Detached;
                return (true, entity, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, T Entity, string ErrorMessage)> UpdateEntity<T>(T entity) where T : class
        {
            try
            {
                var entitySet = Set<T>();

                var dbEntity = GetDbEntity(entity);

                if (dbEntity == null)
                {
                    throw new Exception("Entity does not exist in database!");
                }
                Entry(dbEntity).State = EntityState.Detached;

                entitySet.Update(entity);
                await SaveChangesAsync();
                Entry(entity).State = EntityState.Detached;
                return (true, entity, null);
            }
            catch (Exception ex)
            {
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> DeleteEntity<T>(T entity) where T : class
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

        private T GetDbEntity<T>(T entity) where T : class
        {
            var entitySet = Set<T>();
            var pk = ((IEntity)entity).GetPrimaryKey();
            var dbEntity = entitySet.Find(pk);

            return dbEntity;
        }
    }
}
