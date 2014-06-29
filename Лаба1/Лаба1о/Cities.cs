using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Класс содержащий список всех городов
    ///</summary>
    public class Cities
    {
        public List<string> CitiesList;//Надо ли делать private и как получать доступ для datasource

        public Cities(params string[] mapFiles)
        {
            readCities(mapFiles);
        }
        /// <summary>
        /// Считывает названия городов из файлов карт
        /// <para>Использует пути к файлам из class FilesDirectories</para>
        /// </summary>
        public void readCities(params string[] mapFiles)
        {
            CitiesList = new List<string>();
            for (int i = 0; i < mapFiles.Length; ++i)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(mapFiles[i]);
                string mapType = file.ReadLine().ToLower();
                if (mapType.Contains("воздушная карта"))
                {
                    string City;
                    //Считывание Аэропортов
                    while ((City = file.ReadLine()) != null)
                    {
                        string[] RecordParts = City.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        CitiesList.Add(RecordParts[0]);
                    }
                    file.Close();
                }
                if (mapType.Contains("наземная карта"))
                {
                    string[] GroundCities;
                    //Считывание Городов с графовой карты
                    GroundCities = file.ReadLine().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    file.Close();
                    CitiesList.AddRange(GroundCities);
                    CitiesList = CitiesList.Distinct<string>().ToList<string>();
                }
            }
            CitiesList.Sort();
        }
    }
}

