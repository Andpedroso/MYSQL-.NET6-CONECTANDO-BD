## MySql com .NET 6
---
#### Esse projeto demonstra como configurar uma webapi do .NET 6 com o MySql com o C#.
---
## Requisitos
---
- Instalar o MySql com o servidor e gerenciador
- Instalar o Visual Studio Code ou Visual Studio
- Instalar .NET 6 com o Runtime para ASP.NETCORE
- Instalar a extensão do C# da Microsoft para o VSCode
---
## MySql
---
#### Criar banco de dados com a tabela produtos
---
> MySql
```bash
CREATE DATABASE megavendas;

USE megavendas;

CREATE TABLE produtos(
    Id INT PRIMARY KEY AUTO_INCREMENT NOT NULL;
    Titulo VARCHAR(150) UNIQUE NOT NULL;
    Descricao VARCHAR(250) NOT NULL;
);

INSERT INTO produtos(Titulo, Descricao)
VALUES ("Samsung Galaxy A03", "Perfeito.")

SELECT * FROM produtos;
```
---
## Criar projeto .NET
---
### Criar pasta e criar o projeto nela
---
> Terminal
```bash
dotnet new webapi
```
---
## Configurando API
---
### Pacotes
---
- Microsoft.EntityFrameworkCore.Design Version=6.0.1
- Microsoft.EntityFrameworkCore.Tools Version=6.0.1
- Pomelo.EntityFrameworkCore.MySql Version=6.0.1
---
### Pasta Models
> Produtos
```bash
namespace api01.Models
{
    public class Produto
    {
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }
    }
}
```
### Pasta Context
> AppDbContext
```bash
using Microsoft.EntityFrameworkCore;
using api01.Models;

namespace api01.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Produto> Produtos { get; set; }
    }
}
```
### Pasta Repositories
> ProdutoRepository
```bash
using api01.Context;
using api01.Models;

namespace api01.Repositories
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context){
            _context = context;
        }

        public List<Produto> Listar(){
            return _context.Produtos.ToList();
        }
    }
}
```
### Pasta Controller
> ProdutoController
### Criar controlador API Vazio
```bash
using Microsoft.AspNetCore.Mvc;
using api01.Repositories;

namespace api01.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(ProdutoRepository produtoRepository){
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public IActionResult Listar(){
            try {
                return Ok(_produtoRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
```
---
## Configurar string de conexão
---
> appsettings.json
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=megavendas;user=root;password=server"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
---
## Program
---
> Program.cs
```bash
using api01.Context;
using api01.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextPool<AppDbContext>(options => options.UseMySql(
    mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddScoped<AppDbContext, AppDbContext>();

builder.Services.AddTransient<ProdutoRepository, ProdutoRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```
---
## Considerações finais
---
### Para rodar o projeto, adicionar o localhost com a port demonstrada no dotnet run.
### Adicionar /swagger/ para o swagger
### Adicionar /api/produto para o json
---
## Créditos
---
### André Moura Pedroso
#### Web, Mobile e Games.
#### SENAI - Full-Stack
#### Faculdade Descomplica - Análise e Desenvolvimento de Sistemas