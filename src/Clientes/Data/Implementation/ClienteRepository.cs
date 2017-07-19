using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using MongoDB.Driver;
using Domain.Core.Infrastructure;
using Clientes.Domain.Entities;
using Clientes.Data.Interfaces;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Clientes.Data.Implementation
{
    public class ClienteRepository : IClienteRepository
    {
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

        public async Task<Cliente> ObterPorId(string id)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, objectId);
                return await DbContext.GetCollection<Cliente>(typeName).Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remover(string id)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, objectId);
                var result = await DbContext.GetCollection<Cliente>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cliente> GetMostRecentCliente()
        {
            try
            {
                var sort = Builders<Cliente>.Sort.Descending(x => x.Id);
                var filter = new BsonDocument();
                var result = DbContext.GetCollection<Cliente>(typeName).Find(filter);

                return await result.Sort(sort).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Cliente>> BuscarTodos()
        {
            try
            {
                var collection = DbContext.GetCollection<Cliente>(typeName).AsQueryable();
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
    }
}