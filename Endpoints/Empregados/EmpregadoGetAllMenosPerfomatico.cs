using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace WantApp.Endpoints.Empregados;

public class EmpregadoGetAllMenosPerfomatico
{
    public static string Template => "/empregados";
    public static string[] Metodos => new string[] {HttpMethod.Get.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action(int pagina, int linhas, UserManager<IdentityUser> userManager)
    {
        List<IdentityUser> usuarios = userManager.Users.Skip((pagina - 1) * linhas).Take(linhas).ToList();
        List<EmpregadoResponse> empregadosResponse = new List<EmpregadoResponse>();

        foreach(var item in usuarios)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimNome = claims.FirstOrDefault(c => c.Type == "Nome"); ;
            string nomeUsuario = claimNome != null ? claimNome.Value : string.Empty;
            var claimCodigoEmpregado = claims.FirstOrDefault(c => c.Type == "CodigoEmpregado"); ;
            string codigoEmpregado = claimCodigoEmpregado != null ? claimCodigoEmpregado.Value : string.Empty;

            empregadosResponse.Add(new EmpregadoResponse(item.Email, nomeUsuario));
        }        

        return Results.Ok(empregadosResponse);
    }
}
