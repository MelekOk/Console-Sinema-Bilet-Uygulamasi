using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SinemaSalonuProject.Class
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public bool IsStatus { get; set; } = true;
        public bool IsDelete { get; set; } = false;

    }
}
