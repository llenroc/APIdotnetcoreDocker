using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Clientes.API.ApplicationServices;
using Clientes.Domain.Entities;
using MongoDB.Bson;

namespace Clientes.API.Controllers
{
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        IClienteApplicationService _clienteService { get; }

        public ClienteController(IClienteApplicationService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] Cliente customer)
        {
            if (customer == null)
                return BadRequest("Cliente precisa ser informado");

            try
            {
                await _clienteService.Adicionar(customer);
                var newCustomer = await _clienteService.GetMostRecentCliente();

                return CreatedAtRoute("GetCliente", new { id = newCustomer.Id }, newCustomer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cliente customer)
        {
            if (customer == null)
                return BadRequest("Customer precisa ser informado");

            try
            {
                var _id = customer.Id.ToString();
                var customerItem = await _clienteService.ObterPorId(_id);

                if (customerItem == null)
                    return NotFound("Customer não encontrado");

                await _clienteService.Atualizar(customer);
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
                return BadRequest("É preciso informar o ID do cliente que será removido");

            var customer = await _clienteService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            await _clienteService.Remover(id);
            return new NoContentResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _clienteService.BuscarTodos();

                if (customers != null && customers.Any())
                    return new ObjectResult(customers);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}", Name = "GetCliente")]
        public async Task<IActionResult> GetClienteyId(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var customer = await _clienteService.ObterPorId(id);

            if (customer == null)
                return NotFound();

            return new ObjectResult(customer);
        }
    }
}