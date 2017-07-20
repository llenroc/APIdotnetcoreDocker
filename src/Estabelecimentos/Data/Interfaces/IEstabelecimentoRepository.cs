using System;
using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IEstabelecimentoRepository : IRepository<Estabelecimento>
    {
         Task<Estabelecimento> ObterMaisRecente();
    }
}