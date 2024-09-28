using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
    internal class Program
    {
        public class MyArrayList<T>
        {
            private T[] elementData;
            private int size;
            public MyArrayList()
            {
                elementData = new T[0];
                size = 0;
            }
            public MyArrayList(T[] a)
            {
                size = a.Length;
                elementData = new T[size];
                Array.Copy(a, elementData, size);

            }
            public MyArrayList(int capasity)
            {
                elementData = new T[capasity];
                size = 0;

            }
            public void add(T e)
            {
                if (size >= elementData.Length)
                {
                    T[] newElementData = new T[size + 1 + (size / 2)];
                    for (int i = 0; i < size; i++)
                    {
                        newElementData[i] = elementData[i];
                    }
                    elementData = newElementData;
                }
                elementData[size] = e;
                size++;
            }
            public void addAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (size >= elementData.Length)
                    {
                        T[] newElementData = new T[size + 1 + (size / 2)];
                        for (int j = 0; j < size; j++)
                        {
                            newElementData[j] = elementData[j];
                        }
                        elementData = newElementData;
                    }
                    elementData[size] = a[i];
                    size++;
                }
            }
            public void clear()
            {
                size = 0;
                elementData = new T[0];

            }
            public bool contains(object o)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elementData[i].Equals(o)) return true;
                }
                return false;
            }
            public bool containsAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (contains(a[i]) == false) return false;
                }
                return true;
            }
            public bool ifEmpty()
            {
                if (size == 0) return true;
                else return false;
            }
            public void remove(object o)
            {
                for (int j = 0; j < size; j++)
                {
                    bool flag = false;
                    for (int i = 0; i < size - 1; i++)
                    {
                        if (elementData[i].Equals(o)) flag = true;
                            if (flag == true)
                            {
                                elementData[i] = elementData[i + 1];
                            }
                    }
                    if (flag == true) size--;
                }
            }

  
            public void removeAll(T[] a)
            {
                for (int i = 0; i < size; i++) remove(a[i]);

            }
            public void retainAll(T[] a)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < a.Length; j++)
                    {
                        if (!elementData[i].Equals(a[j])) remove(elementData[i]);
                    }
                }
            }
            public int Size()
            {
                return size;
            }
            public double[] toArray()
            {
                double[] array = new double[size];
                for (int i = 0; i < size; i++) array[i] = Convert.ToDouble(elementData[i]);
                return array;
            }
            public double[] toArray(T[] a)
            {
                double[] array2 = new double[size];
                if (a == null)
                {
                    for (int i = 0; i < size; i++)
                    {
                        array2[i] = Convert.ToDouble(elementData[i]);
                    }
                }
                else
                {
                    int h = 0;
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (elementData[i].Equals(a[j]))
                            {
                                array2[h] = Convert.ToDouble(elementData[i]);
                                h++;
                            }
                        }
                    }
                }
                return array2;
            }
            public void add(int index, T e)
            {
                for (int i = size - 2; i > index; i--)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementData[index] = e;
            }
            public void addAll(int index, T[] a)
            {
                for (int i = size - 1; i >= index; i--)
                {
                    add(index, a[i]);
                }
            }
            public T get(int index)
            {
                return elementData[index];
            }
            public int indexOf(object o)
            {
                for (int i = 0; i < size; i++)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }
            public int lastIndexOf(object o)
            {
                for (int i = size - 1; i >= 0; i++)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }
            public T remove(int index)
            {
                T temp = get(index);
                remove(temp);
                return temp;
            }
            public void set(int index, T e)
            {
                elementData[index] = e;
            }
            public T[] subList(int fromindex, int toindex)
            {
                T[] temp = new T[toindex - fromindex];
                for (int i = fromindex; i <= toindex; i++)
                {
                    temp[i] = elementData[i];
                }
                return temp;
            }
            public String toString()
            {
                StringBuilder string1 = new StringBuilder();
                for (int i = 0; i < size - 1; i++)
                {
                    string1.Append(elementData[i]).Append(",");
                }
                string1.Append(elementData[size - 1]);
                return string1.ToString();
            }
        }
        static void Main(string[] args)
        {
            //MyArrayList<int> Array1 = new MyArrayList<int>();
            //MyArrayList<int> Array2 = new MyArrayList<int>();
            //int[] array3 = new int[3] { 1, 2, 3 };
        }
    }
}