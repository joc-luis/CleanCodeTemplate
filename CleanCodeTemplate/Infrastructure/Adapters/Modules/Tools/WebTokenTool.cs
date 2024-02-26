using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CleanCodeTemplate.Business.Dto.Account;
using CleanCodeTemplate.Business.Dto.Permissions;
using CleanCodeTemplate.Business.Exceptions.Http;
using CleanCodeTemplate.Business.Modules.Tools;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CleanCodeTemplate.Infrastructure.Adapters.Modules.Tools;

public class WebTokenTool : IWebTokenTool
{
    public SessionAccountDto SessionAccount { get; private set; } = new();
    private readonly IDictionary<string, string> _env;

    public WebTokenTool(IDictionary<string, string> env)
    {
        _env = env;
    }

    public Task<string> GenerateTokenAsync(SessionAccountDto sessionAccountDto, int days, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Sid, sessionAccountDto.Id.ToString()),
                    new(ClaimTypes.Role, sessionAccountDto.RoleId.ToString())
                }),
                Expires = DateTime.Now.AddDays(days),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_env["JWT_KEY"])),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }, ct);
    }

    public Task SetSessionAsync(string token, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedException();
            }
            
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_env["JWT_KEY"])),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var principal = handler.ValidateToken(token.Replace("Bearer", "").Trim(), validations, out var tokenSecure);

            if (tokenSecure.ValidTo < DateTime.Now)
            {
                throw new UnauthorizedException();
            }

            SessionAccount = new SessionAccountDto()
            {
                Id = Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value),
                RoleId = Guid.Parse(principal.Claims.First(c => c.Type == ClaimTypes.Role).Value)
            };
        }, ct);
    }
}