using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Program
    {
        static void Main(string[] args)
        {
            
           
                Console.WriteLine("Choose option:\n1\t Multiplication \n2\t Division  \n3\t Floating");
            Console.WriteLine();
            try
            {
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Int16 x, y;
                        Console.WriteLine("Enter 16 bits signed multiplicand:");
                        x = Int16.Parse(Console.ReadLine());
                        Console.WriteLine("Enter 16 bits signed multiplier:");
                        y = Int16.Parse(Console.ReadLine());
                        Multiplication m = new Multiplication(x, y);
                        m.Do();
                        break;

                    case 2:
                        Int16 k, l;
                        Console.WriteLine("Enter 16 bits signed dividend:");
                        k = Int16.Parse(Console.ReadLine());
                        Console.WriteLine("Enter 16 bits signed divisor:");
                        l = Int16.Parse(Console.ReadLine());
                        Division d = new Division(k, l);
                        d.Do();
                        break;

                    case 3:

                        float a, b;
                        Console.WriteLine("Enter first float signed value:");
                        a = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter second float signed value:");
                        b = float.Parse(Console.ReadLine());
                        Floating f = new Floating(a, b);
                        f.Do();
                        Console.ReadKey();
                        break;

                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
          
        }
    }
}
