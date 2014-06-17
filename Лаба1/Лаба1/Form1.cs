using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лаба1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView1.Rows.Clear();
            if (e.ColumnIndex == 3)
            {
                MainTable.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32(MainTable.Rows[e.RowIndex].Cells[2].Value) + 1;
                MainTable.Refresh();
            }
            else if (e.ColumnIndex == 4 && Convert.ToInt32(MainTable.Rows[e.RowIndex].Cells[2].Value) > 0)
            {
                MainTable.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32(MainTable.Rows[e.RowIndex].Cells[2].Value) - 1;
                MainTable.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            /*if (weight < Level1)//Получить weight из Products и задать Level1
            {
                var delivery1 = new Quadracopter();
                textBox1.AppendText("The delivery costs are:"+ delivery1.Cost(from,to));
            }else{
                var delivery1=new Truck();
                textBox1.AppendText("The delivery costs are:"+ delivery1.Cost(from,to));
            }*/
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"H:\Информатика\ООП\XML.xml";
            using (System.IO.FileStream myFile = System.IO.File.Create(path))
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(myFile);

                Products p = new Products();

                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Products));
                for (int i = 0; i < 11; ++i)
                {
                    Product prod = new Product();
                    prod.productname = "Product" + Convert.ToString(i + 1);
                    p.order.Add(prod);
                }
                serializer.Serialize(file, p);
                file.Close();

                /*System.IO.StreamReader input = new System.IO.StreamReader(path);//Почему не пашет с myFile вместо path?
                serializer.Deserialize(input);
                input.Close();*/
            }

        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            Products prod = new Products();
            prod = prod.ReadFromFile();

            productsBindingSource.DataSource = prod.order;
            Наименование.DataPropertyName = "productname";
            Цена.DataPropertyName = "price";
            Количество.DataPropertyName = "quantity";

            /*
            var NameColumn= new MyTextBoxColumn("productname","Наименование");
            //NameColumn.DataGridView.DataBindings.Add(productsBindingSource);    
            var PriceColumn=new MyTextBoxColumn("price","Цена");
            var QuantityColumn=new MyTextBoxColumn("quantity","Кол-во");
            MainTable.Columns.Add(NameColumn);
            MainTable.Columns.Add(PriceColumn);
            MainTable.Columns.Add(QuantityColumn);
             */
        }




    }
    //Структура лучше ибо класс содержит ссылки а не значения полей. И сравнение 2-х классов будет сравнение ссылок а не значений

    /*class MyTextBoxColumn : DataGridViewTextBoxColumn
    {
        public MyTextBoxColumn(string property,string ColumnName)
        {
            DataGridViewColumn NameColumn = new DataGridViewTextBoxColumn();
            DataPropertyName = property;
            Name = ColumnName;            
        }
    }*/
    [System.Serializable]
    public class Products
    {
        public Products()
        {

        }
        public List<Product> order = new List<Product>();//Как сделать так чтобы сделать private, но использовать в Form
        /*public Product this[int index]//Вроде индексатор.Зачем это у Серёги?Или это в class Products?
        {
            get
            {
                return this.order[index];
            }
            set
            {
                this.order[index] = value;
            }
        }*/
        public Products ReadFromFile()
        {
            string path = @"XML.xml";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Products));
            Products prod = new Products();
            prod = (Products)reader.Deserialize(file);//Как тут происходит закрытие file?            
            file.Close();
            return prod;
        }
    }

    public class Product
    {
        //private string _name;//если делаем свойство без поля то можно не писать private string. И если просто присваивания и считывания значения то можно писать get;set. Если хоть одно расписываем то и второе надо в {}       
        public string productname//делать всё через get set
        {
            get;
            set;
        }
        public double price
        {
            get;
            set;
        }
        public int quantity
        {
            get;
            set;
        }
        public double weight
        {
            get;
            set;
        }
        public double dimensions
        {
            get;
            set;
        }
    }
    public class Calculation
    {
        //abstract public double cost(double x,double y);
        public enum Cities { };//Заполнить
    }
    
    public class RoadCalc : Calculation
    {
        const int N = 10;//Нужно ли вообще где-то const?
        const double inf = double.MaxValue;//Бесконечность для алгоритма
        private static double[][] OptimalWays;//Для static понадобится public?
        private static double[][] IncidenceMatrix;
        private string[] PointsNames;
        //Map map;
        public RoadCalc(string mapPath)    
        {            
            readMapFromFile(mapPath);
            CalcOptimalWays(IncidenceMatrix);
            //map = new Map(mapPath);
            //OptimalWays = CalcOptimalWays(map.IncidenceMatrix);
        }
        private virtual bool Communicate(string from, string to)
        {
            bool flag1, flag2;
            flag1 = flag2 = false;
            //Сравниваем есть ли на данной карте связь двух пунктов
            for (int i = 0; i < PointsNames.Length; ++i)
            {
                if (string.Compare(from,PointsNames[i],true)==0)
                {
                    flag1 = true;
                }
                if (string.Compare(to,PointsNames[i],true)==0)
                {
                    flag2 = true;
                }
            }
            if (flag1 && flag2) { return true; }
            else { return false; }
        }
        public double cost(string from, string to)
        {
            if (Communicate(from, to))
            {
                int i,j;
                i=j=0;
                while(string.Compare(from,PointsNames[i],true)!=0){++i;}
                while(string.Compare(to,PointsNames[j],true)!=0){++j;}
                return OptimalWays[i][j];
            }
            else
            {
            } //Сделать вывод что между from и to нет путей сообщения данным транспортом         
        }

        /*protected void FillIncidenceMatrix()//Как сделать так чтобы в наследниках в итоге была одна и та же матрица смежности?(IncidenceMatrix)
        {
            Matrix.rand(ref IncidenceMatrix);
        }*/
        private void readMapFromFile(string path)
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
                IncidenceMatrix[i] = Array.ConvertAll(values, element => Convert.ToDouble(element));//менять convert на tryparse
            }
        }
        

        private void CalcOptimalWays(double[][] IncidenceMatrix)//Что делать. Ведь я получаю ссылку на матрицу в map. Но я получчается меняю её здесь. Допустимо ли это?
        {
            double[][] tempMatr = new double[IncidenceMatrix.Length][];
            for (int i = 0; i < IncidenceMatrix.Length; ++i)
            {
                tempMatr[i] = new double[IncidenceMatrix.Length];
            }
            for (int k = 0; k < N; ++k)
            {
                Array.Copy(IncidenceMatrix, tempMatr, N * N);
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        if (double.Equals(tempMatr[i][k], double.MaxValue) || double.Equals(tempMatr[k][j], double.MaxValue))
                        {
                            IncidenceMatrix[i][j] = tempMatr[i][j];
                        }
                        else
                        {
                            IncidenceMatrix[i][j] = Math.Min(tempMatr[i][j], tempMatr[i][k] + tempMatr[k][j]);//как убрать переполнение если один из операндов суммы =double.maxvalue
                        }
                    }
                }
            }
        }
    }
    public class AirCalc : Calculation//Продумать какую систему координат и как использовать. Центр отправки в 0,0 или существует много центров отправки
    {
        //Делать enum городов 
        private string[] PointsNames;
        static private double[][] DistanceMatrix;//Квадратная симметричная относительно диагонали матрица.
        static private List<Point> Points=new List<Point>();
        public AirCalc(string mapPath)
        {
            readMapFromFrile(mapPath);//можно вызывать эти ф-ции и в cost
            InitDistanceMatrix();                        
        }
        private void readMapFromFrile(string mapPath)//В файле 1 строка- 1 пункт. 1-название 2-х 3-у. Читаю в RecordParts, заношу в List<Point> Points
        {
            System.IO.StreamReader file = new System.IO.StreamReader(mapPath);
            string names = file.ReadLine();            
            string Record;
            while ((Record = file.ReadLine())!=null)
            {
                string[] RecordParts=Record.Split(new char[]{' ','\t'});
                double x;
                double.TryParse(RecordParts[1],out x);//Можно вставить проверки удачно ли считалось
                double y;
                double.TryParse(RecordParts[2],out y);
                Points.Add(new Point(RecordParts[0],x,y));
            }            
        }
        private void InitDistanceMatrix()//Заполнение матрицы расстояний по координатам по прямым
        {
            for (int i = 0; i < Points.Count; ++i)
            {
                for (int j = 0; j < Points.Count; ++j)
                {
                    DistanceMatrix[i][j] = Math.Sqrt(Math.Pow(Points[i].x - Points[j].x, 2) + Math.Pow(Points[i].y - Points[j].y, 2));
                }
            }
        }
        public static double cost(string from,string to)
        {
            Points.
            return DistanceMatrix[]
        }
    }
    class Deliverer
    {
        protected double cost;
        double time;
    }
    class Truck : Deliverer
    {

        public void Cost(int from, int to)
        {
            cost = RoadCalc.cost(from, to);
        }
    }
    class Quadracopter : Deliverer
    {
        public void Cost(double x, double y)
        {       8888
            cost = AirCalc.cost(x, y);
        }
    }
    class Matrix
    {
        public static void rand(ref int[,] a)
        {
            Random rand1 = new Random();
            for (int i = 0; i < a.GetLength(0); ++i)
            {
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    a[i, j] = rand1.Next(51);
                }
            }

        }

    }

    class Point
    {
        public string Name { get; set; }//Наименование Пункта
        //public string runwayType { get; set; }//Делать в алгоритме проверку карты на наличие дороги и аэропорта в пункте назначения
        public double x { get; set; }
        public double y { get; set; }
        public Point(string Name,double x,double y)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
        }
    }
    //Запихнул в Calculation ибо не вижу смысла в этом классе (Map)
    /*class Map//
    {
        //пункты доставки. Хранятся первой строчкой в файле
        public string[] points { get; set; }
        public double[][] IncidenceMatrix;
        public Map(string path)//Ввод карты из файла
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string names = file.ReadLine();
            //Разделяем строчку на названия пунктов
            points = names.Split(new char[] { ' ', ',', '\t' });
            IncidenceMatrix = new double[points.Length][];
            for (int i = 0; i < points.Length; ++i)
            {
                //Читаю строку, делю по пробелам, перевожу в double и заношу в матрицу IncidenceMatrix
                string line = file.ReadLine();
                string[] values = line.Split(new char[] { ' ' });
                IncidenceMatrix[i] = Array.ConvertAll(values, element => Convert.ToDouble(element));//менять convert на tryparse
            }
        }
        //True если можно добраться из пункта А в пункт Б и False иначе
        public virtual bool Communicate(Point from, Point to)
        {
            bool flag1, flag2;
            flag1 = flag2 = false;
            //Сравниваем есть ли на данной карте связь двух пунктов
            for (int i = 0; i < points.Length; ++i)
            {   
                if (from.Name == points[i])
                {
                    flag1 = true;
                }
                if (to.Name == points[i])
                {
                    flag2 = true;
                }
            }
            if (flag1 && flag2) { return true; }
            else { return false; }
        }
    }*/

    class Transport
    {
        public string vehicleModel { get; set; }
        public double weight { get; set; }
        public int peopleQuantity { get; set; }
        public string oilType { get; set; }
        public double fuelConsumption { get; set; }
        abstract double cost();//Для самолётов стоимость взлёта+посадки+путь,для остальных только путь
        //abstract void move(Point from, Point to);
        //abstract void Repair();//На перспективу. Ремонт зависит от километража. Ремонт сбрасывает километраж на 0
        //можно добавить счётчик
    }
    class AirTransport : Transport
    {
        //public string takeoffMethod { get; set; }

        public double FlyingHours { get; set; }
    }
    class Plane : AirTransport
    {
        public string runwayType { get; set; }
        public DayOfWeek weekDay { get; set; }

        public void getMealMenu()
        {

        }
        public void ExtraInfo()//в экстраинфо заношу методы
        {
            getMealMenu();
        }

        public override double cost(Point from, Point to, double[,] map)
        {
            string mapPath;//Куда всунуть path или саму map. В параметры cost или локальными переменными cost?
            AirCalc counter = new AirCalc(mapPath);//Сделать конструктор Aircalc(Map )
            return AirCalc.cost()
        }
    }
    class Hellicopter : AirTransport//Не придумал полей
    {

    }
    class GroundTransport : Transport
    {
        public double mileage { get; set; }//Километраж

    }
    class Car : GroundTransport
    {
        public double tonns { get; set; }
        public double mileage { get; set; }//Километраж
    }
    class Train : GroundTransport
    {
        public int coachesQuantity { get; set; }
    }
    class WaterTransport : Transport
    {
        public double tonnage { get; set; }
        int Kind;//Морской/речной
        public double miles { get; set; }


    }
    class MotorShip : WaterTransport//Теплоход
    {

    }
    class SpeedBoat : WaterTransport
    {

    }
}
