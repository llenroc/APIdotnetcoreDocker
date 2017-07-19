using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Clientes.Domain.Entities;

namespace Clientes.API.ApplicationServices
{
    public interface IClienteApplicationService
    {
        Task Adicionar(Cliente obj);
        Task<Cliente> ObterPorId(string id);
        Task Atualizar(Cliente obj);
        Task Remover(string id);
        Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate);
        Task<Cliente> GetMostRecentCliente();
        Task<IEnumerable<Cliente>> BuscarTodos();
    }
}