using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WantApp.Endpoints.Seguranca;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Metodos => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(LoginRequest loginRequest, IConfiguration configuracao, UserManager<IdentityUser> userManager)
    {
        var usuario = userManager.FindByEmailAsync(loginRequest.Email).Result;

        if (usuario == null)
            return Results.BadRequest();

        if (!userManager.CheckPasswordAsync(usuario, loginRequest.Senha).Result)
            return Results.BadRequest();

        var claims = userManager.GetClaimsAsync(usuario).Result;
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email),  
            new Claim(ClaimTypes.NameIdentifier, usuario.Id)
        });
        subject.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(configuracao["JwtBearerTokenSettings:SecretKey"]);
        var tokenDescriptografar = new SecurityTokenDescriptor
        {
            Subject = subject,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = configuracao["JwtBearerTokenSettings:Audience"],
            Issuer = configuracao["JwtBearerTokenSettings:Issuer"],
            Expires = DateTime.UtcNow.AddMinutes(30)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptografar);
        return Results.Ok(new { token = tokenHandler.WriteToken(token)} );
    }
}
