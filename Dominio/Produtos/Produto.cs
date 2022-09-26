namespace WantApp.Dominio.Produtos;

public class Produto : Entidade
{
    public string Nome { get; set; }
    public Categoria Categoria { get; set; }
    public string Descricao { get; set; }
    public bool TemEstoque { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; } = true;
}
