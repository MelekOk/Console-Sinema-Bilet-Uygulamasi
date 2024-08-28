using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaSalonuProject.Process
{
    internal class HallProccess
    {
        private string connectionString = "Data Source=NIRVANA;Initial Catalog=SinemaBiletSatisDb;Integrated Security=True";
        public SqlConnection db;
        public void ListHalls()
        {
            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("ListSeanslar", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                          Seans Listesi                     ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
                    while (reader.Read())
                    {
                        Console.WriteLine($"Seans Id: {reader["Id"]}, Film Id: {reader["FilmId"]}, Hall Id: {reader["HallId"]}, Price: {reader["Price"]}, Date:  {reader["Date"]}, Hour:  {reader["Hour"]}");
                    }

                    Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    db.Close();
                }
            }

            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
        public void AddHall()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           Seans Ekle                       ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Film Id: ");
            int filmId = Convert.ToInt32(Console.ReadLine());
            Console.Write("║ Hall Id: ");
            int hallId = int.Parse(Console.ReadLine());
            Console.Write("║ Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("║ Date: ");
            string date = Console.ReadLine();
            Console.Write("║ Hour: ");
            string hour = Console.ReadLine();
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("SeanslarInsertIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FilmId", filmId);
                    cmd.Parameters.AddWithValue("@SalonId", hallId);
                    cmd.Parameters.AddWithValue("@Fiyat", price);
                    cmd.Parameters.AddWithValue("@Tarih", date);
                    cmd.Parameters.AddWithValue("@Saat", hour);


                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ Seans ekleme başarılı.                        ║");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    db.Close();
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
        public void UpdateHall()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        Seans Güncelleme                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Seans Id: ");
            int seansId = Convert.ToInt32(Console.ReadLine());
            Console.Write("║ Film Id: ");
            int filmId = Convert.ToInt32(Console.ReadLine());
            Console.Write("║ Hall Id: ");
            int hallId = int.Parse(Console.ReadLine());
            Console.Write("║ Price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Console.Write("║ Date: ");
            string date = Console.ReadLine();
            Console.Write("║ Hour: ");
            string hour = Console.ReadLine();
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("SeanslarUpdateIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeansId", seansId);
                    cmd.Parameters.AddWithValue("@FilmId", filmId);
                    cmd.Parameters.AddWithValue("@SalonId", hallId);
                    cmd.Parameters.AddWithValue("@Fiyat", price);
                    cmd.Parameters.AddWithValue("@Tarih", date);
                    cmd.Parameters.AddWithValue("@Saat", hour);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ Seans Bilgileri Güncellendi.                          ║");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    db.Close();
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
        public void DeleteHall()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         Seans Sil                          ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Seans ID: ");
            int seansId = int.Parse(Console.ReadLine());
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("SeanslarDeleteIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SeansId", seansId);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ seans silindi.                          ║");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    db.Close();
                }
            }

            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Devam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
