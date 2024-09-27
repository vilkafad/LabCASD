using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class TreeNode
{
    public TreeNode(int data)
    {
        Data = data;
    }

    //данные 
    public int Data { get; set; }

    //левая ветка дерева 
    public TreeNode Left { get; set; }

    //правая ветка дерева 
    public TreeNode Right { get; set; }

    //рекурсивное добавление узла в дерево 
    public void Insert(TreeNode node)
    {
        if (node.Data < Data)
        {
            if (Left == null)
            {
                Left = node;
            }
            else
            {
                Left.Insert(node);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = node;
            }
            else
            {
                Right.Insert(node);
            }
        }
    }

    //преобразование дерева в отсортированный массив 
    public int[] Transform(List<int> elements = null)
    {
        if (elements == null)
        {
            elements = new List<int>();
        }

        if (Left != null)
        {
            Left.Transform(elements);
        }

        elements.Add(Data);

        if (Right != null)
        {
            Right.Transform(elements);
        }

        return elements.ToArray();
    }
}
namespace SortingLib
{
    public static class Sorting
    {
        private static int GetNextStep(int s)
        {
            s = s * 1000 / 1247; return s > 1 ? s : 1;
        }
        private static void siftDown(int[] array, int i, int upper)
        {
            while (true)
            {
                int l = 2 * i + 1;
                int r = 2 * i + 2;
                if (Math.Max(l, r) < upper)
                {
                    if (array[i] >= Math.Max(array[l], array[r])) break;
                    else if (array[l] > array[r])
                    {
                        Swap(array, i, l);
                        i = l;
                    }
                    else
                    {
                        Swap(array, i, r);
                    }
                }
                else if (l < upper)
                {
                    if (array[l] > array[i])
                    {
                        Swap(array, i, l);
                        i = l;
                    }
                    else break;
                }
                else if (r < upper)
                {
                    if (array[r] > array[i])
                    {
                        Swap(array, i, r);
                        i = r;
                    }
                    else break;
                }
                else break;
            }
        }
        private static void Merge(int[] array, int lowIndex, int middleIndex, int highIndex)
        {
            int left = lowIndex; int right = middleIndex + 1;
            int[] tempArray = new int[highIndex - lowIndex + 1]; int index = 0;
            while ((left <= middleIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left]; left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }
                index++;
            }
            for (int i = left; i <= middleIndex; i++)
            {
                tempArray[index] = array[i]; index++;
            }
            for (int i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[lowIndex + i] = tempArray[i];
            }
        }
        private static void countSort(int[] array, int n, int exp)
        {
            int[] output = new int[n];
            int i;
            int[] count = new int[10];
            for (int j = 0; j < 10; j++) count[j] = 0;
            for (i = 0; i < n; i++)
                count[(array[i] / exp) % 10]++;
            for (i = 1; i < 10; i++)
                count[i] += count[i - 1];
            for (i = n - 1; i >= 0; i--)
            {
                output[count[(array[i] / exp) % 10] - 1] = array[i];
                count[(array[i] / exp) % 10]--;
            }
            for (i = 0; i < n; i++)
                array[i] = output[i];
        }
        private static int getMax(int[] arr, int n)
        {
            int mx = arr[0];
            for (int i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        }
        private static void BitonicMerge(int[] array, int low, int cnt, Direction dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;
                for (int i = low; i < low + k; ++i)
                {
                    if ((dir == Direction.ASCENDING && array[i] > array[i + k]) ||
                        (dir == Direction.DESCENDING && array[i] < array[i + k]))
                    {
                        Swap(array, i, i + k);
                    }
                }
                BitonicMerge(array, low, k, dir);
                BitonicMerge(array, low + k, k, dir);
            }
        }
        private static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
        private static void Swap(int[] array, int i, int j)
        {
            int step = array[i];
            array[i] = array[j];
            array[j] = step;
        }
        public enum Direction { ASCENDING, DESCENDING };
        public static Random random = new Random();
        public static int[] GenerateRandomArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1000); // Генерация случайных чисел по модулю 1000
            }
            return array;
        }
        public static int[] GenerateSortedSubarrays(int totalSize)
        {
            int i = 0;
            int maxSubarraySize = random.Next(10, totalSize);
            int[] fullArray = new int[totalSize];
            int currentIndex = 0;
            while (totalSize > 0)
            {
                // Генерация случайного размера подмассива
                int subarraySize = random.Next(1, Math.Min(totalSize, maxSubarraySize) + 1);
                int[] subarray = GenerateRandomArray(subarraySize);
                Array.Sort(subarray); // Сортировка подмассива

                // Добавление отсортированного подмассива в полный массив
                for (int j = 0; j < subarray.Length; j++)
                {
                    fullArray[currentIndex++] = subarray[j];
                }

                totalSize -= subarraySize; // Уменьшение оставшегося объема
            }
            return fullArray;
        }
        public static int[] GenerateSwappedSortedArray(int size)
        {
            int[] array = GenerateRandomArray(size);
            Array.Sort(array); // Сортировка массива
            int k = random.Next(array.Length);
            // Перестановка двух случайных элементов
            for (int i = 0; i < k; i++)
            {
                if (size >= 2)
                {
                    int index1 = random.Next(size);
                    int index2 = random.Next(size);
                    Swap(array, index1, index2);
                }
            }

            return array;
        }
        public static int[] RandomBySwapAndRepeat(int length)
        {
            int[] array = GenerateSwappedSortedArray(length);
            Random random = new Random();
            int indexOfRepeat = random.Next(0, length - 1);
            int countOfRepeat = random.Next(0, length / 3);

            while (countOfRepeat > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[indexOfRepeat])
                {
                    array[randomIndex] = array[indexOfRepeat];
                    countOfRepeat--;
                }

            }
            return array;
        }
        
        public static int[] BubbleSort(int[] array)
        {
            int Nums = array.Length;
            for (int i = 0; i < Nums; i++)
            {
                for (int j = i + 1; j < Nums; j++)
                {
                    if (array[i] > array[j])
                    {
                        Swap(array, i, j);
                    }
                }
            }
            return array;
        }
        public static int[] TreeSort(int[] array)
        {
            TreeNode root = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++) root.Insert(new TreeNode(array[i]));
            int[] newArray = root.Transform();
            for (int i = 0; i < array.Length; i++)
            {
                
                array[i] = newArray[i];
            }
            return array;
        }
        public static int[] ShakerSort(int[] array)
        {
            int left = 0;
            int right = array.Length - 1;
            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(array, i, i + 1);
                    }
                }
                --right;
                for (int i = right; i > left; i--)
                {
                    if (array[i - 1] > array[i])
                    {
                        Swap(array, i, i - 1);
                    }
                }
                left++;
            }
            return array;
        }
        public static int[] CombSort(this int[] array)
        {
            int step = array.Length - 1;
            while (step > 1)
            {
                for (int i = 0; i + step < array.Length; i++)
                {
                    if (array[i] > array[i + step])
                    {
                        Swap(array, i, i + step);
                    }
                }
                step = GetNextStep(step);
            }
            BubbleSort(array);
            return array;
        }
        public static int[] InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && (array[j - 1] > array[j]))
                {
                    Swap(array, j, j - 1);
                    j--;
                }
            }
            return array;
        }
        public static int[] ShellSort(int[] array)
        {
            var step = array.Length / 2;
            while (step >= 1)
            {
                for (int i = step; i < array.Length; i++)
                {
                    var j = i;
                    while (j >= step && array[j - step] > array[j])
                    {
                        Swap(array, j, j - step);
                        j -= step;
                    }
                }
                step /= 2;
            }
            return array;
        }
        public static int[] GnomeSort(int[] array)
        {
            int index = 0;
            int size = array.Length;
            while (index < size)
            {
                if (index == 0) index++;
                if (array[index] >= array[index - 1])
                {
                    index++;
                }
                else
                {
                    Swap(array, index, index - 1);
                    index--;
                }
            }
            return array;
        }
        public static int[] SelectionSort(int[] array)
        {
            int minIndex = 0;
            for (int i = 0; i < array.Length; i++)
            {
                minIndex = i;
                for (int j = minIndex + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                Swap(array, minIndex, i);
            }
            return array;
        }
        public static int[] HeapSort(int[] array)
        {
            for (int i = (array.Length - 2) / 2; i > -1; i--)
            {
                siftDown(array, i, array.Length);
            }
            for (int j = array.Length - 1; j > 0; j--)
            {
                Swap(array, 0, j);
                siftDown(array, 0, j);
            }
            return array;
        }
        public static void QuickSort(int[] array, int left, int right)
        {

            if (left > right) return;

            int p = array[(left + right) / 2];
            int i = left;
            int j = right;
            while (i <= j)
            {
                while (array[i] < p) i++;
                while (array[j] > p) j--;
                if (i <= j)
                {
                    Swap(array, i, j);
                    i++;
                    j--;
                }
            }
            QuickSort(array, left, j);
            QuickSort(array, i, right);
            
        }
        public static int[] QuickSort(int[] array)
        {
            QuickSort(array, 0, (array.Length)-1);
            return array;
        }
        public static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                int middleIndex = (lowIndex + highIndex) / 2; MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex); Merge(array, lowIndex, middleIndex, highIndex);
            }
            return array;
        }
        public static int[] MergeSort(int[] array)
        {
            MergeSort(array,0,array.Length-1);
            return array;
        }
        public static int[] CountingSort(int[] array)
        {
            //поиск минимального и максимального значений
            int min = array[0];
            int max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
                else if (element < min)
                {
                    min = element;
                }
            }
            int[] count = new int[max + 1];
            for (int i = 0; i < array.Length; i++)
            {
                count[array[i]]++;
            }
            int index = 0;
            for (int i = 0; i < count.Length; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    array[index] = i;
                    index++;
                }
            }
            return array;
        }
        public static int[] RadixSort(int[] array, int n)
        {
            int m = getMax(array, n);
            for (int exp = 1; m / exp > 0; exp *= 10)
                countSort(array, n, exp);
            return array;
        }
        public static int[] RadixSort(int[] array)
        {
            RadixSort(array, array.Length);
            return array;
        }
        private static void BitonicSortPart(int[] array, int low, int cnt, Direction dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;
                BitonicSortPart(array, low, k, Direction.ASCENDING);
                BitonicSortPart(array, low + k, k, Direction.DESCENDING);
                BitonicMerge(array, low, cnt, dir);
            }
            
        }
        public static int[] BitonicSort(int[] array)
        {

            double length = array.Length;
            int powOfTwo = 0;
            while (length > 1)
            {
                length /= 2;
                powOfTwo++;
            }
            if (length != 1) powOfTwo++;

            int[] bitonicArray = new int[(int)Math.Pow(2, powOfTwo)];
            for (int i = 0; i < bitonicArray.Length; i++)
            {
                if (i < array.Length)
                {
                    bitonicArray[i] = array[i];
                    continue;
                }
                bitonicArray[i] = -1;
            }

            BitonicSortPart(bitonicArray, 0, bitonicArray.Length, Direction.ASCENDING);
            return array;
        }
        public static List<double> InsertSort(List<double> array)
        {
            for (int i = 1; i < array.Count; i++)
            {
                int swapIndex = i;
                while (swapIndex > 0 && ((array[swapIndex] < array[swapIndex - 1]) || ( array[swapIndex] > array[swapIndex - 1])))
                {
                    double temp = array[swapIndex];
                    array[swapIndex] = array[swapIndex - 1];
                    array[swapIndex - 1] = temp;
                    swapIndex--;
                }
            }
            return array;
        }
        public static double[] BucketSort(double[] array)
        {
            int length = array.Length;
            List<double>[] bucket = new List<double>[length];
            for (int i = 0; i < length; i++) bucket[i] = new List<double>();
            int bucketIndex;
            foreach (double i in array)
            {
                bucketIndex = (int)(length * i);
                bucket[bucketIndex].Add(i);
            }

            for (int i = 0; i < length; i++) bucket[i] = InsertSort(bucket[i]);

            bucketIndex = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < bucket[i].Count; j++)
                {
                    array[bucketIndex++] = bucket[i][j];
                }
            }
            return array;
        }

        public static int[] ArrayVersion1(int size)
        {
            // 1) Массив случайных чисел
            int[] array = new int[size];
            array = GenerateRandomArray(size);
            return array;
        }
        public static int[] ArrayVersion2(int size)
        {
            // 2) Массивы, разбитые на отсортированные подмассивы
            int[] sortedSubarrays = GenerateSortedSubarrays(size);
            return sortedSubarrays;
        }
        public static int[] ArrayVersion3(int size)
        {
            // 3) Изначально отсортированные массивы с перестановками
            int[] swappedSortedArray = GenerateSwappedSortedArray(size);
            return swappedSortedArray;
        }
        public static int[] ArrayVersion4(int size)
        {
            // 4) Полностью отсортированные массивы
            int[] sortedArrays = RandomBySwapAndRepeat(size);
            return sortedArrays;
        }
        public delegate void Operation(int[] array);

        
        static void Main(string[] args)
        {

            int size1 = 10, size2 = 100, size3 = 1000, size4 = 10000, size5 = 100000, size6 = 1000000;
            int[]array = new int[size1];
            array = ArrayVersion4(size5);
            
        }
    }
}
