using System.Text;
using AddressManager.Models;

namespace AddressManager.Services;

/// <inheritdoc cref="IEnderecoCsvExporter"/>
public class EnderecoCsvExporter : IEnderecoCsvExporter
{
    // Ponto e vírgula é o separador esperado pelo Excel em português (pt-BR).
    private const char Separador = ';';

    private static readonly string[] Cabecalho =
        ["CEP", "Logradouro", "Numero", "Complemento", "Bairro", "Cidade", "UF"];

    public byte[] Exportar(IEnumerable<Endereco> enderecos)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(Separador, Cabecalho));

        foreach (var e in enderecos)
        {
            sb.AppendLine(string.Join(Separador,
                Escapar(e.CEP),
                Escapar(e.Logradouro),
                Escapar(e.Numero),
                Escapar(e.Complemento),
                Escapar(e.Bairro),
                Escapar(e.Cidade),
                Escapar(e.UF)));
        }

        // BOM UTF-8 garante a leitura correta de acentos no Excel.
        var bom = Encoding.UTF8.GetPreamble();
        var conteudo = Encoding.UTF8.GetBytes(sb.ToString());
        return [.. bom, .. conteudo];
    }

    /// <summary>Aplica as regras de escape de CSV (RFC 4180) ao valor.</summary>
    private static string Escapar(string? valor)
    {
        valor ??= string.Empty;

        if (valor.Contains(Separador) || valor.Contains('"') || valor.Contains('\n') || valor.Contains('\r'))
        {
            valor = "\"" + valor.Replace("\"", "\"\"") + "\"";
        }

        return valor;
    }
}
