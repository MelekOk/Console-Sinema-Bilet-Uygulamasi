using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaSalonuProject.Class
{
    internal class Ticket
    {
        public string? FilmName { get; set; }
        public string? FilmType { get; set; }
        public string? Date { get; set; }
        public string? Hour { get; set; }
        public string? HallName { get; set; }
        public int SeatNumber { get; set; }
    }
}
