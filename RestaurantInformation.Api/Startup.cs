using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantInformation.Core.Helpers;
using RestaurantInformation.Core.Interfaces;
using RestaurantInformation.Infrastructure.Services;
using Serilog;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantInformation.Api
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
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            services.AddHttpClient();

            services.AddScoped<IRestaurant, RestaurantService>();
            services.AddScoped<IRestaurantInfo, RestaurantInfoService>();
            services.AddMemoryCache();
            services.AddSingleton<ICache, InMemoryCacheService>();
            services.AddScoped<JwtToken>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddControllers()
           .ConfigureApiBehaviorOptions(o =>
           {
               // Disable `ClientErrorResultFilter`
               o.SuppressMapClientErrors = true;
               // Custom response format for model validation and binding errors
               o.InvalidModelStateResponseFactory = context =>
               {
                   var dto = new CustomErrorDto
                   {
                       Error = context.ModelState.First().Value.Errors.First().ErrorMessage.Split('|')[0],
                       StatusCode = 400,
                       RequestId = context.HttpContext.TraceIdentifier
                   };
                   return new ObjectResult(dto) { StatusCode = dto.StatusCode };
               };
           })
           .AddJsonOptions(o => { o.JsonSerializerOptions.WriteIndented = true; });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantInformation.Api", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantInformation.Api v1");
                    c.DisplayRequestDuration();
                });

            }


            //It automatically logs http request and response so you dont have to be writing logs everywhere you dont need
            app.UseSerilogRequestLogging(x =>
            {
                x.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                };
            });

        
            app.UseSwagger();

            app.UseHttpsRedirection();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
