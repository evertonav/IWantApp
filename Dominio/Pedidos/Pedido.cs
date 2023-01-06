using Flunt.Validations;
using System.Diagnostics.Contracts;
using WantApp.Dominio.Produtos;

namespace WantApp.Dominio.Pedidos;

public class Pedido : Entidade
{
    public string ClienteId { get; private set; }
    public List<Produto> Produtos { get; private set; }
    public decimal Total { get; private set; }
    public string EnderecoEntrega { get; private set; }

    public Pedido() { }

    public Pedido (string clienteId, string clienteNome, List<Produto> produtos, string enderecoEntrega)
    {
        ClienteId = clienteId;
        Produtos = produtos;        
        EnderecoEntrega = enderecoEntrega;
        CriadoPor = clienteNome;
        EditadoPor = clienteNome;

        Total = 0;
        foreach(var item in Produtos)
        {
            Total += item.Preco;
        }
        Validar();
    }

    private void Validar()
    {
        var contrato = new Contract<Pedido>()
            .IsNotNull(ClienteId, "Cliente")
            .IsTrue(Produtos.Count() > 0, "Produtos")
            .IsNotNull(EnderecoEntrega, "Endereço Entrega");

        AddNotifications(contrato);
            
    }
}
