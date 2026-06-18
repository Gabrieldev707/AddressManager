/* =============================================================================
   AddressManager - Usuário administrador padrão
   -----------------------------------------------------------------------------
   Insere o usuário inicial para acesso ao sistema, caso ainda não exista.

       Login: admin
       Senha: admin123

   A senha é armazenada como hash BCrypt (o mesmo algoritmo usado pela
   aplicação). Qualquer hash BCrypt válido de "admin123" é aceito no login.
   ============================================================================= */

USE [AddressManagerDb];
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Usuarios] WHERE [Usuario] = N'admin')
BEGIN
    INSERT INTO [dbo].[Usuarios] ([Nome], [Usuario], [Senha])
    VALUES (N'Administrador', N'admin', N'$2a$11$MYyn4ca/6YUxEYELpTJOWeesiAyHYe514UpElf9/tvYs51QuuIVve');
END;
GO
