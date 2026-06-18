using AddressManager.Data;
using AddressManager.Models;
using Microsoft.EntityFrameworkCore;

namespace AddressManager.Repositories;

/// <inheritdoc cref="IEnderecoRepository"/>
public class EnderecoRepository : IEnderecoRepository
{
    private readonly AppDbContext _context;

    public EnderecoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Endereco>> ListarPorUsuarioAsync(int usuarioId) =>
        await _context.Enderecos
            .AsNoTracking()
            .Where(e => e.UsuarioId == usuarioId)
            .OrderBy(e => e.Logradouro)
            .ToListAsync();

    public Task<Endereco?> ObterPorIdAsync(int id, int usuarioId) =>
        _context.Enderecos.FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == usuarioId);

    public async Task AdicionarAsync(Endereco endereco)
    {
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Endereco endereco)
    {
        _context.Enderecos.Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Endereco endereco)
    {
        _context.Enderecos.Remove(endereco);
        await _context.SaveChangesAsync();
    }
}
