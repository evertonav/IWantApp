using Dapper;
using Npgsql;
using System.Collections.Generic;
using WantApp.Endpoints.Empregados;
using WantApp.Infra.Dados;
using WantApp.Infra.Dados.Usuarios;

namespace WantApp.Servicos.Produtos.Relatorios;

public class RelatorioProdutosMaisVendidos
{
    private readonly IConfiguration Configuracao;

    public RelatorioProdutosMaisVendidos(IConfiguration configuracao)
    {
        Configuracao = configuracao;
    }

    public IEnumerable<ProdutosMaisVendidosRetorno> Executar(int pagina, int linhas)
    {
        //Ter que ver esse select melhor
        NpgsqlConnection conexao = new NpgsqlConnection(Configuracao["ConnectionStrings:WantDb"]);

        string sql = "select p.\"Id\", p.\"Nome\", count(1) as Quantidade from \"PedidosProdutos\" pp";
        sql += " join \"Produtos\" p on pp.\"ProdutosId\" = p.\"Id\"";
        sql += " group by p.\"Id\", p.\"Nome\" ";
        sql += " order by p.\"Id\" ";        
        sql += " LIMIT @linhas OFFSET (@pagina - 1) * @linhas";        

        return conexao.Query<ProdutosMaisVendidosRetorno>(sql, new { linhas, pagina});        
    }
}
