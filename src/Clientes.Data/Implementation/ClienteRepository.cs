using Domain.Core.Infrastructure;
using Domain.Core.Interfaces;
using Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
<<<<<<< HEAD
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;
=======
>>>>>>> 06565dc... Container de injeção de dependencia

namespace Clientes.Data.Implementation
{
    public class ClienteRepository : IRepository<Cliente>
    {
<<<<<<< HEAD
        IMongoDatabase DbContext { get; }
        string typeName = "BsonDocument";

        public ClienteRepository()
        {
            if (DbContext == null)
                DbContext = MongoDBInstance.GetMongoDatabase;
        }

        public async Task Adicionar(Cliente obj)
        {
            try
            {
                await DbContext.GetCollection<Cliente>(typeName).InsertOneAsync(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Atualizar(Cliente obj)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, obj.Id);
                var result = await DbContext.GetCollection<Cliente>(typeName)
                                            .ReplaceOneAsync(filter, obj, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cliente>> Buscar(Expression<Func<Cliente, bool>> predicate)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Where(predicate);
                var collection = await DbContext.GetCollection<Cliente>(typeName).FindAsync(filter);
                var retList = new List<Cliente>();

                await collection.ForEachAsync((Cliente Entity) =>
                {
                    retList.Add(Entity);
                });

                return await Task.FromResult(retList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cliente> ObterPorId(Guid id)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, id);
                return await DbContext.GetCollection<Cliente>(typeName).Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remover(Guid id)
        {
            try
            {
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, id);
                var result = await DbContext.GetCollection<Cliente>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
=======
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
>>>>>>> 06565dc... Container de injeção de dependencia
        }
    }
}