using AddressManager.Data;
using AddressManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressManager.Repositories;

/// <inheritdoc cref="IUsuarioRepository"/>
public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Usuario?> ObterPorLoginAsync(string login) =>
        _context.Usuarios.FirstOrDefaultAsync(u => u.Login == login);

    public Task<bool> ExisteAlgumAsync() => _context.Usuarios.AnyAsync();

    public async Task AdicionarAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}
