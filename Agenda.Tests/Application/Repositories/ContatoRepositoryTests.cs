using Agenda.Domain.Entities;
using Agenda.Infrastructure.Data;
using Agenda.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Agenda.Tests.Repositories
{
    public class ContatoRepositoryTests
    {
        private readonly ContatoRepository _contatoRepository;
        private readonly AgendaDbContext _dbContext;

        public ContatoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AgendaDbContext>()
                .UseInMemoryDatabase(databaseName: $"AgendaDb_{Guid.NewGuid()}")
                .Options;

            _dbContext = new AgendaDbContext(options);
            _contatoRepository = new ContatoRepository(_dbContext);
        }

        private Contato CriarContato()
        {
            return new Contato
            {
                Nome = "Ana Silva",
                Email = "ana@email.com",
                Telefone = "123456789",
                Endereco = "Rua B, 456",
                DataNascimento = new DateTime(1985, 4, 22),
                Site = "https://ana.com",
                TelefoneComercial = "987654321"
            };
        }

        [Fact]
        public async Task DeveAdicionarContato_ComSucesso()
        {
           
            var contato = CriarContato();

          
            var resultado = await _contatoRepository.AddAsync(contato);

        
            resultado.Should().NotBeNull();
            resultado.Id.Should().BeGreaterThan(0);
            resultado.Nome.Should().Be("Ana Silva");
            resultado.Email.Should().Be("ana@email.com");
        }

        [Fact]
        public async Task DeveRetornarContato_PorId()
        {
            
            var contato = CriarContato();
            await _contatoRepository.AddAsync(contato);

            
            var resultado = await _contatoRepository.GetByIdAsync(contato.Id);

            
            resultado.Should().NotBeNull();
            resultado.Id.Should().Be(contato.Id);
            resultado.Nome.Should().Be("Ana Silva");
        }

        [Fact]
        public async Task DeveAtualizarContato_ComSucesso()
        {
           
            var contato = CriarContato();
            await _contatoRepository.AddAsync(contato);

            contato.Nome = "Ana Atualizada";
            contato.Telefone = "000000000";

            
            var resultado = await _contatoRepository.UpdateAsync(contato);
            var contatoAtualizado = await _contatoRepository.GetByIdAsync(contato.Id);

            
            resultado.Should().NotBeNull();
            contatoAtualizado.Nome.Should().Be("Ana Atualizada");
            contatoAtualizado.Telefone.Should().Be("000000000");
        }

        [Fact]
        public async Task DeveRemoverContato_ComSucesso()
        {
           
            var contato = CriarContato();
            await _contatoRepository.AddAsync(contato);

           
            var resultado = await _contatoRepository.DeleteAsync(contato.Id);
            var contatoRemovido = await _contatoRepository.GetByIdAsync(contato.Id);

            
            resultado.Should().BeTrue();
            contatoRemovido.Should().BeNull();
        }

        [Fact]
        public async Task DeveRetornarTodosOsContatos()
        {
            
            var contato1 = CriarContato();
            var contato2 = new Contato
            {
                Nome = "João Souza",
                Email = "joao@email.com",
                Telefone = "222333444",
                Endereco = "Avenida X, 123",
                DataNascimento = new DateTime(1992, 10, 11),
                Site = "https://joaosite.com",
                TelefoneComercial = "333222111"
            };

            await _contatoRepository.AddAsync(contato1);
            await _contatoRepository.AddAsync(contato2);

            
            var contatos = await _contatoRepository.GetAllAsync();

            
            contatos.Should().HaveCount(2);
            contatos.Should().Contain(c => c.Nome == "Ana Silva");
            contatos.Should().Contain(c => c.Nome == "João Souza");
        }

        [Fact]
        public async Task DeveRetornarFalse_AoDeletarContatoInexistente()
        {
           
            var resultado = await _contatoRepository.DeleteAsync(999);

            
            resultado.Should().BeFalse();
        }
    }
}
