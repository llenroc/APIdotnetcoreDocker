
using Clientes.Data.Implementation;
using Clientes.Data.Interfaces;
using Clientes.Domain.Entities;
using Domain.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            services.AddScoped<IClienteRepository, ClienteRepository>();

        }
    }
}