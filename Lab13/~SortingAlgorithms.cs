using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab13
{
    public static class SortingAlgorithms
    {
        public static void BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }
            }
        }

        public static void ShakerSort(int[] array)
        {
            int left = 0;
            int right = array.Length - 1;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Swap(ref array[i], ref array[i + 1]);
                        swapped = true;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (array[i] < array[i - 1])
                    {
                        Swap(ref array[i], ref array[i - 1]);
                        swapped = true;
                    }
                }
                left++;
            }
        }

        public static void CombSort(int[] array)
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
                    if (array[i] > array[i + gap])
                    {
                        Swap(ref array[i], ref array[i + gap]);
                        sorted = false;
                    }
                }
            }
        }

        public static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
        }

        public static void ShellSort(int[] array)
        {
            int n = array.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;
                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }
                    array[j] = temp;
                }
                gap /= 2;
            }
        }
        public static void TreeSort(int[] array)
        {
            if (array.Length == 0) return;

            BinaryTree tree = new BinaryTree();
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
        private class BinaryTree
        {
            private Node root;

            private class Node
            {
                public int Value;
                public Node Left;
                public Node Right;

                public Node(int value)
                {
                    Value = value;
                    Left = null;
                    Right = null;
                }
            }
            public void Insert(int value)
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
                    if (value < current.Value)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                if (value < parent.Value)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }
            }

            public List<int> IterativeInOrderTraversal()
            {
                List<int> result = new List<int>();
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


        public static void GnomeSort(int[] array)
        {
            int i = 1;
            int j = 2;
            while (i < array.Length)
            {
                if (array[i - 1] <= array[i])
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

        public static void SelectionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIdx = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIdx])
                    {
                        minIdx = j;
                    }
                }
                Swap(ref array[minIdx], ref array[i]);
            }
        }

        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static void HeapSort(int[] array)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i = n - 1; i >= 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i; 
            int left = 2 * i + 1; 
            int right = 2 * i + 2; 

            if (left < n && array[left] > array[largest])
                largest = left;

            if (right < n && array[right] > array[largest])
                largest = right;

            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                Heapify(array, n, largest);
            }
        }
        //метод возвращающий индекс опорного элемента
        public static void QuickSort(int[] array, int left, int right)
        {
            while (left < right)
            {
                int pivotIndex = Partition(array, left, right);

                if (pivotIndex - left < right - pivotIndex)
                {
                    QuickSort(array, left, pivotIndex - 1);
                    left = pivotIndex + 1;
                }
                else
                {
                    QuickSort(array, pivotIndex + 1, right);
                    right = pivotIndex - 1;
                }
            }
        }
        static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;

            return i + 1;
        }

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }


        public static void MergeSort(int[] array)
        {
            if (array.Length <= 1) return;

            int mid = array.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[array.Length - mid];

            Array.Copy(array, left, mid);
            Array.Copy(array, mid, right, 0, array.Length - mid);

            MergeSort(left);
            MergeSort(right);
            Merge(array, left, right);
        }

        private static void Merge(int[] array, int[] left, int[] right)
        {
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
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
        public static void CountingSort(int[] array)
        {
            int max = array.Max();
            int min = array.Min();
            int range = max - min + 1;

            int[] count = new int[range];
            int[] output = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
                count[array[i] - min]++;

            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            Array.Copy(output, array, array.Length);
        }
        public static void RadixSort(int[] array)
        {
            int max = array.Max();

            for (int exp = 1; max / exp > 0; exp *= 10)
                CountingSortByDigit(array, exp);
        }

        private static void CountingSortByDigit(int[] array, int exp)
        {
            int n = array.Length;
            int[] output = new int[n];
            int[] count = new int[10]; 

            for (int i = 0; i < n; i++)
                count[(array[i] / exp) % 10]++;

            for (int i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for (int i = n - 1; i >= 0; i--)
            {
                output[count[(array[i] / exp) % 10] - 1] = array[i];
                count[(array[i] / exp) % 10]--;
            }

            Array.Copy(output, array, n);
        }
        public static void BitonicSort(int[] array)
        {
            BitonicSort(array, 0, array.Length, true);
        }

        private static void BitonicSort(int[] array, int low, int cnt, bool dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                BitonicSort(array, low, k, true);
                BitonicSort(array, low + k, cnt - k, false);

                BitonicMerge(array, low, cnt, dir);
            }
        }

        private static void BitonicMerge(int[] array, int low, int cnt, bool dir)
        {
            if (cnt > 1)
            {
                int k = cnt / 2;

                for (int i = low; i < low + k; i++)
                    if ((dir && array[i] > array[i + k]) || (!dir && array[i] < array[i + k]))
                        Swap(ref array[i], ref array[i + k]);

                BitonicMerge(array, low, k, dir);
                BitonicMerge(array, low + k, cnt - k, dir);
            }
        }

    }

}
