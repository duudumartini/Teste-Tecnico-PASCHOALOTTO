namespace DesafioRandomUser.Models
{
    // Classe que representa o usuário no sistema.
    // Vai ser mapeada para a tabela do banco de dados.
    public class Usuario
    {
        // Id do usuário, chave primária no banco.
        public int Id { get; set; }

        // Nome próprio (primeiro nome).
        public string Nome { get; set; }

        // Sobrenome ou último nome.
        public string Sobrenome { get; set; }

        // E-mail de contato do usuário.
        public string Email { get; set; }

        // Gênero informado (male/female/other).
        public string Genero { get; set; }

        // Data de nascimento (nullable, pode ser nula).
        public DateTime? Nascimento { get; set; }

        // Cidade onde o usuário mora.
        public string Cidade { get; set; }

        // Estado/Província do usuário.
        public string Estado { get; set; }

        // País do usuário.
        public string Pais { get; set; }

        // Telefone de contato.
        public string Telefone { get; set; }

        // Nome de usuário (username/login).
        public string Username { get; set; }
    }
}
