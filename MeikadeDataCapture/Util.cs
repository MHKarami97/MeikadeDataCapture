using System.Security.Cryptography;
using System.Text;

namespace MeikadeDataCapture;

public static class Util
{
    public static string HashSha256Password(string passwordSalt, string password)
    {
        var data = passwordSalt + password + passwordSalt;
        var enc = Encoding.UTF8;
        var hasher = SHA256.Create();

        var result = hasher.ComputeHash(enc.GetBytes(data));
        
        var sb = new StringBuilder();
        
        foreach (var b in result)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }
}