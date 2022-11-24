using Flunt.Validations;

namespace WantApp.Dominio.Produtos;

public class Produto : Entidade
{
    public string Nome { get; set; }
    public Categoria Categoria { get; set; }
    public string Descricao { get; set; }
    public bool TemEstoque { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; } = true;
    public decimal Preco { get; private set ; }

    public Produto()
    {

    }

    public Produto(string nome, Categoria categoria, string descricao, bool temEstoque, 
        decimal preco, bool ativo, string criadoPor)
    {
        Nome = nome;
        Categoria = categoria;
        Descricao = descricao;
        TemEstoque = temEstoque;
        Ativo = ativo;
        CriadoPor = criadoPor;  
        EditadoPor = criadoPor;
        Preco = preco;

        Validar();
    }

    public void Validar()
    {
        Contract<Produto> contrato = new Contract<Produto>()
            .IsNotNullOrEmpty(Nome, "Nome", "Nome é obrigatório!")
            .IsNotNull(Categoria, "Categoria", "A categoria não foi encontrada!")
            .IsNotNullOrEmpty(Descricao, "Descricao", "'Descricao' é obrigatório!")
            .IsNotNullOrEmpty(CriadoPor, "CriadoPor", "'CriadoPor' é obrigatório!")
            .IsNotNullOrEmpty(CriadoPor, "EditadoPor", "'EditadoPor' é obrigatório!")
            .IsGreaterOrEqualsThan(Preco, 1, "Preco", "Você precisa preencher o preço com um valor maior que 0");
            

        AddNotifications(contrato);
    }
}
