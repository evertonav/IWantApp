using System.Security.Claims;
using WantApp.Infra.Dados.Usuarios;
using static System.Net.WebRequestMethods;

namespace WantApp.Servicos.Usuarios;

public class InformacoesTokenServico
{
    private readonly HttpContext Http;

    public InformacoesTokenServico(HttpContext http)
    {
        Http = http;
    }

    public string UsuarioLogado()
    {
        return Http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public string CodigoEmpregado()
    {
        return Http.User.Claims.FirstOrDefault(c => c.Type == "CodigoEmpregado")?.Value;
    }

    public string NomeUsuario()
    {
        return Http.User.Claims.FirstOrDefault(c => c.Type == "Nome")?.Value;
    }
    //Deve ser criado uma classe diferente para essa situação
//    public ConsultaUsuarioPeloId CodigoEmpregado(string idCliente, IConfiguration configaracao)
//    {
//        return new BuscarUsuariosComClaim(configaracao).BuscarPeloId(idCliente, "CodigoEmpregado");
//    }
}
