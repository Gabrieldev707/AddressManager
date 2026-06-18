using System.ComponentModel.DataAnnotations;

namespace AddressManager.Models;

/// <summary>
/// Representa um endereço pertencente a um usuário.
/// </summary>
public class Endereco
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [StringLength(9)]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "O logradouro é obrigatório.")]
    [StringLength(150)]
    public string Logradouro { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Complemento { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório.")]
    [StringLength(100)]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    [StringLength(100)]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "A UF é obrigatória.")]
    [StringLength(2)]
    public string UF { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório.")]
    [StringLength(20)]
    public string Numero { get; set; } = string.Empty;

    /// <summary>Chave estrangeira para o usuário dono do endereço.</summary>
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }
}
