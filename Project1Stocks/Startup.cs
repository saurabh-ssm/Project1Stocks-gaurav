using Hangfire;
using Hangfire.SqlServer;
using Project1Stocks.Data;
using Project1Stocks.Jobs;
using Project1Stocks.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project1Stocks.WebSockets;

namespace Project1Stocks
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
            services.AddControllersWithViews();

            // Add Hangfire services
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add services
            services.AddScoped<WebSocketHandler>();
            services.AddScoped<StockUpdater>();
            services.AddScoped<IStockService, StockService>();

            // Add Hangfire server
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            
            

            

            
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            // Schedule the background jobs
            RecurringJob.AddOrUpdate<StockUpdater>("Wipro", s => s.UpdateWipro(), "0/1 * * * * *");
            
            RecurringJob.AddOrUpdate<StockUpdater>("Zensar", s => s.UpdateZensar(), "2 * * * * *"); 
            RecurringJob.AddOrUpdate<StockUpdater>("TCS", s => s.UpdateTCS(), "2 * * * * *"); 
            RecurringJob.AddOrUpdate<StockUpdater>("Tesla", s => s.UpdateTesla(), "1 * * * * *"); 
            RecurringJob.AddOrUpdate<StockUpdater>("Apple", s => s.UpdateApple(), "1 * * * * *"); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
// "*/1 * * * * *"
