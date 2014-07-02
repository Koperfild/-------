using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Цены на разные виды топлива
    /// </summary>
    public class OilPrices//Или куда-то ещё закинуть List  с ценами топлива?
    {
        private static List<Oil> Oils = new List<Oil>();//Можно переделать с индексатором чтобы без точки сразу к List обращаться
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.OilPrices
        /// </summary>
        public OilPrices(string oilPricesPath)
        {
            readPrices(oilPricesPath);
        }
        /// <summary>
        /// Считывает цены на разные виды топлива из указанного файла
        /// </summary>
        /// <param name="priceSource">Путь к файлу</param>
        /// <exception cref="System.Exception">Ошибка в файле данных</exception>"
        private void readPrices(string priceSource)//Цены записаны 1 строка-1 топливо. Название, цена
        {
            System.IO.StreamReader file = new System.IO.StreamReader(priceSource);
            string Record;
            while ((Record = file.ReadLine()) != null)
            {
                string[] RecordParts = Record.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                double x;
                if (!double.TryParse(RecordParts[1], out x))//Можно вставить проверки удачно ли считалось + проверка названия топлива. Можно сделать перечень существующих видов топлива в виде enum и сравнивать с ним                
                {
                    throw new Exception("Error reading OilPrices");
                }
                Oils.Add(new Oil(RecordParts[0], x));
            }
        }
        /// <summary>
        /// Получает цену на указанный вид топлива Лаба1.oilType
        /// </summary>
        /// <param name="oilType"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Информация о топливе не найдена</exception>"
        public static double getPrice(string oilType)
        {
            Oil oil1 = Oils.Find(
            delegate(Oil oil)
            {
                return string.Compare(oil.Name, oilType, true) == 0;
            }
            );
            if (oil1 != null)
            {
                return oil1.Price;
            }
            else throw new Exception("Oil info was not found");
        }

    }
}
