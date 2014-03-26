using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 

namespace ConsoleApplication1
{
    class Massiv
    {
        public void InputArr(ref int[] B)
        {
			Random rand=new Random();
            for (int i=0;i<B.Length;++i){                
                B[i]=rand.Next(-10,11);//11 т.к. см. метод Next
            }
        }



        public void Move(int[] arr)//Задание 1//Не меняет исходный массив
		{
            ArrayPrint(ref arr);
            int[] outArr = new int[arr.Length];
            int i=0;
            int temp;
            Console.WriteLine("Введите величину сдвига массива");
            int n = int.Parse(Console.ReadLine());
                  
            do{
                //temp=arr[Mod(i+n,arr.Length)];
                outArr[Mod(i+n,arr.Length)]=arr[i];
                //arr[i]=temp;
                ++i;
            }while(i<arr.Length);
			Console.WriteLine("Массив после сдвига");
			i=0;
			do{
				Console.Write(outArr[i]+" ");
				++i;
			}while(i<outArr.Length);
            Console.ReadLine();
		}
			
		public int Mod(int x,int mod)//Для задания 1. Деление по модулю(математическое)
		{
			if (x<0)
			{
				return mod+x%mod;
			}else
			{
                return x % mod;
			}
		}	       
        
        public void Palindrom(ref int[] B)//Задание 3//Разобраться какой массив ей давать Начальный рандомный или самому задавать
        {
            int i = 0;
            while(i++<=(B.Length/2))
			{
                if (B[i]!=B[B.Length-1-i]){
					Console.WriteLine("Массив не является палиндромом");
                    return;                    
                }
            }
            Console.WriteLine("Данный массив является палиндромом");           
        }

        public void isPerestanovka(int[] B, int[] A)//Задание 4. Сначала сортируем, затем сравниваем массивы
        {
			ArrayPrint(ref B);
			ArrayPrint(ref A);
            if (B.Length!=A.Length){
                Console.WriteLine("2 массива имеют разную размерность");
                return;
            }
            BubbleSort(ref B);
            BubbleSort(ref A);
            int i=0;
            do{
                if (B[i]!=A[i]){
                    Console.WriteLine("Массивы не являются перестановками друг друга");
                    return;
                }
                ++i;
            }while(i<B.Length);
            Console.WriteLine("Массивы являются перестановками друг друга");
        }


        public void HaveCopies(int[] A){//Задание 5
            ArrayPrint(ref A);
            BubbleSort(ref A);
            
            for (int i=0;i<A.Length-1;++i){
                if (A[i]==A[i+1]){
                    Console.WriteLine("В массиве есть дубли");
                    return;
                }
            }
            Console.WriteLine("В массиве нет дублей");
        }

        public void Decreasing(int[] A){//Задание 6
            Console.WriteLine("Уменьшение элементов массива до первого обнуления элемента\n");
            ArrayPrint(ref A);
            int i=A.Length;
			//Проверка есть ли вообще положительные элементы в массиве
            bool flag=false;//Нету положительный элементов  в массиве
            bool Zero = false;//Нету нулей
            while (--i>=0){
                if (A[i]>0){
                    flag=true;					
                    break;
                }
                else if (A[i] == 0)
                {
                    Zero = true;//Есть ноль
                }
                
                
            }
            if (flag == false && Zero == false)
            {
                Console.WriteLine("В массиве нет положительных элементов и потому уменьшение его элементов до 0 немыслимо");
                return;
            }
            //flag=false;//нет элемента ==0
            while(!Zero){
                i=0;
                while(i<A.Length){
                    A[i]--;
                    if (A[i]==0){                        
                        Zero=true;
                        
                    }                    
                    ++i;
                }
            }
            
            Console.WriteLine("Получили массив");
            for (i = 0; i < A.Length; ++i)
            {
                Console.Write(A[i] + " ");
            }
            Console.WriteLine();		
        }

        public void SredArif(ref int[] A){//for Задание 8
			ArrayPrint(ref A);
            int max=A[0];
            int min=A[0];
            //int m,n;
            for (int i=1;i<A.Length;++i){
                if (A[i]<min){
                    min=A[i];
                    //m=i;
                }
                if (A[i]>max){
                    max=A[i];
                    //n=i;
                }

            }
            /*int index=Math.min(m,n);
			int index1=Math.max(m,n);
			int sum=0;
			int k=0;
			for (int j=index;j<index1+1;++index){
				sum+=A[j];
				++k;
			}
			Console.WriteLine("Среднее арифметическое между max={0} и min={1} равно:\n{2}",max,min,double(sum)/k);
			return;*/
			int sredArif=(max+min)/2;
			Console.WriteLine("Среднее арифметическое min={0} и max={1} массива равно {2}", min, max, sredArif);
        }
	

