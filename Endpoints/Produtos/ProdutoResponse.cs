namespace WantApp.Endpoints.Produtos;

public record ProdutoResponse(Guid Id, string Nome, string CategoriaNome, string Descricao, bool TemEstoque, 
    decimal Preco, bool Ativo);
