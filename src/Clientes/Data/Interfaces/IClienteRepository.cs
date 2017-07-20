using System.Threading.Tasks;
using Clientes.Domain.Entities;
using Domain.Core.Interfaces;

namespace Clientes.Data.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterMaisRecente();
    }
}