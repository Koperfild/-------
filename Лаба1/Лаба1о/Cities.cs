using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    class Cities
    {
        public List<string> Cities;//Надо ли делать private и как получать доступ для datasource
        public Cities()
        {
            readCities();
        }
        public void readCities()
        {
            Cities = new List<string>();
            FilesDirectories files = new FilesDirectories();
            System.IO.StreamReader file = new System.IO.StreamReader(files.AirMapDirectory);
            string City;
            //Считывание Аэропортов
            while ((City = file.ReadLine()) != null)
            {
                string[] RecordParts = City.Split(new char[] { ' ', '\t' });
                Cities.Add(RecordParts[0]);
            }
            file.Close();
            //Считывание Городов с графовой карты
            file = new System.IO.StreamReader(files.GraphMapDirectory);
            string[] GroundCities;
            GroundCities = file.ReadLine().Split(' ', '\t', ',', ';');
            file.Close();
            Cities.AddRange(GroundCities);
        }
    }
}
