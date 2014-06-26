using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    /// <summary>
    /// Хранит название города и его координаты
    /// </summary>
    public class AirPort
    {
        public string Name { get; set; }//Наименование Пункта
        //public string runwayType { get; set; }//Делать в алгоритме проверку карты на наличие дороги и аэропорта в пункте назначения
        public double x { get; set; }
        public double y { get; set; }
        /// <summary>
        /// Инициализирует новый экземпляр класса Лаба1.Airport
        /// </summary>
        /// <param name="Name">Имя города</param>
        /// <param name="x">Координата</param>
        /// <param name="y">Координата</param>
        public AirPort(string Name, double x, double y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
    }
    abstract class Map
    {
        public abstract bool Communicate(string from, string to);

        public abstract double Distance(string from,string to);
        protected abstract void readMapFromFile(string mapPath);
        //abstract void readMapFromFile(string path);
        //abstract bool Communicate(string from, string to);убрал ибо в AirMap и GraphMap разные параметры у этого метода. Точнее в AirMap добавлен runwayType
    }
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
    class AirMap : Map
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
                throw new Exception("Error reading map from file");
            }
        }
        /// <summary>
        /// Считывает воздушную карту из файла
        /// </summary>
        /// <param name="mapPath"></param>
        /// <exception cref="System.Exception">Ошибка чтения карты из файла</exception>
        protected override void readMapFromFile(string mapPath)//В файле 1 строка- 1 пункт. 1-название 2-х 3-у,4-тип ВПП(runwayType). Читаю в RecordParts, заношу в List<Point> Points
        {
            System.IO.StreamReader file = new System.IO.StreamReader(mapPath);
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
                    throw new Exception("Error reading map from file");
                }
            }
        }
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
            else throw new Exception("Airports are not connected");
        }
        /// <summary>
        /// Проверяет наличие авиасообщения между 2-мя пунктами
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>true если да, false иначе</returns>
        public override bool Communicate(string from, string to)//Сделать простой communicate без runway. Предполагается уже нормальная карта
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
                throw;
            }
        }
    }
    /// <summary>
    /// Наземный рейс между 2-мя пунктами
    /// </summary>
    public struct GroundRace
    {
        public int from;
        public int to;
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.GroundRace
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        public GroundRace(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
    }
    /// <summary>
    /// Карта-граф
    /// </summary>
    class GraphMap : Map
    {
        //protected const double inf = double.MaxValue;//Бесконечность для алгоритма
        protected double[][] OptimalWays;//Для static понадобится public?
        protected double[][] IncidenceMatrix;
        private string[] PointsNames { get; set; }//пункты доставки. Хранятся первой строчкой в файле 
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.GraphMap с указанием пути граф-карты
        /// </summary>
        /// <param name="mapPath">Путь к файлу граф-карты</param>
        /// <exception cref="System.Exception">В файле карты неверные данные</exception>"
        public GraphMap(string mapPath)
        {
            try
            {
                readMapFromFile(mapPath);
                CalcOptimalWays();
            }
            catch (FormatException)
            {
                throw new Exception("Error reading GraphMap");
            }
            catch (OverflowException)
            {
                throw new Exception("Too big read value");
            }
        }
        /// <summary>
        /// Считывание граф-карты из файла
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="System.Exception">Ошибка в файле граф-карты</exception>"
        protected override void readMapFromFile(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string names = file.ReadLine();
            //Разделяем строчку на названия пунктов
            PointsNames = names.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            IncidenceMatrix = new double[PointsNames.Length][];
            for (int i = 0; i < PointsNames.Length; ++i)
            {
                //Читаю строку, делю по пробелам, перевожу в double и заношу в матрицу IncidenceMatrix
                string line = file.ReadLine();
                string[] values = line.Split(new char[] { ' ','\t' },StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    IncidenceMatrix[i] = Array.ConvertAll(values, element => Convert.ToDouble(element));
                }
                catch (FormatException)
                {
                    throw new Exception("Incorrect data in file");
                }
                catch (OverflowException)
                {
                    throw new Exception("Too big read value");
                }
            }
        }
        //True если можно добраться из пункта А в пункт Б и False иначе
        /// <summary>
        /// Проверяет есть ли наземное сообщение и возвращает Лаба1.GroundRace
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception">2 пункта не имеют наземного сообщения</exception>"
        protected GroundRace BuildGroundRace(string from, string to)
        {
            GroundRace race = new GroundRace();
            bool flag1, flag2;
            flag1 = flag2 = false;
            for (int i = 0; i < PointsNames.Length && (!(flag1 && flag2)); ++i)
            {
                if (from == PointsNames[i])
                {
                    race.from = i;
                    flag1 = true;//Нашли откуда едем
                }
                if (to == PointsNames[i])
                {
                    race.to = i;
                    flag2 = true;//Нашли куда едем
                }
            }
            if (flag1 && flag2)
            {
                return race;
            }
            else throw new Exception("2 points doesn't communicate");
        }
        /// <summary>
        /// Проверяет есть ли наземное сообщение между 2-мя пунктами
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>true - есть, false - нету</returns>
        public override bool Communicate(string from, string to)
        {
            try
            {
                BuildGroundRace(from, to);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Расчёт кратчайших путей между всеми пунктами на карте
        /// </summary>
        /// <returns>Матрица кратчайших расстояний между пунктами</returns>
        protected void CalcOptimalWays()//Что делать. Ведь я получаю ссылку на матрицу в map. Но я получчается меняю её здесь. Допустимо ли это?
        {
            int N = IncidenceMatrix.Length;
            double[][] tempMatr = new double[N][];
            OptimalWays = new double[N][];
            for (int i = 0; i < N; ++i)
            {
                tempMatr[i] = new double[N];
                OptimalWays[i]=new double[N];
            }
            CopyJaggedArray(ref IncidenceMatrix,ref OptimalWays);//Далее IncidenceMatrix не нужен
            for (int k = 0; k < N; ++k)
            {
                CopyJaggedArray(ref OptimalWays, ref tempMatr);
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        OptimalWays[i][j] = Math.Min(tempMatr[i][j], tempMatr[i][k] + tempMatr[k][j]);
                    }
                }
            }
        }
        /// <summary>
        /// Копирует double[][] SourceArray в double[][] DestArray
        /// </summary>
        /// <param name="SourceArray"></param>
        /// <param name="DestArray"></param>
        /// <exception cref="System.RankException">Разные размеры сравниваемых массивов</exception>""
        private void CopyJaggedArray(ref double[][] SourceArray,ref double[][] DestArray)
        {
            try
            {
                for (int i = 0; i < SourceArray.Length; ++i)
                {
                    Array.Copy(SourceArray[i], DestArray[i], SourceArray[i].Length);
                }
            }
            catch (System.RankException)
            {
                throw new RankException("Incorrect dimensions of compared arrays");
            }
        }
        /// <summary>
        /// Расчёт расстояний между 2-мя пунктами
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">2 пункта не имеют наземного сообщения</exception>"
        public override double Distance(string from, string to)
        {
            try
            {
                GroundRace race = BuildGroundRace(from, to);
                return OptimalWays[race.from][race.to];
            }
            catch (Exception)
            {
                throw new Exception("2 points doesn't communicate");
            }
        }
    }
}
