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
                MainTable.Rows[e.RowIndex].Cells[2].Value=Convert.ToInt32(MainTable.Rows[e.RowIndex].Cells[2].Value)+1;
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
            string path=@"H:\Информатика\ООП\XML.xml";
            using(System.IO.FileStream myFile = System.IO.File.Create(path))
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
    }
    public class RoadCalc : Calculation
    {
        const int N = 10;//Нужно ли вообще где-то const?
        const double inf = 1e9;//Бесконечность для алгоритма
        static int[,] IncidenceMatrix = new int[N, N];//Тоже можно десериализовать из файла//Делать int или double?
        static int[,] tempMatr = new int[N, N];

        protected void FillIncidenceMatrix()//Как сделать так чтобы в наследниках в итоге была одна и та же матрица смежности?(IncidenceMatrix)
        {
            Matrix.rand(ref IncidenceMatrix);
        }
        public static int cost(int x, int y)//Делать enum городов
        {            
            for (int k = 0; k < N; ++k)
            {
                Array.Copy(IncidenceMatrix, tempMatr, N * N);
                for (int i = 0; i < N; ++i)
                {
                    for (int j = 0; j < N; ++j)
                    {
                        IncidenceMatrix[i, j] = Math.Min(tempMatr[i, j], tempMatr[i, k] + tempMatr[k, j]);
                    }
                }
            }
            return IncidenceMatrix[x, y];
        }
    }
    public class AirCalc : Calculation//Продумать какую систему координат и как использовать. Центр отправки в 0,0 или существует много центров отправки
    {       
        //Делать enum городов                
        public static double cost(double x,double y)
        {
            return Math.Sqrt(x * x + y * y);
        }        
    }
    class Deliverer
    {
        protected double cost;
        double time;
    }
    class Truck:Deliverer
    {
        
        public void Cost(int from,int to)
        {
            cost=RoadCalc.cost(from,to);
        }
    }
    class Quadracopter : Deliverer
    {
        public void Cost(double x, double y)
        {
            cost = AirCalc.cost(x, y);
        }
    }
    class Matrix
    {
        public static void rand(ref int[,] a)
        {
            Random rand1 = new Random();
            for (int i=0;i<a.GetLength(0);++i){
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    a[i, j] = rand1.Next(51);
                }
            }
            
        }

    }
}