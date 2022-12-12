using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PeriodicBatching;
using Serilog.Sinks.PostgreSQL;
using System.Text;
using System.Text.Json;
using WantApp.Endpoints.Categorias;
using WantApp.Endpoints.Clientes;
using WantApp.Endpoints.Empregados;
using WantApp.Endpoints.Pedidos;
using WantApp.Endpoints.Produtos;
using WantApp.Endpoints.Seguranca;
using WantApp.Infra.Dados;
using WantApp.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSerilog((context, configuracao) =>
{
    configuracao
        .WriteTo.Console()       
        .WriteTo.PostgreSQL(
            context.Configuration["ConnectionStrings:WantDb"],
            "LogAPI",
            needAutoCreateTable: true
        );
});

builder.Services.AddNpgsql<ApplicationDbContext>(builder.Configuration["ConnectionStrings:WantDb"]);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(opcoes =>
{
    opcoes.Password.RequireNonAlphanumeric = false;
    opcoes.Password.RequireDigit = false;
    opcoes.Password.RequireUppercase = false;
    opcoes.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

//Colocar como default todos endpoint pedindo token 
builder.Services.AddAuthorization(opcoes =>
{
    opcoes.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();

    opcoes.AddPolicy("EmpregadoPolitica", p =>
    {
        p.RequireAuthenticatedUser().RequireClaim("CodigoEmpregado");
    });

    opcoes.AddPolicy("Empregado005Politica", p =>
    {
        p.RequireAuthenticatedUser().RequireClaim("CodigoEmpregado", "2");
    });
    opcoes.AddPolicy("CPFPolitica", p =>
    {
        p.RequireAuthenticatedUser().RequireClaim("Cpf");
    });
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opcoes =>
{
    opcoes.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
}
);

builder.Services.AddScoped<BuscarTodosUsuariosComClaimName>();
builder.Services.AddScoped<CategoriaServico>();
builder.Services.AddScoped<UsuarioServico>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

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
app.MapMethods(EmpregadoGetAll.Template, EmpregadoGetAll.Metodos, EmpregadoGetAll.Action);
//app.MapMethods(EmpregadoGetAllMenosPerfomatico.Template, EmpregadoGetAllMenosPerfomatico.Metodos, EmpregadoGetAllMenosPerfomatico.Action);
app.MapMethods(TokenPost.Template, TokenPost.Metodos, TokenPost.Action);
app.MapMethods(ProdutoPost.Template, ProdutoPost.Metodos, ProdutoPost.Action);
app.MapMethods(ProdutoGetTodos.Template, ProdutoGetTodos.Metodos, ProdutoGetTodos.Action);
app.MapMethods(ProdutoGetPeloId.Template, ProdutoGetPeloId.Metodos, ProdutoGetPeloId.Action);
app.MapMethods(ProdutoGetVitrine.Template, ProdutoGetVitrine.Metodos, ProdutoGetVitrine.Action);
app.MapMethods(ClientesPost.Template, ClientesPost.Metodos, ClientesPost.Action);
app.MapMethods(ClientesGet.Template, ClientesGet.Metodos, ClientesGet.Action);
app.MapMethods(PedidoPost.Template, PedidoPost.Metodos, PedidoPost.Action);

app.UseExceptionHandler("/erro");
app.Map("/erro", (HttpContext http) => {
    var erro = http.Features?.Get<IExceptionHandlerFeature>()?.Error;

    if (erro != null)
    {
        if (erro is NpgsqlException)
            return Results.Problem(title: "Database fora de serviço!", statusCode: 500);
        else if (erro is BadHttpRequestException)
            return Results.Problem(title: "Erro de conversão de dados. Verifique todas informações enviadas!");
        
    }

    return Results.Problem(title: "Um erro ocorreu!", statusCode: 500);
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}