using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Filmler
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Film Film { get; set; }

        public IActionResult OnPost()
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "INSERT INTO Filmler (FilmAdi, Yonetmen, Sure) VALUES (@FilmAdi, @Yonetmen, @Sure)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FilmAdi", Film.FilmAdi);
                cmd.Parameters.AddWithValue("@Yonetmen", Film.Yonetmen);
                cmd.Parameters.AddWithValue("@Sure", Film.Sure);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("Index");
        }
    }
}