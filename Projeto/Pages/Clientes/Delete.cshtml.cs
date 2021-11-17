using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Core.Entity;
using Projeto.Core.Infrastructure.Exceptions;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Clientes
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Cliente Cliente { get; set; }

        public string Erro { get; set; }

        public void OnGet(int id)
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            this.Cliente = connection.Query<Cliente>("SELECT id, nome, data_nascimento as dataNascimento, email FROM cliente WHERE id = @Id", new { Id = id }).FirstOrDefault();
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new BusinessException("Parametro inválido");
                }

                using var connection = new SQLiteConnection($"Data Source=banco.db");

                connection.Execute("DELETE FROM cliente WHERE id = @Id", new { Id = id });

                // TODO: exibir mensagem de "Registro excluído com sucesso."

                return RedirectToPage("./Index");
            }
            catch (System.Exception ex)
            {

                this.Erro = ex.Message;
            }

            return Page();
        }
    }
}
