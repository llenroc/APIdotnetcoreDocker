using System.Threading.Tasks;
using Domain.Core.Interfaces;
using Pagamentos.Domain.Entities;

namespace Pagamentos.Data.Interfaces
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> ObterMaisRecente();
    }
}