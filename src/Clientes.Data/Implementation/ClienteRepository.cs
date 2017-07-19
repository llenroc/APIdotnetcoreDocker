using Domain.Core.Infrastructure;
using Domain.Core.Interfaces;
using Clientes.Domain.Entities;

namespace Clientes.Data.Implementation
{
    public class ClienteRepository
    {
        IRepository<Cliente> _repositorio {get;}
        
    }
}