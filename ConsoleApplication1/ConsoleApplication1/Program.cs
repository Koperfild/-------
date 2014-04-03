using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Matrix
    {
        public static double[,] Multiply(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0))
            {
                Console.WriteLine("The matrixs couldn't be multiplied due to incompatible dimensions\n");
                Environment.Exit(-1);//Как то с try заморочиться?
            }
            double[,] c = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); ++i)
            {//i- число строк 1-ой матрицы и результирующей
                for (int k = 0; k < b.GetLength(1); ++k)
                {//k-число столбцов результирующей м-цы
                    for (int j = 0; j < a.GetLength(1); ++j)
                    {//j-число столбцов 1-ой, строк 2-ой м-цы
                        c[i, k] = a[i, j] * b[j, k];
                    }
                }
            }
            return c;
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        public static double[][] Multiply(double[][] a, double[][] b)
        {
            if (a[0].Length != b.Length)
            {
                Console.WriteLine("The matrixs couldn't be multiplied due to incompatible dimensions\n");
                Environment.Exit(-1);                
            }
            double[][] c = new double[a.Length][];
            /*for (int i = 0; i < a.Length; ++i)
            {
                c[i] = new double[b.Length];
            }*/
            for (int i = 0; i < a[0].Length; ++i)
            {
                c[i] = new double[b.Length];
                for (int k = 0; k < b[0].Length; ++k)
                {
                    for (int j = 0; j < b.Length; ++j)
                    {
                        c[i][k] = a[i][j] * b[j][k];
                    }
                }
            }
            return c;
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj

        public static double[][] UsualToJagMatrix(double[,] a)
        {
            double[][] c = new double[a.GetLength(0)][];
            for (int i = 0; i < a.GetLength(0); ++i)
            {
                c[i] = new double[a.GetLength(1)];
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    c[i][j] = a[i, j];
                }
            }
            return c;
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        public static void rand(ref double[,] a)
        {
            Random rand1 = new Random();
            for (int i=0; i<a.GetLength(0); ++i){
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    a[i, j] = rand1.Next(-15,16);
                    //Console.Write("{0:0}",(int)a[i, j]);
                }
                //Console.WriteLine();
            }
            
        }

        public void ArrayPrint(ref int[] arr)
        {
            Console.WriteLine("Исходный массив");
            for (int i = 0; i < arr.Length; ++i)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write matrix quantity"); 
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Write matrix row quantity");
            int m = int.Parse(Console.ReadLine());
            Console.WriteLine("Write matrix column quantity");
            int n = int.Parse(Console.ReadLine());
            double[][,] Matr = new double[k][,];
           
            
            //Рандом матриц
            for (int i = 0; i < Matr.Length; ++i){
                Matr[i] = new double[m, n];
                Matrix.rand(ref Matr[i]);
            }

            //Умножение матриц с засечением времени
            double[,] temp= new double[m, n];
            Array.Copy(Matr[0], temp, Matr[0].Length);
            var StartTime=DateTime.Now;//????????????????????????Или лучше Stopwatch?
                        
            for (int i = 1; i < Matr.Length; ++i){
                temp = Matrix.Multiply(temp, Matr[i]);
            }
            Console.WriteLine(DateTime.Now-StartTime+"\n");

            //Матрицы в Ступенчатые матрицы
            double[][][] JagMatr= new double[Matr.Length][][];
            
            for (int i = 0; i < Matr.Length; ++i)
            {
                JagMatr[i] = Matrix.UsualToJagMatrix(Matr[i]);
            }
            
            //Умножение ступенчатых матриц с засечением времени
            double[][] Temp = new double[JagMatr[0].Length][];
            
            for (int j = 0; j < JagMatr[0][0].Length; ++j)
            {
                Temp[j]=new double[JagMatr[0][j].Length];
                JagMatr[0][j].CopyTo(Temp[j], 0);
            }
                      
            StartTime=DateTime.Now;

            for (int i = 1; i < JagMatr.Length; ++i){
                Temp = Matrix.Multiply(Temp,JagMatr[i]);
            }
            Console.WriteLine(DateTime.Now-StartTime+"\n");
            Console.ReadLine();
            return;
        }
    }
}

/*Пользователь вводит с клавиатуры количество матриц и их размер. Создать матрицы и заполнить их случайными числами. Перемножить. 
Также создать ступенчатые массивы на основе этих матриц и также перемножить их. Вывести на консоль время, затраченное на умножение
 в первом и во втором случае. Реализовать для каждого достаточно большого действия отдельный метод.*/
