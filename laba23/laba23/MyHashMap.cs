using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba23
{
    public class MyHashMap<K, V>
    {
        private class Entry
        {
            public K key { get; set; }
            public V value { get; set; }
            public Entry next { get; set; }
            public Entry(K key, V value)
            {
                this.key = key;
                this.value = value;
                next = null;
            }
        }
        Entry[]? table;
        int size;
        double loadFactor;
        public MyHashMap() : this(16, 0.75) { }
        public MyHashMap(int initialCapacity) : this(initialCapacity, 0.75) { }
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            table = new Entry[initialCapacity];
            size = 0;
            this.loadFactor = loadFactor;
        }
        public int GetHashCode(K key) => Math.Abs(key.GetHashCode()) % table.Length;
        public int GetHashCode(V value) => Math.Abs(value.GetHashCode()) % table.Length;
        public void Clear()
        {
            Array.Clear(table);
            size = 0;
        }
        public bool ContainsKey(K key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key)) return true;
                step = step.next;
            }
            return false;
        }
        public bool ContainsValue(V value)
        {
            int index = GetHashCode(value);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(value)) return true;
                step = step.next;
            }
            return false;
        }
        /*реализует итератор, который возвращает все пары ключ-значение из хэш-таблицы
        IEnumerable<KeyValuePair<K, V>>:
        Метод возвращает интерфейс IEnumerable, который позволяет итерироваться по коллекции пар ключ-значение.KeyValuePair<K, V>
        — это структура, представляющая пару ключ-значение, где K — тип ключа, а V — тип значения.*/
        public IEnumerable<KeyValuePair<K, V>> EntrySet()
        {
            foreach (var entry in table)
            {
                for (var step = entry; step != null; step = step.next)
                    yield return new KeyValuePair<K, V>(step.key, step.value);
            }
        }
        public V Get(K key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key)) return step.value;
                step = step.next;
            }
            throw new KeyNotFoundException("Ключ не найден.");
        }
        public bool IsEmpty() => size == 0;
        public K[] KeySet()
        {
            K[] arrayRet = new K[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry step = table[i];

                    while (step != null)
                    {
                        arrayRet[index] = step.key;
                        index++;
                        step = step.next;
                    }
                }
            }
            return arrayRet;
        }
        public void Put(K key, V value)
        {
            double count = (double)(size + 1) / (double)table.Length;
            if (count >= loadFactor)
                ReSize();
            Putс(key, value);
        }
        public void ReSize()
        {
            Entry[] newArray = new Entry[table.Length * 3];
            // Сохраним текущий размер, чтобы его не увеличивать в процессе
            int oldSize = size;
            size = 0;

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry val = table[i];
                    while (val != null)
                    {
                        // Вычисляем индекс для нового массива
                        int index = Math.Abs(val.key.GetHashCode()) % newArray.Length;
                        // Сохраняем следующий элемент перед добавлением в новый массив
                        Entry nextVal = val.next;
                        // Добавляем элемент в новый массив
                        PutInNewArray(newArray, val.key, val.value);
                        val = nextVal;  // Переход к следующему элементу
                    }
                }
            }
            table = newArray;  // Обновляем ссылку на массив
        }

        private void PutInNewArray(Entry[] array, K key, V value)
        {
            int index = Math.Abs(key.GetHashCode()) % array.Length;
            Entry newNode = new Entry(key, value);
            if (array[index] != null)
            {
                // Если элемент уже существует, добавляем в конец
                Entry step = array[index];
                while (step.next != null)
                    step = step.next;
                step.next = newNode;
            }
            else
                // Если пусто, просто добавляем новый элемент
                array[index] = newNode;
            size++;  // Увеличиваем размер
        }
        public void Putс(K key, V value)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            if (step != null)
            {
                int f = 1;
                while (step.next != null)
                {
                    if (step.key.Equals(key))
                    {
                        step.value = value;
                        f = 0;
                    }
                    step = step.next;
                }
                if (step.key.Equals(key))
                {
                    step.value = value;
                    f = 0;
                }
                if (f == 1)
                {
                    Entry node = new Entry(key, value);
                    step.next = node;
                    step = node;
                    size++;
                }
            }
            else
            {
                Entry newNode = new Entry(key, value);
                table[index] = newNode;
                size++;
            }
        }
        public int Size() => size;
        public void Remove(K key)
        {
            int index = GetHashCode(key);
            // Если в индексе нет значения, ничего не делаем
            if (table[index] == null)
                return;
            // Если удаляемый ключ - первый в списке
            if (table[index].key.Equals(key))
            {
                table[index] = table[index].next;
                size--;
                return;
            }
            // Ищем ключ в списке
            Entry current = table[index];
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
    }
}
