using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
    public class AirPort
    {
        public string Name { get; set; }//Наименование Пункта
        //public string runwayType { get; set; }//Делать в алгоритме проверку карты на наличие дороги и аэропорта в пункте назначения
        public double x { get; set; }
        public double y { get; set; }

        public AirPort(string Name, double x, double y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
    }
    abstract class Map
    {
        protected string mapPath;
        public abstract bool Communicate(string from, string to);

        public abstract double Distance(string from,string to);
        //abstract void readMapFromFile(string path);
        //abstract bool Communicate(string from, string to);убрал ибо в AirMap и GraphMap разные параметры у этого метода. Точнее в AirMap добавлен runwayType
    }
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
    class AirMap : Map
    {
        static private List<AirPort> Points = new List<AirPort>();
        //private static double[][] DistanceMatrix;
        public AirMap(string mapPath)
        {
            readMapFromFile(mapPath);
        }
        private void readMapFromFile(string mapPath)//В файле 1 строка- 1 пункт. 1-название 2-х 3-у,4-тип ВПП(runwayType). Читаю в RecordParts, заношу в List<Point> Points
        {
            System.IO.StreamReader file = new System.IO.StreamReader(mapPath);
            string Record;
            while ((Record = file.ReadLine()) != null)
            {
                string[] RecordParts = Record.Split(new char[] { ' ', '\t' });
                double x;
                double.TryParse(RecordParts[1], out x);//Можно вставить проверки удачно ли считалось
                double y;
                double.TryParse(RecordParts[2], out y);
                Points.Add(new AirPort(RecordParts[0], x, y));
            }
        }
        protected AirRace communicate(string from, string to)
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
        public override bool Communicate(string from, string to)//Сделать простой communicate без runway. Предполагается уже нормальная карта
        {
            try
            {
                AirRace rc = communicate(from, to);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public override double Distance(string from, string to)//сДЕЛАТь структуру с from to и отдельный метод communicate заполняющий структуру с полями Airport from,to
        {
            try
            {
                AirRace rc = communicate(from, to);
                return Math.Sqrt(Math.Pow(rc.from.x - rc.to.x, 2) + Math.Pow(rc.from.y - rc.to.y, 2));
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Заполнение матрицы расстояний по координатам по прямым
        //Не нужно. Сразу считаю расстояние в Distance
        /*private void InitDistanceMatrix()
        {
            for (int i = 0; i < Points.Count; ++i)
            {
                for (int j = 0; j < Points.Count; ++j)
                {
                    DistanceMatrix[i][j] = Math.Sqrt(Math.Pow(Points[i].x - Points[j].x, 2) + Math.Pow(Points[i].y - Points[j].y, 2));
                }
            }
        }*/
    }
    public struct GroundRace
    {
        public int from;
        public int to;
        public GroundRace(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
    }
    class GraphMap : Map
    {
        //protected const double inf = double.MaxValue;//Бесконечность для алгоритма
        protected double[][] OptimalWays;//Для static понадобится public?
        protected double[][] IncidenceMatrix;
        private string[] PointsNames { get; set; }
        //пункты доставки. Хранятся первой строчкой в файле 
        public GraphMap(string mapPath)
        {
            try
            {
                readMapFromFile(mapPath);
                CalcOptimalWays();
            }
            catch (FormatException)
            {
                throw new FormatException("Error reading GraphMap");
            }
            catch (OverflowException)
            {
                throw new OverflowException("Too big read value");
            }
        }
        protected void readMapFromFile(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string names = file.ReadLine();
            //Разделяем строчку на названия пунктов
            PointsNames = names.Split(new char[] { ' ', ',', '\t' });
            IncidenceMatrix = new double[PointsNames.Length][];
            for (int i = 0; i < PointsNames.Length; ++i)
            {
                //Читаю строку, делю по пробелам, перевожу в double и заношу в матрицу IncidenceMatrix
                string line = file.ReadLine();
                string[] values = line.Split(new char[] { ' ' });
                try
                {
                    IncidenceMatrix[i] = Array.ConvertAll(values, element => Convert.ToDouble(element));//менять convert на tryparse
                }
                catch (FormatException)
                {
                    throw;
                }
                catch (OverflowException)
                {
                    throw;
                }
            }
        }
        //True если можно добраться из пункта А в пункт Б и False иначе
        protected GroundRace communicate(string from, string to)
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
        public override bool Communicate(string from, string to)
        {
            try
            {
                communicate(from, to);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
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
            Array.Copy(IncidenceMatrix, OptimalWays, N*N);//Далее IncidenceMatrix не нужен
            for (int k = 0; k < N; ++k)
            {
                Array.Copy(OptimalWays, tempMatr, N * N);
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        OptimalWays[i][j] = Math.Min(tempMatr[i][j], tempMatr[i][k] + tempMatr[k][j]);
                    }
                }
            }
        }
        public override double Distance(string from, string to)
        {
            try
            {
                GroundRace race = communicate(from, to);
                return OptimalWays[race.from][race.to];
            }
            catch (Exception)
            {
                throw new Exception("2 points doesn't communicate");
            }
        }
    }
}
