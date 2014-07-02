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
