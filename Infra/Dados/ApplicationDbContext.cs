using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WantApp.Dominio.Produtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WantApp.Dominio.Pedidos;

namespace WantApp.Infra.Dados;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Pedido> Pedido { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        builder.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(255);
        builder.Entity<Produto>().Property(p => p.Nome).IsRequired();
        builder.Entity<Produto>().Property(p => p.Preco).HasColumnType("decimal(10,2)").IsRequired();
        builder.Entity<Categoria>().Property(p => p.Nome).IsRequired();
        builder.Entity<Pedido>().Property(c => c.ClienteId).IsRequired();
        builder.Entity<Pedido>().Property(c => c.EnderecoEntrega).IsRequired();
        builder.Entity<Pedido>()
            .HasMany(p => p.Produtos)
            .WithMany(p => p.Pedidos)
            .UsingEntity(x => x.ToTable("PedidosProdutos"));
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuracao)
    {
        configuracao.Properties<string>().HaveMaxLength(100);
    }
}
