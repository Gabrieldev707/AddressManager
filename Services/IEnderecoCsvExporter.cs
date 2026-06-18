using AddressManager.Models;

namespace AddressManager.Services;

/// <summary>
/// Gera o conteúdo CSV a partir de uma lista de endereços.
/// </summary>
public interface IEnderecoCsvExporter
{
    /// <summary>Retorna os bytes de um arquivo CSV (UTF-8 com BOM) com os endereços informados.</summary>
    byte[] Exportar(IEnumerable<Endereco> enderecos);
}
