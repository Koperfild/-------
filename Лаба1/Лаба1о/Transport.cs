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
        public int peopleQuantity { get; set; }
        public string oilType { get; set; }
        public double fuelConsumption { get; set; }
        //public double time { get; set; }//Или делать просто методом
        public abstract double cost(string from,string to);//Для самолётов стоимость взлёта+посадки+путь,для остальных только путь
        //abstract void move(Point from, Point to);
        //abstract void Repair();//На перспективу. Ремонт зависит от километража. Ремонт сбрасывает километраж на 0
        //можно добавить счётчик
        public Transport()
        {
            //Делать ввод откуда-нибудь параметров
        }
    }
    class AirTransport : Transport
    {
        //public DayOfWeek weekDay { get; set; }

        public override double cost(string from, string to)
        {
            //string mapPath;//Куда всунуть path или саму map. В параметры cost или локальными переменными cost?
            FilesDirectories files = new FilesDirectories();
            AirMap map = new AirMap(files.AirMapDirectory);
            double distance;
            double price;
            /*if (map.Communicate(from, to))
            {
                distance=map.Distance(from, to);
            }*/
            try
            {
                distance = map.Distance(from, to);
            }
            catch (Exception)
            {
                throw;
            }
            OilPrices prices=new OilPrices();
            price=prices.getPrice(oilType);
            return fuelConsumption*price*distance/peopleQuantity;//Можно добавить +luggage+takeoff+посадка

        }
    }
    class GroundTransport : Transport
    {
        //Проданные билеты
        /*int seatPlace;
        double seatPlacePrice;
        int platzkart;
        double platzkartPrice;
        int compartment;//купе
        double compartmentPrice;//Хз где задавать.В конструкторе через функцию считывания или ещё чё
         */
        /*private void SoldTickets()//Считывается из файла 1-тип билета(сидячий, платцкарт,купе),2 - количество. Файл может содержать много строчек
        {
            System.IO.StreamReader file = new System.IO.StreamReader("traintickets.txt");//Определить что за файл и где хранить и вообще где определять файл(в конструкторе или константой как сейчас)
            string Record;
            //Пока не конец файла
            while ((Record = file.ReadLine()) != null)
            {
                //Делим строчку по пробелам и табам
                string[] RecordParts = Record.Split(' ', '\t');
                seatType ticket;
                //смотрим соответствует ли написанное одному из 3 типов билетов
                if (Enum.IsDefined(typeof(seatType), RecordParts[0]))
                {
                    Enum.TryParse<seatType>(RecordParts[0], true, out ticket);
                    int quantity;
                    //Добавляем данные билеты к общему числу проданных билетов
                    if (int.TryParse(RecordParts[1], out quantity))
                    {
                        if (ticket.HasFlag(seatType.seatPlace))
                        {
                            seatPlace += quantity;
                        }
                        else if (ticket.HasFlag(seatType.seatPlace))
                        {
                            platzkart += quantity;
                        }
                        else { compartment += quantity; }
                    }
                }
            }
        }*/
        private int SoldTickets()
        {
            FilesDirectories files = new FilesDirectories();
            System.IO.StreamReader file = new System.IO.StreamReader(files.GroundTransportTicketsDirectory);
            string line;
            line=file.ReadLine();
            int soldtickets;
            if (!int.TryParse(line, out soldtickets))
            {
                throw new Exception("Incorrect file input data");
            }
            return soldtickets;
        }
        //Считает среднюю стоимость места
        public override double cost(string from,string to)
        {
            FilesDirectories files = new FilesDirectories();
            GraphMap map = new GraphMap(files.GraphMapDirectory);
            try
            {
                return fuelConsumption * map.Distance(from, to) / SoldTickets();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
    class WaterTranport : Transport//Теплоход
    {
        public override double cost(string from, string to)
        {
            return 3;
        }
    }
        
}
