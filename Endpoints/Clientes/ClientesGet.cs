using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Endpoints.Clientes;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;
using static System.Net.WebRequestMethods;
using WantApp.Servicos;

namespace WantApp.Endpoints.Clientes;

public class ClientesGet
{
    public static string Template => "/clientes";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(HttpContext http)
    {
        var usuario = http.User;
        ClienteResponse clienteResponse = new ClienteResponse()
        {
           // Id = usuario.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value.ToString(),
            Nome = "Teste"
        };                         

        return Results.Ok(clienteResponse);
    }
}
