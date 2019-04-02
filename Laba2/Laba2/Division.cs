using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    class Division
    {
        public int Dividend { get; set; }
        public int Divisor { get; set; }
        public Division(int x, int y)
        {
            Dividend = x;
            Divisor = y;
        }
        public void Do()
        {
            Int64 register = 0 | Dividend,
                 remainder_register_bits = 0b1_1111_1111_1111_1111_0000_0000_0000_0000,
                 quotient_register_bits = 0b1111_1111_1111_1111,
                 shifted_divisor = Divisor << 16,
                 shifted_minus_divisor = -Divisor << 16;

            const int remainder_bits_amount = 16,
                quotient_bits_amount = 16,
                register_bits_amount = 33;

            Console.WriteLine("\tRegister:\n\t\t          {0}", ToBinary(register, register_bits_amount));

            register <<= 1;
            Console.WriteLine("\tShift left:\n\t\t   {0}", ToBinary(register, register_bits_amount));
            for (int i = 1; i <= 16; ++i)
            {
               
                Console.WriteLine("\n Step " + i);

                Console.WriteLine("Substract divisor: {0}", ToBinary(shifted_minus_divisor, remainder_bits_amount, true));
                register += shifted_minus_divisor;
                Console.WriteLine("\tRegister:\n\t\t   {0}", ToBinary(register, register_bits_amount));

                if ((register >> 32 & 1) == 0)
                {
                    register <<= 1;
                    register |= 1;
                    Console.WriteLine("\tShift left and set last quotient bit to 1:\n\t\t   {0}", ToBinary(register, register_bits_amount));
                }
                else
                {
                    Console.WriteLine("      Add divisor: {0}", ToBinary(shifted_divisor, remainder_bits_amount, true));
                    register += shifted_divisor;
                    Console.WriteLine("\tRegister:\n\t\t   {0}", ToBinary(register, register_bits_amount));
                    register <<= 1;
                    Console.WriteLine("\tSet last quotient bit to 0:\n\t\t   {0}", ToBinary(register, register_bits_amount));

                }
            }
           
            Console.WriteLine("\tShift Right left part of register:\n\t\t   {0}", ToBinary(((register & remainder_register_bits)>>1)
                | (register & quotient_register_bits)
                , register_bits_amount));

            Console.WriteLine("\n\tAnswer is:");
            Console.WriteLine("\t\tRemainder:\t     {0} (in decimal: {1})",
                ToBinary((register & remainder_register_bits) >> 1, remainder_bits_amount, true),
                (register & remainder_register_bits) >> 17);
            Console.WriteLine("\t\tQuotient:\t      {0} (in decimal: {1})",
                ToBinary(register & quotient_register_bits, quotient_bits_amount),
                register & quotient_register_bits);
        }
            private string ToBinary(Int64 register, byte bits_amount, bool is_divisor = false)
        {
            string result = string.Empty;

            int last_index = is_divisor ? 15 : -1;
            for (int i = bits_amount - 1 + (is_divisor ? 16 : 0); i > last_index; --i)
                result += (register >> i & 1) + (i % 4 == 0 && i != 0 ? " " : "");

            return result;
        }

    }
}
