using System;
using Domain.Core.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Clientes.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }

        [BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ClienteId { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}