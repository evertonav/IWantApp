using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WantApp.Dominio.Produtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WantApp.Infra.Dados;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<Notification>();

        builder.Entity<Produto>().Property(p => p.Descricao).HasMaxLength(255);
        builder.Entity<Produto>().Property(p => p.Nome).IsRequired();
        builder.Entity<Categoria>().Property(p => p.Nome).IsRequired();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configuracao)
    {
        configuracao.Properties<string>().HaveMaxLength(100);
    }
}
