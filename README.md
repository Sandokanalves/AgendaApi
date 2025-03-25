# Documentação da API AgendaAPI

## Visão Geral

A **AgendaAPI** é uma aplicação backend desenvolvida utilizando a tecnologia **.NET 7.0** com uma arquitetura limpa, o padrão de **DTOs (Data Transfer Objects)** e autenticação por **JWT** (JSON Web Token). A API foi projetada para gerenciar uma agenda de contatos, permitindo funcionalidades como listar, criar, atualizar e excluir contatos. O banco de dados utilizado é o **SQL Server**.

## Tecnologias Utilizadas

- **.NET 7.0**: Framework para desenvolvimento da API RESTful.
- **SQL Server**: Banco de dados relacional para persistência dos dados.
- **Arquitetura Limpa**: Estrutura modularizada e separação de responsabilidades, com camadas bem definidas (Application, Domain, Infrastructure, Web).
- **DTOs (Data Transfer Objects)**: Para garantir a comunicação eficiente entre as camadas.
- **JWT (JSON Web Token)**: Para autenticação segura e gerenciamento de sessões.
- **Unit Testing com xUnit**: Framework de testes unitários para validar a lógica de negócios.

## Estrutura do Projeto

### 1. **Camadas do Projeto**

- **Agenda.Api**: Contém as controllers e configurações para expor a API.
- **Agenda.Application**: Contém a lógica de negócios, regras de validação e os casos de uso da aplicação.
- **Agenda.Domain**: Representa a camada de entidades, onde as classes de domínio como `Contato` estão localizadas.
- **Agenda.Infrastructure**: Contém as implementações específicas de infraestrutura, como acesso ao banco de dados (via Entity Framework).
- **Agenda.Tests**: Contém os testes unitários da aplicação, utilizando xUnit.

### 2. **Arquitetura Limpa**

A Arquitetura Limpa (Clean Architecture) é baseada no princípio da separação de responsabilidades, onde a lógica de negócios não depende de frameworks, permitindo uma fácil manutenção e testabilidade. As camadas mais externas (como controllers e banco de dados) dependem das camadas internas (como lógica de negócios e modelos).


## Detalhamento dos DTOs

### 1. **ContatoViewModel (DTO)**
### 2. **ContatoDetailsViewModel (DTO)**
### 3. **CreateContatoInputModel (DTO)**
### 4. **UpdateContatoInputModel (DTO)**

## Autenticação com JWT

A autenticação é realizada utilizando **JWT (JSON Web Token)**. O fluxo básico é o seguinte:

1. O usuário envia seu e-mail e senha através de uma requisição POST.
2. Se as credenciais forem válidas, um JWT é gerado e retornado ao cliente.
3. O JWT é armazenado pelo cliente e enviado em todas as requisições subsequentes no cabeçalho `Authorization` com o prefixo `Bearer`.

## Como Rodar o Projeto

### Pré-requisitos

- **.NET 7.0**: A versão mais recente do framework .NET.
- **SQL Server**: O banco de dados relacional para persistência dos dados.
- **Visual Studio 2022** (ou qualquer IDE com suporte a .NET 7.0).
- **Postman ou Swagger**: Para testar as rotas da API.
Obs: No Program.cs está configurado para usar o Swagger como documentação.

### Passos para rodar o projeto

1. **Clone o repositório**:

```bash
git clone <[URL_DO_REPOSITORIO](https://github.com/Sandokanalves/AgendaApi.git)>
```

2. **Restaure os pacotes do projeto**:

No diretório da solução:

```bash
dotnet restore
```

3. **Configuração do Banco de Dados**:

- Verifique se o SQL Server está rodando e a string de conexão está configurada corretamente no arquivo `appsettings.json`.
- Execute as migrations para criar as tabelas necessárias no banco de dados.

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4. **Rodar a aplicação**:

Para iniciar o servidor da API:

```bash
dotnet run
```

O servidor estará disponível em `https://localhost:7067`.

5. **Testar as rotas**:

Utilize o **Postman** ou qualquer cliente HTTP para testar as rotas da API:

- **POST /auth/login**: Autenticação de usuário.
- **GET /api/contato**: Listar contatos.
- **POST /api/contato**: Criar novo contato.
- **PUT /api/contato/{id}**: Atualizar um contato existente.
- **DELETE /api/contato/{id}**: Deletar um contato.

6. **Executar os Testes Unitários**:

Na pasta de testes, rode os testes utilizando o xUnit:

```bash
dotnet test
```

### Configuração de Testes

A pasta **Agenda.Tests** contém testes unitários para validar a lógica de negócios. A configuração é feita utilizando o **xUnit** como framework de testes.

### Conclusão

Essa API AgendaAPI foi projetada de forma modular utilizando as melhores práticas de desenvolvimento, incluindo arquitetura limpa, autenticação JWT, e testabilidade. O uso de DTOs e a separação de responsabilidades tornam a aplicação escalável e fácil de manter.

Se você seguir os passos descritos acima, será capaz de rodar o projeto localmente e realizar os testes necessários para garantir que a API funcione corretamente.

Desenvolvido por Sandokan Alves de Oliveira
