﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task17.Gr
{
    class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> pred;
        public Node(T element)
        {
            next = null;
            pred = next;
            value = element;
        }
    }
    public class MyLinkedList<T>
    {
        Node<T> first;
        Node<T> last;
        int size;
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinkedList(T[] a)
        {
            foreach (T el in a)
            {
                add(el);
            }
        }
        public MyLinkedList(int capacity)
        {
            first = null;
            last = null;
            size = 0;
        }
        public void add(T el)
        {
            Node<T> newNode = new Node<T>(el);
            if (size == 0)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                last.next = newNode;
                newNode.pred = last;
                last = newNode;
            }
            size++;

        }
        public void AddAll(T[] a)
        {
            foreach (T el in a)
                add(el);
        }
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object o)
        {
            Node<T> step = first;
            while (step != null)
            {
                if (step.value.Equals(o))
                    return true;
                step = step.next;

            }
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            bool[] check = new bool[array.Length];
            Node<T> step = first;
            while (step != null)
            {
                int i = 0;
                if (step.Equals(array[i])) check[i] = true;
                i++;
                step = step.next;
            }
            for (int i = 0; i < check.Length; i++)
                if (!check[i]) return false;
            return true;
        }
        public bool Empty() => size == 0;
        public void Remove(T obj)
        {
            if (Contains(obj))
            {
                if (first.value.Equals((T)obj))
                {
                    first = first.next;
                    size--;
                    return;
                }
                Node<T> step = first;
                while (step != null)
                {
                    if (step.next.value.Equals((T)obj))
                    {
                        step.next = step.next.next; size--; return;
                    }
                    else step = step.next;
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            foreach (T el in a)
                Remove(el);
        }
        public T get(int index)
        {
            int curIndex = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.value;
            if (index == 0)
                return first.value;
            Node<T> step = first;
            while (curIndex != index)
            {
                step = step.next;
                curIndex++;
            }
            return step.value;
        }
        public void RetainAll(T[] a)
        {
            T[] tmp = new T[a.Length];
            int ind = 0;
            for (int i = 0; i < size; i++)
            {
                int flag = 0;
                for (int j = 0; j < a.Length; j++)
                {
                    if (get(i).Equals(a[j]))
                    {
                        flag = 0;
                        break;
                    }
                    else flag = 1;
                }
                if (flag == 1)
                    Remove(get(i));
            }
        }
        public int Size() => size;
        public T[] ToArray()
        {
            T[] newAr = new T[size];
            for (int i = 0; i < size; i++)
                newAr[i] = get(i);
            return newAr;
        }
        public T[] ToArray(T[] a)
        {
            if (a == null) return ToArray();
            else
            {
                T[] newAr = new T[a.Length + size];
                for (int i = 0; i < a.Length; i++)
                    newAr[i] = a[i];
                for (int i = a.Length; i < newAr.Length; i++)
                    newAr[i] = get(i);
                return newAr;
            }
        }
        public T Element() => first.value;
        public T Peek()
        {
            if (first == null)
                return default(T);
            return first.value;
        }
        public T Poll()
        {
            T obj = first.value;
            Remove(first.value);
            return obj;
        }
        public T GetFirst()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.value;
        }
        public T GetLast()
        {
            if (last == null)
                throw new IndexOutOfRangeException();
            return last.value;

        }
        public T PeekFirst()
        {
            if (size == 0)
                return default(T);
            return first.value;
        }
        public T PeekLast()
        {
            if (size == 0)
                return default(T);
            return first.value;
        }
        public T PollFirst()
        {
            T obj = first.value;
            Remove(first.value);
            return obj;
        }
        public T PollLast()
        {
            T obj = last.value;
            Remove(last.value);
            return obj;
        }
        public T RemoveFirst()
        {
            T obj = first.value;
            Remove(first.value);
            return obj;
        }
        public T RemoveLast()
        {
            T obj = last.value;
            Remove(last.value);
            return obj;
        }
        public T Pop()
        {
            T obj = first.value;
            Remove(first.value);
            return obj;
        }
        public bool Offer(T obj)
        {
            add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Add(int index, T obj)
        {
            if (index == 0)
            {
                Node<T> step = new Node<T>(obj);
                step.next = first;
                first.pred = step;
                first = step;
                return;
            }
            else if (index == size - 1)
            {
                Node<T> step = new Node<T>(obj);
                step.pred = last;
                last.next = step;
                last = step;
                return;
            }
            else
            {
                int tind = 0;
                Node<T> step = new Node<T>(obj);
                step = first;
                while (tind != index)
                {
                    step = step.next;
                    tind++;
                }
                if (tind == index)
                {
                    Node<T> el = new Node<T>(obj);
                    el.next = step;
                    el.pred = step.pred;
                    step.pred.next = el;
                    step.pred = el;
                }
            }
        }
        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
                Add(index, a[i]);
        }
        public int IndexOf(T o)
        {
            Node<T> step = new Node<T>(o);
            step = first;
            int i = 0;
            while (step != null)
            {
                if (step.value.Equals(o))
                    return i;
                i++;
                step = step.next;
            }
            return -1;
        }
        public int LastIndexOf(T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int retInd = -1;
            int ind = 0;
            while (step != null)
            {
                if (step.value.Equals(obj)) retInd = ind;
                ind++;
                step = step.next;
            }
            return retInd;
        }
        public T remove(int index)
        {
            T obj = get(index);
            Remove(obj);
            return obj;
        }
        public void set(int index, T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int ind = 0;
            while (ind != index)
            {
                ind++;
                step = step.next;
            }
            step.value = obj;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] a = new T[toIndex - fromIndex + 1];
            Node<T> step = new Node<T>(first.value);
            step = first;
            int ind1 = 0;
            while (ind1 != fromIndex)
            {
                step = step.next;
                ind1++;
            }
            int ind2 = 0;
            while (ind1 <= toIndex)
            {
                ind2++;
                ind1++;
                a[ind2] = step.value;
                step = step.next;
            }
            return a;

        }
        public void AddFirst(T obj)
        {
            Add(0, obj);
        }
        public void AddLast(T obj)
        {
            Add(size - 1, obj);
        }
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Push(T obj)
        {
            AddFirst(obj);
        }
        public bool RemoveLastOccurrence(T obj)
        {
            int ind = LastIndexOf(obj);
            if (ind != -1)
            {
                remove(ind);
                return true;
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            int index = IndexOf(obj);
            if (index != -1)
            {
                remove(index);
                return true;
            }
            return false;
        }
        public void Print()
        {
            Node<T> step = new Node<T>(first.value);
            step = first;
            while (step != null)
            {
                Console.WriteLine($"{step.value}");
                step = step.next;
            }
        }
        
    }
}

