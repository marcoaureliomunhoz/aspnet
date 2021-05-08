using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using webapiv1.Interfaces;
using webapiv1.Jobs;
using webapiv1.Services;

namespace webapiv1
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
            // services
            services.AddScoped<IMyDateHelper, MyDateHelper>();

            // jobs
            services.AddScoped(typeof(MyDateJob));

            // hangfire
            services.AddHangfire(x => x.UseStorage(
                new MySqlStorage(
                    "server=localhost;uid=hangfirev1;pwd=hangfirev1;database=hangfirev1;Allow User Variables=True",
                    new MySqlStorageOptions
                    {
                        TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 50000,
                        TransactionTimeout = TimeSpan.FromMinutes(1),
                        TablesPrefix = "Hangfire"
                    })));

            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(ss => ss.FullName);
            });

            // mvc
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseHangfireServer();
	        app.UseHangfireDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
