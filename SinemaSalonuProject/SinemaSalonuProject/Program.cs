using System.Data.SqlClient;
namespace SinemaSalonuProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Login();
            menu.MainMenu();
        }
    }
}
