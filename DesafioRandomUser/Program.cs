using DesafioRandomUser.Data;        // Importa o DbContext que criamos (AppDbContext).
using DesafioRandomUser.Services;    // Importa o serviço que consome a API RandomUser.
using Microsoft.EntityFrameworkCore; // Biblioteca do Entity Framework Core para usar o PostgreSQL.

var builder = WebApplication.CreateBuilder(args);

// Pega a connection string do appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra o DbContext no container de injeção de dependência
// Assim o EF Core sabe como se conectar no PostgreSQL usando o Npgsql.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registra o serviço que consome a API RandomUser.
// AddHttpClient cria um cliente HTTP gerenciado.
// AddScoped garante que o serviço RandomUserService seja criado por request.
builder.Services.AddHttpClient<RandomUserService>();
builder.Services.AddScoped<RandomUserService>();

// Adiciona suporte a Controllers (nossas APIs).
builder.Services.AddControllers();

// Adiciona suporte ao Swagger (documentação/testes da API).
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa o Swagger na aplicação (UI no navegador).
app.UseSwagger();
app.UseSwaggerUI();

// Força redirecionamento para HTTPS.
app.UseHttpsRedirection();

app.UseDefaultFiles(); // procura index.html no wwwroot
app.UseStaticFiles();  // habilita servir arquivos estáticos (css, js, html)


// Mapeia os controllers para rotas /api/...
app.MapControllers();

// Inicia o servidor web.
app.Run();
