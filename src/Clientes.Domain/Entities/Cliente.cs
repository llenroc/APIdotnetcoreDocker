
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Domain.Core.Models;

namespace Clientes.Domain.Entities
{
public class  Cliente : Entity
{
   
        public Cliente(string nome, string cpf, DateTime dataNascimento, string numeroCartao)
        {
            Nome = nome;
            CPF = cpf;
            DataNascimento = dataNascimento;
            NumeroCartao = numeroCartao;
        }
        
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string NumeroCartao { get; private set; }
        public override bool EhValido()
        {
            throw new NotImplementedException();
        }

}
}