using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
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
            set { ticketsQuantity = value; }
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
            return fuelConsumption * OilPrices.getPrice(oilType) * distance / ticketsQuantity;//Можно добавить +luggage+takeoff+посадка

        }
    }
}
