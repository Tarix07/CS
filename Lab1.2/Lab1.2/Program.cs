using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1._2
{
    class Program
    {
        static void Main(string[] args)
        {

            byte[] data = new byte[0];
            byte[] data1 = new byte[0];
            bool isDone = false;
            int Switch;
            string pathIN = "";
            Console.WriteLine("Enter the number of your text:");
            while (!isDone)
            {
                try
                {
                    Switch = int.Parse(Console.ReadLine());
                    switch (Switch)
                    {
                        case 1:
                            isDone = true;
                            pathIN = @"C:\КОМП Сист\Laba1\Laba1\Texts\Sun\Vechirne_sonce.txt";

                            break;
                        case 2:
                            isDone = true;
                            pathIN = @"C:\КОМП Сист\Laba1\Laba1\Texts\Fox\Farbovanyi_lys.txt";
                            break;
                        case 3:
                            isDone = true;
                            pathIN = @"C:\КОМП Сист\Laba1\Laba1\Texts\Zapyt\Zapyt.txt";
                            break;

                    }
                    data = Encoding.UTF8.GetBytes(File.ReadAllText(pathIN, Encoding.Default));
                    data1 = Encoding.UTF8.GetBytes(File.ReadAllText(Ext(pathIN,".gz"), Encoding.Default));
                }
                catch
                {
                    Console.WriteLine("The number must be in the range from 1 to 3. Try again:");
                }
            }


            Console.WriteLine(  );
            Console.WriteLine(pathIN.Split('\\')[6]);
            Console.WriteLine();
            double am1 = Amount_File(data);
            Compare(am1, pathIN, ".txt");

            Console.WriteLine();
            Console.WriteLine((Ext(pathIN,".gz")).Split('\\')[6]);
            Console.WriteLine();
            double am2 = Amount_File(data1);
            Compare(am2, pathIN, ".gz");

        }

        private static char GetCharFromIndexTable(int b)
        {
            char[] indexTable = new char[64] {
        'A','B','C','D','E','F','G','H','I','J','K','L','M',
        'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
        'a','b','c','d','e','f','g','h','i','j','k','l','m',
        'n','o','p','q','r','s','t','u','v','w','x','y','z',
        '0','1','2','3','4','5','6','7','8','9','+','/'};

            if ((b >= 0) && (b <= 63))
            {
                return indexTable[b];
            }
            else
            {
                return ' ';
            }
        }

        public static char[] Base64Encoding(byte[] data)
        {
            int length, length2;
            int blockCount;
            int paddingCount;
            length = data.Length;

            if ((length % 3) == 0)
            {
                paddingCount = 0;
                blockCount = length / 3;
            }
            else
            {
                paddingCount = 3 - (length % 3);
                blockCount = (length + paddingCount) / 3;
            }

            length2 = length + paddingCount;

            byte[] source2 = new byte[length2];

            for (int x = 0; x < length2; x++)
            {
                if (x < length)
                {
                    source2[x] = data[x];
                }
                else
                {
                    source2[x] = 0;
                }
            }

            byte b1, b2, b3; //для маніпулювання з бітами
            string BinaryCod8;
            string temp1, temp2, temp3, temp4;      //використовується для розбиття на 6 бітів з 8
            int[] buffer = new int[blockCount * 4];
            char[] result = new char[blockCount * 4];

            for (int x = 0; x < blockCount; x++)    // зсуви для того щоб досягти значення в 6 біт і не втратити нічого
            {

                BinaryCod8 = "";
                b1 = source2[x * 3];
                b2 = source2[x * 3 + 1];
                b3 = source2[x * 3 + 2];

                BinaryCod8 += FromByte((int)b1);
                BinaryCod8 += FromByte((int)b2);
                BinaryCod8 += FromByte((int)b3);

                temp1 = BinaryCod8.Substring(0, 6);
                temp2 = BinaryCod8.Substring(6, 6);
                temp3 = BinaryCod8.Substring(12, 6);
                temp4 = BinaryCod8.Substring(18, 6);


                buffer[x * 4] = ToByte(temp1);
                buffer[x * 4 + 1] = ToByte(temp2);
                buffer[x * 4 + 2] = ToByte(temp3);
                buffer[x * 4 + 3] = ToByte(temp4);

            }

            for (int x = 0; x < blockCount * 4; x++)
            {
                result[x] = GetCharFromIndexTable(buffer[x]);
            }

            switch (paddingCount)
            {
                case 0:
                    break;
                case 1:
                    result[blockCount * 4 - 1] = '=';  // 6 бітів зі значенням 0 позначаються як =
                    break;
                case 2:
                    result[blockCount * 4 - 1] = '=';
                    result[blockCount * 4 - 2] = '=';
                    break;
                default:
                    break;
            }

            return result;
        }

        static double Amount_File(byte[] data)
        {

            char[] value = Base64Encoding(data);
            Console.WriteLine(value);
            string sValue = "";
            for (int i = 0; i < value.LongLength; i++)
            {
                sValue += value[i].ToString();
            }

            int tLength = sValue.Length;

            Dictionary<char, double> frequency = new Dictionary<char, double>();

            foreach (var symbol in sValue)
            {
                    if (frequency.ContainsKey(symbol))
                        frequency[symbol]++;
                    else
                        frequency[symbol] = 1;
            }


            double entr = 0;
            for (int i=0; i < frequency.LongCount(); i++)
            { 
                
                    double p;
                    p = frequency.ElementAt(i).Value / tLength;
                    entr += p * Math.Log(1.0 / p, 2);
                
            }

            Console.WriteLine();
            Console.WriteLine("Entropiya = {0:F7}", entr);

            double infa = entr * tLength;

            Console.WriteLine("Amount of information = {0:F0}", infa);


            Console.WriteLine();

            return infa;
        }

        static void Compare(double amount, string path, string extension)
        {
            FileInfo file = new FileInfo(Ext(path,extension));
            long file_size = file.Length;

            Console.WriteLine("In case of using " + extension + ", we have next results:");
            if (amount / 8 < file_size)
                Console.WriteLine(" amount of information is less than file size for: {0:f0} bytes", (file_size - amount / 8));
            else if (amount / 8 > file_size)
                Console.WriteLine(" amount of information is bigger than file size for: {0:f0} bytes", (amount / 8 - file_size));
            else
                Console.WriteLine(" amount of information is the same as file size: {0:f0} bytes", (amount / 8));

        }

        static string Ext(string path,string extension)
        {
            string result = path.Split('.')[0] + extension;
            return result;
        }

        static string FromByte(int b)
        {
            string TempCode="";
            int numberBit = 0;
            while ((b > 1) || (numberBit != 8))
            {
                TempCode += b % 2;
                b = b / 2;
                numberBit++;
            }

            if (TempCode.Length < 8)
            {
                while (TempCode.Length < 8)
                {
                    TempCode += 0;
                }
            }
            char[] charArray = TempCode.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        static int ToByte(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            string ByteCode = new string(charArray);

            int b = 0;
            for (int x =0; x < ByteCode.Length; x++)
            {
                b += (int)Math.Pow(2, x) * int.Parse(ByteCode[x].ToString());
            }

            return b;
        }
    }
}