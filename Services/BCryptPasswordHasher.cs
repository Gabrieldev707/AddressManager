namespace AddressManager.Services;

/// <summary>
/// Implementação de <see cref="IPasswordHasher"/> utilizando o algoritmo BCrypt,
/// que aplica salt automático e fator de trabalho configurável.
/// </summary>
public class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);

    public bool Verify(string senha, string hash) => BCrypt.Net.BCrypt.Verify(senha, hash);
}
