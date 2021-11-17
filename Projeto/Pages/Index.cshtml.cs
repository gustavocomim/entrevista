using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Database;
using Projeto.Core.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICrudService<Tarefa> _crudService;

        private const string DBNAME = "banco.db";

        [BindProperty]
        public List<Tarefa> Tarefas { get; set; }

        public IndexModel(
            ILogger<IndexModel> logger,
            IHostingEnvironment hostingEnvironment,
            ICrudService<Tarefa> crudService)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _crudService = crudService;
        }

        public void OnGet()
        {
            // TODO: O código abaixo parece estar no lugar errado, refatore esse trecho (use as melhores práticas que puder).

            if (!System.IO.File.Exists(DBNAME))
            {
                SQLiteConnection.CreateFile("banco.db");
            }

            using var connection = new SQLiteConnection($"Data Source=banco.db");

            var migrationTable = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'migration'").FirstOrDefault();

            if (string.IsNullOrEmpty(migrationTable))
            {
                connection.Execute("CREATE TABLE migration (code TEXT PRIMARY KEY)");
            }
            
            // TODO: Comente como a aplicação está controlando para não passar por uma mesma instrução de CREATE TABLE.

            var migrations = connection.Query<string>("SELECT code FROM migration").ToList();

            if (!migrations.Contains("m1"))
            {
                connection.Execute(
                    @"CREATE TABLE cliente (
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       nome             TEXT        NOT NULL,
                       data_nascimento  DATETIME    NOT NULL
                    )");

                connection.Execute("INSERT INTO migration VALUES('m1')");
            }

            if (!migrations.Contains("m2"))
            {
                connection.Execute(
                    @"ALTER TABLE cliente ADD email TEXT");

                connection.Execute("INSERT INTO migration VALUES('m2')");
            }

            if (!migrations.Contains("seed1"))
            {
                string sql = "INSERT INTO cliente (nome, data_nascimento, email) Values (@Nome, @DataNascimento, @Email);";

                connection.Execute(sql,
                    new[]
                    {
                        new Cliente() { Nome = "Joaquim Ferreira", Email = "joaquim.ferreira@evup.com.br", DataNascimento = new DateTime(1990, 01, 17) },
                        new Cliente() { Nome = "Maria Fernandes", Email = "maria.fernandes@evup.com.br", DataNascimento = new DateTime(1993, 03, 21) },
                        new Cliente() { Nome = "Guilmar Moreira", Email = "joaquimguilmar.moreira@evup.com.br", DataNascimento = new DateTime(1994, 05, 05) },
                        new Cliente() { Nome = "Robson Silvano", Email = "robson.silvano@evup.com.br", DataNascimento = new DateTime(2000, 05, 10) },
                        new Cliente() { Nome = "Anderson da Silva", Email = "anderson.silva@evup.com.br", DataNascimento = new DateTime(2001, 08, 17) },
                        new Cliente() { Nome = "Elaine Juscen", Email = "elaine.juscen@evup.com.br", DataNascimento = new DateTime(2001, 10, 21) },
                        new Cliente() { Nome = "Beatriz Filippa", Email = "beatriz.filippa@evup.com.br", DataNascimento = new DateTime(2002, 01, 11) },
                        new Cliente() { Nome = "Dorotti Galvão", Email = "dorotti.galvao@evup.com.br", DataNascimento = new DateTime(2002, 02, 01) },
                    });

                connection.Execute("INSERT INTO migration VALUES('seed1')");
            }

            if (!migrations.Contains("tarefa"))
            {
                ((TarefaService)_crudService).InjetarConexao(connection);

                connection.Execute(
                    @"CREATE TABLE tarefa (
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       tipo       TEXT       NOT NULL,
                       descricao  TEXT       NOT NULL,
                       concluida  BOOLEAN    NOT NULL
                    )");

                _crudService.Insert(new Tarefa("💄style", "O formulário de edição de clientes está sendo exibido na direita da tela. Coloque-o logo abaixo do título;"));
                _crudService.Insert(new Tarefa("💄style", "No formulário de exclusão de clientes, altere a cor do botão de azul para vermelho;"));
                _crudService.Insert(new Tarefa("🐛bug", "Na edição do cadastro de clientes, o campo e-mail está com uma restrição para aceitar apenas 12 caracteres, aumente para 120;"));
                _crudService.Insert(new Tarefa("🐛bug", "Na listagem do cadastro de clientes, a edição está apresentando um comportamento estranho, os dados apresentados nem sempre representam o registro selecionado. Corrija esse problema;"));
                _crudService.Insert(new Tarefa("🐛bug", "Na edição do cadastro de clientes, o campo data de nascimento não está sendo atualizado. Corrija este problema;"));
                _crudService.Insert(new Tarefa("✨feature", "O cadastro de clientes está incompleto, inclua os campos telefone, cidade e gênero;"));
                _crudService.Insert(new Tarefa("✨feature", "Inclua as colunas faltantes na exibição da listagem de clientes (data de nascimento, telefone, cidade e gênero);"));
                _crudService.Insert(new Tarefa("♻️refactor", "As migrations (controle de criação e atualização das tabelas do banco de dados) estão em um local inapropriado no código fonte. Faça um melhor gerenciamento para isso;")); 
                _crudService.Insert(new Tarefa("♻️refactor", "Encapsule todo código fonte inerente a regras de negócio dentro de classes de serviço (reaproveite o máximo de código que puder);"));
                _crudService.Insert(new Tarefa("♻️refactor", "A conexão com o banco de dados em toda a aplicação está sendo feita de forma redundante. Faça um melhor gerenciamento para isso;"));
                _crudService.Insert(new Tarefa("♻️refactor", "Aplique injeção de dependência para fornecer os serviços para a aplicação;"));
                _crudService.Insert(new Tarefa("✨feature", "Inclua a funcionalidade para cadastrar um novo cliente;"));
                _crudService.Insert(new Tarefa("💡comments", "Existem TODOs (tarefas pendentes) espalhadas pelo código fonte (bem como adicionar comentários em classes ou tratar algum erro). Localize esses itens e implemente a solução;"));
                _crudService.Insert(new Tarefa("✨feature", "Crie um sistema de log para a aplicação. Grave todas as vezes que um registro for alterado no sistema;"));
                _crudService.Insert(new Tarefa("✨feature", "Crie um cadastro de usuários para o sistema. O cadastro deve ter login, senha, nome, se é administrador ou não e se está ativo ou inativo. (não é necessário implementar edição ou exclusão de registros);"));
                _crudService.Insert(new Tarefa("✨feature", "Crie um campo de usuário responsável no cadastro de clientes, deve ser do tipo seleção e refletir os dados do cadastro de usuários;"));
                _crudService.Insert(new Tarefa("✨feature", "Implemente o login da aplicação, fazendo a autenticação dos usuários com base no cadastro de usuários;"));
                _crudService.Insert(new Tarefa("✨feature", "Garanta que apenas usuários administradores possam dar manutenção no cadastro de usuários;"));

                connection.Execute("INSERT INTO migration VALUES('tarefa')");
            }

            this.Tarefas = connection.Query<Tarefa>("SELECT id, tipo, descricao, concluida FROM tarefa").ToList();
        }

        public ActionResult OnPost()
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            string sql = "UPDATE tarefa SET concluida = @Concluida WHERE id = @Id";

            connection.Execute(sql, this.Tarefas);

            return Page();
        }
    }
}
