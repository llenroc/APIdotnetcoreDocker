
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Core.Models;
using MongoDB.Bson;

namespace Clientes.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario(string login, string senha)
        {
            Login = login;
            Senha = senha;
        }
        public ObjectId ClienteId { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}