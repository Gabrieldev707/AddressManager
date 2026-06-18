using System.Security.Claims;
using AddressManager.Models;
using AddressManager.Repositories;
using AddressManager.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressManager.Controllers;

/// <summary>
/// CRUD dos endereços do usuário logado.
/// </summary>
[Authorize]
public class EnderecosController : Controller
{
    private readonly IEnderecoRepository _enderecos;

    public EnderecosController(IEnderecoRepository enderecos)
    {
        _enderecos = enderecos;
    }

    /// <summary>Id do usuário autenticado, extraído das claims do cookie.</summary>
    private int UsuarioId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // GET: /Enderecos
    public async Task<IActionResult> Index()
    {
        var enderecos = await _enderecos.ListarPorUsuarioAsync(UsuarioId);
        return View(enderecos);
    }

    // GET: /Enderecos/Create
    [HttpGet]
    public IActionResult Create() => View(new EnderecoViewModel());

    // POST: /Enderecos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EnderecoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var endereco = new Endereco { UsuarioId = UsuarioId };
        AplicarViewModel(model, endereco);

        await _enderecos.AdicionarAsync(endereco);
        TempData["Sucesso"] = "Endereço cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Enderecos/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var endereco = await _enderecos.ObterPorIdAsync(id, UsuarioId);
        if (endereco is null)
        {
            return NotFound();
        }

        return View(MapearParaViewModel(endereco));
    }

    // POST: /Enderecos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EnderecoViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var endereco = await _enderecos.ObterPorIdAsync(id, UsuarioId);
        if (endereco is null)
        {
            return NotFound();
        }

        AplicarViewModel(model, endereco);

        await _enderecos.AtualizarAsync(endereco);
        TempData["Sucesso"] = "Endereço atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Enderecos/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var endereco = await _enderecos.ObterPorIdAsync(id, UsuarioId);
        if (endereco is null)
        {
            return NotFound();
        }

        return View(endereco);
    }

    // POST: /Enderecos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var endereco = await _enderecos.ObterPorIdAsync(id, UsuarioId);
        if (endereco is null)
        {
            return NotFound();
        }

        await _enderecos.RemoverAsync(endereco);
        TempData["Sucesso"] = "Endereço excluído com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    private static void AplicarViewModel(EnderecoViewModel model, Endereco endereco)
    {
        endereco.CEP = model.CEP;
        endereco.Logradouro = model.Logradouro;
        endereco.Numero = model.Numero;
        endereco.Complemento = model.Complemento;
        endereco.Bairro = model.Bairro;
        endereco.Cidade = model.Cidade;
        endereco.UF = model.UF.ToUpperInvariant();
    }

    private static EnderecoViewModel MapearParaViewModel(Endereco endereco) => new()
    {
        Id = endereco.Id,
        CEP = endereco.CEP,
        Logradouro = endereco.Logradouro,
        Numero = endereco.Numero,
        Complemento = endereco.Complemento,
        Bairro = endereco.Bairro,
        Cidade = endereco.Cidade,
        UF = endereco.UF
    };
}
