using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Описание вида топлива
    /// </summary>
    public class Oil
    {
        public string Name { get; set; }
        public double Price { get; set; }
        /// <summary>
        /// Инициализирует экземпляр Лаба1.Oil
        /// </summary>
        /// <param name="Name">Наименование топлива</param>
        /// <param name="Price">Цена</param>
        public Oil(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
}