        public void Menu()//Задание 9
		{
			Console.WriteLine("Нажмите клавишу от 1 до 9 для перехода к соответствующему меню или Escape для выхода");
			System.ConsoleKeyInfo Button= Console.ReadKey();
			while(Button.Key!=ConsoleKey.Escape)
			{				
				switch(Button.Key)
				{
					case ConsoleKey.D0:
						Console.WriteLine("Menu 1\n1");
						break;
					case ConsoleKey.D2:
						Console.WriteLine("Menu 2\n2");
						break;
					case ConsoleKey.D3:
						Console.WriteLine("Menu 3\n3");
						break;
					case ConsoleKey.D4:
						Console.WriteLine("Menu 4\n4");
						break;
					case ConsoleKey.D5:
						Console.WriteLine("Menu 5\n5");
						break;
					case ConsoleKey.D6:
						Console.WriteLine("Menu 6\n6");
						break;
					case ConsoleKey.D7:
						Console.WriteLine("Menu 7\n7");
						break;
					case ConsoleKey.D8:
						Console.WriteLine("Menu 8\n8");
						break;
					case ConsoleKey.D9:
						Console.WriteLine("Menu 9\n9");
						break;
					default: break;
				}
				Button = Console.ReadKey();
			}
		}


        public void BubbleSort(ref int[] B)// Сортировка для задания 4
        {
            int temp;
			bool flag=true;//Есть неправильно стоящие элементы
            for (int i=0;i<B.Length-1;++i)
			{
				while (flag==true)//Есть неправильно стоящие элементы
				{	
					flag=false;//Нет неправильно стоящих элементов
					for (int j=i+1;j<B.Length;++j)
					{
						if (B[i]<B[j])
						{
							temp=B[i];
							B[i]=B[j];
							B[j]=temp;
							flag=true;//Есть неправильно стоящие элементы
						}
					}	
				}
			}
        }
        public void ArrayPrint(ref int[] arr)
		{
			Console.WriteLine("Исходный массив");
			for (int i=0;i<arr.Length;++i){
				Console.Write(arr[i]+" ");
			}
            Console.WriteLine();
		}
	}

    class Program
    {
        const int N = 7;
        static void Main(string[] args)
        {
            int[] arr = new int[N];
            //Заполнить массив
            Massiv mas1 = new Massiv();
            mas1.InputArr(ref arr);
			
            mas1.Move(arr);//Смотреть как реагирует на преобразование const int-> int
        
            //Задание 2

            bool flag;
            Console.WriteLine("Input number till which to search simple number\n");
            int n=int.Parse(Console.ReadLine());
            Console.WriteLine("Simple numbers till {0} are\n",n);
            for (int j=2;j<=n;++j){//j<n если не включать само число
                flag=true;//число простое
                for (int i = 2; i <= Math.Sqrt(System.Convert.ToDouble(j)); ++i)
                {//Можно тупо до (j-1)-самого проверяемого числа  //Подумать нужно ли Math.sqrt(double(j))+1
                    if ((j%i)==0){
                        flag=false;//число не простое
                        break;
                    }
                }
                if (flag==true){
                    Console.WriteLine(j);
                }
            }
			Console.ReadLine();
			//Задание 3
			int[] arrPalindrom = new int[7]{1, 2, 8, 0, 8, 2, 1};
			mas1.ArrayPrint(ref arrPalindrom);
			mas1.Palindrom(ref arrPalindrom);
			Console.ReadLine();
        
			//Задание 4
			int[] arr2 = new int[N];
			mas1.InputArr(ref arr2);
			mas1.isPerestanovka(arr,arr2);//Вставить массивы
			Console.ReadLine();
			
			//Задание 5
            mas1.HaveCopies(arr);//Вставить массивы
			Console.ReadLine();
			
			//Задание 6
			mas1.Decreasing(arr);//Вставить массив
            Console.ReadLine();
			
			//Задание 7
            string[] s={"20","14","3","-22222","-30"};
			Console.WriteLine("Исходный массив строк");
			for (int i=0;i<s.Length;++i){
				Console.WriteLine(s[i]);
			}
            Console.WriteLine("\n\tВ массиве строк есть следующие круглые числа\a");
            int k=0;
            do
            {
                if ((int.Parse(s[k])%10)==0){
                    Console.Write(s[k]+" ");
                }
            }
            while(++k<s.Length);
            Console.WriteLine("\n\n");
			//////////////////////////////////////
			
			//Задание 8
			mas1.SredArif(ref arr);//Вставить массив
			Console.ReadLine();
			
			//Задание 9
			mas1.Menu();
            Console.WriteLine();
            Console.ReadLine();
						
			//Задание 10//Определить массив вместо mas1
			mas1.ArrayPrint(ref arr);
			Console.WriteLine("Введите индекс элемента\n");
			n=int.Parse(Console.ReadLine());
			if (n>=arr.Length){//Если надо переделаю чтобы считывало до тех пор пока i не подойдёт
				Console.WriteLine("Неправильно задано i");
			}
			k=0;
			while(k<arr.Length){
				if (k==n){
                    ++k;
					continue;
				}
				Console.Write(arr[k]+" ");
                k++;
			}
			Console.ReadLine();
			////////////////////////////
			
                        
            
        }
	}

}