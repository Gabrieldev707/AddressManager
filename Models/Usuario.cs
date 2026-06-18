using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressManager.Models;

/// <summary>
/// Representa um usuário do sistema. A senha é sempre armazenada como hash.
/// </summary>
public class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    /// <summary>Nome de login do usuário (único). Mapeado para a coluna "Usuario".</summary>
    [Required]
    [StringLength(50)]
    [Column("Usuario")]
    public string Login { get; set; } = string.Empty;

    /// <summary>Hash BCrypt da senha do usuário.</summary>
    [Required]
    [StringLength(255)]
    public string Senha { get; set; } = string.Empty;

    /// <summary>Endereços cadastrados por este usuário.</summary>
    public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
}
