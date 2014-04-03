using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Наследование
{
    public class Car
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
    public class ConvertibleCar : Car
    {
        public new void ShowDetails()
        {
            System.Console.WriteLine("A roof that opens up.");
        }
    }
    public class Car2: ConvertibleCar
    {
        public override void ShowDetails
        {
            
            Console.WriteLine("car 2");
        }
    }
  
    class Program
    {
        static void Main(string[] args)
        {
            ConvertibleCar car = new ConvertibleCar();
            car.DescribeCar();
            System.Console.WriteLine("----------");
            car.ShowDetails();
            Car2 car2 = new Car2();
            car2.ShowDetails();
            Console.ReadLine();
        }
    }
}
//Выдаётся
// Four wheels and an engine.
// Standard transportation.