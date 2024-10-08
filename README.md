# Projeto API de Serviços

Este projeto é uma API para gerenciar administradores e veículos. Ele utiliza ASP.NET Core e Entity Framework Core para a camada de dados.

## Tecnologias Utilizadas

- ASP.NET Core
- Entity Framework Core
- MySQL
- Swagger

## Configuração do Projeto

### Pré-requisitos

- .NET 6 SDK
- MySQL


### Endpoints

#### Home

- `GET /` - Retorna a visão inicial.

#### Administradores

- `POST /administradores/login` - Realiza o login de um administrador.

#### Veículos

- `POST /Veiculos` - Adiciona um novo veículo.
- `GET /Veiculos` - Retorna todos os veículos com paginação.
- `GET /Veiculos/{id}` - Retorna um veículo pelo ID.
- `PUT /Veiculos/{id}` - Atualiza um veículo pelo ID.
- `DELETE /Veiculos/{id}` - Deleta um veículo pelo ID.

### Validações

- O nome do veículo não pode ser vazio.
- A marca do veículo não pode ser vazia.
- O ano do veículo deve ser maior que 1950.

### Documentação

A documentação da API pode ser acessada através do Swagger na URL `/swagger`.


