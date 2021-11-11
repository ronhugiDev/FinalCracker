using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Minion.Repositories;
using Minion.Services;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minion
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

            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowAnyOrigin();
                });
            });

            //get settings from appsettings
            services.Configure<ServiceSettings>(Configuration.GetSection(nameof(ServiceSettings)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minion", Version = "v1" });
            });


            services.AddHttpClient<MasterHealthCheck>().AddTransientHttpErrorPolicy(builder =>
               builder.WaitAndRetryAsync(10, retryAtt => TimeSpan.FromSeconds(Math.Pow(2, retryAtt))))
                //Circuit Breaker 3 tries
                .AddTransientHttpErrorPolicy(builder =>
                builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(10)));



            services.AddHealthChecks().AddCheck<MasterHealthCheck>("MasterHealthCheck");
           
            services.AddHttpClient<HealthCheack>().AddTransientHttpErrorPolicy(builder =>
               builder.WaitAndRetryAsync(10, retryAtt => TimeSpan.FromSeconds(Math.Pow(2, retryAtt))))
                //Circuit Breaker 3 tries
                .AddTransientHttpErrorPolicy(builder =>
                builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(10)));


            services.AddTransient<IPasswordCracekr, PasswordCracekr>();
            services.AddSingleton<IPhoneNumberReposetory, PhoneNumberReposetory>();
            services.AddSingleton<IHealthCheack, HealthCheack>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minion v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
