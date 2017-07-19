using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task Adicionar(T obj);
        Task<T> ObterPorId(Guid id);
        Task Atualizar(T obj);
        Task Remover(Guid id);
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
    }
}