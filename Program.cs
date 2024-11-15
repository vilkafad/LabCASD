using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting.Contexts;


namespace laba15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyArrayDeque<string> array = new MyArrayDeque<string>();
            string inputFile = ("input.txt");
            string outputFile = ("sorted.txt");
            StreamReader str = new StreamReader(inputFile);
            StreamWriter sw = new StreamWriter(outputFile);
            int countHead = 0;
            while (!str.EndOfStream)
            {
                string str1 = str.ReadLine();
                int countNext = str1.Count(c => c >= '0' && c <= '9');
                if (array.Size() == 0)
                {
                    array.add(str1);
                    countHead = countNext;
                }
                else
                {
                    for (int i = 0; i < str1.Length; i++)
                    {
                        if (str1[i] >= '0' && str1[i] <= '9')
                        {
                            countNext++;
                        }
                    }
                    if (countHead >= countNext)
                    {
                        array.addFirst(str1);
                        countHead = countNext;
                    }
                    else array.add(str1);
                }
            }
            sw.WriteLine(array.print());
            sw.Close();
            Console.WriteLine("Введите число пробелов: ");
            string str2 = Console.ReadLine();
            int n = Convert.ToInt32(str2);
            for (int i = 0; i < array.Size(); i++)
            {
                string str3 = array.get(i);
                int count = str3.Count(c => c == ' ');
                if (count>n) { array.remove(str3); }
            }
            Console.WriteLine(array.print());
        }
    }
}