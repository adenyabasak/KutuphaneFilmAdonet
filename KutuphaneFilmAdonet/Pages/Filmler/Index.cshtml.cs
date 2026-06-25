using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Filmler
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Film> FilmListesi { get; set; } = new List<Film>();

        public void OnGet(string search)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM Filmler";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE FilmAdi LIKE @search";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Film film = new Film
                    {
                        FilmId = Convert.ToInt32(reader["FilmId"]),
                        FilmAdi = reader["FilmAdi"].ToString(),
                        Yonetmen = reader["Yonetmen"].ToString(),
                        Sure = Convert.ToInt32(reader["Sure"])
                    };

                    FilmListesi.Add(film);
                }
            }
        }
    }
}