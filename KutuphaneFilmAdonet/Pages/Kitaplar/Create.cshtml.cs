using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Kitaplar
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Kitap Kitap { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "INSERT INTO Kitaplar (KitapAdi, Yazar, SayfaSayisi) VALUES (@KitapAdi, @Yazar, @SayfaSayisi)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@KitapAdi", Kitap.KitapAdi);
                cmd.Parameters.AddWithValue("@Yazar", Kitap.Yazar);
                cmd.Parameters.AddWithValue("@SayfaSayisi", Kitap.SayfaSayisi);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("Index");
        }
    }
}