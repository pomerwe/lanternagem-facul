using lanternagem_api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Database
{
  public class LanternagemDbContext : DbContext
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
    public LanternagemDbContext(DbContextOptions options) : base(options)
    {

    }
  }
}
