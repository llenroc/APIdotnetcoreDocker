using System.Threading.Tasks;
using Domain.Entities;
using RestEase;

namespace Estabelecimentos.API
{
    [Header("User-Agent", "RestEase")]
    public interface IReceitaWS
    {
        [Get("cnpj/{cnpj}")]
        Task<Estabelecimento> GetEstabelecimentoAsync([Path] string cnpj);
    }
}
