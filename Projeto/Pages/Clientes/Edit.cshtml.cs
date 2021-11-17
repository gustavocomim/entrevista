using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto.Core.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Linq;

namespace Projeto.Pages.Clientes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Cliente Cliente { get; set; }

        public string Erro { get; set; }

        public void OnGet(int id)
        {
            using var connection = new SQLiteConnection($"Data Source=banco.db");

            this.Cliente = connection.Query<Cliente>("SELECT id, nome, data_nascimento as dataNascimento, email FROM cliente").FirstOrDefault();

        }

        public IActionResult OnPost(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                using var connection = new SQLiteConnection($"Data Source=banco.db");

                connection.Execute("UPDATE cliente SET nome = @Nome, email = @Email WHERE id = @Id", this.Cliente);

                // TODO: exibir mensagem de "Registro atualizado com sucesso."

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
