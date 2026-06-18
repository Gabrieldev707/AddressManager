namespace AddressManager.Services;

/// <summary>
/// Abstração para geração e verificação de hash de senhas.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>Gera o hash de uma senha em texto puro.</summary>
    string Hash(string senha);

    /// <summary>Verifica se a senha em texto puro corresponde ao hash informado.</summary>
    bool Verify(string senha, string hash);
}
