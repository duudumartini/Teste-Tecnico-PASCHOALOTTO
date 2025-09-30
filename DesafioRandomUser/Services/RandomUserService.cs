using System.Text.Json;               // Usado para trabalhar com JSON (parse manual).
using DesafioRandomUser.Data;         // Importa o AppDbContext (conexão com o banco).
using DesafioRandomUser.Models;       // Importa a classe Usuario (modelo que vira tabela).

namespace DesafioRandomUser.Services
{
    // Serviço responsável por consumir a API RandomUser e salvar os dados no banco.
    public class RandomUserService
    {
        private readonly HttpClient _http;   // Cliente HTTP para chamadas externas.
        private readonly AppDbContext _db;   // Contexto do banco de dados (Entity Framework).

        // Construtor recebe HttpClient e DbContext via injeção de dependência.
        public RandomUserService(HttpClient http, AppDbContext db)
        {
            _http = http;
            _db = db;
        }

        // Método que baixa N usuários da API e salva no banco.
        public async Task<List<Usuario>> FetchAndSaveAsync(int results = 1)
        {
            // Monta a URL da API, pedindo N resultados e restringindo nacionalidade.
            var url = $"https://randomuser.me/api/?results={results}&nat=us,br,gb";

            // Faz a chamada HTTP e pega a resposta como string.
            var resp = await _http.GetStringAsync(url);

            // Converte a resposta em um objeto JSON navegável.
            using var doc = JsonDocument.Parse(resp);

            // Lista para guardar os usuários que serão salvos.
            var saved = new List<Usuario>();

            // Percorre o array "results" que vem na resposta da API.
            foreach (var item in doc.RootElement.GetProperty("results").EnumerateArray())
            {
                // Quebra o JSON em partes (nome, localização, login, data nascimento).
                var name = item.GetProperty("name");
                var location = item.GetProperty("location");
                var login = item.GetProperty("login");
                var dob = item.GetProperty("dob");

                // Cria objeto Usuario preenchendo com os dados do JSON.
                var usuario = new Usuario
                {
                    Nome = name.GetProperty("first").GetString(),
                    Sobrenome = name.GetProperty("last").GetString(),
                    Email = item.GetProperty("email").GetString(),
                    Genero = item.GetProperty("gender").GetString(),
                    Nascimento = dob.GetProperty("date").GetDateTime(),
                    Cidade = location.GetProperty("city").GetString(),
                    Estado = location.GetProperty("state").GetString(),
                    Pais = location.GetProperty("country").GetString(),
                    Telefone = item.GetProperty("phone").GetString(),
                    Username = login.GetProperty("username").GetString()
                };

                // Adiciona ao contexto (ainda não grava no banco).
                _db.Usuarios.Add(usuario);

                // Guarda numa lista para retornar depois.
                saved.Add(usuario);
            }

            // Efetiva as alterações no banco (INSERT).
            await _db.SaveChangesAsync();

            // Retorna a lista de usuários que foram salvos.
            return saved;
        }
    }
}
