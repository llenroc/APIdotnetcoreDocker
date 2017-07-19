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
using MongoDB.Driver;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using CrossCutting.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using CrossCutting.Identity.Authorization;

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

            //  services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
            // services.AddDbContext<ApplicationDbContext>(options =>
            //     options.Use (Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Cookies.ApplicationCookie.AutomaticChallenge = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            //  services.AddOptions();
            // Add framework services.
            services.AddMvc(options =>
       {

           // options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));

           var policy = new AuthorizationPolicyBuilder()
               .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
               .RequireAuthenticatedUser()
               .Build();

           options.Filters.Add(new AuthorizeFilter(policy));

       });

            services.AddAuthorization(options =>
         {
             options.AddPolicy("PodeLerEventos", policy => policy.RequireClaim("Eventos", "Ler"));
             options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Eventos", "Gravar"));
         });

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));

            // services.Configure<JwtTokenOptions>(options =>
            // {
            //     options.Issuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)];
            //     options.Audience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)];
            //     options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            // });

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseIdentity();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
