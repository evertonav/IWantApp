using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using WantApp.Servicos.Usuarios;
using static System.Net.WebRequestMethods;

namespace WantApp.Endpoints.Empregados;

public class EmpregadoPost
{
    public static string Template => "/empregados";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(EmpregadoRequest empregadoRequest, 
        HttpContext http, UsuarioServico usuarioServico)//, UserManager<IdentityUser> userManager)
    {       
        var usuarioId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        List<Claim> claims = new List<Claim>()
        {
            new Claim("CodigoEmpregado", empregadoRequest.CodigoEmpregado),
            new Claim("Nome", empregadoRequest.Nome),
            new Claim("CriadoPor", usuarioId)
        };

        (IdentityResult resultado, string idUsuario) resultadoCriacao = await usuarioServico.CriarAsync(empregadoRequest.Senha, 
                                                                                                        empregadoRequest.Email, 
                                                                                                        claims);        

        if (!resultadoCriacao.resultado.Succeeded)
            return Results.ValidationProblem(resultadoCriacao.resultado.Errors.ConverterParaProblemaDetalhado());                      

        return Results.Created($"/empregados/{resultadoCriacao.idUsuario}", resultadoCriacao.idUsuario);
    }
}
