using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ZedGraph;
using System.Collections;
using Task16;
namespace Task17.Gr
{
    public partial class Исследование : Form
    {
        public Исследование()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("add");
            comboBox1.Items.Add("get");
            comboBox1.Items.Add("set");
            comboBox1.Items.Add("randomAdd");
            comboBox1.Items.Add("remove");
            comboBox1.Text = "выбирайте";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PointPairList pointsOfArray = new PointPairList();
            PointPairList pointsOfLinkedArray = new PointPairList(); //point on graph
            GraphPane pane = zedGraphControl1.GraphPane;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    MyArrayList<int> arrayList = new MyArrayList<int>(10);
                    MyLinkedList<int> linkedList = new MyLinkedList<int>();
                    int size;
                    for (size = 100; size <= 100000; size *= 10)
                    {
                        double timeOfSumArray = 0;
                        double timeOfSumLinked = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Stopwatch timerFirst = new Stopwatch();
                            timerFirst.Start();
                            for (int j = 0; j < size; j++) arrayList.add(j);
                            timerFirst.Stop();
                            timeOfSumArray += timerFirst.ElapsedMilliseconds;
                            Stopwatch timerSecond = new Stopwatch();
                            timerSecond.Start();
                            for (int j = 0; j < size; j++) linkedList.add(j);
                            timerSecond.Stop();
                            timeOfSumLinked += timerSecond.ElapsedMilliseconds;
                        }
                        double resultOfArray = timeOfSumArray / 20;
                        double resultOfLinked = timeOfSumLinked / 20;
                        pointsOfArray.Add(size, resultOfArray);
                        pointsOfLinkedArray.Add(size, resultOfLinked);
                        arrayList.Clear();
                        linkedList.Clear();
                    }
                    break;
                case 1:
                    arrayList = new MyArrayList<int>(10);
                    linkedList = new MyLinkedList<int>();
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double timeOfSumArray = 0;
                        double timeOfSumLinked = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < size; i++)
                                arrayList.add(i);
                            for (int i = 0; i < size; i++)
                                linkedList.add(i);
                            Random random = new Random();
                            int randomIndex = random.Next(0, size - 1);
                            Stopwatch timerFirst = new Stopwatch();
                            timerFirst.Start();
                            for (int i = 0; i < size; i++)
                                arrayList.get(randomIndex);
                            timerFirst.Stop();
                            timeOfSumArray += timerFirst.ElapsedMilliseconds;
                            Stopwatch timerSecond = new Stopwatch();
                            timerSecond.Start();
                            for (int i = 0; i < size; i++)
                                linkedList.get(randomIndex);
                            timerSecond.Stop();
                            timeOfSumLinked += timerSecond.ElapsedMilliseconds;
                        }
                        double resultOfArray = timeOfSumArray / 20;
                        double resultOfLinked = timeOfSumLinked / 20;
                        pointsOfArray.Add(size, resultOfArray);
                        pointsOfLinkedArray.Add(size, resultOfLinked);
                        arrayList.Clear();
                        linkedList.Clear();
                    }
                    break;
                case 2:
                    arrayList = new MyArrayList<int>(10);
                    linkedList = new MyLinkedList<int>();
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double timeOfSumArray = 0;
                        double timeOfSumLinked = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                                arrayList.add(i);
                            for (int j = 0; j < size; j++)
                                linkedList.add(j);
                            Random random = new Random();
                            Stopwatch timerFirst = new Stopwatch();
                            timerFirst.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int indexOfElement = random.Next(0, arrayList.Size - 1);
                                int number = random.Next(0, 100000);
                                arrayList.set(indexOfElement, number);
                            }
                            timerFirst.Stop();
                            timeOfSumArray += timerFirst.ElapsedMilliseconds;
                            Stopwatch timerSecond = new Stopwatch();
                            timerSecond.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int indexOfElement = random.Next(0, linkedList.Size() - 1);
                                int number = random.Next(0, 100000);
                                linkedList.set(indexOfElement, number);
                            }
                            timerSecond.Stop();
                            timeOfSumLinked += timerSecond.ElapsedMilliseconds;
                        }
                        double resultOfArray = timeOfSumArray / 20;
                        double resultOfLinked = timeOfSumLinked / 20;
                        pointsOfArray.Add(size, resultOfArray);
                        pointsOfLinkedArray.Add(size, resultOfLinked);
                        arrayList.Clear();
                        linkedList.Clear();
                    }
                    break;
                case 3:
                    arrayList = new MyArrayList<int>(10);
                    linkedList = new MyLinkedList<int>();
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double timeOfSumArray = 0;
                        double timeOfSumLinked = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < size; i++)
                                arrayList.add(i);
                            for (int i = 0; i < size; i++) 
                                linkedList.add(i);
                            Random random = new Random();
                            int randomIndex = random.Next(0, size - 1);
                            Stopwatch timerFirst = new Stopwatch();
                            timerFirst.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int indexOfElement = random.Next(0, arrayList.Size - 1);
                                int number = random.Next(0, 100000);
                                arrayList.addIndex(indexOfElement, number);
                            }
                            timerFirst.Stop();
                            timeOfSumArray += timerFirst.ElapsedMilliseconds;
                            Stopwatch timerSecond = new Stopwatch();
                            timerSecond.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int indexOfElement = random.Next(0, linkedList.Size() - 1);
                                int number = random.Next(0, 100000);
                                linkedList.set(indexOfElement, number);
                            }
                            timerSecond.Stop();
                            timeOfSumLinked += timerSecond.ElapsedMilliseconds;
                        }
                        double resultOfArray = timeOfSumArray / 20;
                        double resultOfLinked = timeOfSumLinked / 20;
                        pointsOfArray.Add(size, resultOfArray);
                        pointsOfLinkedArray.Add(size, resultOfLinked);
                        arrayList.Clear();
                        linkedList.Clear();
                    }
                    break;
                case 4:
                    arrayList = new MyArrayList<int>(10);
                    linkedList = new MyLinkedList<int>();
                    for (size = 100; size <= 1000; size *= 10)
                    {
                        double timeOfSumArray = 0;
                        double timeOfSumLinked = 0;
                        for (int j = 0; j < 20; j++)
                        {
                            for (int i = 0; i < size; i++)
                                arrayList.add(i);
                            for (int i = 0; i < size; i++) linkedList.add(i);
                            Random random = new Random();
                            Stopwatch timerFirst = new Stopwatch();
                            timerFirst.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int index = random.Next(0, arrayList.Size - 1);
                                arrayList.remove(index);
                            }
                            timerFirst.Stop();
                            timeOfSumArray += timerFirst.ElapsedMilliseconds;
                            Stopwatch timerSecond = new Stopwatch();
                            timerSecond.Start();
                            for (int i = 0; i < size; i++)
                            {
                                int index = random.Next(0, linkedList.Size() - 1); //
                                linkedList.remove(index);
                            }
                            timerSecond.Stop();
                            timeOfSumLinked += timerSecond.ElapsedMilliseconds;
                        }
                        double resultOfArray = timeOfSumArray / 20;
                        double resultOfLinked = timeOfSumLinked / 20;
                        pointsOfArray.Add(size, resultOfArray);
                        pointsOfLinkedArray.Add(size, resultOfLinked);
                        arrayList.Clear();
                        linkedList.Clear();
                    }
                    break;
            }
            pane.XAxis.Title.Text = "размер массива";
            pane.YAxis.Title.Text = "время";
            pane.Title.Text = "сравнение работы";
            pane.XAxis.Scale.Max = 100000;
            pane.AddCurve("array", pointsOfArray, Color.Purple, SymbolType.Default);
            pane.AddCurve("linked", pointsOfLinkedArray, Color.Blue, SymbolType.Default);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
