 <h1>Projeto CRUD em C# utilizando MVC e SQL Server</h1>
 
<p>Este é um projeto de exemplo que implementa um sistema CRUD (Create, Read, Update, Delete) em C# usando a técnica Model-View-Controller (MVC) e uma base de dados SQL Server chamada Consultório, com duas tabelas: Médicos e Clientes.</p>

<h3><ul>Requisitos</ul></h3>
<li>Microsoft Visual Studio (ou outra IDE para C#)</li>
<li>SQL Server instalado</li>
<li>Conexão com o banco de dados Consultório configurada no projeto</li>
<li>Configuração do Projeto</li>
<li>Clone o repositório em sua máquina local.</li>
<li>Abra o projeto no Visual Studio.</li>
<br>

<p>Certifique-se de ter uma instância do SQL Server em execução e atualize as configurações de conexão com o banco de dados no arquivo Web.config ou no arquivo de configuração apropriado.</p>

  `<connectionStrings>`<br>
    `<add name="consultorio" connectionString="Server=.\SQLEXPRESS;Database=consultorio;Trusted_Connection=True;"/>` 
  `</connectionStrings>`
<br>
<h3>Estrutura do Banco de Dados</h3>
<p>O banco de dados Consultório possui duas tabelas:</p>

<ul><strong>Tabela Medicos</strong></ul>
<li>Id: Identificador único do médico (chave primária)</li>
<li>Nome: Nome do médico</li>
<li>CRM: Número de registro do médico</li>
<li>Especialidade: Especialidade do médico</li>
<br>
<ul><strong>Tabela Clientes</strong></ul>
<li>Id: Identificador único do cliente (chave primária)</li>
<li>Nome: Nome do cliente</li>
<li>DataNascimento: Data de nascimento do cliente</li>

<h3>Funcionalidades do Projeto</h3>
<ul><strong>Este projeto oferece as seguintes funcionalidades:</strong></ul>
<li>Listar todos os médicos e clientes.</li>
<li>Visualizar informações de um médico ou cliente específico por meio do seu ID.</li>
<li>Adicionar novos médicos ou clientes.</li>
<li>Atualizar os dados de um médico ou cliente específico por meio do seu ID.</li>
<li>Excluir um médico ou cliente específico por meio do seu ID.</li>

<h3>Uso do Projeto</h3>
Para utilizar as funcionalidades, siga as instruções do arquivo Program.cs ou do controlador correspondente a cada operação CRUD.

<h3>Contribuindo</h3>
Sinta-se à vontade para contribuir para este projeto. Se encontrar problemas ou tiver sugestões, abra uma issue ou envie um pull request.
