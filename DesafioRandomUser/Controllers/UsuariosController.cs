using Microsoft.AspNetCore.Mvc;       // Usado para criar controllers e endpoints REST.
using Microsoft.EntityFrameworkCore;  // Usado para consultas ao banco com EF Core.
using DesafioRandomUser.Data;         // Importa o DbContext.
using DesafioRandomUser.Models;       // Importa a classe Usuario.
using DesafioRandomUser.Services;     // Importa o serviço que consome a RandomUser API.

namespace DesafioRandomUser.Controllers
{
    // Marca a classe como um Controller de API.
    [ApiController]
    // Define a rota base: /api/usuarios
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _db;        // DbContext para acessar o PostgreSQL.
        private readonly RandomUserService _random; // Serviço para gerar usuários aleatórios.

        // Construtor com injeção de dependência
        public UsuariosController(AppDbContext db, RandomUserService random)
        {
            _db = db;
            _random = random;
        }

        // GET /api/usuarios
        // Retorna todos os usuários do banco (relatório).
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
            => await _db.Usuarios.AsNoTracking().ToListAsync();

        // POST /api/usuarios
        // Cria um usuário manualmente (informado no body da requisição).
        [HttpPost]
        public async Task<ActionResult<Usuario>> Create([FromBody] Usuario u)
        {
            _db.Usuarios.Add(u);
            await _db.SaveChangesAsync();
            // Retorna 201 Created com o objeto salvo
            return CreatedAtAction(nameof(GetAll), new { id = u.Id }, u);
        }

        // POST /api/usuarios/fetch-random/{count}
        // Usa a API RandomUser para gerar N usuários e salvar no banco.
        [HttpPost("fetch-random/{count:int}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> FetchRandom(int count = 1)
            => await _random.FetchAndSaveAsync(count);

        // PUT /api/usuarios/{id}
        // Atualiza os dados de um usuário existente.
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario up)
        {
            // Procura o usuário pelo ID
            var u = await _db.Usuarios.FindAsync(id);
            if (u == null) return NotFound(); // Se não existe, retorna 404

            // Atualiza os campos
            u.Nome = up.Nome;
            u.Sobrenome = up.Sobrenome;
            u.Email = up.Email;
            u.Genero = up.Genero;
            u.Nascimento = up.Nascimento;
            u.Cidade = up.Cidade;
            u.Estado = up.Estado;
            u.Pais = up.Pais;
            u.Telefone = up.Telefone;
            u.Username = up.Username;

            // Salva as alterações no banco
            await _db.SaveChangesAsync();
            return Ok(u);
        }
    }
}
