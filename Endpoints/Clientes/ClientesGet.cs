using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Endpoints.Clientes;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using static System.Net.WebRequestMethods;
using WantApp.Servicos;
using WantApp.Servicos.Usuarios;

namespace WantApp.Endpoints.Clientes;

public class ClientesGet
{
    public static string Template => "/clientes";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static IResult Action(HttpContext http)
    {
        InformacoesTokenServico informacoesTokenServico = new InformacoesTokenServico(http);
        
        return Results.Ok(new ClienteResponse(informacoesTokenServico.UsuarioLogado(), informacoesTokenServico.NomeUsuario()));     
    }
}
