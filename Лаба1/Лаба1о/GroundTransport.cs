using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
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
        protected override int ticketsQuantity { get { return 20; } }
        /// <summary>
        /// Считает стоимость 1 билета
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>Цена билета</returns>
        /// <exception cref="System.Exception">Ошибка расчёта расстояния или ошибка считывания количества проданных билетов из файла</exception>"
        public override double cost(string from, string to)
        {
            GraphMap map = new GraphMap(FilesDirectories.GraphMap);
            try
            {
                return fuelConsumption * fuelprice * map.Distance(from, to) / SoldTickets(FilesDirectories.GroundTransportTickets);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
