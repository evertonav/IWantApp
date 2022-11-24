namespace WantApp.Endpoints.Produtos;

public record ProdutoRequest(string Nome, Guid CategoriaId, string Descricao, bool TemEstoque, decimal preco,
    bool Ativo);
