using lanternagem_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Database.Configurations
{
    public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(EntityTypeBuilder<WorkOrder> builder)
        {
            builder.HasOne(w => w.Accident)
                .WithMany(a => a.WorkOrders);

            builder.HasOne(w => w.Customer)
              .WithMany(c => c.WorkOrders);

            builder.HasOne(w => w.Vehicle)
              .WithMany(v => v.WorkOrders);

            builder.HasOne(w => w.Service)
              .WithMany(s => s.WorkOrders);
        }
    }
}
