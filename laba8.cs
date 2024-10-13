using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp18
{
    internal class Program
    {
        public class MyVector<T>
        {
            private T[] elementData;
            private int elementCount;
            private int capacityIncrement;
            public MyVector(int initialCapacity, int capacityIncrement2)
            {
                elementData = new T[initialCapacity];
                elementCount = initialCapacity;
                capacityIncrement = capacityIncrement2;
            }
            public MyVector(int initialCapacity)
            {
                elementData = new T[initialCapacity];
                elementCount = initialCapacity;
                capacityIncrement = 0;
            }
            public MyVector()
            {
                elementData = new T[1];
                elementCount = 1;
                capacityIncrement = 0;
            }
            public MyVector(T[] a)
            {
                elementData = new T[a.Length];
                for (int i = 0; i < a.Length; i++)
                {
                    elementData[i] = a[i];
                }
                elementCount = a.Length;
                capacityIncrement = 0;
            }
            public void add(T e)
            {
                if (elementCount == elementData.Length)
                {
                    T[] elementDataOne;
                    if (capacityIncrement == 0)
                    {
                        elementDataOne = new T[elementData.Length * 2];
                    }
                    else
                    {
                        elementDataOne = new T[elementData.Length * capacityIncrement];
                    }
                    for (int i = 0; i < elementCount; i++)
                        elementDataOne[i] = elementData[i];
                    elementData = elementDataOne;
                }
                elementData[elementCount++] = e;
            }
            public void addAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    add(a[i]);
                }
            }
            public void clear()
            {
                elementData = new T[0];
                elementCount = 0;
                capacityIncrement = 0;
            }
            public bool contains(object o)
            {
                for (int i = 0; i < elementCount; i++)
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
            public bool isEmpty()
            {
                if (elementCount == 0) return true;
                else return false;
            }
            public void remove(object o)
            {
                bool flagPirate = false;
                int countEnt = 0;
                for (int i = 0; i < elementCount - countEnt; i++)
                {
                    if (elementData[i].Equals(o))
                    {
                        flagPirate = true;
                        countEnt += 1;
                    }
                    if (flagPirate == true)
                    {
                        elementData[i] = elementData[i + countEnt];
                    }
                }
                if (elementData[elementCount - countEnt].Equals(o))
                {
                    elementCount--;
                }
                elementCount -= countEnt;
            }
            public void removeAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    remove(a[i]);
                }
            }
            public void retainAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    for (int j = 0; j < elementCount; i++)
                    {
                        if (!elementData[j].Equals(a[i])) remove(elementData[j]);
                    }
                }
            }
            public int size() => elementCount;
            public int[] toArray()
            {
                int[] Array = new int[elementCount];
                for (int i = 0; i < elementCount; i++)
                {
                    Array[i] = Convert.ToInt32(elementData[i]);
                }
                return Array;
            }
            public int[] toArray(T[] a)
            {
                if (a == null)
                {
                    return toArray();
                }
                else
                {
                    int[] Array = new int[elementCount];
                    for (int i = 0; i < elementCount; i++)
                    {
                        Array[i] = Convert.ToInt32(a[i]);
                    }
                    return Array;
                }
            }
            public void add(int index, T e)
            {
                T temp = elementData[elementCount - 1];
                for (int i = index - 1; i < elementCount - 1; i++)
                {
                    elementData[i + 1] = elementData[i];
                }
                elementData[index - 1] = e;
                add(temp);
            }
            public void addAll(int index, T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    add(index, a[i]);
                }
            }
            public T get(int index) => elementData[index];
            public int indexOf(object o)
            {
                for (int i = 0; i < elementCount; i++)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }
            public int lastIndexOf(object o)
            {
                for (int i = elementCount - 1; i >= 0; i--)
                {
                    if (elementData[i].Equals(o)) return i;
                }
                return -1;
            }
            public T remove(int index)
            {
                T temporary = elementData[index];
                for (int i = index; i < elementCount - 1; i++)
                {
                    elementData[i] = elementData[i + 1];
                }
                elementCount--;
                return temporary;
            }
            public void set(int index, T e)
            {
                elementData[index] = e;
            }
            public T[] subList(int fromIndex, int toIndex)
            {
                T[] superVector = new T[toIndex - fromIndex];
                for (int j = 0; j < toIndex - fromIndex; j++)
                {
                    for (int i = fromIndex - 1; i < toIndex; i++)
                    {
                        superVector[j] = elementData[i];
                    }
                }
                return superVector;
            }
            public T firstElement() => elementData[0];
            public T lastElement() => elementData[elementCount - 1];
            public void removeElementAt(int pos)
            {
                remove(pos);
            }
            public void removeRange(int begin, int end)
            {
                for (int i = 0; i < end - begin + 1; i++)
                {
                    remove(begin);
                }
            }
            public string toString()
            {
                string output = "";
                for (int i = 0; i < elementCount; i++)
                {
                    output += elementData[i] + ".";
                    if ((i + 1) % 4 == 0) output += "\n";
                }
                return output;
            }
        }

        public class MyStack<T> : MyVector<T>
        {
            private MyVector<T> stack;
            private int top;
            public MyStack()
            {
                stack = new MyVector<T>();
                top = 0;
            }
            public void Push(T item)
            {
                stack.add(item);
                top++;
            }
            public T Pop()
            {
                if (top == 0) throw new Exception("Стек пуст");
                else
                {
                    T item = stack.get(top);
                    stack.remove(top);
                    top--;
                    return item;
                }
            }
            public T Peek()
            {
                if (top==0) throw new Exception("Стек пуст");
                else
                {
                    return stack.get(top);
                }
            }
            public bool empty()
            {
                return isEmpty(); 
            }
            public int search(T item)
            {
                if (stack.indexOf(item)==-1) return -1;
               return top-stack.indexOf(item)+1;
            }
            public void print()
            {
                for (int i=1;i<=top;i++) Console.Write(stack.get(i));
            }
        }

        static void Main(string[] args)
        {
            MyStack<int> array = new MyStack<int>();
            for (int i = 1; i < 11; i++) array.Push(i);
            array.print();
            //Console.WriteLine("\n");
            //Console.WriteLine (array.Pop());
            //Console.WriteLine (array.Peek());
            //array.print();
            Console.WriteLine("\n");
            Console.WriteLine (array.search(10));
        }
    }
}
