using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba23
{
    public class MyHashSet<K>
    {
        private class Entry
        {
            public K key { get; set; }
            public bool value { get; set; }
            public Entry next { get; set; }
            public Entry(K key, bool value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }
        }
        Entry[]? map;
        int size;
        double loadFactor;

        public MyHashSet() : this(16, 0.75) { }
        public MyHashSet(K[] a)
        {

        }
        public MyHashSet(int initialCapacity) : this(initialCapacity, 0.75) { }
        public MyHashSet(int initialCapacity, double loadFactor)
        {
            map = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
        }
        public int GetHashCode(K key) => Math.Abs(key.GetHashCode()) % map.Length;
        //public int GetHashCode(V value) => Math.Abs(value.GetHashCode()) % map.Length;
        public void add(K key)
        {
            int index = GetHashCode(key);
            Entry step = map[index];
            if (step != null)
            {
                //    int f = 1;
                //    while (step.next != null)
                //    {
                //        if (step.key.Equals(key))
                //        {
                //            step.value = value;
                //            f = 0;
                //        }
                //        step = step.next;
                //    }
                //    if (step.key.Equals(key))
                //    {
                //        step.value = value;
                //        f = 0;
                //    }
                //    if (f == 1)
                //    {
                //        Entry node = new Entry(key, value);
                //        step.next = node;
                //        step = node;
                //        size++;
                //    }
                //}
                //else
                //{
                Entry newNode = new Entry(key, true);
                map[index] = newNode;
                size++;
            }
        }
        public void addAll(K[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                add(a[i]);
            }
        }
        public void clear()
        {
            Array.Clear(map);
            size = 0;
        }
        public bool contains(K key)
        {
            int index = GetHashCode(key);
            Entry step = map[index];
            while (step != null)
            {
                if (step.key.Equals(key)) return true;
                step = step.next;
            }
            return false;
        }
        public bool containsAll(K[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!contains(a[i])) return false;
            }
            return true;
        }
        public bool isEmpty() => size == 0;
        public void remove(K key)
        {
            int index = GetHashCode(key);
            //если в индексе нет значения, ничего не делаем
            if (map[index] == null)
                return;
            //если удаляемый ключ - первый в списке
            if (map[index].key.Equals(key))
            {
                map[index] = map[index].next;
                size--;
                return;
            }
            //ищем ключ в списке
            Entry current = map[index];
            Entry previous = null;
            while (current != null)
            {
                if (current.key.Equals(key))
                {
                    previous.next = current.next;
                    size--;
                    return;
                }
                previous = current;
                current = current.next;
            }
        }
        public void removeAll(K[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                remove(a[i]);
            }

        }
        public void retainAll(K[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int index = GetHashCode(a[i]);
                Entry step = map[index];
                if (!map[index].key.Equals(a))
                {
                    remove(a[index]);
                }
                else
                {
                    step = step.next;
                }
            }
        }
        public int Size() => size;
        public K[] toArray()
        {
            K[] array = new K[size];
            int i = 0;
            foreach (var entry in map)
            {
                for (var step = entry; step != null; step = step.next)
                {
                    array[i] = step.key;
                    i++;
                }
            }
            return array;
        }
        public K[] toArray(K[] a)
        {
            if (a == null) return toArray();
            else
            {
                K[] array = new K[a.Length + Size()];
                for (int i = 0; i < a.Length; i++)
                {
                    array[i] = a[i];
                }
                int j = a.Length;
                foreach (var entry in map)
                {
                    for (var step = entry; step != null; step = step.next)
                    {
                        array[j] = step.key;
                        j++;
                    }
                }
                return array;
            }
        }
        public K first()
        {
            Entry step = map[0];
            return step.key;
        }
        public K last()
        {
            Entry step = map[size - 1];
            while (step.next != null)
            {
                step = step.next;
            }
            return step.key;
        }
        //public K subSet(K fromElement,K toElement)
        //{

        //}

        //        public IEnumerable<KeyValuePair<K, V>> entrySet()
        //        {
        //            foreach (var entry in map)
        //            {
        //                for (var step = entry; step != null; step = step.next)
        //                    yield return new KeyValuePair<K, V>(step.key, step.value);
        //            }
        //        }
        //        public V get(K key)
        //        {
        //            int index = GetHashCode(key);
        //            Entry step = map[index];
        //            while (step != null)
        //            {
        //                if (step.key.Equals(key)) return step.value;
        //                step = step.next;
        //            }
        //            throw new KeyNotFoundException("Ключ не найден.");
        //        }

        //        public K[] keySet()
        //        {
        //            K[] arrayRet = new K[size];
        //            int index = 0;
        //            for (int i = 0; i < map.Length; i++)
        //            {
        //                if (map[i] != null)
        //                {
        //                    Entry step = map[i];

        //                    while (step != null)
        //                    {
        //                        arrayRet[index] = step.key;
        //                        index++;
        //                        step = step.next;
        //                    }
        //                }
        //            }
        //            return arrayRet;
        //        }
        //        public void put(K key, V value)
        //        {
        //            double count = (double)(size + 1) / (double)map.Length;
        //            if (count >= loadFactor)
        //                reSize();
        //            putс(key, value);
        //        }
        //        public void reSize()
        //        {
        //            Entry[] newArray = new Entry[map.Length * 3];
        //            //cохраним текущий размер, чтобы его не увеличивать в процессе
        //            int oldSize = size;
        //            size = 0;
        //            for (int i = 0; i < map.Length; i++)
        //            {
        //                if (map[i] != null)
        //                {
        //                    Entry val = map[i];
        //                    while (val != null)
        //                    {
        //                        //вычисляем индекс для нового массива
        //                        int index = Math.Abs(val.key.GetHashCode()) % newArray.Length;
        //                        //сохраняем следующий элемент перед добавлением в новый массив
        //                        Entry nextVal = val.next;
        //                        //добавляем элемент в новый массив
        //                        putInNewArray(newArray, val.key, val.value);
        //                        val = nextVal;  //переход к следующему элементу
        //                    }
        //                }
        //            }
        //            map = newArray;  //обновляем ссылку на массив
        //        }

        //        private void putInNewArray(Entry[] array, K key, V value)
        //        {
        //            int index = Math.Abs(key.GetHashCode()) % array.Length;
        //            Entry newNode = new Entry(key, value);
        //            if (array[index] != null)
        //            {
        //                //если элемент уже существует, добавляем в конец
        //                Entry step = array[index];
        //                while (step.next != null)
        //                    step = step.next;
        //                step.next = newNode;
        //            }
        //            else
        //                //если пусто, просто добавляем новый элемент
        //                array[index] = newNode;
        //            size++;  //увеличиваем размер
        //        }
        //        public void putс(K key, V value)
        //        {
        //            int index = GetHashCode(key);
        //            Entry step = map[index];
        //            if (step != null)
        //            {
        //                int f = 1;
        //                while (step.next != null)
        //                {
        //                    if (step.key.Equals(key))
        //                    {
        //                        step.value = value;
        //                        f = 0;
        //                    }
        //                    step = step.next;
        //                }
        //                if (step.key.Equals(key))
        //                {
        //                    step.value = value;
        //                    f = 0;
        //                }
        //                if (f == 1)
        //                {
        //                    Entry node = new Entry(key, value);
        //                    step.next = node;
        //                    step = node;
        //                    size++;
        //                }
        //            }
        //            else
        //            {
        //                Entry newNode = new Entry(key, value);
        //                map[index] = newNode;
        //                size++;
        //            }
        //        }



        //    }
        //}
    }
}
