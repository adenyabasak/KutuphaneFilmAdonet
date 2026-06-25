using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using KutuphaneFilmAdonet.Models;

namespace KutuphaneFilmAdonet.Pages.Kitaplar
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Kitap> KitapListesi { get; set; } = new List<Kitap>();

        public void OnGet(string search)
        {
            string connStr = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT * FROM Kitaplar";

                if (!string.IsNullOrEmpty(search))
                {
                    query += " WHERE KitapAdi LIKE @search";
                }

                SqlCommand cmd = new SqlCommand(query, conn);

                if (!string.IsNullOrEmpty(search))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + search + "%");
                }

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Kitap k = new Kitap
                    {
                        KitapId = Convert.ToInt32(reader["KitapId"]),
                        KitapAdi = reader["KitapAdi"].ToString(),
                        Yazar = reader["Yazar"].ToString(),
                        SayfaSayisi = Convert.ToInt32(reader["SayfaSayisi"])
                    };

                    KitapListesi.Add(k);
                }
            }
        }
    }
}