using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Filmler
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Film Film { get; set; }

        public void OnGet(int id)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM Filmler WHERE FilmId=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Film = new Film
                    {
                        FilmId = Convert.ToInt32(reader["FilmId"]),
                        FilmAdi = reader["FilmAdi"].ToString(),
                        Yonetmen = reader["Yonetmen"].ToString(),
                        Sure = Convert.ToInt32(reader["Sure"])
                    };
                }
            }
        }

        public IActionResult OnPost()
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"UPDATE Filmler 
                                 SET FilmAdi=@FilmAdi, 
                                     Yonetmen=@Yonetmen, 
                                     Sure=@Sure
                                 WHERE FilmId=@FilmId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@FilmId", Film.FilmId);
                cmd.Parameters.AddWithValue("@FilmAdi", Film.FilmAdi);
                cmd.Parameters.AddWithValue("@Yonetmen", Film.Yonetmen);
                cmd.Parameters.AddWithValue("@Sure", Film.Sure);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("Index");
        }
    }
}