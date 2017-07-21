using System;
namespace Estabelecimentos.API
{
    public class PagamentoViewModel
    {
        public string ClientID { get; set; }
        public string EstabelecimentoID { get; set; }
        public double Valor { get; set; }
    }
}
