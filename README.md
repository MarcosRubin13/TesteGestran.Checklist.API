# TesteGestran.Checklist.API

## Descrição do Projeto
Este projeto é uma API para gerenciar checklists diários de veículos em uma empresa de transporte. A API permite a criação, atualização, verificação de itens e mudança de status dos checklists, garantindo que todos os itens sejam verificados antes da finalização.

## Pré-requisitos
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) ou [Visual Studio Code](https://code.visualstudio.com/)
- [Download da Collection do Postman](https://we.tl/t-JVg2Yae5vH)

## Configuração do Ambiente

### Clonar o Repositório
```bash
git clone <URL_DO_REPOSITORIO>
cd TesteGestran.Checklist.API
```



## Configurar o Banco de Dados
  1. Configure sua instância do SQL Server.
  2. Atualize a string de conexão no arquivo appsettings.json:
  ```bash
  "ConnectionStrings": {
  "DefaultConnection": "Server=<SERVIDOR>;Database=ChecklistDB;User Id=<USUARIO>;Password=<SENHA>;"
  }
  ```

### Aplicar Migrações e Criar o Banco de Dados

```bash
dotnet ef database update
```

### Script SQL inicial para criar usuários
```bash
INSERT INTO Users (Username, Password, Role) VALUES 
('user1', '123', 'Executor'),
('user2', '123', 'Executor'),
('super', '123', 'Supervisor');
```

## Instalação e Execução
### Instalar Dependências

```bash
dotnet restore
```
### Rodar a Aplicação

```bash
dotnet run
```

## Uso da API

### Autenticação de Usuário:
Descrição: Autentica um usuário e retorna um token JWT.
```bash
POST /api/Auth/login
```

#### Corpo da Requisição:

```bash
{
  "username": "super",
  "password": "123"
}
```

#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
```
#### Exemplo de Resposta:

```bash
{
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

### Obter Checklist por ID:
Descrição: Obtém os detalhes de um checklist específico pelo ID.

#### Cabeçalho:
```bash
Authorization: Bearer {{authToken}}
```

#### Exemplo de URL:
```bash
https://localhost:7072/api/Checklist/22
```

### Criar Checklist:
Descrição: Cria um novo checklist.

```bash
POST /api/Checklist
```
#### Corpo da Requisição:

```bash {
  "usuario": "user1",
  "tipo": "SAIDA",
  "placa": "ABC-2A12",
  "motorista": "Marcos"
}
```

#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
Content-Type: application/json
```

### Iniciar Checklist:
Descrição: Inicia um checklist específico, alterando o status para "INICIADO".

```bash
PUT /api/Checklist/iniciar
```
#### Corpo da Requisição:

```bash
{
  "id": 22,
  "etapa": "INICIADO"
}
```
#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
Content-Type: application/json
```
### Verificar Item de Checklist
Descrição: Verifica um item específico de um checklist.

```bash
PUT /api/Checklist/{checklistId}/item/{itemId}/verificar
```

Corpo da Requisição:

```bash{
  "checklistId": 22,
  "itemId": 46,
  "observacao": "Luz queimada"
}
```

#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
Content-Type: application/json
```

### Enviar Checklist para Análise
Descrição: Atualiza o status do checklist para "AGUARDANDO_FINALIZACAO" se todos os itens estiverem verificados.

```bash
PUT /api/Checklist/{id}/status/aguardando-finalizacao
```
#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
Content-Type: application/json
```

### Finalizar Checklist
Descrição: Finaliza um checklist específico, alterando o status para "FINALIZADO".

#### Corpo da Requisição:

```bash
{
  "id": 22,
  "etapa": "FINALIZADO",
  "situacao": true
}
```
#### Cabeçalho
```bash
Authorization: Bearer {{authToken}}
Content-Type: application/json
```


















