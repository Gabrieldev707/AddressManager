using AddressManager.Models;
using AddressManager.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;

namespace AddressManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEnderecoRepository _enderecos;

        public HomeController(IEnderecoRepository enderecos)
        {
            _enderecos = enderecos;
        }

        private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index()
        {
            var enderecos = await _enderecos.ListarPorUsuarioAsync(UsuarioId);
            return View(enderecos);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
