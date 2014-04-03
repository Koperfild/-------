using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Наследование
{
    class Car
    {
        public void DescribeCar()
        {
            System.Console.WriteLine("Four wheels and an engine.");
            ShowDetails();
        }

        public virtual void ShowDetails()
        {
            System.Console.WriteLine("Standard transportation.");
        }
    }
    class ConvertibleCar : Car
    {
        public new void ShowDetails()
        {
            System.Console.WriteLine("A roof that opens up.");
        }
    }
  
    class Program
    {
        static void Main(string[] args)
        {
            ConvertibleCar car2 = new ConvertibleCar();
            car2.DescribeCar();
            System.Console.WriteLine("----------");
            car2.ShowDetails();
            Console.ReadLine();
        }
    }
}
//Выдаётся
// Four wheels and an engine.
// Standard transportation.