using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Хранит информацию о рейсе
    /// </summary>
    public struct AirRace
    {
        public AirPort from;
        public AirPort to;
        public AirRace(AirPort from, AirPort to)
        {
            this.from = from;
            this.to = to;
        }
    }
    /// <summary>
    /// Реализует методы Map. Определяет расстояние и связь 2-х городов по воздушной карте
    /// </summary>
    public class AirMap : Map
    {
        static private List<AirPort> Points = new List<AirPort>();
        //private static double[][] DistanceMatrix;
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.AirMap для указанного имени файла карты
        /// </summary>
        /// <param name="mapPath">Путь к файлу карты</param>
        /// <exception cref="System.Exception">Ошибка чтения карты из файла</exception>
        public AirMap(string mapPath)
        {
            try
            {
                readMapFromFile(mapPath);
            }
            catch (Exception)
            {
                throw new Exception(ErrorReadingMapFromFile);
            }
        }
        public const string ErrorReadingMapFromFile = "Error reading map from file";//const всегда static
        /// <summary>
        /// Считывает воздушную карту из файла
        /// </summary>
        /// <param name="mapPath"></param>
        /// <exception cref="System.Exception">Ошибка чтения карты из файла</exception>
        protected override void readMapFromFile(string mapPath)//В файле 1 строка- 1 пункт. 1-название 2-х 3-у,4-тип ВПП(runwayType). Читаю в RecordParts, заношу в List<Point> Points
        {
            System.IO.StreamReader file = new System.IO.StreamReader(mapPath);
            file.ReadLine();
            string Record;
            while ((Record = file.ReadLine()) != null)
            {
                string[] RecordParts = Record.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    double x;
                    x = Convert.ToDouble(RecordParts[1]);//Можно вставить проверки удачно ли считалось
                    double y;
                    y = Convert.ToDouble(RecordParts[2]);
                    Points.Add(new AirPort(RecordParts[0], x, y));
                }
                catch (Exception)
                {
                    throw new Exception(ErrorReadingMapFromFile);
                }
            }
        }
        public const string AirportsDoesntCommunicate = "Airports are not connected";
        /// <summary>
        /// Проверяет есть ли авиасообщение между 2-мя пунктами и в случае успеха возвращает Лаба1.AirRace
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>Возвращает экземпляр Лаба1.AirRace</returns>
        /// <exception cref="System.Exception">2 заданных пункта не связаны авиасообщением</exception>
        protected AirRace BuildAirRace(string from, string to)
        {
            AirPort pt1 = Points.Find(
            delegate(AirPort pt)//анонимный делегат
            {
                return string.Compare(pt.Name, from) == 0;
            }
            );
            /*Лямбда выражение. Можно использовать вместо анонимного делегата
            Airport pt1 = Points.Find(pt => string.Compare(pt.Name, from) == 0);
            */
            AirPort pt2 = Points.Find(
            delegate(AirPort pt)
            {
                return string.Compare(pt.Name, to) == 0;
            }
            );
            //Если на карте найдены from и to то вернуть Race
            if (pt1 != null && pt2 != null)
            {
                return new AirRace(pt1, pt2);
            }
            else throw new Exception(AirportsDoesntCommunicate);
        }
        /// <summary>
        /// Проверяет наличие авиасообщения между 2-мя пунктами
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>true если да, false иначе</returns>
        public override bool Communicate(string from, string to)
        {
            try
            {
                AirRace rc = BuildAirRace(from, to);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Расчёт расстояния по прямой между 2-мя пунктами
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">2 пункта не имеют авиасообщения</exception>"
        public override double Distance(string from, string to)//сДЕЛАТь структуру с from to и отдельный метод communicate заполняющий структуру с полями Airport from,to
        {
            try
            {
                AirRace rc = BuildAirRace(from, to);
                return Math.Sqrt(Math.Pow(rc.from.x - rc.to.x, 2) + Math.Pow(rc.from.y - rc.to.y, 2));
            }
            catch (Exception)
            {
                throw new Exception(AirMap.AirportsDoesntCommunicate);
            }
        }
    }
}
