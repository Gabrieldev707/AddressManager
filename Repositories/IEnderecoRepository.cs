using AddressManager.Models;

namespace AddressManager.Repositories;

/// <summary>
/// Operações de acesso a dados para a entidade <see cref="Endereco"/>.
/// Todas as consultas são restritas ao usuário dono dos endereços.
/// </summary>
public interface IEnderecoRepository
{
    /// <summary>Lista todos os endereços de um usuário, ordenados por logradouro.</summary>
    Task<IReadOnlyList<Endereco>> ListarPorUsuarioAsync(int usuarioId);

    /// <summary>Obtém um endereço específico garantindo que pertence ao usuário informado.</summary>
    Task<Endereco?> ObterPorIdAsync(int id, int usuarioId);

    Task AdicionarAsync(Endereco endereco);

    Task AtualizarAsync(Endereco endereco);

    Task RemoverAsync(Endereco endereco);
}
