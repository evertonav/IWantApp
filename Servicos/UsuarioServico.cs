using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WantApp.Endpoints.Clientes;
using WantApp.Endpoints.Empregados;
using WantApp.Infra.Dados;
using WantApp.Infra.Dados.Usuarios;

namespace WantApp.Servicos;

public class UsuarioServico
{
    private readonly UserManager<IdentityUser> GerenciarUsuario;

    public UsuarioServico(UserManager<IdentityUser> gerenciarUsuario)
    {
        GerenciarUsuario = gerenciarUsuario; 
    }

    public async Task<(IdentityResult, string)> CriarAsync(string senha, string email, List<Claim> claims)
    {
        IdentityUser usuario = new IdentityUser()
        {
            UserName = email,
            Email = email
        };

        var resultadoCriacao = await GerenciarUsuario.CreateAsync(usuario, senha);

        if (!resultadoCriacao.Succeeded)
            return (resultadoCriacao, "");

        return (await AdicionarClaims(usuario, claims), usuario.Id);
    }

    private async Task<IdentityResult> AdicionarClaims(IdentityUser usuario, List<Claim> claims)
    {
        return await GerenciarUsuario.AddClaimsAsync(usuario, claims);
    }
    
    public string UsuarioLogado(HttpContext http)
    {
        return http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public string CodigoEmpregado(HttpContext http)
    {
        return http.User.Claims.FirstOrDefault(c => c.Type == "CodigoEmpregado")?.Value;
    }

    public ConsultaUsuarioPeloId CodigoEmpregado(string idCliente, IConfiguration configaracao)
    {
        return new BuscarUsuariosComClaim(configaracao).BuscarPeloId(idCliente, "CodigoEmpregado");
    }
}
