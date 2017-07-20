using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CrossCutting.IoC;
using CrossCutting.Identity;
using CrossCutting.Identity.Data;

using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CrossCutting.Identity.Models;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CrossCutting.Identity.Authorization;
using API.Helpers;
using Clientes.API.ApplicationServices;
using Clientes.API.Services;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Clientes.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {



            //  services.AddOptions();
            // Add framework services.
            services.AddMvc(options =>
       {

           // options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));

           options.Filters.Add(typeof(CustomExceptionFilter));

       });



            services.AddAuthorization();
            services.AddScoped<IClienteApplicationService, ClienteApplicationService>();
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            var keyBytes = Encoding.UTF8.GetBytes("authorizationkey");
            var jwtOptions = new JwtBearerOptions()
            {
                Audience = "http://localhost:5000/",
                AutomaticAuthenticate = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5000/",
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes)

                },
            };

            app.UseJwtBearerAuthentication(jwtOptions);
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
