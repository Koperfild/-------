using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    class Graph
    {
        protected double amplitude;//Амплитуда
        protected double Amplitude
        {
            get
            {
                return amplitude;
            }
            set
            {
                amplitude = value;
            }
        }
        protected double fhz;//Частота
        protected double Fhz
        {
            get
            {
                return fhz;
            }
            set
            {
                fhz = value;
            }
        }
        protected double w;//Угловая частота
        protected double W
        {
            get
            {
                return w;
            }
            set
            {
                w = value;
            }
        }
        protected double t;//Период
        protected double T2;
        public double ConvertTtoFhz(double T2)
        {
            double Fhz = 1 / T2;
            return Fhz;
        }
        void SetFHz(double _Fhz)
        {
            fhz = _Fhz;
        }
        public void SetT2(double _T2)
        {
            T2 = _T2;
            double Fhz = ConvertTtoFhz(T2);
            SetFHz(Fhz);
        }
        protected double T
        {
            get
            {
                return t;
            }
            set
            {
                t = value;
            }
        }
        protected double fi;//Начальная фаза
        protected double Fi
        {
            get
            {
                return fi;
            }
            set
            {
                fi = value;
            }
        }
        protected double ableft;//Левая граница интервала
        protected double ABleft
        {
            get
            {
                return ableft;
            }
            set
            {
                ableft = value;
            }
        }
        protected double abright;//Правая граница интервала
        protected double ABright
        {
            get
            {
                return abright;
            }
            set
            {
                abright = value;
            }
        }
        protected double dx;//Шаг графика
        protected double Dx
        {
            get
            {
                return dx;
            }
            set//Как задавать T через W или наоборот и как это отразится тут на Dx
            {
                Console.WriteLine("Input enough points per period = {0} to build graph", T);
                int n = int.Parse(Console.ReadLine());
                dx = T/(double)n;//Можно ли делать set без использования value?
            }
        }
        public delegate double Del(double x); 
        public double Func1(double x)//Функция графика
        {
            return Amplitude * Math.Sin(w * x + Fi);
        }
        //protected double x, y;
        public struct Point
        {
            public double x,y;
            //double x=this.x;//Так ????????????????
            //double y=this.y;//Или сделать свойство с get set для x,y ?
        }
        public List<Point> Points = new List<Point>();//Список точек для построителя графика
        public void CreateGraph()
        {
            double x = ABleft;
            Point TempPoint = new Point();
            Del handler = Func1;
            while (x < ABright + Dx)
            {

                TempPoint.x = x;
                TempPoint.y = handler(x);
                Points.Add(TempPoint);
                x += Dx;
            }
        }


    }
}
