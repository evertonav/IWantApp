using Flunt.Notifications;

namespace WantApp.Dominio.Produtos;

public abstract class Entidade : Notifiable<Notification>
{
    public Entidade()
    {
        Id = Guid.NewGuid();
        CriadoPor = "";
        EditadoPor = "";
    }

    public Guid Id { get; set; }    
    public string CriadoPor { get; set; }
    public DateTime DataCriacao { get; set; }
    public string EditadoPor { get; set; }
    public DateTime DataEdicao { get; set; }
}
