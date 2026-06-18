using AddressManager.Models;
using AddressManager.Repositories;
using AddressManager.Services;

namespace AddressManager.Data;

/// <summary>
/// Popula dados iniciais necessários para o funcionamento da aplicação.
/// </summary>
public static class DbSeeder
{
    public const string UsuarioPadrao = "admin";
    public const string SenhaPadrao = "admin123";

    /// <summary>
    /// Cria um usuário administrador padrão caso ainda não exista nenhum usuário.
    /// </summary>
    public static async Task SeedAsync(IUsuarioRepository usuarios, IPasswordHasher hasher)
    {
        if (await usuarios.ExisteAlgumAsync())
        {
            return;
        }

        var admin = new Usuario
        {
            Nome = "Administrador",
            Login = UsuarioPadrao,
            Senha = hasher.Hash(SenhaPadrao)
        };

        await usuarios.AdicionarAsync(admin);
    }
}
