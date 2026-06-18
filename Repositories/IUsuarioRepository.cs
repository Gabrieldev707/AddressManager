using AddressManager.Models;

namespace AddressManager.Repositories;

/// <summary>
/// Operações de acesso a dados para a entidade <see cref="Usuario"/>.
/// </summary>
public interface IUsuarioRepository
{
    /// <summary>Busca um usuário pelo seu login. Retorna null se não existir.</summary>
    Task<Usuario?> ObterPorLoginAsync(string login);

    /// <summary>Indica se já existe ao menos um usuário cadastrado.</summary>
    Task<bool> ExisteAlgumAsync();

    /// <summary>Adiciona um novo usuário e persiste as alterações.</summary>
    Task AdicionarAsync(Usuario usuario);
}
