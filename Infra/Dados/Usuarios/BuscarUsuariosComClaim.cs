using Dapper;
using Npgsql;
using WantApp.Endpoints.Empregados;

namespace WantApp.Infra.Dados.Usuarios;

public class BuscarUsuariosComClaim
{
    private readonly IConfiguration configuracao;

    public BuscarUsuariosComClaim(IConfiguration configuracao)
    {
        this.configuracao = configuracao;
    }

    public async Task<IEnumerable<EmpregadoResponse>> BuscarTodos(int pagina, int linhas)
    {
        NpgsqlConnection conexao = new NpgsqlConnection(configuracao["ConnectionStrings:WantDb"]);

        string sql = "Select \"Email\", \"ClaimValue\" as \"Nome\" "
          + "  from \"AspNetUsers\" u "
          + "  join \"AspNetUserClaims\" c"
          + "    on u.\"Id\" = c.\"UserId\" "
          + "    and c.\"ClaimType\" = 'Nome'"
          + " Order by \"ClaimValue\""
          + " LIMIT @linhas OFFSET (@pagina - 1) * @linhas";

        return await conexao.QueryAsync<EmpregadoResponse>(sql, new { pagina, linhas });
    }

    public ConsultaUsuarioPeloId BuscarPeloId(string id, string claimType)
    {
        //Ter que ver esse select melhor
        NpgsqlConnection conexao = new NpgsqlConnection(configuracao["ConnectionStrings:WantDb"]);

        string sql = "Select \"Email\", \"ClaimValue\" as \"Valor\" "
          + "  from \"AspNetUsers\" u "
          + "  join \"AspNetUserClaims\" c"
          + "    on u.\"Id\" = c.\"UserId\" "
          + "    and c.\"ClaimType\" = @claimType"
          + "  Where c.\"UserId\" = @id  "
          + " Order by \"ClaimValue\"";
        //+ " LIMIT @linhas OFFSET (@pagina - 1) * @linhas";

        return conexao.Query<ConsultaUsuarioPeloId>(sql, new { claimType, id }).FirstOrDefault();
    }
}
