using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaSalonuProject.Process
{
    internal class BiletProccess
    {
        private string connectionString = "Data Source=NIRVANA;Initial Catalog=SinemaBiletSatisDb;Integrated Security=True";
        public SqlConnection db;
        public int employeeId = 0;
        public void ListTicket()
        {
            db = new SqlConnection(connectionString);
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("ListBiletMusteri", db);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($" Film Türü : {reader["FilmType"]},Film Adı : {reader["FilmName"]}, Seans Tarihi : {reader["Date"]}, Seans Saati : {reader["Hour"]}, Salon Adı : {reader["HallName"]}, Koltuk numarası : {reader["SeatNumber"]}");
                    }
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
        public void ListSeanslar()
        {
            db = new SqlConnection(connectionString);
            Console.Clear();
            try
            {
                db.Open();
                SqlCommand filmSeansListe = new SqlCommand("ListFilmVeSeans", db);
                filmSeansListe.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = filmSeansListe.ExecuteReader();

                Console.WriteLine("Film ve Seans Listesi");
                Console.WriteLine("---------------");
                while (sqlDataReader.Read())
                {
                    Console.WriteLine($"Id: {sqlDataReader["Id"]} : Film Adı: {sqlDataReader["FilmName"]},\t Seans: {sqlDataReader["Hour"]},\t Seans Ücreti: {sqlDataReader["Price"]}");
                }
                Console.Write("Id Seçiminiz : ");
                int secim = Convert.ToInt32(Console.ReadLine());
                SeansIslemSecimi(secim);
                db.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bağlantı Hatası");
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        public class Seans
        {
            public int Id { get; set; }
            public string? FilmName { get; set; }
            public string? FilmType { get; set; }
            public string? Hour { get; set; }
            public decimal Price { get; set; }
            public int Capacity { get; set; }
            public int FilmId { get; set; }
            public DateTime Date { get; set; }
            public DateTime TicketDate { get; set; }
            public int EmployeId { get; set; }
        }
        public void SeansIslemSecimi(int secim)
        {
            db = new SqlConnection(connectionString);
            //Bu sorguların procları sql'de var ancak hata veriyor, buraya direkt sorgu olarak ekleyince çalıştı
            string query = @"
                SELECT 
                    Sesssions.Id,
                    Movies.FilmName,
                    Movies.FilmType,
                    Sesssions.[Hour],
                    Sesssions.[Price],
                    Halls.Capacity,
                    Sesssions.FilmId,
                    Sesssions.[Date]
                FROM 
                    Sesssions
                INNER JOIN 
                    Movies ON Sesssions.FilmId = Movies.Id
                INNER JOIN 
                    Halls ON Sesssions.HallId = Halls.Id
                 WHERE
                    Sesssions.Id = @SeansId";
               

            string query2 = @"
                SELECT 
                    s.SeatNumber
                FROM 
                    Seats s
                INNER JOIN
                    Tickets t ON s.Id = t.SeatId
                INNER JOIN 
                    Sesssions ss ON t.SesssionsId = ss.Id
                WHERE
                    ss.FilmId = @FilmId
                    AND ss.Date = @Tarih
                    AND ss.Hour = @Saat";

             db = new SqlConnection(connectionString);
            {
                try
                {
                    db.Open();
                    Seans selectedSeans = null;
                    SqlCommand cmd = new SqlCommand(query, db);
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@SeansId", secim);

                        //Birden fazla dataReader açtığım için hata veriyor o yüzden using kullanarak
                        //kapsülledim. Yani ilk dataReaderı kapatarak ikinciyi çalıştırıyor hatayı kaldırıyor.
                        using (SqlDataReader sqlDataReader = cmd.ExecuteReader()) 
                        {
                            Console.WriteLine("\nKoltuk Seçimi");
                            Console.WriteLine("---------------");

                            if (sqlDataReader.Read())
                            {
                                selectedSeans = new Seans
                                {
                                    Id = Convert.ToInt32(sqlDataReader["Id"]),
                                    FilmName = sqlDataReader["FilmName"].ToString(),
                                    FilmType = sqlDataReader["FilmType"].ToString(),
                                    Hour = sqlDataReader["Hour"].ToString(),
                                    Price = Convert.ToDecimal(sqlDataReader["Price"]),
                                    Capacity = Convert.ToInt32(sqlDataReader["Capacity"]),
                                    FilmId = Convert.ToInt32(sqlDataReader["FilmId"]),
                                    Date = Convert.ToDateTime(sqlDataReader["Date"])
                                };
                            }
                        }
                    }

                    if (selectedSeans != null)
                    {
                        using (SqlCommand komut = new SqlCommand(query2, db))
                        {
                            komut.CommandType = CommandType.Text;
                            komut.Parameters.AddWithValue("@FilmId", selectedSeans.FilmId);
                            komut.Parameters.AddWithValue("@Tarih", selectedSeans.Date);
                            komut.Parameters.AddWithValue("@Saat", selectedSeans.Hour);

                            List<int> satilanlar = new List<int>();
                            SqlDataReader sqlDataReader2 = komut.ExecuteReader();
                            {
                                while (sqlDataReader2.Read())
                                {
                                    satilanlar.Add(Convert.ToInt32(sqlDataReader2["SeatNumber"]));
                                }
                            }

                            Console.WriteLine($"Film Adı: {selectedSeans.FilmName}, \tFilm Türü: {selectedSeans.FilmType}, \tSaat: {selectedSeans.Hour}, \tKoltuk Kapasitesi: {selectedSeans.Capacity}");
                            Console.WriteLine("-------Koltuklar-------");
                            for (int i = 1; i <= selectedSeans.Capacity; i++)
                            {
                                if (satilanlar.Contains(i))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.Write(i + "  ");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(i + "  ");
                                }
                            }

                            Console.ResetColor();
                            Console.WriteLine();
                            Console.Write("Koltuk Seçiminiz (virgülle ayırarak girin) : ");
                            string ksecimStr = Console.ReadLine();
                            List<int> ksecimler = ksecimStr.Split(',').Select(int.Parse).ToList();
                            decimal fiyatBirKoltuk = selectedSeans.Price;
                            decimal toplamFiyat = ksecimler.Count * fiyatBirKoltuk;
                            Console.WriteLine($"Seçilen Koltuklar: {string.Join(", ", ksecimler)}");
                            Console.WriteLine($"Toplam Fiyat: {toplamFiyat} lira");
                            Console.Write("Müşteri Adı : ");
                            string mName = Console.ReadLine();
                            Console.Write("Müşteri Soyadı : ");
                            string mSoyad = Console.ReadLine();
                            Console.Write("Bilet satışını onaylıyor musunuz? (evet / hayır) :");
                            string kaydet = Console.ReadLine();

                            if (kaydet.ToLower() == "evet")
                            {
                                SqlTransaction transaction = db.BeginTransaction();
                                {
                                    try
                                    {
                                        int customerId;
                                        SqlCommand insertCustomerCmd = new SqlCommand("INSERT INTO Customers (Name, Surname) VALUES (@Name, @Surname); SELECT SCOPE_IDENTITY();", db, transaction);
                                        {
                                            insertCustomerCmd.Parameters.AddWithValue("@Name", mName);
                                            insertCustomerCmd.Parameters.AddWithValue("@Surname", mSoyad);

                                            customerId = Convert.ToInt32(insertCustomerCmd.ExecuteScalar());
                                        }

                                        foreach (int ksecim in ksecimler)
                                        {
                                            SqlCommand insertCmd = new SqlCommand("KaydetBilet", db, transaction);
                                            {
                                                insertCmd.CommandType = CommandType.StoredProcedure;
                                                insertCmd.Parameters.AddWithValue("@SeatNumber", ksecim);
                                                insertCmd.Parameters.AddWithValue("@SesssionId", selectedSeans.Id);
                                                insertCmd.Parameters.AddWithValue("@SumPrice", fiyatBirKoltuk);
                                                insertCmd.Parameters.AddWithValue("@CustomerId", customerId);
                                                insertCmd.Parameters.AddWithValue("@EmployeId", employeeId);
                                                insertCmd.Parameters.AddWithValue("@TicketDate", DateTime.Now);

                                                int satirSay = insertCmd.ExecuteNonQuery();
                                                if (satirSay > 0)
                                                {
                                                    Console.WriteLine($"Koltuk {ksecim} için bilet başarıyla satın alındı.");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"Koltuk {ksecim} için bilet satın alırken bir sorun oluştu.");
                                                }
                                            }
                                        }
                                        transaction.Commit();
                                    }
                                    catch (Exception ex)
                                    {
                                        transaction.Rollback();
                                        Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("İşleminiz iptal edildi.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Seçilen seans bulunamadı.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                }
                finally
                {
                    db.Close();
                }
            }
        }
   

    }
}
