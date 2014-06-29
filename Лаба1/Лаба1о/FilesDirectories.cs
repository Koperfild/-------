    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Хранит пути всех файлов, использующихся в программе,включая тесты
    /// </summary>
    public static class FilesDirectories
    {
        public static string Лаба1оDirectory { get; set; }
        public static string AirMapTestDirectory { get; set; }
        public static string AirMap { get; set; }
        public static string GraphMap { get; set; }
        public static string GroundTransportTickets { get; set; }
        public static string AirTransportTickets { get; set; }
        public static string OilPrices { get; set; }
        public static string vehicleCharacteristics { get; set; }
        public static string AirMapTest { get; set; }
        public static string GraphMapTest { get; set; }
        public static string Test_BuildAirRace { get; set; }
        static FilesDirectories()//Или можно откуда-то считывать все нужные файлы
        {
            AirMap = @"AirMaps\AirMap.txt";
            GraphMap = @"GroundMaps\GraphMap.txt";
            GroundTransportTickets = @"Tickets\Ground.txt";
            OilPrices = @"Prices\Oil.txt";
            vehicleCharacteristics = @"VehicleModels\Planes.txt";
            AirTransportTickets = @"Tickets\Air.txt";
            AirMapTest = @"AirMapTest.txt";
            Test_BuildAirRace = @"AirMap_Test_Doesn't Communicate.txt";
            Лаба1оDirectory = @"Лаба1о\bin\Debug";
            AirMapTestDirectory = @"AirMapTest\bin\Debug";
            GraphMapTest = "GraphMapTest.txt";
        }
        static public string getFilePath(string Directory, string fileName)//По идее надо везде в тестах заменить вызов FilesDirectories.GraphMap на FilesDirectories.getFilePath(
        {
            const int N=3;//Число поднятия вверх по пути к файлу
            string path=System.IO.Directory.GetCurrentDirectory();
            for (int i=0;i<N;++i)
            {
                path = System.IO.Directory.GetParent(path).ToString();
            }
            return System.IO.Path.Combine(path, Directory, fileName);
        }
    }
}
