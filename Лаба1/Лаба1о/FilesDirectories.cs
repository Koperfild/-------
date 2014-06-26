    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Хранит пути всех файлов, использующихся в программе
    /// </summary>
    public static class FilesDirectories
    {
        public static string AirMapDirectory { get; set; }
        public static string GraphMapDirectory { get; set; }
        public static string GroundTransportTicketsDirectory { get; set; }
        public static string AirTransportTicketsDirectory { get; set; }
        public static string OilPricesDirectory { get; set; }
        public static string vehicleCharacteristics { get; set; }
        static FilesDirectories()//Или можно откуда-то считывать все нужные файлы
        {
            AirMapDirectory = @"Maps\AirMap.txt";
            GraphMapDirectory = @"Maps\GraphMap.txt";
            GroundTransportTicketsDirectory = @"Tickets\Ground.txt";
            OilPricesDirectory = @"Prices\Oil.txt";
            vehicleCharacteristics = @"VehicleModels\Planes.txt";
            AirTransportTicketsDirectory = @"Tickets\Air.txt";
        }
    }
}
