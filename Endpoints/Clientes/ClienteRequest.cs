namespace WantApp.Endpoints.Clientes;

public record ClienteRequest(string Email, string Senha, string Nome, string CPF);
