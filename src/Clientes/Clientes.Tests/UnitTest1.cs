using System;
using Clientes.Domain.Entities;
using Clientes.Data.Interfaces;
using FluentAssertions;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace Clientes.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Testa_Cliente_Adicionar_Se_Adiciona()
        {
            var mock = new Mock<IClienteRepository>();
            mock
                .Setup(x => x.Adicionar(It.IsAny<Cliente>()))
                .Returns(Task.FromResult(new Cliente()));

            var clienteService = mock.Object;
            var result = clienteService.Adicionar(new Cliente());


        }
    }
}
