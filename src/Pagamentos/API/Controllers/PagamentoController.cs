using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pagamentos.API.ApplicationServices;
using Pagamentos.Domain.Entities;

namespace Pagamentos.API.Controllers
{
    [Route("api/[controller]")]
    public class PagamentoController : Controller
    {
        IPagamentoApplicationService _PagamentoService { get; }

        public PagamentoController(IPagamentoApplicationService PagamentoService)
        {
            _PagamentoService = PagamentoService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Pagamento payment)
        {
            if (payment == null)
                return BadRequest("Pagamento precisa ser informado");

            try
            {
                await _PagamentoService.Adicionar(payment);
                var newpayment = await _PagamentoService.ObterMaisRecente();

                return CreatedAtRoute("GetPagamento", new { id = newpayment.Id }, newpayment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pagamento payment)
        {
            if (payment == null)
                return BadRequest("payment precisa ser informado");

            try
            {
                var _id = payment.Id.ToString();
                var paymentItem = await _PagamentoService.ObterPorId(_id);

                if (paymentItem == null)
                    return NotFound("payment não encontrado");

                await _PagamentoService.Atualizar(payment);
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("É preciso informar o ID do Pagamento que será removido");

            var payment = await _PagamentoService.ObterPorId(id);

            if (payment == null)
                return NotFound();

            await _PagamentoService.Remover(id);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllpayments()
        {
            try
            {
                var payments = await _PagamentoService.BuscarTodos();

                if (payments != null && payments.Any())
                    return new ObjectResult(payments);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}", Name = "GetPagamento")]
        public async Task<IActionResult> GetPagamentoyId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            Expression<Func<Pagamento,bool>> filter =(x)=> x.EstabelecimentoID == id;
            var payment = await _PagamentoService.Buscar(filter);

            if (payment == null)
                return NotFound();

            return new ObjectResult(payment);
        }
    }
}
