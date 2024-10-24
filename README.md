# 123vendas

## Índice
1. [Descrição do Projeto](#descrição-do-projeto)
2. [Tecnologias Utilizadas](#tecnologias-utilizadas)
   - [Backend](#backend)
   - [Banco de Dados](#banco-de-dados)
   - [Validações e Middleware](#validações-e-middleware)
   - [Mensageria](#mensageria)
   - [Testes](#testes)
3. [Entidades de Negócio](#entidades-de-negócio)
4. [Estrutura do Banco de Dados](#estrutura-do-banco-de-dados)
   - [Diagrama do Banco](#diagrama-do-banco)
5. [Endpoints da API](#endpoints-da-api)
   - [Branches](#branches)
   - [BranchProducts](#branchproducts)
   - [Customers](#customers)
   - [Products](#products)
   - [Sales](#sales)
6. [Arquitetura de Eventos: Integração com RabbitMQ](#arquitetura-de-eventos-integração-com-rabbitmq)
   - [Exchange e Routing Keys](#exchange-e-routing-keys)
   - [Eventos e Payloads](#eventos-e-payloads)
   - [Estrutura do Pub/Sub](#estrutura-pubsub)
7. [Como Executar o Projeto](#como-executar-o-projeto)
   - [Pré-requisitos](#pré-requisitos)
   - [Configuração Inicial](#configuração-inicial)
   - [Executando o Projeto](#executando-o-projeto)
   - [Variáveis de Ambiente](#variáveis-de-ambiente)
     
---

### Descrição do Projeto
O **123vendas** é uma plataforma que facilita o gerenciamento de vendas, produtos e clientes para empresas que operam com múltiplas filiais. O sistema permite a gestão centralizada de produtos e clientes, com flexibilidade para personalizar estoque e preços por filial. Ele oferece uma interface robusta para administrar o ciclo completo de vendas, incluindo a criação, atualização e cancelamento de pedidos, além da gestão de estoque.

---

### Tecnologias Utilizadas

#### Backend
- **.NET Core 6:** Framework utilizado para o desenvolvimento da aplicação.
- **DDD (Domain-Driven Design):** Padrão de arquitetura adotado para estruturar o domínio da aplicação de forma modular e coesa.
- **Serilog:** Sistema de logging utilizado para registrar informações relevantes em diversas partes da aplicação, incluindo o middleware de exceção, permitindo rastreamento e monitoramento eficazes.
- **AutoMapper:** Biblioteca utilizada para simplificar a conversão entre objetos, facilitando a transferência de dados entre diferentes camadas da aplicação.
- **Microsoft.AspNetCore.Mvc.Versioning:** Permite a versão da API de forma organizada, garantindo que diferentes versões possam coexistir, facilitando a manutenção e a evolução da interface.
- **Swagger:** Ferramenta que fornece uma interface interativa para explorar e testar os endpoints da API, melhorando a documentação e a usabilidade.

#### Banco de Dados
- **SQL Server:** Banco de dados utilizado para persistência das informações.
- **Entity Framework Core:** ORM (Object-Relational Mapper) utilizado para comunicação com o banco de dados.
- **IQueryable:** Interface que permite consultas dinâmicas e eficientes aos dados, facilitando a construção de consultas complexas e a filtragem de resultados em tempo de execução.
- **Migrations:** Ferramenta utilizada para gerenciar a evolução do banco de dados de forma controlada e versionada.

#### Validações e Middleware
- **Middleware de Exceção:** Camada responsável pelo tratamento centralizado de exceções, garantindo consistência no retorno de erros.
- **FluentValidation:** Biblioteca para validação de regras de negócios e dados de entrada de forma fluida e intuitiva.

#### Mensageria
- **RabbitMQ:** Broker de mensagens utilizado para a comunicação assíncrona e integração entre serviços.

#### Testes
- **XUnit:** Framework de testes utilizado para escrever e executar testes unitários.
- **FluentAssertions:** Biblioteca utilizada para facilitar a escrita de asserções em testes.
- **Bogus:** Biblioteca para gerar dados fictícios de forma fácil e controlada para os testes.
- **NSubstitute:** Framework para criação de mocks e stubs, permitindo simular dependências nos testes.

---

### Entidades de Negócio
As principais entidades do domínio incluem:
- **Branch (Filial):** Representa uma filial da empresa, contendo informações como nome, endereço e telefone.
- **Product (Produto):** Detalha os produtos disponíveis para venda, com informações como nome, descrição, categoria e preço base.
- **Customer (Cliente):** Representa um cliente da empresa, incluindo dados pessoais e de contato.
- **Sale (Venda):** Representa o pedido de compra de um cliente, com status, data e itens comprados.
- **SaleItem (Item de Venda):** Detalha cada item de uma venda, incluindo quantidade, preço unitário e descontos.

---

### Estrutura do Banco de Dados

#### Diagrama do Banco
![Diagrama do Banco](https://github.com/user-attachments/assets/aa6f0dad-5877-46b4-b42d-aa5a50606c6f)

---

### Endpoints da API

#### Branches
| Método | Endpoint                    | Descrição                       |
|--------|-----------------------------|---------------------------------|
| GET    | `/api/v1/Branches`          | Retorna a lista de filiais      |
| POST   | `/api/v1/Branches`          | Cria uma nova filial            |
| GET    | `/api/v1/Branches/{id}`     | Retorna os detalhes de uma filial específica |
| PUT    | `/api/v1/Branches/{id}`     | Atualiza as informações de uma filial |
| DELETE | `/api/v1/Branches/{id}`     | Exclui uma filial               |

#### BranchProducts
| Método | Endpoint                            | Descrição                               |
|--------|-------------------------------------|------------------------------------------|
| GET    | `/api/v1/BranchProducts`            | Retorna a lista de produtos por filial   |
| POST   | `/api/v1/BranchProducts`            | Adiciona um produto a uma filial         |
| GET    | `/api/v1/BranchProducts/{id}`       | Retorna os detalhes de um produto de uma filial específica |
| PUT    | `/api/v1/BranchProducts/{id}`       | Atualiza as informações de um produto de filial |
| DELETE | `/api/v1/BranchProducts/{id}`       | Remove um produto de uma filial          |

#### Customers
| Método | Endpoint                    | Descrição                             |
|--------|-----------------------------|--------------------------------------|
| GET    | `/api/v1/Customers`          | Retorna a lista de clientes          |
| POST   | `/api/v1/Customers`          | Cria um novo cliente                 |
| GET    | `/api/v1/Customers/{id}`     | Retorna os detalhes de um cliente específico |
| PUT    | `/api/v1/Customers/{id}`     | Atualiza as informações de um cliente |
| DELETE | `/api/v1/Customers/{id}`     | Exclui um cliente                    |

#### Products
| Método | Endpoint                    | Descrição                             |
|--------|-----------------------------|--------------------------------------|
| GET    | `/api/v1/Products`           | Retorna a lista de produtos          |
| POST   | `/api/v1/Products`           | Cria um novo produto                 |
| GET    | `/api/v1/Products/{id}`      | Retorna os detalhes de um produto específico |
| PUT    | `/api/v1/Products/{id}`      | Atualiza as informações de um produto |
| DELETE | `/api/v1/Products/{id}`      | Exclui um produto                    |

#### Sales
| Método | Endpoint                                           | Descrição                                          |
|--------|----------------------------------------------------|--------------------------------------------------|
| GET    | `/api/v1/Sales`                                    | Retorna a lista de vendas                        |
| POST   | `/api/v1/Sales`                                    | Cria uma nova venda                              |
| GET    | `/api/v1/Sales/{id}`                               | Retorna os detalhes de uma venda específica      |
| PUT    | `/api/v1/Sales/{id}`                               | Atualiza as informações de uma venda             |
| DELETE | `/api/v1/Sales/{id}`                               | Exclui uma venda                                 |
| PUT    | `/api/v1/Sales/{id}/cancel`                        | Cancela uma venda                                |
| PUT    | `/api/v1/Sales/{id}/Items/{sequence}/cancel`      | Cancela um item específico dentro de uma venda   |
| GET    | `/api/v1/Sales/{id}/Items/{sequence}`              | Retorna os detalhes de um item específico dentro de uma venda |

---

Aqui está a versão revisada da seção sobre a arquitetura de eventos com RabbitMQ, focando apenas nos exemplos de payload de cada evento, conforme solicitado.

---

### Arquitetura de Eventos: Integração com RabbitMQ

Nesta seção, explicamos a integração da aplicação com RabbitMQ, utilizando a arquitetura de Pub/Sub para eventos relacionados a vendas. A *exchange* utilizada é `ex_sale`, do tipo *direct*, e os consumidores podem criar suas próprias filas e fazer o *bind* com as *routing keys* dos eventos que desejam consumir. Essa abordagem garante flexibilidade e permite que diferentes serviços processem eventos de maneira independente.

#### Exchange: ex_sale
- **Tipo:** Direct

### Eventos e Exemplos de Payload

#### 1. **SaleCancelledEvent**
- **Routing Key:** `SaleCancelledEvent`
  
  Este evento é disparado quando uma venda é cancelada.

  **Exemplo de Payload:**
  ```json
  {
      "Id": 1,
      "CancelledAt": "2024-10-24T15:30:00Z"
  }
  ```

#### 2. **SaleCreatedEvent**
- **Routing Key:** `SaleCreatedEvent`
  
  Este evento é disparado quando uma nova venda é criada.

  **Exemplo de Payload:**
  ```json
  {
      "Id": 1,
      "Date": "2024-10-24T14:00:00Z"
  }
  ```

#### 3. **SaleItemCancelledEvent**
- **Routing Key:** `SaleItemCancelledEvent`
  
  Este evento é disparado quando um item de uma venda é cancelado.

  **Exemplo de Payload:**
  ```json
  {
      "SaleId": 1,
      "SaleItemId": 2,
      "Sequence": 1,
      "CancelledAt": "2024-10-24T15:00:00Z"
  }
  ```

#### 4. **SaleUpdatedEvent**
- **Routing Key:** `SaleUpdatedEvent`
  
  Este evento é disparado quando uma venda existente é atualizada.

  **Exemplo de Payload:**
  ```json
  {
      "Id": 1,
      "UpdatedAt": "2024-10-24T16:00:00Z"
  }
  ```

### Estrutura do Pub/Sub

Com a arquitetura Pub/Sub, os consumidores têm a flexibilidade de criar suas próprias filas e fazer o *bind* com as *routing keys* dos eventos de seu interesse. Isso permite que cada consumidor receba somente os eventos relevantes, otimizando o fluxo de processamento. Sempre que um evento é publicado na *exchange* `ex_sale`, todos os consumidores vinculados à *routing key* correspondente são notificados, possibilitando a execução de ações específicas para cada tipo de evento.

---

### Como Executar o Projeto

#### Pré-requisitos
1. **.NET Core 6 SDK** - Certifique-se de ter o SDK do .NET Core 6 instalado. Você pode baixá-lo [aqui](https://dotnet.microsoft.com/download/dotnet/6.0).
2. **SQL Server** - É necessário ter uma instância do SQL Server rodando.
3. **RabbitMQ** - O projeto usa o RabbitMQ, portanto, você precisará configurar o serviço com suas próprias credenciais.

#### Configuração Inicial
1. **Clonar o repositório**
   ```bash
   git clone https://github.com/LucasFernandes0101/123vendas-server.git
   cd 123vendas-server
   ```

2. **Configurar o banco de dados**
   Verifique a string de conexão no arquivo `launchSettings.json`:
   ```json
   "SQL_CONNECTION_STRING": "server=localhost;database=123vendas_db;user=root;password=root;Integrated Security=SSPI;TrustServerCertificate=True"
   ```
   Ajuste a string conforme necessário para seu ambiente de SQL Server.

3. **Rodar as Migrations**
   Para criar o banco de dados e aplicar as migrations, navegue até o diretório `123vendas-server/src/123vendas.Infrastructure` e execute os seguintes comandos no terminal:
   ```bash
   dotnet ef database update
   ```
   Este comando irá criar o banco de dados e aplicar todas as migrações definidas.

4. **Configurar RabbitMQ**
   Certifique-se de que o RabbitMQ está configurado corretamente. Adicione suas credenciais de RabbitMQ no arquivo `launchSettings.json` na seção de

 configurações de ambiente.

#### Executando o Projeto
1. **Iniciar o projeto**
   No diretório raiz do projeto, execute o comando:
   ```bash
   dotnet run
   ```

2. **Acessar a API**
   A API estará disponível em `https://localhost:5001/swagger/index.html` para interação via Swagger.

#### Variáveis de Ambiente
As seguintes variáveis de ambiente podem ser configuradas para personalizar o comportamento da aplicação:
- `ASPNETCORE_ENVIRONMENT`: Defina o ambiente da aplicação (`Development`, `Staging`, `Production`).
- `SQL_CONNECTION_STRING`: String de conexão com o banco de dados.
- `RABBITMQ_HOST`: Host do RabbitMQ.
- `RABBITMQ_USERNAME`: Nome de usuário para acessar o RabbitMQ.
- `RABBITMQ_PASSWORD`: Senha para acessar o RabbitMQ.
