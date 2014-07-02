using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Хранит название города и его координаты
    /// </summary>
    public class AirPort
    {
        public string Name { get; set; }//Наименование Пункта
        //public string runwayType { get; set; }//Делать в алгоритме проверку карты на наличие дороги и аэропорта в пункте назначения
        public double x { get; set; }
        public double y { get; set; }
        /// <summary>
        /// Инициализирует новый экземпляр класса Лаба1.Airport
        /// </summary>
        /// <param name="Name">Имя города</param>
        /// <param name="x">Координата</param>
        /// <param name="y">Координата</param>
        public AirPort(string Name, double x, double y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
    }
}
