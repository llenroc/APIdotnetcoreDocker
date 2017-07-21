
using Clientes.Data.Implementation;
using Clientes.Data.Interfaces;
using Data.Implematations;
using Data.Implementations;
using Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Pagamentos.Data.Interfaces;

namespace CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
			services.AddScoped<IPagamentoRepository, PagamentoRepository>();
		}
    }
}