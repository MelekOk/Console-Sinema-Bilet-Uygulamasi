
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SinemaSalonuProject.Process;

namespace SinemaSalonuProject
{
    internal class Menu
    {
        private string connectionString = "Data Source=NIRVANA;Initial Catalog=SinemaBiletSatisDb;Integrated Security=True";
        public SqlConnection db;
        public int employeeId = 0;
        public void Login()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║            Sinema Sistemi           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║           Kullanıcı Girişi          ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.Write("║ Username:                           ║");
                string username = Console.ReadLine();
                Console.SetCursorPosition(0, 7);
                Console.Write("║ Password:                           ║");
                string password = Console.ReadLine();
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("╚═════════════════════════════════════╝");
                Console.SetCursorPosition(12, 7);
        

                if (ValidateUser(username, password))
                {
                    MainMenu();
                    break;
                }
                else
                {
                    Console.SetCursorPosition(0, 8);
                    Console.WriteLine("╔═════════════════════════════════════╗");
                    Console.WriteLine("║ Invalid username or password.       ║");
                    Console.WriteLine("╚═════════════════════════════════════╝");
                    Thread.Sleep(2000);
                }
            }
        }

        private bool ValidateUser(string UserName, string Password)
        {
            using (db = new SqlConnection(connectionString))
            {
                db.Open();
                string query = "SELECT Id FROM Employees WHERE UserName = @UserName AND Password = @Password AND IsDelete = @IsDelete AND IsStatus = @IsStatus";
                using (SqlCommand cmd = new SqlCommand(query, db))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@IsDelete", false);
                    cmd.Parameters.AddWithValue("@IsStatus", true);
                    int result = (int)cmd.ExecuteScalar();
                    employeeId = result;

                    return result > 0;
                }
            }
        }

        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║            Sinema Sistemi           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║              Main Menu              ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║ 1. Film İşlemleri                   ║");
                Console.WriteLine("║ 2. Seans İşlemleri                  ║");
                Console.WriteLine("║ 3. Bilet İşlemleri                  ║");
                Console.WriteLine("║ 0. Exit System                      ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.Write("║ Select Process:                     ║");
                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╚═════════════════════════════════════╝");

                Console.SetCursorPosition(19, 11);
                string process = Console.ReadLine();

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╠═════════════════════════════════════╣");

                Console.SetCursorPosition(0, 13);

                switch (process)
                {
                    case "1":
                        MovieMenu();
                        break;
                    case "2":
                        HallMenu();
                        break;
                    case "3":
                        BiletMenu();
                        break;
                    case "0":
                        Console.Write("║ System is Shutting Down             ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(25, 13);

                        for (int i = 0; i < 5; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(1000);
                        }
                        Console.SetCursorPosition(25, 15);

                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("║ Undefined Choice.                   ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(25, 13);
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void MovieMenu()
        {
            MovieProcess movieProccess = new MovieProcess();
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║           Sinema Siztemi            ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║          Film İşlemleri             ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║ 1. Filmleri Listele                 ║");
                Console.WriteLine("║ 2. Film Ekle                        ║");
                Console.WriteLine("║ 3. Film Güncelle                    ║");
                Console.WriteLine("║ 4. Film Sil                         ║");
                Console.WriteLine("║ 0. Ana Menüye dönmek için tıklayın  ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.Write("║ Select Process:                     ║");
                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╚═════════════════════════════════════╝");

                Console.SetCursorPosition(19, 11);
                string process = Console.ReadLine();

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╠═════════════════════════════════════╣");

                Console.SetCursorPosition(0, 13);

                switch (process)
                {
                    case "1":
                         movieProccess.ListMovies();
                        break;
                    case "2":
                        movieProccess.AddMovie();
                        break;
                    case "3":
                        movieProccess.UpdateMovie();
                        break;
                    case "4":
                        movieProccess.DeleteMovie();
                        break;
                    case "0":
                        Console.Write("║ Going Back                          ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(12, 13);

                        for (int i = 0; i < 2; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(1000);
                        }
                        Console.SetCursorPosition(12, 15);

                        status = !status;
                        break;
                    default:
                        Console.WriteLine("║ Undefined Choice.                   ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(25, 13);
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void HallMenu()
        {
            HallProccess hallProccess = new HallProccess();
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║            Sinema Sistemi           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║           Seans İşlemleri           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║ 1. seansları Listele                ║");
                Console.WriteLine("║ 2. Seans Ekle                       ║");
                Console.WriteLine("║ 3. Seans Güncelle                   ║");
                Console.WriteLine("║ 4. Seans Sil                        ║");
                Console.WriteLine("║ 0. Ana Menüye dönmek için tıklayın  ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.Write("║ Select Process:                     ║");
                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╚═════════════════════════════════════╝");

                Console.SetCursorPosition(19, 11);
                string process = Console.ReadLine();

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╠═════════════════════════════════════╣");

                Console.SetCursorPosition(0, 13);

                switch (process)
                {
                    case "1":
                        hallProccess.ListHalls();
                        break;
                    case "2":
                        hallProccess.AddHall();
                        break;
                    case "3":
                        hallProccess.UpdateHall();
                        break;
                    case "4":
                        hallProccess.DeleteHall();
                        break;
                    case "0":
                        Console.Write("║ Going Back                          ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(12, 13);

                        for (int i = 0; i < 2; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(1000);
                        }
                        Console.SetCursorPosition(12, 15);

                        status = !status;
                        break;
                    default:
                        Console.WriteLine("║ Undefined Choice.                   ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(25, 13);
                        Console.ReadLine();
                        break;
                }
            }
        }

        public void BiletMenu()
        {
            BiletProccess biletProccess = new BiletProccess();
            // Yukarıda giriş yapılan kullanıcıya ait id bilgisi BiletProccess sınıfına gönderiliyor.
            biletProccess.employeeId = employeeId;
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("╔═════════════════════════════════════╗");
                Console.WriteLine("║            Sinema Sistemi           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║           Bilet İşlemleri           ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.WriteLine("║ 1. Biletleri Listele                ║");
                Console.WriteLine("║ 2. Bilet Ekle                       ║");  
                Console.WriteLine("║ 0. Ana Menüye dönmek için tıklayın  ║");
                Console.WriteLine("╠═════════════════════════════════════╣");
                Console.Write("║ Select Process:                     ║");
                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╚═════════════════════════════════════╝");

                Console.SetCursorPosition(19, 11);
                string process = Console.ReadLine();

                Console.SetCursorPosition(0, 12);
                Console.WriteLine("╠═════════════════════════════════════╣");

                Console.SetCursorPosition(0, 13);

                switch (process)
                {
                    case "1":
                        biletProccess.ListTicket();
                        break;
                    case "2":
                        biletProccess.ListSeanslar();
                        break;
                    case "0":
                        Console.Write("║ Going Back                          ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(12, 13);

                        for (int i = 0; i < 2; i++)
                        {
                            Console.Write(".");
                            Thread.Sleep(1000);
                        }
                        Console.SetCursorPosition(12, 15);

                        status = !status;
                        break;
                    default:
                        Console.WriteLine("║ Undefined Choice.                   ║");
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("╚═════════════════════════════════════╝");
                        Console.SetCursorPosition(25, 13);
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
