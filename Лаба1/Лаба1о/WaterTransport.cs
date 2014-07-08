using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Водный транспорт
    /// </summary>
    class WaterTranport : Transport//Теплоход
    {
        protected override int ticketsQuantity { get; set; }
        public WaterTranport()
        {
            fuelConsumption = 100;
            ticketsQuantity = 20;
            try
            {
                OilPrices oilprices = new OilPrices(FilesDirectories.OilPrices);
            }
            catch (Exception)
            {
                throw new Exception("Eror reading oil prices from file");
            }
            fuelprice = OilPrices.getPrice("Солярка");
        }
        /// <summary>
        /// Расчёт стоимости 1 билета из одного пункта в другой
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns></returns>
        public override double cost(string from, string to) 
        {
            GraphMap map = new GraphMap(FilesDirectories.GraphMap);
            return fuelConsumption * fuelprice * map.Distance(from, to) / ticketsQuantity;
        }
    }
}
