using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SinemaSalonuProject.Class
{
    internal class Hall
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public int HallId { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }  
        public string? Hour { get; set; }
        public bool IsStatus { get; set; } = true;
        public bool IsDelete { get; set; } = false;
    }
}
