
# Considerações

1. O código foi escrito todo em inglês, incluindo as mensagens de validações, erros e documentações.
2. Para validar os tokens para autenticação nas APIs, foi utilizado o JWKS e foi implementada na Authentication-Api.
3. Sobre a arquitetura do projeto, adicionei uma api para tratar ações relacionadas a autenticação e informações do usuário.
4. Não foram aplicados testes unitários e testes de integração.
5. Foram realizados ajustes no script para que seja possível utilizar em um banco de dados Oracle.

Na tabela **CONTACORRENTE** removi os campos ```salt``` e ```senha``` pois como criei uma API para manter e autenticar usuários,o próprio AspNetIdentity ao criar um usuário  já realiza a geração do salt aleatório por usuário e cria o hash da senha como salt + algoritmo de hash forte e serializa em uma única string no campo passwordHash. Também, existe o suporte a verificação do password onde ele considera a senha e o salt.

Na tabela **CONTACORRENTE** adicionei o campo ```idusuario``` para criar o vínculo entre a conta corrente e o usuário; é um vínculo contextual e faz sentido que esta informação esteja no agregado relacionado a contas correntes. Para o contrário, entendo que que não faria muito sentido para a responsabilidade da API de autenticação e também, para as alterações que fiz, trafegar o id do usuário no token é o suficiente para trabalhar com as necessidades da Account API.

Na tabela **CONTACORRENTE** o campo ```ativo``` estão como padrão 0 (desativado), entendo que deve ser 1 (ativado) pois existe uma opção para inativação da conta e tem mais sentido que a conta esteja ativa ao criar já que não foi exigido um critério para ativação da conta.

# Frameworks e Ferramentas principais utilizados

1. Linguagem de programação C# e projeto WebAPI na plataforma .NET 8
2. Mediatr
3. FluentValidation
4. AspNetIdentity
5. Entity Framework Core
6. Banco de dados Oracle
7. Swagger (Documentação)

# Estrutura do projeto

O projeto e estruturado com as seguintes camadas

1. Application: Ocorre a orquestração dos fluxos relacionados às regras de negócios e algumas especificações de serviços relacionados ao domínio.
2. Core: O core da aplicação, nela o domínio do negócio e abstrações são definidas.
3. Infrastructure: São realizadas as especificações das abstrações de serviços, repositórios e algumas configurações do projeto.
4. Infrastructure.CrossCutting: Contém as classes que compôem a base para o funcionamento do projeto.

# Documentação da API

## Authentication API

<img width="1867" height="1207" alt="image" src="https://github.com/user-attachments/assets/92423b1c-5ba1-4884-bb4c-e4e037aa227a" />

<img width="1674" height="1803" alt="image" src="https://github.com/user-attachments/assets/5b4b4f91-3bf9-44c1-a5e6-ca51e32cd082" />

## Account API

<img width="1852" height="1032" alt="image" src="https://github.com/user-attachments/assets/f66cc472-ac26-4f83-b317-9ea8b0fcbb0e" />

<img width="1760" height="1877" alt="image" src="https://github.com/user-attachments/assets/adddf505-f81e-49c3-9685-978ae642949a" />

<img width="1765" height="1787" alt="image" src="https://github.com/user-attachments/assets/d314d8d2-b8b6-40f5-8987-6122c1e18ef2" />
