using Authentication.Application.Users.Commands.Dto;
using Authentication.Core.UserAggregate;
using Authentication.Infrastructure.Sections;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Authentication.Application.Services;

public class AuthenticationService(UserManager<User> _userManager, IOptions<Identity> identity) : IAuthenticationService
{
    private readonly Identity _identity = identity.Value!;

    protected static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    protected async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim("UserId", user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    protected string CodeToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_identity.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _identity.Issuer,
            Audience = _identity.ValidOn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_identity.ExpiratesIn),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    protected AuthenticationDto GetToken(string encodedToken, User user, IEnumerable<Claim> claims)
    {
        return new AuthenticationDto
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_identity.ExpiratesIn).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }

    public async Task<AuthenticationDto> GenerateJwtToken(string cpf)
    {
        var user = await _userManager.FindByNameAsync(cpf);
        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await GetUserClaims(claims, user);
        var encodedToken = CodeToken(identityClaims);

        return GetToken(encodedToken, user, claims);
    }
}