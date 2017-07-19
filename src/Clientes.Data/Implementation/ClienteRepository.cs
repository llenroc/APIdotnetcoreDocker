using Domain.Core.Infrastructure;
using Domain.Core.Interfaces;
using Clientes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Linq;

namespace Clientes.Data.Implementation
{
    public class ClienteRepository : IRepository<Cliente>
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

        public async Task<Cliente> ObterPorId(Guid id)
        {
            try
            {
                var objectId = ObjectId.Parse(id.ToString());
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, objectId);
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
                var objectId = ObjectId.Parse(id.ToString());
                var filter = Builders<Cliente>.Filter.Eq(x => x.Id, objectId);
                var result = await DbContext.GetCollection<Cliente>(typeName).FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}