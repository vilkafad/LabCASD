using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Task16;
using Arrayy;
namespace Task22
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Put");
            comboBox1.Items.Add("Get");
            comboBox1.Items.Add("Remove");
            comboBox1.Text = "Варианты";
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            GraphPane pane = zedGraphControl1.GraphPane;
            Random random = new Random();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    MyHashMap<int, int> list = new MyHashMap<int, int>(10);
                    MyTreeMap<int, int> linkedlist = new MyTreeMap<int, int>();
                    int size;
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double sum = 0;
                        double sum1 = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int n = random.Next(1, size);
                                list.Put(i, n);
                            }
                            timer.Stop();
                            sum += timer.ElapsedMilliseconds;
                            Stopwatch timer1 = new Stopwatch();
                            timer1.Start();
                            for (int i = 0; i < size; i++) linkedlist.Put(i, 2);
                            timer1.Stop();
                            sum1 += timer1.ElapsedMilliseconds;
                        }
                        double rez = sum / 20;
                        double rez2 = sum1 / 20;
                        list1.Add(size, rez);
                        list2.Add(size, rez2);
                        list.Clear();
                        linkedlist.Clear();
                    }
                    break;
                case 1:
                    list = new MyHashMap<int, int>(10);
                    linkedlist = new MyTreeMap<int, int>();
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double sum = 0;
                        double sum1 = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int n = random.Next(1, size);
                                list.Put(i, n);
                            }
                            for (int i = 0; i < size; i++) linkedlist.Put(i, 2);
                            Random rand = new Random();
                            int w = rand.Next(0, size - 1);
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            for (int i = 0; i < size; i++) list.Get(w);
                            stopwatch.Stop();
                            sum += stopwatch.ElapsedMilliseconds;
                            Stopwatch stopwatch1 = new Stopwatch();
                            stopwatch1.Start();
                            for (int i = 0; i < size; i++) linkedlist.Get(w);
                            stopwatch1.Stop();
                            sum1 += stopwatch1.ElapsedMilliseconds;
                        }
                        double rez = sum / 20;
                        double rez2 = sum1 / 20;
                        list1.Add(size, rez);
                        list2.Add(size, rez2);
                        list.Clear();
                        linkedlist.Clear();
                    }
                    break;

                case 2:
                    list = new MyHashMap<int, int>(10);
                    linkedlist = new MyTreeMap<int, int>();
                    for (size = 100; size <= 100; size *= 10)
                    {
                        double sum = 0;
                        double sum1 = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < size; i++)
                            {
                                int n = random.Next(1, size);
                                list.Put(i, n);
                            }
                            for (int i = 0; i < size; i++) linkedlist.Put(i, 2);
                            Random rand = new Random();
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int index = rand.Next(0, list.Size() - 1); list.Remove(index);
                            }
                            stopwatch.Stop();
                            sum += stopwatch.ElapsedMilliseconds;
                            Stopwatch stopwatch1 = new Stopwatch();
                            stopwatch1.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int index = rand.Next(0, linkedlist.Size() - 1); linkedlist.Remove(index);
                            }
                            stopwatch1.Stop();
                            sum1 += stopwatch1.ElapsedMilliseconds;
                        }
                        double rez = sum / 20;
                        double rez2 = sum1 / 20;
                        list1.Add(size, rez);
                        list2.Add(size, rez2);
                        list.Clear();
                        linkedlist.Clear();
                    }
                    break;
            }
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование времени работы структур";
            pane.XAxis.Scale.Max = 100000;
            pane.AddCurve("HASHMAP", list1, Color.Black, SymbolType.Default);
            pane.AddCurve("TREEMAP", list2, Color.Red, SymbolType.Default);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
