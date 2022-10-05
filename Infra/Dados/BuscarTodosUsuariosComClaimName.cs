using Dapper;
using Npgsql;
using WantApp.Endpoints.Empregados;

namespace WantApp.Infra.Dados;

public class BuscarTodosUsuariosComClaimName
{
	private readonly IConfiguration configuracao;

	public BuscarTodosUsuariosComClaimName(IConfiguration configuracao)
	{
		this.configuracao = configuracao;
	}

	public IEnumerable<EmpregadoResponse> Executar(int pagina, int linhas)
	{
        NpgsqlConnection conexao = new NpgsqlConnection(configuracao["ConnectionStrings:WantDb"]);

        string sql = "Select \"Email\", \"ClaimValue\" as \"Nome\" "
          + "  from \"AspNetUsers\" u "
          + "  join \"AspNetUserClaims\" c"
          + "    on u.\"Id\" = c.\"UserId\" "
          + "    and c.\"ClaimType\" = 'Nome'"
          + " Order by \"ClaimValue\""
          + " LIMIT @linhas OFFSET (@pagina - 1) * @linhas";

        return conexao.Query<EmpregadoResponse>(sql, new { pagina, linhas });
    }
}
