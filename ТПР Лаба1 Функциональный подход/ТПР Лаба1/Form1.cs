using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ТПР_Лаба1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            MathGraph();
        }
        private void MathGraph()
        {            
            const int m = 8, n = 8;  
            int[][] Matr=new int[m][] 
            {
                new int[] {10,1,-3,0,-6,7,-17,2},
                new int[] {-4,3,12,2,10,1,5,0},
                new int[] {-5,7,9,4,15,1,11,8},
                new int[] {12,1,0,11,-7,9,-3,1},
                new int[] {14,2,14,-9,8,3,-8,10},
                new int[] {7,9,5,-4,7,0,2,18},
                new int[] {-2,11,1,12,3,-13,15,0},
                new int[] {7,-4,10,-2,6,8,12,1}
            };
            int[] row=new int[n];//Промежуточкая Строка 
            int[] column=new int[m];//Промежуточный Столбец

            //List<PointF> Points1 = new List<PointF>();
            //List<PointF> Points2 = new List<PointF>();
            
            Console.WriteLine("Input number of iterations");
            //int N = int.Parse(Console.ReadLine());//Число итераций
            int N = 25;
            Console.WriteLine("Input initial row to begin");
            //int ik=int.Parse(Console.ReadLine());//Начальная строка с которой начинаем
            int ik = 0;
            int jk;
            float max;
            float min;
            float gamma;
            float maxgamma=0;
            float mingamma=0;
            for(int i = 0; i < N; ++i)
            {
                for (int k = 0; k < row.Length; ++k)
                {
                    row[k] = Matr[ik][k] + row[k];
                }
                 //   Matr[ik].CopyTo(row, 0);
                //Array.Copy(Matr[ik], row, Matr[ik].Length);
                min = row[0];
                jk=0;
                for (int j = 1; j < row.Length; ++j)
                {                    
                    if(row[j]<min)
                    {
                        min = row[j];
                        jk = j;
                    }
                }
                gamma = min/(i+1);
                if (i==0)
                {
                    maxgamma=gamma;                    
                }
                else if (gamma>maxgamma)
                {
                    maxgamma = gamma;
                }
                this.chart1.Series["Lower"].Points.AddXY(i+1, maxgamma);                                   
                for (int j = 0; j < Matr.Length; ++j)
                {
                    column[j] = Matr[j][jk] + column[j];
                }                
                ik=0;
                max = column[0];

                for (int j=1;j<column.Length;++j)
                {
                    if (column[j]>max)
                    {
                        max = column[j];
                        ik=j;
                    }
                }
                gamma=max/(i+1);
                if (i == 0)
                {
                    mingamma = gamma;
                }
                else if (gamma < mingamma)
                {
                    mingamma = gamma;
                }                
                this.chart1.Series["Upper"].Points.AddXY(i + 1, mingamma);
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
