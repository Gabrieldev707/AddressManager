using System.ComponentModel.DataAnnotations;

namespace AddressManager.ViewModels;

/// <summary>
/// Dados informados na tela de login.
/// </summary>
public class LoginViewModel
{
    [Required(ErrorMessage = "Informe o usuário.")]
    [Display(Name = "Usuário")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a senha.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Senha { get; set; } = string.Empty;

    /// <summary>URL para redirecionamento após o login bem-sucedido.</summary>
    public string? ReturnUrl { get; set; }
}
