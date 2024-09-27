using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortingLib;
using System.Runtime;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using ZedGraph;
namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        
        int flag = 0;
        public Form1()
        {
            InitializeComponent();
            
        }
        //Создание модели интерфейса
        private void Form1_Load(object sender, EventArgs e)
        {
            this.comboBox2.Items.Add("Первая группа");
            this.comboBox2.Items.Add("Вторая группа");
            this.comboBox2.Items.Add("Третья группа");
            this.comboBox2.SelectedText = "Выберите группу";
            this.comboBox4.Items.Add("Массив случайных чисел");
            this.comboBox4.Items.Add("Разбитые на подмассивы");
            this.comboBox4.Items.Add("Массивы с перестановками");
            this.comboBox4.Items.Add("Массивы с повторением");
            this.comboBox4.SelectedText = "Текстовые данные";
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива,млрд шт";
            pane.YAxis.Title.Text = "Время выполнения,мс";
            pane.Title.Text = "Зависимость времени выполнения от размера массива";

        }
        //проводка пути к файлам
        public void InsertPath()
        {
            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            pathToArray = appDir + @"\arrayy.txt";
            pathToTime = appDir + @"\time.txt";
        }
        string pathToArray;
        string pathToTime;
        //метод выполняющия щапись массивов в файл, и подсчет времени работы сортировок+запись этого времени  в файл
        private void SpeedTime(Func<int, int[]> Generate, int length, params Func<int[], int[]>[] SortMethods)
        {
            InsertPath();
            double[] sumSpeed = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] array = Generate(length);
                try
                {
                    StreamWriter sw = File.AppendText(pathToArray);
                    sw.WriteLine("Unsorted array: ");
                    foreach (int item in array) sw.Write(item.ToString() + " ");

                    sw.Write("\n");

                    int[] sortedArray = null;
                    int index = 0;
                    foreach (Func<int[], int[]> Method in SortMethods)
                    {
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        sortedArray = Method(array);
                        timer.Stop();
                        sumSpeed[index] += timer.ElapsedMilliseconds;
                        index++;
                    }
                    sw.WriteLine("Sorted array: ");
                    foreach (int item in sortedArray) sw.Write(item.ToString() + " ");
                    sw.Write("\n");
                    sw.Close();
                }
                catch (Exception ex) { Console.WriteLine(ex); };
            }
            try
            {
                StreamWriter sw = File.AppendText(pathToTime);
                for (int i = 0; i < sumSpeed.Length; i++)
                {
                    if (i == 0)
                    {
                        sw.Write(((double)(sumSpeed[i] / 20)).ToString());
                        continue;
                    }
                    sw.Write(" " + ((double)(sumSpeed[i] / 20)).ToString());
                }
                sw.WriteLine();
                sw.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex); };
        }
        //Вызов метеда по подсчету времени и записи в файл, посредством нажатия на кнопку
        //Реализация через операторы выбора, для удобного понимая выбора данных пользователем 
        private void button5_Click(object sender, EventArgs e)
        {
            
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    flag = 1;
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 1000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.BubbleSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion2, length, Sorting.BubbleSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion3, length, Sorting.BubbleSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion4, length, Sorting.BubbleSort, Sorting.InsertionSort, Sorting.SelectionSort, Sorting.ShakerSort, Sorting.GnomeSort);
                            break;
                    }
                    break;
                case 1:
                    flag = 2;
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.ShellSort, Sorting.BitonicSort,Sorting.TreeSort);
                            
                            break;
                        case 1:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.ShellSort, Sorting.BitonicSort, Sorting.TreeSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.ShellSort, Sorting.BitonicSort, Sorting.TreeSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 1000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.ShellSort, Sorting.BitonicSort, Sorting.TreeSort);
                            break;
                    }
                    break;
                case 2:
                    flag = 3;
                    switch (comboBox4.SelectedIndex)
                    {
                        case 0:
                            for (int length = 10; length <= 100000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion1, length, Sorting.CombSort, Sorting.HeapSort, Sorting.QuickSort, Sorting.MergeSort, Sorting.CountingSort, Sorting.RadixSort);
                            break;
                        case 1:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion2, length, Sorting.CombSort, Sorting.HeapSort, Sorting.QuickSort, Sorting.MergeSort, Sorting.CountingSort, Sorting.RadixSort);
                            break;
                        case 2:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion3, length, Sorting.CombSort, Sorting.HeapSort, Sorting.QuickSort, Sorting.MergeSort, Sorting.CountingSort, Sorting.RadixSort);
                            break;
                        case 3:
                            for (int length = 10; length <= 10000000; length *= 10)
                                SpeedTime(Sorting.ArrayVersion4, length, Sorting.CombSort, Sorting.HeapSort, Sorting.QuickSort, Sorting.MergeSort, Sorting.CountingSort, Sorting.RadixSort);
                            break;
                    }
                    break;
            }
        }
        
        //Построение графиков по результатам записей файла timee
        private void button6_Click(object sender, EventArgs e)
        {
            GraphPane pane = zedGraph.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Время выполнения";
            pane.Title.Text = "Исследование скорости работы сортировок";
            InsertPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathToTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray)
                    {
                        speed.Add(Convert.ToDouble(str));
                    }
                    time.Add(speed);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            
            for (int i = 0; i < time[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j < time.Count(); j++)
                {
                   
                    pointList.Add(x, time[j][i]);
                    x *= 10;
                }
                switch (i)
                {
                    case 0:
                        if (flag == 1)
                        {
                            pane.AddCurve("Пузырек: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Ракушка: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Расческа: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1)
                        {
                            pane.AddCurve("Вставками: " + i, pointList, Color.Red, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Деревом: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Кучей: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 2:
                        if (flag == 1)
                        {
                            pane.AddCurve("Выбором: " + i, pointList, Color.Pink, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.AddCurve("Битон: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Быстрая: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 3:
                        if (flag == 1)
                        {
                            pane.AddCurve("Шейкерная: " + i, pointList, Color.Yellow, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Слияние: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 4:
                        if (flag == 1)
                        {
                            pane.AddCurve("Гномья: " + i, pointList, Color.Green, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.AddCurve("Подсчетом: " + i, pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 5:
                        pane.AddCurve("Разрядная: " + i, pointList, Color.Aquamarine, SymbolType.Default);
                        break;
                }
            }

            zedGraph.AxisChange();
            zedGraph.Invalidate();

        }
    

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void zedGraphControl2_Load(object sender, EventArgs e)
        {

        }
    }
}
