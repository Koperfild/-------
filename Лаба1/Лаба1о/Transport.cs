using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    abstract class Transport
    {
        public string vehicleModel { get; set; }
        //public double weight { get; set; }
        protected abstract int ticketsQuantity { get; }
        public string oilType { get; set; }
        public double fuelConsumption { get; set; }
        public double fuelprice { get; set; }
        //public double time { get; set; }//Или делать просто методом
        public abstract double cost(string from,string to);//Для самолётов стоимость взлёта+посадки+путь,для остальных только путь
        //abstract void move(Point from, Point to);
        //abstract void Repair();//На перспективу. Ремонт зависит от километража. Ремонт сбрасывает километраж на 0
        //можно добавить счётчик
        public Transport()
        {
            //Делать ввод откуда-нибудь параметров
        }
        /// <summary>
        /// Количество проданных билетов
        /// </summary>
        /// <param name="soldTicketsFile">Путь к файлу с проданными билетами</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Некорретные данные в файле</exception>"
        public virtual int SoldTickets(string soldTicketsFile)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(soldTicketsFile);
            string line;
            line = file.ReadLine();
            int soldtickets;
            if (!int.TryParse(line, out soldtickets))
            {
                throw new Exception("Incorrect file input data");
            }
            return soldtickets;
        }
    }
}
