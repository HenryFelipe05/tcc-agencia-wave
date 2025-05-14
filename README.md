<h1 align="center">Website Agencia Wave</h1>

<div align="center">
  <strong>🚀 Este repositório contém o código-fonte do site da WAVE! 📚</strong>
</div>

<div align="center">
  <p>O projeto está sendo desenvolvido como parte do Trabalho de Conclusão de Curso (TCC) da ETEC Comendador João Rays.</p>
</div>

<br />
<br />

<h1 align="center">🔭 Visão Geral</h1>

<h3>Arquitetura do Projeto</h3>

Este projeto segue os princípios da **Arquitetura Limpa (Clean Architecture)**, com uma separação clara de responsabilidades em camadas.

<h3>Diagrama do Banco de Dados</h3>

![Diagrama do Banco de Dados](./agencia-wave-site/database/diagrams/diagrama-db-wave.jpg)

<h3>Ferramentas Utilizadas </h3>

### Design e Prototipação
  - **Figma**: Utilizado para o design e prototipação de interfaces.
  - **Photoshop**: Criação de templates e recursos gráficos.

### Banco de Dados
  - **ER/Studio**: Utilizado para modelagem de dados e diagramas ER.
  - **SQL Server**: Sistema de gerenciamento de banco de dados utilizado no projeto.
  - **SSMS (SQL Server Management Studio)**: Ferramenta de gerenciamento e administração do banco de dados.

### Back-end - Web API
  - **Dapper**: Micro ORM utilizado para facilitar as interações com o banco de dados.
  - **Dapper Contrib**: Extensão do Dapper para simplificação de operações CRUD.
  - **Entity Framework**: ORM utilizado para o mapeamento de objetos relacionais.
  - **JWT (JSON Web Token)**: Autenticação baseada em token para controle de acesso.
  - **C#**: Linguagem de programação utilizada no desenvolvimento da Web API.
  - **ASP.NET**: Framework para construção da Web API.

### Front-end
  - **Angular**: Framework front-end utilizado para construir a interface do usuário.
  - **Typescript**: Linguagem de programação que expande o JavaScript com tipagem estática.
  - **HTML**: Linguagem de marcação para estruturar as páginas web.
  - **CSS**: Linguagem de estilos para customizar a aparência das páginas web.

### Teste de API
  - **Swagger**: Ferramenta para documentar e testar a API de forma interativa.
  - **Postman**: Utilizado para testar e depurar as chamadas da API.

### IDEs
  - **Visual Studio Code**: Editor de código utilizado para o desenvolvimento front-end.
  - **Visual Studio Community 2022**: IDE utilizada para o desenvolvimento da Web API

<br />
<br />

<h1 align="center">🚀 Executando o Projeto</h1>

Siga as instruções abaixo para executar o projeto em seu ambiente local:

**Clone o repositório:**

   ```
   git clone https://github.com/HenryFelipe05/tcc-agencia-wave.git
   ```

<h2>Projeto wave-web</h2>

1. **Navegue até o diretório do projeto wave-web:**

   ```
   cd tcc-agencia-wave
   ```
   ```
   cd agencia-wave-site
   ```
   ```
   cd wave-web
   ```

2. **Instale as dependências:**

   ```
   npm i
   ```

3. **Execute o servidor:**

   ```
   ng serve
   ```

<br />

<h2>Projeto wave-api</h2>

2. **Navegue até o diretório do projeto wave-api:**

   ```
   cd tcc-agencia-wave
   ```
   ```
   cd agencia-wave-site
   ```
   ```
   cd wave-api
   ```
   
3. **Execute o script do banco de dados:**

   ```
   ng serve
   ```
   
4. **Configure a connection string:**

   <img src="./readme-img/appsettings.png" alt="appsettings.json">   

   <p>Altere a propriedade server da connection string para o servidor conectado no seu SSMS</p>
   <img src="./readme-img/serversql.png" alt="conexão com o sql server">  

**Agora você pode começar a utilizar a aplicação!!**

