/* =============================================================================
   AddressManager - Criação das tabelas Usuarios e Enderecos (SQL Server)
   -----------------------------------------------------------------------------
   Este script é uma alternativa manual às migrations do Entity Framework Core.
   O schema reproduz exatamente o modelo gerado pela migration InitialCreate.

   Uso (sqlcmd):
       sqlcmd -S "(localdb)\MSSQLLocalDB" -i 01_CriarTabelas.sql
   ============================================================================= */

/* Cria o banco de dados, caso ainda não exista. */
IF DB_ID(N'AddressManagerDb') IS NULL
BEGIN
    CREATE DATABASE [AddressManagerDb];
END;
GO

USE [AddressManagerDb];
GO

/* -------------------------------------------------------------------------
   Tabela: Usuarios
   Colunas: Id, Nome, Usuario, Senha
   ------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.Usuarios', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Usuarios]
    (
        [Id]      INT            IDENTITY(1, 1) NOT NULL,
        [Nome]    NVARCHAR(100)  NOT NULL,
        [Usuario] NVARCHAR(50)   NOT NULL,
        [Senha]   NVARCHAR(255)  NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );

    /* O login (coluna Usuario) deve ser único. */
    CREATE UNIQUE INDEX [IX_Usuarios_Usuario]
        ON [dbo].[Usuarios] ([Usuario]);
END;
GO

/* -------------------------------------------------------------------------
   Tabela: Enderecos
   Colunas: Id, CEP, Logradouro, Complemento (nullable), Bairro, Cidade,
            UF, Numero, UsuarioId (FK -> Usuarios.Id)
   ------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.Enderecos', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Enderecos]
    (
        [Id]          INT            IDENTITY(1, 1) NOT NULL,
        [CEP]         NVARCHAR(9)    NOT NULL,
        [Logradouro]  NVARCHAR(150)  NOT NULL,
        [Complemento] NVARCHAR(100)  NULL,
        [Bairro]      NVARCHAR(100)  NOT NULL,
        [Cidade]      NVARCHAR(100)  NOT NULL,
        [UF]          NVARCHAR(2)    NOT NULL,
        [Numero]      NVARCHAR(20)   NOT NULL,
        [UsuarioId]   INT            NOT NULL,
        CONSTRAINT [PK_Enderecos] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Enderecos_Usuarios_UsuarioId] FOREIGN KEY ([UsuarioId])
            REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE
    );

    /* Índice para acelerar a busca dos endereços por usuário. */
    CREATE INDEX [IX_Enderecos_UsuarioId]
        ON [dbo].[Enderecos] ([UsuarioId]);
END;
GO

/* -------------------------------------------------------------------------
   Histórico de migrations do EF Core.
   A aplicação executa Database.Migrate() ao iniciar. Registrar a migration
   InitialCreate aqui evita que o EF tente recriar as tabelas quando o banco
   é provisionado por este script. (Remova esta seção se não for usar o EF.)
   ------------------------------------------------------------------------- */
IF OBJECT_ID(N'dbo.__EFMigrationsHistory', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory]
    (
        [MigrationId]    NVARCHAR(150) NOT NULL,
        [ProductVersion] NVARCHAR(32)  NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[__EFMigrationsHistory]
               WHERE [MigrationId] = N'20260618231019_InitialCreate')
BEGIN
    INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260618231019_InitialCreate', N'10.0.0');
END;
GO
