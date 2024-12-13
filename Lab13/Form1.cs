using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Lab13
{
    public partial class Form1 : Form
    {
        private List<Array> testArrays;
        private volatile Dictionary<string, List<double>> sortResults;
        public Form1()
        {
            InitializeComponent();
        }

        private async void buttonRunTests_Click(object sender, EventArgs e)
        {
            if (testArrays == null || testArrays.Count == 0)
            {
                MessageBox.Show("Сначала сгенерируйте массивы.");
                return;
            }
            sortResults = new Dictionary<string, List<double>>();//созд словаря для резов, кл-имена алг, знач-врем выполн мас
            //определ выбранный тип данных
            Type selectedType = GetSelectedDataType(); //метод для получения типа из формы
            if (selectedType == typeof(int))
            {
                RunSortingTestsForType<int>(Comparer<int>.Default, x => x);
            }
            else if (selectedType == typeof(float))
            {
                RunSortingTestsForType<float>(Comparer<float>.Default, x => (int)(x * 1000)); 
            }
            else if (selectedType == typeof(double))
            {
                RunSortingTestsForType<double>(Comparer<double>.Default, x => (int)(x * 1000)); 
            }
            else if (selectedType == typeof(DateTime))
            {
                RunSortingTestsForType<DateTime>(Comparer<DateTime>.Default, x => (int)(x - new DateTime(2000, 1, 1)).TotalDays);
            }
            else
            {
                MessageBox.Show("Выбранный тип данных не поддерживается.");
            }

            DisplayGraph(sortResults);
        }

        private Type GetSelectedDataType()
        {
            
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    return typeof(int);
                case 1:
                    return typeof(float);
                case 2:
                    return typeof(double);
                case 3:
                    return typeof(DateTime);
                default:
                    return typeof(int);
            }
        }

        private void RunSortingTestsForType<T>(IComparer<T> comparer, Func<T, int> keySelector)
        {
            int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

            switch (selectedGroupIndex)
            {
                case 0:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.BubbleSort, "Bubble Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.ShakerSort, "Shaker Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.GnomeSort, "Gnome Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.SelectionSort, "Selection Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.InsertionSort, "Insertion Sort", comparer)
                    );
                    break;
                case 1:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.ShellSort, "Shell Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.TreeSort, "Tree Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.BitonicSort, "Bitonic Sort", comparer)
                    );
                    break;
                case 2:
                    Parallel.Invoke(
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.CombSort, "Comb Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.HeapSort, "Heap Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.QuickSort, "Quick Sort", comparer),
                        () => RunSortingTestsWithResult<T>(GenericSortingAlgorithms.MergeSort, "Merge Sort", comparer),
                        () => RunSortingTestsWithResult<T>((array, c) => GenericSortingAlgorithms.CountingSort(array, keySelector), "Counting Sort", comparer), //оно определяет анонимную функцию, которая принимает два аргумента:  array (массив для сортировки) и c компаратор
                        () => RunSortingTestsWithResult<T>((array, c) => GenericSortingAlgorithms.RadixSort(array, keySelector), "Radix Sort", comparer)
                    );
                    break;
                default:
                    MessageBox.Show("Выберите группу алгоритмов.");
                    break;
            }
        }

        // передовать различные алгоритмы сортировки,  которые должны соответствовать этому делегату
        // (принимать массив и комп в качестве параметров
        private void RunSortingTestsWithResult<T>(Action<T[], IComparer<T>> sortMethod, string sortName, IComparer<T> comparer)
        {
            var times = new List<double>();
            int numRuns = 5; //количество запусков 

            foreach (Array array in testArrays)
            {
                double totalTime = 0;

                Parallel.For(0, numRuns, run =>
                {
                    T[] arrayCopy = array.Cast<T>().ToArray();

                    Stopwatch stopwatch = Stopwatch.StartNew();
                    sortMethod(arrayCopy, comparer); //передача компаратора
                    stopwatch.Stop();

                    lock (times)
                    {
                        totalTime += stopwatch.ElapsedMilliseconds;
                    }
                });

                double avgTime = totalTime / numRuns;
                times.Add(avgTime);
            }

            lock (sortResults)
            {
                sortResults[sortName] = times;
            }
        }

        

        private void DisplayGraph(Dictionary<string, List<double>> sortResults)
        {
            var pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            pane.Title.Text = "Сравнение эффективности алгоритмов сортировки";
            pane.XAxis.Title.Text = "Размер массива";
            pane.YAxis.Title.Text = "Среднее время (мс)";

            var sizes = testArrays.Select(arr => (double)arr.Length).ToArray();

            var colors = new Color[]
            {
                Color.Red,
                Color.Blue,
                Color.Green,
                Color.Purple,
                Color.Orange,
                Color.Black,
            };

            int colorIndex = 0;

            foreach (var sortResult in sortResults)
            {
                var curve = pane.AddCurve(sortResult.Key, sizes, sortResult.Value.ToArray(), colors[colorIndex++ % colors.Length]);
                curve.Symbol.Type = SymbolType.Circle;
                curve.Line.Width = 2;
            }

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            if (testArrays == null || testArrays.Count == 0)
            {
                MessageBox.Show("Сначала сгенерируйте массивы.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Сохранить результаты";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine("Результаты сортировки:");

                    writer.WriteLine("Сгенерированные массивы:");
                    foreach (var array in testArrays)
                    {
                        writer.WriteLine($"Массив: {string.Join(", ", array.Cast<object>())}");
                    }

                    writer.WriteLine();

                    var selectedType = GetSelectedDataType();
                    int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

                    switch (selectedType)
                    {
                        case Type t when t == typeof(int):
                            SaveSortGroup<int>(writer, selectedGroupIndex, Comparer<int>.Default);
                            break;

                        case Type t when t == typeof(float):
                            SaveSortGroup<float>(writer, selectedGroupIndex, Comparer<float>.Default);
                            break;

                        case Type t when t == typeof(double):
                            SaveSortGroup<double>(writer, selectedGroupIndex, Comparer<double>.Default);
                            break;

                        case Type t when t == typeof(DateTime):
                            SaveSortGroup<DateTime>(writer, selectedGroupIndex, Comparer<DateTime>.Default);
                            break;

                        default:
                            MessageBox.Show("Выбранный тип данных не поддерживается.");
                            break;
                    }

                    MessageBox.Show("Результаты успешно сохранены.");
                }
            }
        }
        //записывает результаты сортировки для выбранной группы алгоритмов в файл
        private void SaveSortGroup<T>(StreamWriter writer, int selectedGroupIndex, IComparer<T> comparer)
        {
            switch (selectedGroupIndex)
            {
                case 0:
                    SaveSortedArray(GenericSortingAlgorithms.BubbleSort, "Bubble Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.ShakerSort, "Shaker Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.GnomeSort, "Gnome Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.SelectionSort, "Selection Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.InsertionSort, "Insertion Sort", writer, comparer);
                    break;

                case 1:
                    SaveSortedArray(GenericSortingAlgorithms.ShellSort, "Shell Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.TreeSort, "Tree Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.BitonicSort, "Bitonic Sort", writer, comparer);
                    break;

                case 2:
                    SaveSortedArray(GenericSortingAlgorithms.CombSort, "Comb Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.HeapSort, "Heap Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.QuickSort, "Quick Sort", writer, comparer);
                    SaveSortedArray(GenericSortingAlgorithms.MergeSort, "Merge Sort", writer, comparer);
                    SaveCountingSort(writer, comparer);
                    SaveRadixSort(writer, comparer);
                    break;

                default:
                    MessageBox.Show("Выберите группу алгоритмов для сортировки.");
                    break;
            }
        }

        private void SaveSortedArray<T>(Action<T[], IComparer<T>> sortMethod, string sortName, StreamWriter writer, IComparer<T> comparer)
        {
            writer.WriteLine($"Результаты для {sortName}:");

            foreach (Array array in testArrays)
            {
                T[] arrayCopy = array.Cast<T>().ToArray();
                sortMethod(arrayCopy, comparer);
                writer.WriteLine($"Отсортированный массив: {string.Join(", ", arrayCopy)}");
            }

            writer.WriteLine();
        }
        //для целых
        private void SaveCountingSort<T>(StreamWriter writer, IComparer<T> comparer)
        {
            writer.WriteLine("Результаты для Counting Sort:");

            if (typeof(T) == typeof(int))
            {
                foreach (Array array in testArrays)
                {
                    int[] arrayCopy = array.Cast<int>().ToArray();
                    GenericSortingAlgorithms.CountingSort(arrayCopy, x => x);
                    writer.WriteLine($"Отсортированный массив: {string.Join(", ", arrayCopy)}");
                }
            }
            else
            {
                writer.WriteLine("Counting Sort поддерживает только целочисленные ключи.");
            }

            writer.WriteLine();
        }

        private void SaveRadixSort<T>(StreamWriter writer, IComparer<T> comparer)
        {
            writer.WriteLine("Результаты для Radix Sort:");

            if (typeof(T) == typeof(int))
            {
                foreach (Array array in testArrays)
                {
                    int[] arrayCopy = array.Cast<int>().ToArray();
                    GenericSortingAlgorithms.RadixSort(arrayCopy, x => x);
                    writer.WriteLine($"Отсортированный массив: {string.Join(", ", arrayCopy)}");
                }
            }
            else
            {
                writer.WriteLine("Radix Sort поддерживает только целочисленные ключи.");
            }

            writer.WriteLine();
        }

        private void buttonGenerateArrays_Click(object sender, EventArgs e)
        {
            int selectedGroupIndex = comboBoxDataGroup.SelectedIndex;
            int selectedTypeIndex = comboBox1.SelectedIndex;

            if (selectedTypeIndex < 0)
            {
                MessageBox.Show("Выберите тип данных.");
                return;
            }

            Type selectedType = GetTypeByIndex(selectedTypeIndex);
            if (selectedType == null)
            {
                MessageBox.Show("Неизвестный тип данных.");
                return;
            }

            var sizes = GetArraySizesToAlgorithm();
            // С помощью метода Select каждый сгенерированный массив преобразуется в тип Array
            // и затем собирается в список с помощью ToList()
            if (selectedType == typeof(int))
                testArrays = GenerateForType<int>(selectedGroupIndex, sizes).Select(arr => (Array)arr).ToList();
            else if (selectedType == typeof(float))
                testArrays = GenerateForType<float>(selectedGroupIndex, sizes).Select(arr => (Array)arr).ToList();
            else if (selectedType == typeof(double))
                testArrays = GenerateForType<double>(selectedGroupIndex, sizes).Select(arr => (Array)arr).ToList();
            else if (selectedType == typeof(DateTime))
                testArrays = GenerateForType<DateTime>(selectedGroupIndex, sizes).Select(arr => (Array)arr).ToList();


            MessageBox.Show("Массивы сгенерированы.");

        }
        private Type GetTypeByIndex(int index)
        {
            switch (index)
            {
                case 0: return typeof(int);
                case 1: return typeof(float);
                case 2: return typeof(double);
                case 3: return typeof(DateTime);
                default: return null;
            }
        }
        private List<T[]> GenerateForType<T>(int groupIndex, int[] sizes)
        {
            switch (groupIndex)
            {
                case 0:
                    return GenerateArrays(size => GenerateRandomArray<T>(size), sizes);
                case 1:
                    return GenerateArrays(size => GeneratePartiallySortedArray<T>(size), sizes);
                case 2:
                    return GenerateArrays(size => GenerateNearlySortedArray<T>(size), sizes);
                case 3:
                    return GenerateArrays(size => GenerateModifiedSortedArray<T>(size), sizes);
                default:
                    throw new InvalidOperationException("Неизвестная группа данных.");
            }
        }

        private int[] GetArraySizesToAlgorithm()
        {
            int selectedGroupIndex = comboBoxSortGroup.SelectedIndex;

            var sizes = new List<int>();
            for (int i = 1; i <= 4 + selectedGroupIndex; i++)
                sizes.Add((int)Math.Pow(10.0, i));

            return sizes.ToArray();

        }

        public static List<T[]> GenerateArrays<T>(Func<int, T[]> generatorFunc, int[] sizes)
        {
            var arrays = new List<T[]>();
            foreach (var size in sizes)
            {
                arrays.Add(generatorFunc(size));
            }
            return arrays;
        }

        public static T[] GenerateRandomArray<T>(int size)
        {
            Random rand = new Random();
            if (typeof(T) == typeof(int))
            {
                return Enumerable.Range(0, size).Select(_ => (T)(object)rand.Next(0, 1000)).ToArray();
            }
            if (typeof(T) == typeof(double))
            {
                return Enumerable.Range(0, size).Select(_ => (T)(object)(rand.NextDouble() * 1000)).ToArray();
            }
            if (typeof(T) == typeof(float))
            {
                return Enumerable.Range(0, size).Select(_ => (T)(object)((float)rand.NextDouble() * 1000)).ToArray();
            }
            if (typeof(T) == typeof(DateTime))
            {
                return Enumerable.Range(0, size).Select(_ => (T)(object)DateTime.Now.AddDays(rand.Next(-1000, 1000))).ToArray();
            }
            throw new NotSupportedException($"Тип {typeof(T)} не поддерживается.");
        }

        public static T[] GeneratePartiallySortedArray<T>(int size)
        {
            var array = GenerateRandomArray<T>(size);
            Random rand = new Random();
            int subArrayCount = rand.Next(1, 6);
            int subArraySize = size / subArrayCount;

            for (int i = 0; i < subArrayCount; i++)
            {
                int startIndex = i * subArraySize;
                Array.Sort(array, startIndex, Math.Min(subArraySize, array.Length - startIndex));
            }

            return array;
        }

        public static T[] GenerateNearlySortedArray<T>(int size)
        {
            var array = GenerateRandomArray<T>(size);
            Random rand = new Random();
            int swaps = rand.Next(1, size / 10);

            for (int i = 0; i < swaps; i++)
            {
                int index1 = rand.Next(0, size);
                int index2 = rand.Next(0, size);
                (array[index1], array[index2]) = (array[index2], array[index1]);
            }

            return array;
        }


        public static T[] GenerateModifiedSortedArray<T>(int size)
        {
            var array = GenerateRandomArray<T>(size);
            Random rand = new Random();
            int modifications = rand.Next(1, size / 5);

            for (int i = 0; i < modifications; i++)
            {
                int index = rand.Next(0, size);
                if (typeof(T) == typeof(int))
                {
                    array[index] = (T)(object)rand.Next(0, 1000);
                }
                else if (typeof(T) == typeof(double))
                {
                    array[index] = (T)(object)(rand.NextDouble() * 1000);
                }
                else if (typeof(T) == typeof(float))
                {
                    array[index] = (T)(object)((float)rand.NextDouble() * 1000);
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    array[index] = (T)(object)DateTime.Now.AddDays(rand.Next(-100, 100));
                }
            }

            return array;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
