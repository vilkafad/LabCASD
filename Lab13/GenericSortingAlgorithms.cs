using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{

    public class IntComp : IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return x > y ? 1:0;
        }
    }
    public static class GenericSortingAlgorithms
    {
        public static void BubbleSort<T>(T[] array, IComparer<T> comparer)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) > 0)
                    {
                        Swap(ref array[j], ref array[j + 1]); //на прямую измен
                    }
                }
            }
        }

        public static void ShakerSort<T>(T[] array, IComparer<T> comparer)
        {
            int left = 0;
            int right = array.Length - 1;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (comparer.Compare(array[i], array[i + 1]) > 0)
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (comparer.Compare(array[i], array[i - 1]) < 0)
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        swapped = true;
                    }
                }
                left++;
            }
        }

        public static void CombSort<T>(T[] array, IComparer<T> comparer)
        {
            int gap = array.Length;
            const double shrinkFactor = 1.3;
            bool sorted = false;

            while (gap > 1 || !sorted)
            {
                if (gap > 1)
                {
                    gap = (int)(gap / shrinkFactor);
                }

                sorted = true;
                for (int i = 0; i + gap < array.Length; i++)
                {
                    if (comparer.Compare(array[i], array[i + gap]) > 0)
                    {
                        Swap(ref array[i], ref array[i + gap]);
                        sorted = false;
                    }
                }
            }
        }

        public static void InsertionSort<T>(T[] array, IComparer<T> comparer)
        {
            for (int i = 1; i < array.Length; i++)
            {
                T key = array[i];
                int j = i - 1;

                while (j >= 0 && comparer.Compare(array[j], key) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void ShellSort<T>(T[] array, IComparer<T> comparer)
        {
            int n = array.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    T temp = array[i];
                    int j = i;
                    while (j >= gap && comparer.Compare(array[j - gap], temp) > 0)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
                gap /= 2;
            }
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void TreeSort<T>(T[] array, IComparer<T> comparer)
        {
            if (array.Length == 0) return;

            var tree = new BinaryTree<T>(comparer);
            foreach (var value in array)
            {
                tree.Insert(value);
            }

            var sortedList = tree.IterativeInOrderTraversal();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = sortedList[i];
            }
        }

        private class BinaryTree<T>
        {
            private Node root;
            private readonly IComparer<T> comparer;

            public BinaryTree(IComparer<T> comparer)
            {
                this.comparer = comparer;
            }

            private class Node
            {
                public T Value;
                public Node Left;
                public Node Right;

                public Node(T value)
                {
                    Value = value;
                    Left = null;
                    Right = null;
                }
            }

            public void Insert(T value)
            {
                Node newNode = new Node(value);
                if (root == null)
                {
                    root = newNode;
                    return;
                }

                Node current = root;
                Node parent = null;

                while (current != null)
                {
                    parent = current;
                    if (comparer.Compare(value, current.Value) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (comparer.Compare(value, parent.Value) < 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }

            public List<T> IterativeInOrderTraversal()
            {
                List<T> result = new List<T>();
                Stack<Node> stack = new Stack<Node>();
                Node current = root;

                while (current != null || stack.Count > 0)
                {
                    while (current != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }

                    current = stack.Pop();
                    result.Add(current.Value);

                    current = current.Right;
                }

                return result;
            }
        }

        public static void GnomeSort<T>(T[] array, IComparer<T> comparer)
        {
            int i = 1;
            int j = 2;
            while (i < array.Length)
            {
                if (comparer.Compare(array[i - 1], array[i]) <= 0)
                {
                    i = j;
                    j++;
                }
                else
                {
                    Swap(ref array[i - 1], ref array[i]);
                    i--;
                    if (i == 0)
                    {
                        i = j;
                        j++;
                    }
                }
            }
        }

        public static void SelectionSort<T>(T[] array, IComparer<T> comparer)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (comparer.Compare(array[j], array[minIdx]) < 0)
                    {
                        minIdx = j;
                    }
                }
                Swap(ref array[minIdx], ref array[i]);
            }
        }

        public static void HeapSort<T>(T[] array, IComparer<T> comparer)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i, comparer);

            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref array[0], ref array[i]);

                Heapify(array, i, 0, comparer);
            }
        }

        private static void Heapify<T>(T[] array, int n, int i, IComparer<T> comparer)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && comparer.Compare(array[left], array[largest]) > 0)
                largest = left;

            if (right < n && comparer.Compare(array[right], array[largest]) > 0)
                largest = right;

            if (largest != i)
            {
                Swap(ref array[i], ref array[largest]);

                Heapify(array, n, largest, comparer);
            }
        }
       
        public static void QuickSort<T>(T[] array, int left, int right, IComparer<T> comparer)
        {
            while (left < right)
            {
                int pivotIndex = Partition(array, left, right, comparer);

                if (pivotIndex - left < right - pivotIndex)
                {
                    QuickSort(array, left, pivotIndex - 1, comparer);
                    left = pivotIndex + 1;
                }
                else
                {
                    QuickSort(array, pivotIndex + 1, right, comparer);
                    right = pivotIndex - 1;
                }
            }
        }

        private static int Partition<T>(T[] array, int left, int right, IComparer<T> comparer)
        {
            T pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (comparer.Compare(array[j], pivot) <= 0)
                {
                    i++;
                    Swap(ref array[i], ref array[j]);
                }
            }

            Swap(ref array[i + 1], ref array[right]);
            return i + 1;
        }

        public static void QuickSort<T>(T[] array, IComparer<T> comparer)
        {
            QuickSort(array, 0, array.Length - 1, comparer);
        }

        public static void MergeSort<T>(T[] array, IComparer<T> comparer)
        {
            if (array.Length <= 1) return;

            int mid = array.Length / 2;
            T[] left = new T[mid];
            T[] right = new T[array.Length - mid];

            Array.Copy(array, left, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSort(left, comparer);
            MergeSort(right, comparer);
            Merge(array, left, right, comparer);
        }

        private static void Merge<T>(T[] array, T[] left, T[] right, IComparer<T> comparer)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (comparer.Compare(left[i], right[j]) <= 0)
                {
                    array[k++] = left[i++];
                }
                else
                {
                    array[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                array[k++] = left[i++];
            }

            while (j < right.Length)
            {
                array[k++] = right[j++];
            }
        }
        //испол для извлечения ключа из каждого эл мас
        public static void CountingSort<T>(T[] array, Func<T, int> keySelector)
        {
            if (array.Length == 0) return;

            int min = array.Min(keySelector);
            int max = array.Max(keySelector);
            int range = max - min + 1;

            int[] count = new int[range];
            T[] output = new T[array.Length];

            // подсчет колва элементов
            foreach (var item in array)
                count[keySelector(item) - min]++;

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];
            // заполнение выходного массива
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int key = keySelector(array[i]) - min;
                output[count[key] - 1] = array[i];
                count[key]--;
            }

            Array.Copy(output, array, array.Length);
        }
        
        public static void RadixSort<T>(T[] array, Func<T, int> keySelector)
        {
            if (array.Length == 0) return;

            int max = array.Max(keySelector);

            for (int exp = 1; max / exp > 0; exp *= 10)
                CountingSortByDigit(array, keySelector, exp);
        }

        private static void CountingSortByDigit<T>(T[] array, Func<T, int> keySelector, int exp)
        {
            int n = array.Length;
            T[] output = new T[n];
            int[] count = new int[10];

            // Подсчет количества элементов для текущего разряда
            foreach (var item in array)
                count[(keySelector(item) / exp) % 10]++;

            // Кумулятивный подсчет
            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            // Заполнение выходного массива
            for (int i = n - 1; i >= 0; i--)
            {
                int digit = (keySelector(array[i]) / exp) % 10;
                output[count[digit] - 1] = array[i];
                count[digit]--;
            }

            Array.Copy(output, array, n);
        }

        public static void BitonicSort<T>(T[] array, IComparer<T> comparer)
        {
            BitonicSort(array, 0, array.Length, true, comparer);
        }

        private static void BitonicSort<T>(T[] array, int low, int cnt, bool dir, IComparer<T> comparer)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                BitonicSort(array, low, k, true, comparer);
                BitonicSort(array, low + k, cnt - k, false, comparer);

                BitonicMerge(array, low, cnt, dir, comparer);
            }
        }

        private static void BitonicMerge<T>(T[] array, int low, int cnt, bool dir, IComparer<T> comparer)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                for (int i = low; i < low + k; i++)
                {
                    if ((dir && comparer.Compare(array[i], array[i + k]) > 0) ||
                        (!dir && comparer.Compare(array[i], array[i + k]) < 0))
                    {
                        Swap(ref array[i], ref array[i + k]);
                    }
                }

                BitonicMerge(array, low, k, dir, comparer);
                BitonicMerge(array, low + k, cnt - k, dir, comparer);
            }
        }

    }

}


