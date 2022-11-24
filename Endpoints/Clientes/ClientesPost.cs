using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Endpoints.Clientes;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using static System.Net.WebRequestMethods;
using WantApp.Servicos;

namespace WantApp.Endpoints.Clientes;

public class ClientesPost
{
    public static string Template => "/clientes";
    public static string[] Metodos => new string[] {HttpMethod.Post.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(ClienteRequest clienteRequest, HttpContext http, UsuarioServico usuarioServico)
    {
        var usuarioId = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        List<Claim> claims = new List<Claim>()
        {
            new Claim("Cpf", clienteRequest.CPF),
            new Claim("Nome", clienteRequest.Nome),
        };

        (IdentityResult resultado, string idUsuario) resultadoCriacao = await usuarioServico.CriarAsync(clienteRequest.Senha, clienteRequest.Email, claims);

        if (!resultadoCriacao.resultado.Succeeded)
            return Results.ValidationProblem(resultadoCriacao.resultado.Errors.ConverterParaProblemaDetalhado());        

        return Results.Created($"/clientes/{resultadoCriacao.idUsuario}", resultadoCriacao.idUsuario);
    }
}
