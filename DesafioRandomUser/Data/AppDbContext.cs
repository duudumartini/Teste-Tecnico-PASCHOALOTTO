using Microsoft.EntityFrameworkCore;       // Biblioteca do Entity Framework Core, usada para acessar o banco.
using DesafioRandomUser.Models;            // Importa a pasta Models, onde está a classe Usuario.

namespace DesafioRandomUser.Data
{
    // Classe responsável por fazer a ponte entre o C# e o banco de dados.
    // É aqui que registramos quais entidades viram tabelas no PostgreSQL.
    public class AppDbContext : DbContext
    {
        // Construtor que recebe as opções de configuração (como a connection string)
        // e repassa para a classe base (DbContext).
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Representa a tabela de usuários no banco.
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
