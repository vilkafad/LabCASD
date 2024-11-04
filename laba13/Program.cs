using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace ConsoleApp27
{
    public class PriorityQueue : IComparable<PriorityQueue>
    {
        int priority { get; set; }
        int OrderNumber { get; set; }
        int step { get; set; }
        public PriorityQueue(int _priority, int _OrderNumber, int _step)
        {
            priority = _priority;
            OrderNumber = _OrderNumber;
            step = _step;
        }
        public int CompareTo(PriorityQueue other)
        {
            return priority.CompareTo(other.priority);
        }
        static void Main(string[] args)
        {
            string path = "log.txt";
            MyPriorityQueue<PriorityQueue> order = new MyPriorityQueue<PriorityQueue>();
            int n = Convert.ToInt32(Console.ReadLine());
            int cnt = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            StreamWriter sw = new StreamWriter(path);
           
            for (int i = 0; i < n; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(1, 11);
                for (int j = 0; j < num; j++)
                {
                    
                    int prt = rnd.Next(1, 6);
                    PriorityQueue list = new PriorityQueue(prt, j, i);
                    order.Add(list);
                    sw.WriteLine("добавление:" + " " + list.OrderNumber + " " + list.priority + " " + list.step);
                    ++cnt;
                }
                PriorityQueue temporaryPr = order.Poll();
                sw.WriteLine("удаление:" + " " + temporaryPr.OrderNumber + " " + temporaryPr.priority + " " + temporaryPr.step);
                cnt--;
            }

            for (int i = 0; i < cnt; i++)
            {

                PriorityQueue temporaryPr = order.Peek();
                sw.WriteLine("удаление:" + " " + temporaryPr.OrderNumber + " " + temporaryPr.priority + " " + temporaryPr.step);
                order.Remove(order.Peek());
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            sw.WriteLine($"время выполнения: {elapsedTime.TotalSeconds} секунд");
            sw.Close();
        }
    }
}
