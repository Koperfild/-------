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

        private void Form1_Load(object sender, EventArgs e)
        {
            Cities cities=new Cities();
            citiesBindingSource.DataSource = cities.CitiesList;
            citiesBindingSource1.DataSource = cities.CitiesList;
            PriceDGW.Rows.Add();
            //AirTransport plane=new Лаба1.AirTransport("Airbus320");
            //PriceDGW.Rows.Add(plane.cost("Москва", "Питер"));
        }
        private void executeButton_Click(object sender, EventArgs e)
        {
            string from = comboBox1.SelectedItem as string;
            string to = comboBox2.SelectedItem as string;
            if (from == to)
            {
                PriceDGW.Rows[1].SetValues(0, 0, 0);
                return;
            }
            AirTransport plane = new Лаба1.AirTransport("Airbus320");
            GroundTransport train = new GroundTransport();
            WaterTranport ship = new WaterTranport();
            GraphMap map=new GraphMap(FilesDirectories.GraphMap);//Это для проверки алгоритма кратчайшего пути в графе
            double distance = map.Distance(from, to);//См пред коммент
            string planecost;
            string traincost;
            string shipcost;
            try
            {
                planecost = Math.Round(plane.cost(from, to), 2).ToString();
            }
            catch (Exception)
            {
                planecost = "N/A";
            }
            try
            {
                traincost = Math.Round(train.cost(from, to), 2).ToString();
            }
            catch (Exception)
            {
                traincost = "N/A";
            }
            try
            {
                shipcost = Math.Round(ship.cost(from, to), 2).ToString();
            }
            catch (Exception)
            {
                shipcost = "N/A";
            }
            PriceDGW.Rows[1].SetValues(planecost,traincost,shipcost);
        }

    }
}