/*
      public static void BubbleSort<T>(T[] array, IComparer<T> comparer)
        public static void ShakerSort<T>(T[] array, IComparer<T> comparer)
        public static void CombSort<T>(T[] array, IComparer<T> comparer)
        public static void InsertionSort<T>(T[] array, IComparer<T> comparer)
        public static void ShellSort<T>(T[] array, IComparer<T> comparer)
        private static void Swap<T>(ref T a, ref T b)
        public static void TreeSort<T>(T[] array, IComparer<T> comparer)
        private class BinaryTree<T>
        public static void GnomeSort<T>(T[] array, IComparer<T> comparer)
        public static void SelectionSort<T>(T[] array, IComparer<T> comparer)
        public static void HeapSort<T>(T[] array, IComparer<T> comparer)
        private static void Heapify<T>(T[] array, int n, int i, IComparer<T> comparer)
        public static void QuickSort<T>(T[] array, int left, int right, IComparer<T> comparer)
        private static int Partition<T>(T[] array, int left, int right, IComparer<T> comparer)
        public static void QuickSort<T>(T[] array, IComparer<T> comparer)
        public static void MergeSort<T>(T[] array, IComparer<T> comparer)
        private static void Merge<T>(T[] array, T[] left, T[] right, IComparer<T> comparer)
        public static void CountingSort<T>(T[] array, Func<T, int> keySelector)
        public static void RadixSort<T>(T[] array, Func<T, int> keySelector)
        private static void CountingSortByDigit<T>(T[] array, Func<T, int> keySelector, int exp)
        public static void BitonicSort<T>(T[] array, IComparer<T> comparer)
        private static void BitonicSort<T>(T[] array, int low, int cnt, bool dir, IComparer<T> comparer)
        private static void BitonicMerge<T>(T[] array, int low, int cnt, bool dir, IComparer<T> comparer)

*/
