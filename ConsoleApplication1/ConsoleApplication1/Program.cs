﻿using System;
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
            for (int i = 0; i < a.Length; ++i)
            {
                a[i] = new double[b.Length];
            }
            for (int i = 0; i < a[i].Length; ++i)
            {
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
            }
            for (int i = 0; i < a.GetLength(0); ++i)
            {
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    c[i][j] = a[i, j];
                }
            }
            return c;//?????????????????????????????????Тип double[][] отличается от double[,] ?И тот и тот можно передать в Multiply?
        }
        //jjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj
        public static void rand(ref double[,] a)
        {
            Random rand1 = new Random();
            for (int i=0;i<a.GetLength(0);++i){
                for (int j = 0; j < a.GetLength(1); ++j)
                {
                    a[i, j] = rand1.NextDouble();
                }
            }
            
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write matrix quantity");
            //Matrix[][][] a= new Matrix[Console.ReadLine()];
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Write matrix row quantity");
            int m = int.Parse(Console.ReadLine());
            Console.WriteLine("Write matrix column quantity");
            int n = int.Parse(Console.ReadLine());
            double[][,] Matr = new double[k][,];
            for (int i = 0; i < Matr.Length; ++i)
            {
                Matr[i] = new double[m, n];
            }
            /*double[,] x= new double[m,n];
            double[] Matr = new x[k];*/
            //Matrix a = new Matrix();
            for (int i=0;i<Matr.Length;++i){
                Matrix.rand(ref Matr[i]);
            }
             //       rand(ref a[i]); }
            
            double[,] temp= new double[m,n];
            Array.Copy(Matr[0], temp, Matr[0].Length);//????????????????Можно последнее что-то типа a.GetLength() ?
            var StartTime=DateTime.Now;//????????????????????????Или лучше Stopwatch?
            for (int i=1;i<Matr.Length;++i){//Умножение матриц
                temp=Matrix.Multiply(temp,Matr[i]);
            }
            Console.WriteLine(DateTime.Now-StartTime+"\n");

            // Ступенчатые матрицы
            double[][][] JagMatr= new double[Matr.Length][][];
            for (int i=0;i<JagMatr.GetLength(0);++i){
                JagMatr[i] = new double[Matr[0].GetLength(0)][];
                for (int m=0;m<Matr[0].GetLength(0);++m){//почему не даёт объявить int m=0
                    JagMatr[i][m]= new double[Matr[0].GetLength(1)];
                }
            }

            
            for (int i=0;i<JagMatr.GetLength(0);++i){
                JagMatr[i]=Matrix.UsualToJagMatrix(Matr[i]);
            }
            double[][] Temp = new double[a.GetLength(0)];
            for (int i=0;i<A.GetLength(0);++i){
                Temp[i]=new double[a.GetLength(1)];
            }
            Array.Copy(A[0],Temp,A.GetLength(0)*A.GetLength(1));
            StartTime=DateTime.Now;
            for (int i=1;i<A.GetLength;++i){
                Temp=Matrix.Multiply(Temp,A[i]);//Тут по идее можно допилить Multiply по кол-ву строк 1-ой и кол-ву столбцов последней м-цы и можно умножать любую последовательность подходящих матриц а не только одинаковые.
            }
            Console.WriteLine(DateTime.Now-StartTime+"\n");
            Console.ReadLine();
            return 0;
        }
    }
}

/*Пользователь вводит с клавиатуры количество матриц и их размер. Создать матрицы и заполнить их случайными числами. Перемножить. 
Также создать ступенчатые массивы на основе этих матриц и также перемножить их. Вывести на консоль время, затраченное на умножение
 в первом и во втором случае. Реализовать для каждого достаточно большого действия отдельный метод.*/

