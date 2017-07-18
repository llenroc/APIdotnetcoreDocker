using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Core.Models;

namespace Domain.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Adicionar(T obj);
        T ObterPorId(Guid id);
        void Atualizar(T obj);
        void Remover(Guid id);
        IEnumerable<T> Buscar(Expression<Func<T, bool>> predicate);
        int SaveChanges();
    }

}