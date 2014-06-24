using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    public class Oil
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Oil(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
    public class OilPrices//Или куда-то ещё закинуть List  с ценами топлива?
    {
        private string priceSource { get; set; }
        private List<Oil> Oils = new List<Oil>();//Можно переделать с индексатором чтобы без точки сразу к List обращаться
        public OilPrices()
        {
            priceSource = "input.txt";
            readPrices(priceSource);//input.txt файл с ценами на топливо
        }
        private void readPrices(string priceSource)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(priceSource);
            string Record;
            while ((Record = file.ReadLine()) != null)
            {
                string[] RecordParts = Record.Split(new char[] { ' ', '\t' });
                double x;
                if (!double.TryParse(RecordParts[1], out x))//Можно вставить проверки удачно ли считалось + проверка названия топлива. Можно сделать перечень существующих видов топлива в виде enum и сравнивать с ним                
                {
                    throw new Exception("Error reading OilPrices");
                }
                Oils.Add(new Oil(RecordParts[0], x));
            }
        }
        public double getPrice(string oilType)
        {
            Oil oil1 = Oils.Find(
            delegate(Oil oil)
            {
                return string.Compare(oil.Name, oilType) == 0;
            }
            );
            return oil1.Price;
        }

    }
}
