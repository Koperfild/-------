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
            Products prod = new Products();
            prod=prod.ReadFromFile();
            MainTable.DataSource = prod;
            textBox1.Refresh();
            for (int i = 0; i < 5; ++i)
            {
                textBox1.AppendText(prod.order[i].productname + prod.order[i].price);
            }
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
                    //p.order[i].productname = "Product" + Convert.ToString(i+1);//Почему меняет неправильно
                }
                serializer.Serialize(file, p);
                //myFile.Close();
                //НАдо закрывать поток прежде чем открыть его на чтение?
                System.IO.StreamReader input = new System.IO.StreamReader(myFile);
                serializer.Deserialize(input);
            }
            
        }
        /*private void Form1_Load(object sender, System.EventArgs e)
        {
            count += 1;
        }*/

        /*private void MainTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {

        }*/
    }
    //Структура лучше ибо класс содержит ссылки а не значения полей. И сравнение 2-х классов будет сравнение ссылок а не значений
    //Существует ли прямой доступ по индексу?

    [System.Serializable]
    public class Products
    {
        public Products()
        {

        }
        public List<Product> order = new List<Product>();//Как сделать так чтобы сделать private, но использовать в Form
        /*public Product this[int index]//Зачем это у Серёги?Или это в class Products?
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
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"H:\Информатика\ООП\XML.xml");
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Product));            
            return (Products)reader.Deserialize(file);
            
            //Сделать заполнение dataGridView этими данными
        }
        
        
    }
       
    public class Product
    {
        //private string _name;//если делаем свойство без поля то можно не писать private string. И если просто присваивания и считывания значения то можно писать get;set. Если хоть одно расписываем то и второе надо в {}
        public Product()
        {
            
        }
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
        //
        //Убрал для Сериализации
        /*
        public Product(string productname,string price="0", string quantity="0", string weight="0", string dimensions="")
        {
            this.productname = productname;
            this.price = price;
            this.quantity = quantity;
            this.weight = weight;
            this.dimensions = dimensions;
        }//Разбираться в каком виде у меня в XML это будет. все string или double тоже. И как потом в DataGridView конвертировать double в string
        
        
        public void FillDataGridView()
        {
            for (int i = 0; i < order.Count; ++i)
            {
                ;
            }
        }
         */
        /*public Order(string productname,double price, int quantity, double weight)
        {
            this.productname = productname;
            this.price = price;
            this.quantity = quantity;
            this.weight = weight;
        }*/
    }
    abstract class Calculation
    {
        abstract public double cost();
    }
    class RoadCalc : Calculation
    {
        const int N = 10;//Нужно ли вообще где-то const?
        const double inf = 1e9;//Бесконечность для алгоритма
        int[,] IncidenceMatrix = new int[N, N];//Тоже можно десериализовать из файла//Делать int или double?
        int[,] tempMatr = new int[N, N];

        protected void FillIncidenceMatrix()//Как сделать так чтобы в наследниках в итоге была одна и та же матрица смежности?(IncidenceMatrix)
        {
            Matrix.rand(ref IncidenceMatrix);
        }
        public override double cost(int x, int y)//Делать enum городов
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
            return (double)IncidenceMatrix[x, y];
        }
    }
    class AirCalc : Calculation
    {       
        //Делать enum городов                
        public override double cost(double x,double y)
        {
            return Math.Sqrt(x * x + y * y);
        }
    }

//Создать метод который создаст лист товаров и сделает прорисовку.
/*public InfFile this[string key]
        {
            get
            {
                return this.DictionaryInfFile[key];
            }
            set
            {
                this.DictionaryInfFile[key] = value;
            }
        }*/

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