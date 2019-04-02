using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Multiplication
    {
        public int Multiplicand { get; set; }
        public int Multiplier { get; set; }
        public Multiplication(int x, int y)
        {
            Multiplicand = x;
            Multiplier = y;
        }
        public void Do()
        {
            Int64 P = 0;
            
            string A_bits = ToBinary(Multiplicand), B_bits = ToBinary(Multiplier);
            Console.WriteLine("Multiplicand:\t" + A_bits); 
            Console.WriteLine("Multiplier:\t" + B_bits);

            bool is_same = true;

            if (Multiplicand * Multiplier < 0)
                is_same = false;

            if (Multiplicand < 0)
            {
                Multiplicand = ~Multiplicand+1;
                Console.WriteLine("\nMultiplicand is negative number: we'll work with complement code");
                A_bits = ToBinary(Multiplicand);
                Console.WriteLine("Multiplicand:\t" + A_bits);
            }

            if (Multiplier < 0)
            {
                Multiplier = ~Multiplier+1;
                Console.WriteLine("\nMultiplicand is negative number: we'll work with complement code");
                B_bits = ToBinary(Multiplier);
                Console.WriteLine("Multiplier:\t" + B_bits);
            }

            Console.WriteLine("\nMultiply how it is:");
            for (int i = 1; i < 17; ++i)
            {
                Console.WriteLine("  Step " + i + ":");
                if((Multiplier & 1) == 1)
                {
                    Console.WriteLine("  \tAdd Multiplicand:\t{0}\n\tTo Product:\t\t{1}", A_bits, ToBinary(P));
                    P += Multiplicand;                  
                }
                Console.WriteLine("  \tProduct :\t\t{0}", ToBinary(P));

                Console.WriteLine("  \tShift Multiplicand left:" + ToBinary(Multiplicand));
                Multiplicand <<= 1;
                Console.WriteLine("  \t\t\t\t" + ToBinary(Multiplicand));

                Console.WriteLine("  \tShift Multiplier right:\t" + ToBinary(Multiplier));
                Multiplier >>= 1;
                Console.WriteLine("  \t\t\t        " + ToBinary(Multiplier));
            }
            if (!is_same)
            {
                P = ~P+1;
                Console.WriteLine("\n  Result is complement code, because our multiplicands have different sign");
            }
            Console.WriteLine("  Answer is:\n\tIn decemal: {0}\n\tIn binary: {1}", P, ToBinary(P));
        }
         
        public string ToBinary(Int64 number)
        {
            string binary = string.Empty;
            for (int i = 1; i < 33; ++i)
            {
                binary = (i % 4 == 0 ? " " : "") + (number & 1) + binary;
                number >>= 1;
            }
            return binary;
        }
    }
}
