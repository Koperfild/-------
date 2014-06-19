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
    enum runwayType { No, Short, Middle, Long };
    enum seatType { seatPlace=1, platzkart=2, compartment=4 };
    public class Oil
    {
        public string Name{get;set;}
        public double Price{get;set;}
        public Oil(string Name,double Price)
        {
            this.Name = Name;
            this.Price = Price;
        }
    }
    public class OilPrices//Или куда-то ещё закинуть List  с ценами топлива?
    {
        private List<Oil> Oils = new List<Oil>();//Можно переделать с индексатором чтобы без точки сразу к List обращаться
        public OilPrices()
        {
            readPrices("input.txt");//input.txt файл с ценами на топливо
        }
        private void readPrices(string priceSource)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(priceSource);
            string Record;
            while ((Record = file.ReadLine()) != null)
            {
                string[] RecordParts = Record.Split(new char[] { ' ', '\t' });
                double x;
                double.TryParse(RecordParts[1], out x);//Можно вставить проверки удачно ли считалось + проверка названия топлива. Можно сделать перечень существующих видов топлива в виде enum и сравнивать с ним                
                Oils.Add(new Oil(RecordParts[0], x));
            }
        }
        public double getPrice(string oilType)
        {
            Oil oil1 = Oils.Find(
            delegate(Oil oil)
            {
                return string.Compare(oil.Name,oilType)==0;
            }
            );
            return oil1.Price;
        }

    }
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
    class AirPort
    {
        public string Name { get; set; }//Наименование Пункта
        //public string runwayType { get; set; }//Делать в алгоритме проверку карты на наличие дороги и аэропорта в пункте назначения
        public double x { get; set; }
        public double y { get; set; }

        private runwayType runway;
        public AirPort(string Name, double x, double y, string runwayType)
        {
            this.Name = Name;
            this.x = x;
            this.y = y;
            if (Enum.IsDefined(typeof(runwayType), runwayType))
            {
                Enum.TryParse<runwayType>(runwayType, true, out runway);
            }
        }
    }
    class Map
    {
        private string mapPath;
        abstract void readMapFromFile(string path);
        //abstract bool Communicate(string from, string to);убрал ибо в AirMap и GraphMap разные параметры у этого метода. Точнее в AirMap добавлен runwayType
    }
    class AirMap
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
                //string runwayType = RecordParts[3];
                Points.Add(new AirPort(RecordParts[0], x, y,RecordParts[3]));
            }
        }
        public bool Communicate(string from, string to,string runwayType)
        {
            AirPort pt1 = Points.Find(
            delegate(AirPort pt)//анонимный делегат
            {
                return (string.Compare(pt.Name, from) == 0)&&(pt1.)
            }
            );
            AirPort pt2 = Points.Find(
            delegate(AirPort pt)
            {
                return string.Compare(pt.Name, to) == 0;
            }
            );
            if (pt1 != null && pt2 != null)
            {
                return true;
            }
            else return false;
        }
        public double Distance(string from, string to)
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
            if (pt1 != null && pt2 != null)
            {
                return Math.Sqrt(Math.Pow(pt1.x - pt2.x, 2) + Math.Pow(pt1.y - pt2.y, 2));
            }
            else//Делать else. Возможно 0 и при обрабатывании считать 0 как отсутствие путей сообщения между from  и to
            {
                return ;
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
    
    class GraphMap:Map
    {
        const int N = 10;//Нужно ли вообще где-то const?
        const double inf = double.MaxValue;//Бесконечность для алгоритма
        private static double[][] OptimalWays;//Для static понадобится public?
        private static double[][] IncidenceMatrix;        
        private string[] PointsNames{ get; set; }
        //пункты доставки. Хранятся первой строчкой в файле 
        public GraphMap(string mapPath)
        {
            readMapFromFile(mapPath);
        }           
        private override void readMapFromFile(string path)
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
        //True если можно добраться из пункта А в пункт Б и False иначе
        public override bool Communicate(string from, string to)
        {
            bool flag1, flag2;
            flag1 = flag2 = false;
            //Сравниваем есть ли на данной карте связь двух пунктов
            for (int i = 0; i < PointsNames.Length; ++i)
            {   
                if (from == PointsNames[i])
                {
                    flag1 = true;
                }
                if (to == PointsNames[i])
                {
                    flag2 = true;
                }
            }
            if (flag1 && flag2) { return true; }
            else { return false; }
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
        public double Distance(string from, string to)
        {
            int i, j;
            i = j = 0;
            while (string.Compare(from, PointsNames[i], true) != 0) { ++i; }
            while (string.Compare(to, PointsNames[j], true) != 0) { ++j; }
            return OptimalWays[i][j];
        }
    }

    class Transport
    {
        public string vehicleModel { get; set; }
        //public double weight { get; set; }
        public int peopleQuantity { get; set; }
        public string oilType { get; set; }
        public double fuelConsumption { get; set; }
        //public double time { get; set; }//Или делать просто методом
        public abstract double cost();//Для самолётов стоимость взлёта+посадки+путь,для остальных только путь
        //abstract void move(Point from, Point to);
        //abstract void Repair();//На перспективу. Ремонт зависит от километража. Ремонт сбрасывает километраж на 0
        //можно добавить счётчик
        public Transport()
        {
            //Делать ввод откуда-нибудь параметров
        }
    }
    class Plane : Transport
    {
        public string runwayType { get; set; }
        public DayOfWeek weekDay { get; set; }
        public void getMealMenu()
        {

        }
        public void ExtraInfo()//в экстраинфо заношу методы
        {
            getMealMenu();
            //Время полёта, высота полёта, vehicleModel
        }

        public override double cost(string from, string to,string mapPath)
        {
            //string mapPath;//Куда всунуть path или саму map. В параметры cost или локальными переменными cost?
            AirMap map = new AirMap(mapPath);//Сделать конструктор Aircalc(Map )
            double distance;
            double price;
            if (map.Communicate(from, to,runwayType))
            {
                distance=map.Distance(from, to);
            }
            else { ..}
            OilPrices prices=new OilPrices();
            price=prices.getPrice(oilType);
            return fuelConsumption*price/peopleQuantity;//Можно добавить +luggage+takeoff+посадка

        }
    }
    class Hellicopter : Transport//Не придумал полей
    {
        public void ExtraInfo()
        {

        }
        
    }
    class Car : Transport
    {
        //public double tonns { get; set; }
        //public double mileage { get; set; }//Километраж
    }
    class Train : Transport
    {
        
        int seatPlace;
        double seatPlacePrice;
        int platzkart;
        double platzkartPrice;
        int compartment;//купе
        double compartmentPrice;//Хз где задавать.В конструкторе через функцию считывания или ещё чё
        private void SoldTickets()//Считывается из файла 1-тип билета(сидячий, платцкарт,купе),2 - количество. Файл может содержать много строчек
        {
            System.IO.StreamReader file = new System.IO.StreamReader("traintickets.txt");//Определить что за файл и где хранить и вообще где определять файл(в конструкторе или константой как сейчас)
            string Record;
            //Пока не конец файла
            while ((Record = file.ReadLine()) != null)
            {
                //Делим строчку по пробелам и табам
                string[] RecordParts = Record.Split(' ', '\t');
                seatType ticket;
                //смотрим соответствует ли написанное одному из 3 типов билетов
                if (Enum.IsDefined(typeof(seatType), RecordParts[0]))
                {
                    Enum.TryParse<seatType>(RecordParts[0], true, out ticket);
                    int quantity;
                    //Добавляем данные билеты к общему числу проданных билетов
                    if (int.TryParse(RecordParts[1], out quantity))
                    {
                        if (ticket.HasFlag(seatType.seatPlace))
                        {
                            seatPlace += quantity;
                        }
                        else if (ticket.HasFlag(seatType.seatPlace))
                        {
                            platzkart += quantity;
                        }
                        else { compartment += quantity; }
                    }
                }
            }
        }
        public string ExtraInfo()
        {
            return getMealMenu() + getBedLinen();
        }
        private string getMealMenu()
        {
            return "The meal can be ordered by place\nor you can visit restaurant wagon";
        }
        private string getBedLinen()
        {
            return "1 bed linen set costs 10 Euro";
        }
        //Считает среднюю стоимость места
        public double cost(string from,string to)
        {
            GraphMap map = new GraphMap("trainmap.txt");
            if (map.Communicate(from, to))
            {
                return fuelConsumption * map.Distance(from, to) / (seatPlace + platzkart + compartment);
            }
            else
            {
                ..//Наверно надо return 0 или double.maxvalue и обрабатывать извне
            }

        }
    }
    /*class WaterTransport : Transport
    {
        //public double tonnage { get; set; }
        //int Kind;//Морской/речной
        //public double miles { get; set; }


    }*/
    class MotorShip : Transport//Теплоход
    {

    }
    class SpeedBoat : Transport
    {

    }
}







//Полезный код
/*Point pt1 = Points.Find(
            delegate(Point pt)//анонимный делегат
            {
                return string.Compare(pt.Name, from) == 0;
            }
            );
            Point pt2 = Points.Find(
            delegate(Point pt)
            {
                return string.Compare(pt.Name, to) == 0;
            } 
            );
            if (pt1 != null && pt2 != null)
            {
                return Math.Sqrt(Math.Pow(pt1.x - pt2.x, 2) + Math.Pow(pt1.y - pt2.y, 2));
            }*/