using System.Security.Cryptography;
using System.Text;
using Condominio.Domain.Interfaces.Util;

namespace Condominio.Util.Cryptography;

public class Sha256Cryptograph : ICryptograph
{
    private readonly HashAlgorithm _algoritmo;

    public Sha256Cryptograph()
    {
        _algoritmo = SHA256.Create();
    }

    public string EncryptPassword(string password)
    {
        var encodedValue = Encoding.UTF8.GetBytes(password);
        var senhaEncriptada = _algoritmo.ComputeHash(encodedValue);

        var sb = new StringBuilder();
        foreach (var caracter in senhaEncriptada) sb.Append(caracter.ToString("X2"));
        return sb.ToString();
    }

    public bool VerifyPassword(string password, string encryptedPassword)
    {
        var encryptedPasswordVar = _algoritmo.ComputeHash(Encoding.UTF8.GetBytes(password));

        var sb = new StringBuilder();
        foreach (var caractere in encryptedPasswordVar) sb.Append(caractere.ToString("X2"));

        return sb.ToString() == encryptedPassword;
    }
}