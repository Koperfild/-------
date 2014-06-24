using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    class FilesDirectories
    {
        public string AirMapDirectory { get; set; }
        public string GraphMapDirectory { get; set; }
        public string GroundTransportTicketsDirectory { get; set; }
        public FilesDirectories()//Или можно откуда-то считывать все нужные файлы
        {
            AirMapDirectory = "Maps/AirMap.txt";
            GraphMapDirectory = "Maps/GraphMap.txt";
            GroundTransportTicketsDirectory = "Tickets/Ground.txt";
        }
    }
}
