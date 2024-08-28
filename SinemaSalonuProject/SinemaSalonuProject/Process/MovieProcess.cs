using SinemaSalonuProject.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaSalonuProject.Process
{
    internal class MovieProcess
    {
        private string connectionString = "Data Source=NIRVANA;Initial Catalog=SinemaBiletSatisDb;Integrated Security=True";
        public SqlConnection db;
        public void ListMovies()
        {
            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("ListFilmler", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║                          Movie List                        ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
                    Console.WriteLine("║ Id    ║ FilmName                    ║ FilmType                   ║ IsDelete  ║ IsStatus  ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

                    while (reader.Read())
                    {
                        Console.WriteLine($"║ {reader["Id"],-5} ║ {reader["FilmName"],-25} ║ {reader["FilmType"],-25} ║ {reader["IsDelete"],-8} ║ {reader["IsStatus"],-8} ║");
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

        public void AddMovie()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                          Add Movie                         ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Film Name: ");
            string filmName = Console.ReadLine();
            Console.Write("║ Film Type: ");
            string filmType = Console.ReadLine();
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("FilmlerInsertIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FilmName", filmName);
                    cmd.Parameters.AddWithValue("@FilmTuru", filmType);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ Movie information has been saved.                          ║");
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

        public void UpdateMovie()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        Update Movie                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Film ID: ");
            string filmId = Console.ReadLine();
            Console.Write("║ Film Name: ");
            string filmName = Console.ReadLine();
            Console.Write("║ Film Type: ");
            string filmType = Console.ReadLine();
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("FilmlerUpdateIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FilmId", filmId);
                    cmd.Parameters.AddWithValue("@FilmName", filmName);
                    cmd.Parameters.AddWithValue("@FilmTuru", filmType);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ Movie information has been updated.                        ║");
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

        public void DeleteMovie()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        Delete Movie                        ║");
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
            Console.Write("║ Film ID: ");
            string filmId = Console.ReadLine();
            Console.WriteLine("╠════════════════════════════════════════════════════════════╣");

            using (db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();

                    SqlCommand cmd = new SqlCommand("FilmlerDeleteIslemi", db);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FilmId", filmId);

                    cmd.ExecuteNonQuery();

                    Console.WriteLine("║ Movie has been marked as deleted.                          ║");
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
