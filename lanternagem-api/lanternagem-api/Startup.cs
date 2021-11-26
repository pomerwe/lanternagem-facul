using lanternagem_api.Database;
using lanternagem_api.Domain;
using lanternagem_api.Interfaces;
using lanternagem_api.Providers;
using lanternagem_api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lanternagem_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<InsuranceDbContext>(op => op.UseInMemoryDatabase("LanternagemDatabase"));

            services.AddScoped(typeof(ICustomerProvider), typeof(CustomerProvider));
            services.AddScoped(typeof(IInsuranceCompanyProvider), typeof(InsuranceCompanyProvider));
            services.AddScoped(typeof(IInsuranceService), typeof(InsuranceService));
            services.AddScoped(typeof(Interfaces.IServiceProvider), typeof(Providers.ServiceProvider));
            services.AddScoped(typeof(IWorkOrderProvider), typeof(WorkOrderProvider));
            services.AddScoped(typeof(IWorkOrderService), typeof(WorkOrderService));
            services.AddScoped(typeof(ISystemUserProvider), typeof(SystemUserProvider));
            services.AddScoped(typeof(ISystemManagementService), typeof(SystemManagementService));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IAccidentProvider), typeof(AccidentProvider));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Lanternagem API" });
            });


            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("Secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Configuration V1");
            });
        }
    }
}
