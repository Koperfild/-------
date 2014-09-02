using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба1
{
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
    public class GraphMap : Map
    {
        //protected const double inf = double.MaxValue;//Бесконечность для алгоритма
        protected double[][] OptimalWays;//Для static понадобится public?
        protected double[][] IncidenceMatrix;
        private string[] PointsNames { get; set; }//пункты доставки. Хранятся первой строчкой в файле 
        public const string ErrorReadFile = "Error reading file";
        public const string TooBigValueInFile = "Too big read Value";
        /// <summary>
        /// Инициализирует новый экземпляр Лаба1.GraphMap с указанием пути граф-карты
        /// </summary>
        /// <param name="mapPath">Путь к файлу граф-карты</param>
        /// <exception cref="System.FormatException">В файле карты неверный формат данных</exception>"
        /// <exception cref="System.OverflowException">Слишком большое число в файле</exception>"
        public GraphMap(string mapPath)
        {
            try
            {
                readMapFromFile(mapPath);
                CalcOptimalWays();
            }
            catch (FormatException)
            {
                throw new FormatException(GraphMap.ErrorReadFile);
            }
            catch (OverflowException)
            {
                throw new OverflowException(GraphMap.TooBigValueInFile);
            }
        }
        public const string InternalErrorFileReading = "Internal error reading file";
        /// <summary>
        /// Считывание граф-карты из файла
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="System.Exception">Ошибка в файле граф-карты</exception>"
        protected override void readMapFromFile(string path)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            file.ReadLine();//Пропуск строки с типом карты
            string names = file.ReadLine();
            //Разделяем строчку на названия пунктов
            PointsNames = names.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            IncidenceMatrix = new double[PointsNames.Length][];
            for (int i = 0; i < PointsNames.Length; ++i)
            {
                //Читаю строку, делю по пробелам, перевожу в double и заношу в матрицу IncidenceMatrix
                string line = file.ReadLine();
                string[] values = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (values == null) { --i; continue; }//Случай пустых строчек между именем.Уменьшаем счётчик за фальшивую строчку
                try
                {
                    IncidenceMatrix[i] = Array.ConvertAll(values, element => Convert.ToDouble(element));
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentNullException(InternalErrorFileReading);
                }
                catch (FormatException)
                {
                    throw new FormatException(ErrorReadFile);
                }
                catch (OverflowException)
                {
                    throw new OverflowException(TooBigValueInFile);
                }
            }
        }
        public const string PointsDoesntCommunicate = "2 points doesn't communicate";
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
            else throw new Exception(PointsDoesntCommunicate);
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
                OptimalWays[i] = new double[N];
            }
            CopyJaggedArray(ref IncidenceMatrix, ref OptimalWays);//Далее IncidenceMatrix не нужен
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
        private void CopyJaggedArray(ref double[][] SourceArray, ref double[][] DestArray)
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
