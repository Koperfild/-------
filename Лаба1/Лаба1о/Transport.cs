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
    /// <summary>
    /// Воздушный транспорт
    /// </summary>
    class AirTransport : Transport
    {
        protected override int ticketsQuantity
        {
            get
            {
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(FilesDirectories.AirTransportTickets);
                    return Convert.ToInt32(file.ReadLine());
                }
                catch (Exception)
                {
                    throw new Exception("Incorrect data in file");
                }
            }
        }
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.AirTransport и устанавливает характеристики указанного транспортного средства
        /// </summary>
        /// <param name="vehicleModel"></param>
        /// <exception cref="System.Exception">Ошибка считывания характеристик из файла</exception>"
        public AirTransport(string vehicleModel)//Можно передавать модель самолёта. И откуда то брать его расход топлива
        {
            try
            {
                readCharacteristicsFromFile(vehicleModel);
            }
            catch (Exception)
            {
                throw new Exception("Error reading vehicle characteristics from file");
            }
        }
        /// <summary>
        /// Считывание характеристик транспорта из файла
        /// </summary>
        /// <param name="vehicleModel"></param>
        /// <exception cref="System.Exception">Ошибка в файле характеристик</exception>"
        protected void readCharacteristicsFromFile(string vehicleModel)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(FilesDirectories.vehicleCharacteristics);
            string Record;
            string[] RecordParts;
            bool flag = false;//Нашли нужный нам самолёт
            while ((Record = file.ReadLine()) != null && !flag)
            {
                RecordParts = Record.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (RecordParts[0] == vehicleModel)
                {
                    try
                    {
                        oilType = Convert.ToString(RecordParts[1]);
                        fuelConsumption = Convert.ToDouble(RecordParts[2]);
                    }
                    catch (Exception)
                    {
                        throw new Exception("Incorrect data in vehicleCharacteristics file");
                    }
                }
            }
        }
        /// <summary>
        /// Расчёт стоимости проезда из пункта 1 в пункт 2
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>Стоимость</returns>
        /// <exception cref="System.Exception">Ошибка расчёта расстояния между пунктами</exception>"
        /// <exception cref="System.Exception">Ошибка чтения цен топлива из файла</exception>"

        public override double cost(string from, string to)
        {
            //string mapPath;//Куда всунуть path или саму map. В параметры cost или локальными переменными cost?
            AirMap map = new AirMap(FilesDirectories.AirMap);
            double distance;
            try
            {
                distance = map.Distance(from, to);
            }
            catch (Exception)
            {
                throw new Exception("2 airports doesn't communicate");
            }
            try
            {
                OilPrices oilprice = new OilPrices(FilesDirectories.OilPrices);
            }
            catch (Exception)
            {
                throw new Exception("Error reading oil prices");
            }
            return fuelConsumption*OilPrices.getPrice(oilType)*distance/ticketsQuantity;//Можно добавить +luggage+takeoff+посадка

        }
    }
    /// <summary>
    /// Наземный транспорт
    /// </summary>
    class GroundTransport : Transport
    {
        public GroundTransport()
        {
            fuelConsumption = 33;
            try
            {
                OilPrices oilprices = new OilPrices(FilesDirectories.OilPrices);
            }
            catch (Exception)
            {
                throw new Exception("Eror reading oil prices from file");
            }
            fuelprice = OilPrices.getPrice("Уголь");
        }
        protected override int ticketsQuantity{ get{return 20;}}
        /// <summary>
        /// Считает стоимость 1 билета
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>Цена билета</returns>
        /// <exception cref="System.Exception">Ошибка расчёта расстояния или ошибка считывания количества проданных билетов из файла</exception>"
        public override double cost(string from,string to)
        {
            GraphMap map = new GraphMap(FilesDirectories.GraphMap);
            try
            {
                return fuelConsumption * fuelprice * map.Distance(from, to) / SoldTickets(FilesDirectories.GroundTransportTickets);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
    /// <summary>
    /// Водный транспорт
    /// </summary>
    class WaterTranport : Transport//Теплоход
    {
        protected override int ticketsQuantity { get { return 20; } }
        /// <summary>
        /// Расчёт стоимости 1 билета из одного пункта в другой
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns></returns>
        public override double cost(string from, string to)
        {
            return 3;
        }
    }
        
}
