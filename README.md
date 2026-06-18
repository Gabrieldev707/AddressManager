# AddressManager

Aplicação web **ASP.NET Core MVC (.NET 10)** para gerenciamento de endereços, com
autenticação manual, CRUD por usuário, integração com a API ViaCEP e exportação CSV.

## Stack

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core 10 (SQL Server / LocalDB)
- Autenticação por cookie (sem ASP.NET Core Identity)
- BCrypt para hash de senhas
- Bootstrap 5, HTML, CSS e JavaScript

## Funcionalidades

1. **Login** com autenticação manual, validação de credenciais e sessão por cookie.
2. **CRUD de endereços** (listar, criar, editar e excluir) restrito ao usuário logado.
3. **Integração com ViaCEP**: ao informar o CEP, os campos são preenchidos via `fetch`.
4. **Exportação CSV** dos endereços do usuário logado.
5. **Scripts SQL** de criação das tabelas (alternativa às migrations).

## Pré-requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/)
- SQL Server **LocalDB** (instância `MSSQLLocalDB`)

## Como executar

```powershell
dotnet tool restore   # restaura o dotnet-ef (opcional)
dotnet run
```

A aplicação cria o banco e aplica as migrations automaticamente ao iniciar, além de
semear o usuário administrador padrão. Acesse a URL exibida no console (por ex.
`https://localhost:7218`).

### Credenciais padrão

| Usuário | Senha |
| --- | --- |
| `admin` | `admin123` |

## Banco de dados

A connection string está em `appsettings.json` (`DefaultConnection`) apontando para o
LocalDB. Há duas formas de provisionar o schema:

- **Automática (padrão):** o `Database.Migrate()` no `Program.cs` cria as tabelas na
  primeira execução.
- **Manual (SQL):** scripts em [`Database/`](Database/README.md) para criar as tabelas
  e o usuário admin via `sqlcmd`.

### Tabelas

- **Usuarios**: `Id`, `Nome`, `Usuario`, `Senha` (hash BCrypt)
- **Enderecos**: `Id`, `CEP`, `Logradouro`, `Complemento` (nullable), `Bairro`,
  `Cidade`, `UF`, `Numero`, `UsuarioId` (FK)

## Estrutura

```
Controllers/    AccountController, EnderecosController, HomeController
Models/         Usuario, Endereco
ViewModels/     LoginViewModel, EnderecoViewModel
Data/           AppDbContext, DbSeeder
Repositories/   IUsuarioRepository, IEnderecoRepository (+ implementações)
Services/       IPasswordHasher (BCrypt), IEnderecoCsvExporter
Views/          Account, Enderecos, Home, Shared
Migrations/     migrations do EF Core
Database/       scripts SQL
wwwroot/js/     viacep.js
```
