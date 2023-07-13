using AcceptedTechnicalTest.DataRepository;
using AcceptedTechnicalTest.DataRepository.Classes;
using AcceptedTechnicalTest.DataRepository.Interfaces;
using AcceptedTechnicalTest.Mappings;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

namespace AcceptedTechnicalTest
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
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                .CreateLogger();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            // options
            services.AddSingleton<ILogger>(logger);

            string connectionString = Configuration.GetConnectionString("AcceptedDb");

            services.AddDbContext<AcceptedDb_Context>(options =>
                    options.UseSqlServer(connectionString))
                .AddScoped<IDbRepository, DbRepository>();



            services.AddControllers();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Accepted Technical Test", Version = "v1" });
                });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Accepted Technical Test");
                c.RoutePrefix = "swagger";
            });
        }
    }
}
