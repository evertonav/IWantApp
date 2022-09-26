using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Empregados;

public class EmpregadoPost
{
    public static string Template => "/empregados";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(EmpregadoRequest empregadoRequest, UserManager<IdentityUser> userManager)
    {
        IdentityUser usuario = new IdentityUser() 
            { 
                UserName = empregadoRequest.Email, 
                Email = empregadoRequest.Email 
            };        

        var resultadoCriacao = userManager.CreateAsync(usuario, empregadoRequest.Senha).Result;

        if (!resultadoCriacao.Succeeded)
            return Results.ValidationProblem(resultadoCriacao.Errors.ConverterParaProblemaDetalhado());

        List<Claim> claims = new List<Claim>()
        {
            new Claim("CodigoEmpregado", empregadoRequest.CodigoEmpregado),
            new Claim("Nome", empregadoRequest.Nome)
        };

        var claimResultado = userManager.AddClaimsAsync(usuario, claims).Result;

        if (!claimResultado.Succeeded)
            return Results.ValidationProblem(claimResultado.Errors.ConverterParaProblemaDetalhado());        

        return Results.Created($"/empregados/{usuario.Id}", usuario.Id);
    }
}
