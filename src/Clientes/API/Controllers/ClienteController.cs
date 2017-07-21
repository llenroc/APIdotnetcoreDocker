using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Clientes.API.ApplicationServices;
using Clientes.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CrossCutting.Identity.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Clientes.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ClienteController : Controller
    {
        IClienteApplicationService _clienteService { get; }
        private ICustomJwtSecurityToken _customJwtSecurityToken { get; }

        public ClienteController(IClienteApplicationService clienteService, ICustomJwtSecurityToken customJwtSecurityToken)
        {
            _clienteService = clienteService;
            _customJwtSecurityToken = customJwtSecurityToken;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Insert([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente precisa ser informado");

            try
            {
                await _clienteService.Adicionar(cliente);
                var newCustomer = await _clienteService.ObterMaisRecente();
                //Todo:Criar usuário com login e senha
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
                return BadRequest("Cliente precisa ser informado");

            try
            {
                var _id = customer.Id.ToString();
                var customerItem = await _clienteService.ObterPorId(_id);

                if (customerItem == null)
                    return NotFound("Cliente não encontrado");

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



        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] string login, string senha)
        {
            //TODO:Fazer login
            var user = new Usuario(login, senha);
            var token = await GetJwtSecurityToken(user);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        private async Task<JwtSecurityToken> GetJwtSecurityToken(Usuario user)
        {
            //TODO: Carregar as claims do usuário
            var clains = new List<Claim>();
            clains.Add(new Claim("Nome", "Teste Nome"));
            clains.Add(new Claim("Acesso", "Teste Acesso"));
            clains.Add(new Claim("Tela", "Teste Tela"));
            var token = await _customJwtSecurityToken.GerarToken(clains);
            return token;
        }
    }
}