using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WantApp.Endpoints.Categorias;
using WantApp.Endpoints.Empregados;
using WantApp.Endpoints.Seguranca;
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
        p.RequireAuthenticatedUser().RequireClaim("CodigoEmpregado", "3");
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
        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBearerTokenSettings:SecretKey"]))
    };
}
);

builder.Services.AddScoped<BuscarTodosUsuariosComClaimName>();

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


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}