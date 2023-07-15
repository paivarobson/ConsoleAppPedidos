# Sistema de Pedidos
Este é um sistema de pedidos que permite cadastrar produtos, criar pedidos, consultar produtos e imprimir detalhes dos pedidos. O sistema foi desenvolvido em C# e utiliza tecnologias como .NET Core 7, Entity Framework Core e SQL Server.

# Arquitetura, Princípios e Padrões Utilizados

Arquitetura: Aplicação baseada em camadas.
Padrões Arquiteturais: DDD (Domain-Driven Design) - Implementação parcial, separando as entidades em suas respectivas classes e definindo interfaces para os repositórios e serviços relacionados a essas entidades.
Injeção de Dependência: O projeto utiliza o recurso de injeção de dependência para gerenciar a criação e resolução de dependências entre os componentes.
Princípios de Design: S.O.L.I.D (Single Responsibility Principle, Open/Closed Principle, Liskov Substitution Principle, Interface Segregation Principle, Dependency Inversion Principle). Foram aplicados na arquitetura e no design do sistema, buscando criar código limpo, de fácil manutenção e com baixo acoplamento.
Repositórios: Foi adotado o padrão Repository para separar a lógica de acesso a dados da lógica de negócio, facilitando a manutenção e a testabilidade do sistema.
Camada de Serviços: Utilização de serviços para encapsular a lógica de negócio relacionada às entidades.
Padrão MVC: A arquitetura do sistema segue o padrão MVC (Model-View-Controller), separando as responsabilidades de modelagem dos dados, apresentação e controle das interações.
Acesso a Dados: Utilização do Entity Framework Core para interagir com o banco de dados SQL Server.
Migrações: Utilizado migrações do Entity Framework Core (Migrations) para controlar as alterações no esquema do banco de dados. Possibilitando atualizar o banco de dados de forma consistente e automatizada, mantendo o histórico das alterações realizadas.
Documentação: Inclusão de documentação nos métodos e interfaces para descrever a funcionalidade, parâmetros e exceções lançadas.
Data Annotations: Utilizado as Data Annotations para aplicar validações e restrições aos modelos de dados, garantindo a integridade dos dados armazenados e facilitando a validação dos inputs fornecidos pelos usuários.
