using Domain.Core.Infrastructure;
using Domain.Core.Interfaces;
using Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Clientes.Data.Implementation
{
    public class ClienteRepository : IRepository<Cliente>
    {
        IRepository<Cliente> _repositorio;
        public ClienteRepository(IRepository<Cliente> repository)
        {
            _repositorio = repository;
        }


        void IRepository<Cliente>.Adicionar(Cliente obj)
        {
            throw new NotImplementedException();
        }

        void IRepository<Cliente>.Atualizar(Cliente obj)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Cliente> IRepository<Cliente>.Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        Cliente IRepository<Cliente>.ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        void IRepository<Cliente>.Remover(Guid id)
        {
            throw new NotImplementedException();
        }

        int IRepository<Cliente>.SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}