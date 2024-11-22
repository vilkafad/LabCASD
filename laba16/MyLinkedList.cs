using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba16
{
    class linkListNode<T>
    {
        public T value;
        public linkListNode<T>? next;
        public linkListNode<T>? pred;
        public linkListNode(T el)
        {
            next = null;
            pred = next;
            value = el;
        }
    }
        public class MyLinkedList<T>
        {
            linkListNode<T>? first;
            linkListNode<T>? last;
            int size;
            public MyLinkedList()
            {
                first = null;
                last = null;
                size = 0;
            }
            public MyLinkedList(T[] a)
            {
                if (a.Length == 0)
                {
                    first = null;
                    last = null;
                    return;
                }
                first = new linkListNode<T>(a[0]);
                last = first;
                for (int i = 1; i < a.Length; i++)
                {
                    linkListNode<T> node = new linkListNode<T>(a[i]);
                    last.next = node;
                    node.pred = last;
                    last = node;
                }
            }
            public void add(T e)
            {
                linkListNode<T> step = new linkListNode<T>(e);
                if (size == 0)
                {
                    first = step;
                    last = step;
                }
                else
                {
                if (last == null) throw new Exception("ошибка");
                    last.next = step;
                    step.pred = last;
                    last = step;
                }
                size++;
            }
            public void addAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++) add(a[i]);
            }
            public void clear()
            {
                first = null;
                last = null;
                size = 0;
            }
            public bool contains(object o)
            {
                linkListNode<T>? step = first;
                while (step != null)
                {
                    if (step.value.Equals(o)) return true;
                    step = step.next;
                }
                return false;
            }
            public bool containsAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!contains(a[i])) return false;
                }
                return true;
            }
            public bool isEmpty() => size == 0;
            public void remove(T o) 
            {
                if (contains(o))
                {
                    if (first.value.Equals(o))
                    {
                        first = first.next;
                        size--;
                        return;
                    }
                    linkListNode<T>? step = first;
                    while (step != null)
                    {
                        if (step.next.value.Equals(o))
                        {
                            step.next = step.next.next;
                            size--;
                            return;
                        }
                        else step = step.next;
                    }
                }
            }
            public void removeAll(T[] a)//ня
            {
                for (int i = 0; i < a.Length; i++) remove(a[i]);
            }
            public void retainAll(T[] a)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    linkListNode<T>? step = first;
                    while (step != null)
                    {
                        if (!step.value.Equals(a[i])) remove(step.value);
                        step = step.next;
                    }
                }
            }
            public int Size() => size;
            public T[] toArray()
            {
                T[] a = new T[size];
                linkListNode<T>? step = first;
                int i = 0;
                while (step != null)
                {
                    a[i] = step.value;
                    i++;
                    step = step.next;
                }
                return a;
            }
            public T[] toArray(ref T[] a)
            {
                if (a == null) return toArray();
                else
                {
                    T[] newArray = new T[a.Length + size];
                    for (int i = 0; i < a.Length; i++) newArray[i] = a[i];
                    linkListNode<T>? step = first;
                    int index = a.Length;
                    while (step != null)
                    {
                        newArray[index] = step.value;
                        index++; step = step.next;
                    }
                    return newArray;
                }
            }
            public void add(int index, T e)
            {
                linkListNode<T> node = new linkListNode<T>(e);
                linkListNode<T>? step = first;
                if (index == 0)
                {
                    node.next = first;
                    first.pred = node;
                    first = node;
                    return;
                }
                else if (index == size - 1)
                {
                    node.pred = last;
                    last.next = node;
                    last = node;
                    return;
                }
                for (int i = 0; step != null; i++)
                {
                    if (i == index)
                    {
                        node.next = step;
                        node.pred = step.pred;
                        step.pred.next = node;
                        step.pred = node;
                    }
                    step = step.next;
                }
                size++;
            }
            public void addAll(int index, T[] a)
            {
                for (int i = a.Length - 1; i >= 0; i--)
                {
                    add(index, a[i]);
                }
            }
            public T get(int index)
            {
                linkListNode<T>? step = first;
                int i = 0;
                if (index >= size) throw new Exception("ошибка 1");
                if (index < 0) throw new Exception("ошибка 2");
                if (index == 0) return first.value;
                if (index == size - 1) return last.value;
                while (i != index)
                {
                    step = step.next;
                    i++;
                }
                return step.value;
            }
            public int indexOf(T value)
            {
                linkListNode<T>? step = first;
                for (int i = 0; step != null; i++, step = step.next)
                {
                    if (step.value.Equals(value)) return i;
                }
                return -1;
            }
        public int lastIndexOf(object o)
        {
            linkListNode<T>? step = last;
            int index = -1;
            int indexOfEl = 0;
            while (step != null)
            {
                if (step.value.Equals(o)) index = indexOfEl;
                indexOfEl++;
                step = step.pred;
            }
            return index;
        }
            public T remove(int index)
            {
                linkListNode<T>? step = first;
                for (int i = 0; step != null; i++, step = step.next)
                {
                    if (i == index)
                    {
                        T Temp = step.value;
                        remove(step.value);
                        return Temp;
                    }
                }
                throw new Exception("не работает ремув");
            }
            public void set(int index, T e)
            {
                linkListNode<T>? step = first;
                for (int i = 0; step != null; i++, step = step.next)
                {
                    if (i == index)
                    {
                        step.value = e;
                    }
                }
            }
            public T[] subList(int fromIndex, int toIndex)
            {
                T[] a = new T[toIndex - fromIndex];
                linkListNode<T>? step = first;
                for (int i = 0, j = fromIndex; j != toIndex; i++, j++)
                {
                    a[i] = get(j);
                }
                return a;
            }
            public T element() => first.value;
            public bool offer(T obj)
            {
                add(obj);
                if (contains(obj)) return true;
                return false;
            }
            public T peek()
            {
                if (size == 0) throw new Exception("пусто");
                return first.value;
            }
            public T poll()
            {
                if (size == 0) throw new Exception("пусто");
                T Temp = first.value;
                remove(first.value);
                return Temp;
            }
            public void addFirst(T obj) => add(0, obj);
            public void addLast(T obj) => add(obj);
            public T getFirst() => first.value;
            public T getLast() => last.value;
            public bool offerFisrt(T obj)
            {
                addFirst(obj);
                if (obj.Equals(getFirst())) return true;
                else return false;
            }
            public bool offerLast(T obj)
            {
                addLast(obj);
                if (obj.Equals(getLast())) return true;
                else return false;
            }
            public T pop()
            {
                T Temp = first.value;
                remove(first.value);
                return Temp;
            }
            public void push(T obj) => addFirst(obj);
            public T peekFirst()
            {
                if (size == 0) throw new Exception("массив пуст");
                return getFirst();
            }
            public T peekLast()
            {
                if (size == 0) throw new Exception("массив пуст");
                return getLast();
            }
            public T pollFirst()
            {
                if (size == 0) throw new Exception("массив пуст");
                T Temp = first.value;
                remove(first.value);
                return Temp;
            }
            public T pollLast()
            {
                if (size == 0) throw new Exception("массив пуст");
                T Temp = last.value;
                remove(last.value);
                return Temp;
            }
            public T removeLast()
            {
                T Temp = last.value;
                remove(last.value);
                return Temp;
            }
            public T removeFirst()
            {
                T Temp = first.value;
                remove(first.value);
                return Temp;
            }
            public bool removeLastOccurrence(object obj)
            {
                int indexOfElement = lastIndexOf(obj);
                if (indexOfElement != -1)
                {
                    remove(indexOfElement);
                    return true;
                }
                return false;
            }
            public bool removeFirstOccurrence(object obj)
            {
                int indexOfElement = indexOf((T)obj);
                if (indexOfElement != -1)
                {
                    remove(indexOfElement);
                    return true;
                }
                return false;
            }
            public string print()
            {
                string output = "";
                linkListNode<T>? step = first;
                while (step != null)
                {
                    output += Convert.ToString(step.value);
                    step = step.next;
                }
                return output;
            }
        }
    }


