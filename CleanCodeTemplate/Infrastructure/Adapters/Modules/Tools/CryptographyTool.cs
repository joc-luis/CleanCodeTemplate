using System.Security.Cryptography;
using System.Text;
using CleanCodeTemplate.Business.Modules.Tools;
using Org.BouncyCastle.Crypto.Generators;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules.Tools;

public class CryptographyTool : ICryptographyTool
{
    private readonly Random _random = new Random();
    public Task<string> HashAsync(string raw)
    {
        return Task.Run(() => BCrypt.Net.BCrypt.EnhancedHashPassword(raw));
    }

    public Task<bool> VerifyHashAsync(string hashed, string raw)
    {
        return Task.Run(() => BCrypt.Net.BCrypt.EnhancedVerify(raw, hashed));
    }

    public Task<string> RandomStringAsync(int length = 10)
    {
        return Task.Run(() =>
        {
            string source = "0123456789abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            var builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                builder.Append(source[_random.Next(0, source.Length - 1)].ToString());
            }

            return builder.ToString();
        });
    }
}