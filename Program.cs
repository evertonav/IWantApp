using Microsoft.AspNetCore.Identity;
using WantApp.Endpoints.Categorias;
using WantApp.Endpoints.Empregados;
using WantApp.Infra.Dados;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration["ConnectionStrings:WantDb"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opcoes =>
{
    opcoes.Password.RequireNonAlphanumeric = false;
    opcoes.Password.RequireDigit = false;
    opcoes.Password.RequireUppercase = false;
    opcoes.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoriaPost.Template, CategoriaPost.Metodos, CategoriaPost.Handle);
app.MapMethods(CategoriaGetTodos.Template, CategoriaGetTodos.Metodos, CategoriaGetTodos.Action);
app.MapMethods(CategoriaPut.Template, CategoriaPut.Metodos, CategoriaPut.Action);
app.MapMethods(EmpregadoPost.Template, EmpregadoPost.Metodos, EmpregadoPost.Action);

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}