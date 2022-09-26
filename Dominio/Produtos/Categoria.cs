using Flunt.Validations;

namespace WantApp.Dominio.Produtos;

public class Categoria : Entidade
{
    public string Nome { get; private set; }
    public bool Ativo { get; private set; } = true;

	public Categoria(string nome, string criadoPor, string editadoPor)
	{
		Nome = nome;
		Ativo = true;
		CriadoPor = criadoPor;
		EditadoPor = editadoPor;

		Validar();
	}

	private void Validar()
	{
		Contract<Categoria> contrato = new Contract<Categoria>()
			.IsNotNullOrEmpty(Nome, "Nome", "Nome é obrigatório!")
			.IsNotNullOrEmpty(CriadoPor, "CriadoPor", "'CriadoPor' é obrigatório!")
			.IsNotNullOrEmpty(CriadoPor, "EditadoPor", "'EditadoPor' é obrigatório!");

		AddNotifications(contrato);
	}

	public void EditarInformacoes(string nome, bool ativo)
	{
		Ativo = ativo;
		Nome = nome;

		Validar();
    }
}
