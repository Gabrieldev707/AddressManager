using System.Security.Claims;
using AddressManager.Repositories;
using AddressManager.Services;
using AddressManager.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressManager.Controllers;

/// <summary>
/// Responsável pela autenticação manual de usuários (login e logout).
/// </summary>
public class AccountController : Controller
{
    private readonly IUsuarioRepository _usuarios;
    private readonly IPasswordHasher _hasher;

    public AccountController(IUsuarioRepository usuarios, IPasswordHasher hasher)
    {
        _usuarios = usuarios;
        _hasher = hasher;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        // Usuário já autenticado não precisa ver a tela de login.
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectParaArea();
        }

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var usuario = await _usuarios.ObterPorLoginAsync(model.Login);

        // Mensagem genérica para não revelar se o usuário existe.
        if (usuario is null || !_hasher.Verify(model.Senha, usuario.Senha))
        {
            ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Nome),
            new("Login", usuario.Login)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = true });

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return RedirectParaArea();
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    private IActionResult RedirectParaArea() => RedirectToAction("Index", "Enderecos");
}
