using System;
using Domain.Core.Models;

namespace Domain.Entities
{
    public class Pagamento : Entity
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int ClienteID { get; set; }
        public int EstabelecimentoID { get; set; }
        public bool IsCanceled { get; set; }

        public override bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}