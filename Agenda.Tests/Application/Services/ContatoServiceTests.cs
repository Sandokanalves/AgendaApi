using Agenda.Application.Services;
using Agenda.Application.DTOS.InputModels;
using Agenda.Domain.Entities;
using Agenda.Application.Interfaces;
using AutoMapper;
using FluentValidation;
using Moq;
using FluentAssertions;
using Agenda.Application.DTOS.ViewModels;

namespace Agenda.Tests.Application.Services;

public class ContatoServiceTests
{
    private readonly Mock<IContatoRepository> _contatoRepositoryMock;
    private readonly Mock<IValidator<CreateContatoInputModel>> _validatorMock;
    private readonly Mock<IValidator<UpdateContatoInput>> _updatevalidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ContatoService _contatoService;

    public ContatoServiceTests()
    {
        _contatoRepositoryMock = new Mock<IContatoRepository>();
        _validatorMock = new Mock<IValidator<CreateContatoInputModel>>();
        _updatevalidatorMock = new Mock<IValidator<UpdateContatoInput>>();
        _mapperMock = new Mock<IMapper>();

        _contatoService = new ContatoService(_contatoRepositoryMock.Object, _validatorMock.Object, _updatevalidatorMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddAsync_DeveAdicionarContato_QuandoValido()
    {
        // Arrange
        var inputModel = new CreateContatoInputModel
        {
            Nome = "Nome do Contato",
            Email = "email@gmail.com",
            Telefone = "(81) 99292-7867",
            Endereco = "Rua A, 123",
            DataNascimento = new DateTime(1990, 5, 10),
            Site = "https://joaosite.com",
            TelefoneComercial = "987654321"
        };

        var contato = new Contato
        {
            Id = 1,
            Nome = inputModel.Nome,
            Email = inputModel.Email,
            Telefone = inputModel.Telefone,
             Endereco = "Rua A, 123",
            DataNascimento = new DateTime(1990, 5, 10),
            Site = "https://joaosite.com",
            TelefoneComercial = "987654321"
        };

        var contatoViewModel = new ContatoViewModel(contato.Id, inputModel.Nome, contato.Email, contato.Telefone);

        
        _validatorMock.Setup(v => v.ValidateAsync(inputModel, default))
                      .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        
        _contatoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Contato>()))
                              .ReturnsAsync(contato);

        
        _mapperMock.Setup(m => m.Map<ContatoViewModel>(It.IsAny<Contato>()))
                   .Returns(contatoViewModel);

        // Act
        var resultado = await _contatoService.AddAsync(inputModel);

        // Assert
        resultado.Should().NotBeNull(); 
        resultado.Nome.Should().Be(inputModel.Nome); 
       
    }




    [Fact]
    public async Task UpdateAsync_DeveAtualizarContato_QuandoValido()
    {
      
        var inputModel = new UpdateContatoInput
        {
            Email = "email@gmail.com",
            Telefone = "123456789",
            Endereco = "Rua A, 123",
            DataNascimento = new DateTime(1990, 5, 10),
            Site = "https://joaosite.com",
            TelefoneComercial = "987654321"
        };

        var contatoExistente = new Contato
        {
            Id = 1,
            Nome = "Nome Antigo",
            Email = "antigoemail@gmail.com",
            Telefone = "123456789",
            Endereco = "Rua A, 123",
            DataNascimento = new DateTime(1990, 5, 10),
            Site = "https://joaosite.com",
            TelefoneComercial = "987654321"
        };

        var contatoAtualizado = new Contato
        {
            Id = 1,
            Email = inputModel.Email,
            Telefone = inputModel.Telefone,
            Endereco = inputModel.Endereco,
            DataNascimento = inputModel.DataNascimento,
            Site = inputModel.Site,
            TelefoneComercial = inputModel.TelefoneComercial
        };
        var contatoViewModel = new ContatoViewModel(contatoAtualizado.Id, contatoExistente.Nome, contatoExistente.Email,contatoExistente.Telefone);

       
        _updatevalidatorMock.Setup(v => v.ValidateAsync(It.IsAny<UpdateContatoInput>(), default))
                            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        
        _contatoRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync(contatoExistente);

        
        _contatoRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Contato>()))
                              .ReturnsAsync(contatoAtualizado);

       
        _mapperMock.Setup(m => m.Map<ContatoViewModel>(It.IsAny<Contato>()))
                   .Returns(contatoViewModel);

        
        var resultado = await _contatoService.UpdateAsync(1, inputModel);

     
        resultado.Should().NotBeNull();
        resultado.Id.Should().Be(1);
        resultado.Nome.Should().Be("Nome Antigo");

    }

    [Fact]
    public async Task DeleteAsync_DeveExcluirContato_QuandoExistir()
    {
        
        var contatoExistente = new Contato
        {
            Id = 1,
            Nome = "Nome para Excluir",
            Email = "emailparaexcluir@gmail.com",
            Telefone = "123456789",
            Endereco = "Rua A, 123",
            DataNascimento = new DateTime(1990, 5, 10),
            Site = "https://joaosite.com",
            TelefoneComercial = "987654321"
        };
         

       
        _contatoRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>()))
                              .ReturnsAsync(true);

       
        var resultado = await _contatoService.DeleteAsync(1);

      
        resultado.Should().BeTrue();
    }


    [Fact]
    public async Task GetAllAsync_DeveRetornarContatos()
    {
       
        var contatos = new List<Contato>
    {
        new Contato { Id = 1, Nome = "Contato 1", Email = "contato1@gmail.com", Telefone = "(81) 99111-2222" },
        new Contato { Id = 2, Nome = "Contato 2", Email = "contato2@gmail.com", Telefone = "(81) 99222-3333" }
    };

        
        _contatoRepositoryMock.Setup(repo => repo.GetAllAsync())
                              .ReturnsAsync(contatos);

        
        var contatoViewModels = contatos.Select(c => new ContatoViewModel(c.Id, c.Nome, c.Email, c.Telefone)).ToList();

        _mapperMock.Setup(m => m.Map<IEnumerable<ContatoViewModel>>(contatos))
                   .Returns(contatoViewModels);

        
        var resultado = await _contatoService.GetAllAsync();

       
        resultado.Should().NotBeNull()
                 .And.HaveCount(2) 
                 .And.Contain(c => c.Id == 1 && c.Nome == "Contato 1")
                 .And.Contain(c => c.Id == 2 && c.Nome == "Contato 2");

        _contatoRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }



}