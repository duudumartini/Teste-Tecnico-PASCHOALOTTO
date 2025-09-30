# Desafio Técnico - Random User API + PostgreSQL + ASP.NET Core

Este projeto foi desenvolvido como parte de um desafio técnico.  
Ele consiste em uma **API REST em C# (.NET 8)** integrada ao **PostgreSQL**, consumindo a API pública [Random User Generator](https://randomuser.me/), e uma página HTML simples para relatório e manipulação de usuários.

---

## 🚀 Tecnologias usadas
- C# / ASP.NET Core Web API
- Entity Framework Core + Migrations
- PostgreSQL (Npgsql)
- Swagger (documentação e testes da API)
- HTML + CSS + JavaScript (front-end simples)

---

## ⚙️ Funcionalidades
- **Integração com PostgreSQL**: criação automática da tabela `Usuarios`.
- **Consumo da API RandomUser**: insere usuários aleatórios no banco.
- **Endpoints da API**:
  - `GET /api/usuarios` → lista todos os usuários (relatório).
  - `POST /api/usuarios` → cria um usuário manualmente.
  - `POST /api/usuarios/fetch-random/{count}` → consome a API RandomUser e insere *count* usuários.
  - `PUT /api/usuarios/{id}` → atualiza um usuário existente.
- **Front-end básico**:
  - Botão para gerar 3 usuários aleatórios.
  - Formulário para criação manual.
  - Listagem em tabela com edição inline e botão de salvar.

---

## 📂 Estrutura do Projeto
```
DesafioRandomUser/
│── Controllers/        # Endpoints da API
│── Data/               # DbContext (Entity Framework)
│── Models/             # Classe Usuario
│── Services/           # Serviço que consome RandomUser API
│── wwwroot/            # index.html + style.css (front)
│── Program.cs
│── appsettings.json
```

---

## 🛠️ Como rodar

### 1. Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### 2. Configurar banco de dados
No PostgreSQL:
```sql
CREATE DATABASE desafio;
```

### 3. Configurar credenciais
No arquivo `appsettings.json`, ajuste a senha do usuário `postgres`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=desafio;Username=postgres;Password=SUA_SENHA"
}
```

### 4. Criar tabelas (migrations)
No terminal, dentro da pasta do projeto:
```bash
dotnet ef database update
```

### 5. Rodar a API
```bash
dotnet run
```

API disponível em:
```
http://localhost:5229/swagger
```

Front-end disponível em:
```
http://localhost:5229/
```

---

## 🎨 Interface
- `index.html` → tabela com usuários, formulário de criação e edição inline.
- `style.css` → estilo minimalista com botões, tabela responsiva e inputs destacados.

---
