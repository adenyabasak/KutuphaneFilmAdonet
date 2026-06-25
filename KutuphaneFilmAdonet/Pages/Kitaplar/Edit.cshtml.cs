using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Kitaplar
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Kitap Kitap { get; set; }

        public void OnGet(int id)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM Kitaplar WHERE KitapId=@id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Kitap = new Kitap
                    {
                        KitapId = Convert.ToInt32(reader["KitapId"]),
                        KitapAdi = reader["KitapAdi"].ToString(),
                        Yazar = reader["Yazar"].ToString(),
                        SayfaSayisi = Convert.ToInt32(reader["SayfaSayisi"])
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

                string query = @"UPDATE Kitaplar 
                                 SET KitapAdi=@KitapAdi, 
                                     Yazar=@Yazar, 
                                     SayfaSayisi=@SayfaSayisi
                                 WHERE KitapId=@KitapId";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@KitapId", Kitap.KitapId);
                cmd.Parameters.AddWithValue("@KitapAdi", Kitap.KitapAdi);
                cmd.Parameters.AddWithValue("@Yazar", Kitap.Yazar);
                cmd.Parameters.AddWithValue("@SayfaSayisi", Kitap.SayfaSayisi);

                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("Index");
        }
    }
}