# 123vendas - Plataforma de Gerenciamento de Vendas

## Sumário
- [Objetivo do Projeto](#objetivo-do-projeto)
- [Tecnologias e Padrões Utilizados](#tecnologias-e-padrões-utilizados)
- [Entidades de Negócio](#entidades-de-negócio)
- [Estrutura do Banco de Dados](#estrutura-do-banco-de-dados)
- [Arquitetura de Eventos: Integração com RabbitMQ](#arquitetura-de-eventos-integração-com-rabbitmq)
- [Execução do Projeto](#execução-do-projeto)
- [Pontos de Melhoria](#pontos-de-melhoria)

## Objetivo do Projeto
O **123vendas** é uma plataforma inovadora que facilita o gerenciamento de vendas, produtos e clientes para empresas com múltiplas filiais. A solução centraliza a gestão de produtos e clientes, permitindo personalizar estoque e preços de acordo com cada filial. Com uma interface robusta, o sistema suporta todo o ciclo de vendas, desde a criação e atualização de pedidos até o cancelamento e a administração do estoque.

Este projeto demonstra o uso de tecnologias e padrões modernos, evidenciando a aplicação de DDD, autenticação e autorização com JWT, integração com RabbitMQ para eventos de vendas e uma arquitetura de software escalável e de fácil manutenção.

## Tecnologias e Padrões Utilizados
- **.NET 8**: Base para desenvolvimento de aplicações escaláveis e robustas.
- **MediatR**: Utilizado para promover comunicação desacoplada. O padrão foi aplicado especificamente na parte de usuários e autenticação.
- **Fluent Validations**: Validação de dados de forma fluida e intuitiva.
- **BCrypt**: Criptografia segura para senhas.
- **Middleware de Exception**: Gerenciamento centralizado de exceções com respostas HTTP apropriadas.
- **DDD (Domain-Driven Design)**: Estruturação do sistema com foco no domínio do negócio.
- **JWT**: Autenticação e autorização baseada em roles.
- **Serilog**: Logging estruturado para monitoramento e diagnóstico.
- **Automapper**: Mapeamento automático entre entidades e DTOs.
- **Microsoft.AspNetCore.Mvc.Versioning**: Gerenciamento de versões da API.
- **Swagger**: Documentação interativa e acessível.
- **Entity Framework Core**: Mapeamento objeto-relacional com configurações via IEntityTypeConfiguration.
- **IQueryable**: Consultas dinâmicas e otimizadas.
- **RabbitMQ.Client**: Integração com RabbitMQ para comunicação via eventos.

### Testes Unitários
- **xUnit**: Framework de testes para .NET, garantindo modularidade e facilidade na escrita de testes.
- **FluentAssertions**: Sintaxe fluida para asserções, tornando os testes mais legíveis.
- **Shouldly**: Melhor legibilidade para mensagens de erro em testes.
- **Bogus**: Geração de dados fake para testes unitários.
- **NSubstitute**: Mocking de dependências para facilitar testes isolados.

## Entidades de Negócio
O projeto foi concebido com os princípios do Domain-Driven Design, onde as entidades representam componentes essenciais do domínio de vendas e gestão. Em vez de expor os detalhes das classes, destacam-se os seguintes aspectos:

- **Entidades Base**: Todas as entidades compartilham atributos comuns, como identificador único, controle de exclusão lógica e registros de criação/atualização, garantindo rastreabilidade e consistência.
- **Filiais e Produtos**: As filiais são mapeadas para possibilitar a customização de preços e controle de estoque por unidade. Os produtos são associados às filiais, permitindo a gestão centralizada com flexibilidade local.
- **Carrinho de Compras**: Modela a seleção de produtos realizada pelos usuários, mantendo a relação entre os itens escolhidos e suas quantidades.
- **Vendas e Itens de Venda**: Capturam os detalhes de cada transação, incluindo o estado da venda, itens vendidos e a possibilidade de cancelamento parcial ou total.
- **Usuários**: Armazenam informações de acesso e dados pessoais, definindo os papéis (roles) que determinam as permissões de cada usuário.

Cada entidade foi cuidadosamente desenhada para refletir as regras e necessidades do negócio, estabelecendo relações claras e integridade no modelo de dados.

## Estrutura do Banco de Dados
O projeto utiliza o **Postgres** como banco de dados relacional. A estrutura é definida utilizando o **Entity Framework Core** com configurações modulares via `IEntityTypeConfiguration`, promovendo um design limpo e organizado.

Abaixo está o diagrama do banco de dados utilizado neste projeto:

![Diagrama_DB_123Vendas](https://github.com/user-attachments/assets/2bbb0886-3591-4ead-bed4-2d9dc7111b71)
> **Nota:** Este diagrama representa a estrutura do banco e pode ser atualizado conforme necessário.

No arquivo **PostgreDbContext**, o construtor garante que o banco seja criado automaticamente se não existir:
```csharp
public PostgreDbContext(DbContextOptions<PostgreDbContext> options) : base(options)
{
    base.Database.EnsureCreated();
}
```

## Arquitetura de Eventos: Integração com RabbitMQ
A aplicação integra-se ao **RabbitMQ** utilizando uma arquitetura de Pub/Sub, permitindo o processamento distribuído e independente dos eventos de vendas. A exchange **ex_sale** (tipo **direct**) possibilita que os consumidores criem filas customizadas e se vinculem às routing keys específicas dos eventos de seu interesse.

### Detalhes dos Eventos
- **SaleCancelledEvent**  
  - **Routing Key**: `SaleCancelledEvent`  
  - **Descrição**: Disparado quando uma venda é cancelada.  
  - **Payload Exemplo**:

    ```json
    {
        "Id": 1,
        "CancelledAt": "2024-10-24T15:30:00Z"
    }
    ```

- **SaleCreatedEvent**  
  - **Routing Key**: `SaleCreatedEvent`  
  - **Descrição**: Disparado quando uma nova venda é criada.  
  - **Payload Exemplo**:
    
    ```json
    {
        "Id": 1,
        "Date": "2024-10-24T14:00:00Z"
    }
    ```

- **SaleItemCancelledEvent**  
  - **Routing Key**: `SaleItemCancelledEvent`  
  - **Descrição**: Disparado ao cancelar um item específico de uma venda.  
  - **Payload Exemplo**:
    
    ```json
    {
        "SaleId": 1,
        "SaleItemId": 2,
        "Sequence": 1,
        "CancelledAt": "2024-10-24T15:00:00Z"
    }
    ```

- **SaleUpdatedEvent**  
  - **Routing Key**: `SaleUpdatedEvent`  
  - **Descrição**: Disparado ao atualizar uma venda existente.  
  - **Payload Exemplo**:
    
    ```json
    {
        "Id": 1,
        "UpdatedAt": "2024-10-24T16:00:00Z"
    }
    ```

Essa arquitetura garante que cada serviço consuma apenas os eventos relevantes, otimizando a performance e facilitando a escalabilidade.

## Execução do Projeto

Para executar o projeto, é necessário configurar as seguintes variáveis de ambiente, que definem o comportamento e as conexões com os serviços externos utilizados pela aplicação. **Observação:** Para execução local, essas variáveis são configuradas no arquivo `launchSettings.json`.

```json
"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development",
  "JWT_SECRETKEY": "dR8!v9Kp@zL3xWq#N5gT7mYb$FcJ2sV0",
  "POSTGRES_CONNECTION_STRING": "",
  "RABBITMQ_HOSTNAME": "",
  "RABBITMQ_USERNAME": "",
  "RABBITMQ_VIRTUALHOST": "",
  "RABBITMQ_PASSWORD": ""
}
```

### Descrição de Cada Variável

- **ASPNETCORE_ENVIRONMENT**: Define o ambiente em que a aplicação será executada (por exemplo, Development, Staging ou Production). Isso influencia configurações específicas, como logging e detalhes de erros.
- **JWT_SECRETKEY**: Chave secreta utilizada para assinar e validar os tokens JWT, garantindo a integridade e a segurança do mecanismo de autenticação.
- **POSTGRES_CONNECTION_STRING**: String de conexão para o banco de dados Postgres, configurando o endereço do servidor, nome do banco de dados, credenciais de acesso e outras opções necessárias para a conexão.
- **RABBITMQ_HOSTNAME**: Nome do host ou endereço IP do servidor RabbitMQ, utilizado para publicar e consumir eventos.
- **RABBITMQ_USERNAME**: Nome de usuário para autenticação no servidor RabbitMQ.
- **RABBITMQ_VIRTUALHOST**: Virtual host no RabbitMQ, permitindo a separação lógica de ambientes ou aplicações no mesmo servidor.
- **RABBITMQ_PASSWORD**: Senha correspondente ao usuário definido para acessar o RabbitMQ.

> **Observações:**
> - **Postgres**: Certifique-se de ter o Postgres instalado e devidamente configurado na sua máquina.
> - **PostgreDbContext**: O construtor do `PostgreDbContext` contém a chamada `base.Database.EnsureCreated();`, garantindo que o banco de dados seja criado automaticamente se ainda não existir.
> - **JWT Secret Key**: A chave secreta do JWT pode ser alterada conforme necessário no arquivo `launchSettings.json`.

### Passos para Iniciar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/LucasFernandes0101/123vendas-server.git
   ```
2. Navegue até a pasta do projeto:
   ```bash
   cd 123vendas-server
   ```
3. Execute o projeto:
   ```bash
   dotnet run
   ```
   
## Pontos de Melhoria
Para aprimorar a segurança, a escalabilidade e a manutenibilidade do projeto, considere os seguintes pontos:
- **Key Vault**: Migrar a secret key do JWT, bem como as credenciais do banco de dados e do RabbitMQ, para um Key Vault. Dessa forma, informações sensíveis são gerenciadas com segurança.
- **Configurações Centralizadas**: Adotar um gerenciador de configurações centralizado para facilitar a manutenção e o deploy em diferentes ambientes.
- **Testes Automatizados**: Expandir a cobertura dos testes unitários, funcionais e de integração, garantindo a robustez e confiabilidade do sistema.
- **Monitoramento e Logging**: Integrar ferramentas avançadas de monitoramento e logging para identificar e resolver problemas de forma proativa.
- **Escalabilidade do Pub/Sub**: Revisar e otimizar a arquitetura de eventos para suportar um volume maior de transações e múltiplos serviços consumidores.
