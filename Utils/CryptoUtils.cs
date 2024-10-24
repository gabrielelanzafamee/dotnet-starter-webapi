using System.Text;
using System.Security.Cryptography;

namespace App.Utils;

public class CryptoUtils
{
    private readonly static string _salt = "NaCl";
    private readonly static string _secret = "secret";

    public static string sha256(string value)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(_salt + _secret + value));

            foreach (byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }

    public static bool VerifyPassword(string password, string hash) {
        return CryptoUtils.sha256(password) == hash;
    }
}