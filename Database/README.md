# Scripts SQL — AddressManager

Scripts de criação do banco de dados em SQL Server. São uma **alternativa manual**
às migrations do Entity Framework Core (a aplicação cria o banco automaticamente ao
iniciar). O schema é idêntico ao gerado pela migration `InitialCreate`.

## Arquivos

| Arquivo | Descrição |
| --- | --- |
| `01_CriarTabelas.sql` | Cria o banco `AddressManagerDb` e as tabelas `Usuarios` e `Enderecos` (com índices, FK e registro da migration do EF). |
| `02_InserirUsuarioAdmin.sql` | Insere o usuário administrador padrão (`admin` / `admin123`). |

## Como executar (sqlcmd)

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i 01_CriarTabelas.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i 02_InserirUsuarioAdmin.sql
```

Os scripts são idempotentes: podem ser executados mais de uma vez sem erro.
