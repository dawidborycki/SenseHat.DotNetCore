#region Using

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SenseHat.DotNetCore.Common.Helpers;
using System;

#endregion

namespace SenseHat.DotNetCore.WebApp
{
    public class Startup
    {
        #region Fields

        private readonly OpenApiInfo openApiInfo = new OpenApiInfo
        {
            Title = "SenseHAT API",
            Version = "v1"
        };

        #endregion

        #region Constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Methods (Public)

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            // Swagger generator
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(openApiInfo.Version, openApiInfo);
            });

            // SenseHatService (emulation mode is obtained from appsettings.json)
            var emulationMode = string.Equals(
                Configuration["Emulation"], "Y",
                StringComparison.OrdinalIgnoreCase);

            var senseHatService = SenseHatServiceHelper.GetService(emulationMode);
            services.AddSingleton(senseHatService);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint($"/swagger/{openApiInfo.Version}/swagger.json",
                    openApiInfo.Title);
            });
        }

        #endregion
    }
}
