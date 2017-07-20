using System;
using Domain.Core.Models;

namespace Domain.Entities
{
    public class Estabelecimento : Entity
    {
        public Estabelecimento(string nome, string cnpj, string naturezaJuridica, string situacao)
        {
            Nome = nome;
            CNPJ = cnpj;
            NaturezaJuridica = naturezaJuridica;
            Situacao = situacao;
        }

        public string Nome { get; private set; }
        public string CNPJ { get; private set; }
        public string NaturezaJuridica { get; private set; }
        public string Situacao { get; private set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}