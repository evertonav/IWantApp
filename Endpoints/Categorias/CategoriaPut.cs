using Microsoft.AspNetCore.Mvc;
using WantApp.Dominio.Produtos;
using WantApp.Infra.Dados;

namespace WantApp.Endpoints.Categorias;

public class CategoriaPut
{
    public static string Template => "/categorias/{id:Guid}";
    public static string[] Metodos => new string[] {HttpMethod.Put.ToString()};
    public static Delegate Handle => Action;

    public static IResult Action([FromRoute] Guid Id, CategoriaRequest categoriaRequest, ApplicationDbContext context)
    {

        Categoria categoria = context.Categorias.FirstOrDefault(x => x.Id == Id);

        if (categoria == null)
        {
            return Results.NotFound();
        }

        categoria.EditarInformacoes(categoriaRequest.Nome, categoriaRequest.Ativo);        

        if (!categoria.IsValid)
        {
            return Results.ValidationProblem(categoria.Notifications.ConverterParaProblemaDetalhado());
        }
        
        context.SaveChanges();

        return Results.Ok();
    }
}
