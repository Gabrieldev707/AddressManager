using System.ComponentModel.DataAnnotations;

namespace AddressManager.ViewModels;

/// <summary>
/// Dados de um endereço usados nos formulários de criação e edição.
/// Não expõe o UsuarioId, que é sempre definido a partir do usuário logado.
/// </summary>
public class EnderecoViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    [RegularExpression(@"\d{5}-?\d{3}", ErrorMessage = "Informe um CEP válido (ex.: 01001-000).")]
    [Display(Name = "CEP")]
    public string CEP { get; set; } = string.Empty;

    [Required(ErrorMessage = "O logradouro é obrigatório.")]
    [StringLength(150)]
    [Display(Name = "Logradouro")]
    public string Logradouro { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório.")]
    [StringLength(20)]
    [Display(Name = "Número")]
    public string Numero { get; set; } = string.Empty;

    [StringLength(100)]
    [Display(Name = "Complemento")]
    public string? Complemento { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório.")]
    [StringLength(100)]
    [Display(Name = "Bairro")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    [StringLength(100)]
    [Display(Name = "Cidade")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "A UF é obrigatória.")]
    [RegularExpression("[A-Za-z]{2}", ErrorMessage = "A UF deve ter 2 letras.")]
    [Display(Name = "UF")]
    public string UF { get; set; } = string.Empty;
}
