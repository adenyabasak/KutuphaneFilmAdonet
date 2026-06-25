using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace KutuphaneFilmAdonet.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int KitapSayisi { get; set; }
        public int FilmSayisi { get; set; }

        public void OnGet()
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Kitaplar", conn);
                KitapSayisi = (int)cmd1.ExecuteScalar();

               
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Filmler", conn);
                FilmSayisi = (int)cmd2.ExecuteScalar();
            }
        }
    }
}