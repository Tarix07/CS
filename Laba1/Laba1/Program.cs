using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Laba1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            try
            {
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

                        Console.WriteLine();
                        Console.WriteLine(pathIN.Split('\\')[6]);
                        Console.WriteLine();
                        double am1 = Amount_File(pathIN);

                        Compare(am1, pathIN, ".txt");
                        Compare(am1, pathIN, ".gz");
                        Compare(am1, pathIN, ".tar");
                        Compare(am1, pathIN, ".zip");
                        Compare(am1, pathIN, ".rar");
                        Compare(am1, pathIN, ".xz");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("The file could not be open:");
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch
            {
                Console.WriteLine("The number must be in the range from 1 to 3. Try again:");
            }



        }

        static double Amount_File(string path)
        {
            StreamReader reader = new StreamReader(path, Encoding.GetEncoding(1251));
            char[] alphabet = new char[33] {'а', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и',
                                            'і', 'ї', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с',
                                            'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я' };

            string text = ReadAllText(reader).ToLower();
            int tLength = text.Count(Char.IsLetter);

            Dictionary<char, double> frequency = new Dictionary<char, double>();

            for (int i = 0; i < alphabet.Length; i++)
            {
                frequency[alphabet[i]] = 0;
            }
            foreach (var symbol in text)
            {
                if (frequency.ContainsKey(symbol))
                    frequency[symbol]++;
            }


            double entr = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (frequency.ElementAt(i).Value!= 0)
                {
                    double p;
                    p = frequency[alphabet[i]] / tLength;
                    Console.WriteLine("Frequency of symbol \"{0}\" in tetx = {1:f5}", alphabet[i], p);

                    entr += p * Math.Log(1.0 / p, 2);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Entropiya = {0:F7}", entr);

            double infa = entr * tLength;

            Console.WriteLine("Amount of information = {0:F0}", infa);


            Console.WriteLine();

            return infa;
        }

        static String ReadAllText(StreamReader stream)
        {
            string text = "";
            while (stream.Peek() != -1)
            {
                text += stream.ReadLine();
            }
            return text;
        }

        static void Compare(double amount, string path, string extension)
        {
            FileInfo file = new FileInfo(path.Split('.')[0] + extension);
            long file_size = file.Length;

            Console.WriteLine("In case of using " + extension + ", we have next results:");
            if (amount / 8 < file_size)
                Console.WriteLine(" amount of information is less than file size for: {0:f0} bytes\n", (file_size - amount / 8));
            else if (amount / 8 > file_size)
                Console.WriteLine(" amount of information is bigger than file size for: {0:f0} bytes\n", (amount / 8 - file_size));
            else
                Console.WriteLine(" amount of information is the same as file size: {0:f0} bytes\n", (amount / 8));

        }
    }
}
