using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> ObterMaisRecente();
    }
}