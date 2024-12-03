using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task16;

namespace Task22
{

    public class MyTreeMap<K, V> where K : IComparable
    {
        private class Node
        {
            public K Key { get; set; }

            public V Value { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public Node(K Key, V Value)
            {
                this.Key = Key;
                this.Value = Value;
                Left = null;
                Right = null;
            }
        }

        private IComparer<K> comparator;

        private Node root;

        private int size;

        public MyTreeMap()
        {
            comparator = new MyComparator<K>();
            size = 0;
        }

        public MyTreeMap(IComparer<K> comp)
        {
            comparator = comp;
        }

        public void Clear()
        {
            root = null;
            size = 0;
        }

        public bool ContainsKey(K key)
        {
            Node node = root;
            while (node != null)
            {
                if (comparator.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                    continue;
                }

                if (comparator.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                    continue;
                }

                return true;
            }

            return false;
        }

        public bool ContainsValue(V value)
        {
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    if (node.Value.Equals(value))
                    {
                        return true;
                    }

                    stack.Push(node);
                    node = node.Left;
                }

                stack.Pop();
                node = node.Right;
            }

            return false;
        }

        public List<KeyValuePair<K, V>> EntrySet()
        {
            List<KeyValuePair<K, V>> list = new List<KeyValuePair<K, V>>();
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    list.Add(new KeyValuePair<K, V>(node.Key, node.Value));
                    stack.Push(node);
                    node = node.Left;
                }

                node = stack.Pop();
                node = node.Right;
            }

            return list;
        }

        public V Get(K key)
        {
            Node node = root;
            while (node != null)
            {
                if (comparator.Compare(key, node.Key) < 0)
                {
                    node = node.Left;
                    continue;
                }

                if (comparator.Compare(key, node.Key) > 0)
                {
                    node = node.Right;
                    continue;
                }

                return node.Value;
            }

            return default(V);
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public K[] KeySet()
        {
            K[] array = new K[size];
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            int num = 0;
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    array[num] = node.Key;
                    stack.Push(node);
                    node = node.Left;
                    num++;
                }

                node = stack.Pop();
                node = node.Right;
            }

            return array;
        }

        public void Put(K key, V value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                size++;
                return;
            }

            Node node = new Node(key, value);
            size++;
            Node node2 = root;
            while (node2 != null)
            {
                if (comparator.Compare(node.Key, node2.Key) < 0)
                {
                    if (node2.Left == null)
                    {
                        node2.Left = node;
                        break;
                    }

                    node2 = node2.Left;
                    continue;
                }

                if (comparator.Compare(node.Key, node2.Key) > 0)
                {
                    if (node2.Right == null)
                    {
                        node2.Right = node;
                        break;
                    }

                    node2 = node2.Right;
                    continue;
                }

                node2.Value = value;
                size--;
                break;
            }
        }

        public void Remove(K key)
        {
            if (comparator.Compare(key, root.Key) == 0 && root.Right == null && root.Left == null)
            {
                root = null;
                size--;
                return;
            }

            Node node = root;
            Node node2 = root;
            if (comparator.Compare(key, root.Key) < 0)
            {
                node2 = root.Left;
            }
            else if (comparator.Compare(key, root.Key) > 0)
            {
                node2 = root.Right;
            }

            while (node2 != null)
            {
                if (comparator.Compare(key, node2.Key) == 0)
                {
                    if (node2.Left == null && node2.Right == null)
                    {
                        if (comparator.Compare(node2.Key, node.Key) < 0)
                        {
                            size--;
                            node.Left = null;
                        }
                        else
                        {
                            size--;
                            node.Right = null;
                        }

                        break;
                    }

                    if ((node2.Left == null && node2.Right != null) || (node2.Right == null && node2.Left != null))
                    {
                        if (node2.Left != null)
                        {
                            node2.Value = node2.Left.Value;
                            node2.Key = node2.Left.Key;
                            node2.Right = node2.Left.Right;
                            node2.Left = node2.Left.Left;
                            size--;
                            break;
                        }

                        if (node2.Right != null)
                        {
                            node2.Value = node2.Right.Value;
                            node2.Key = node2.Right.Key;
                            node2.Right = node2.Right.Right;
                            node2.Left = node2.Right.Left;
                            size--;
                            break;
                        }
                    }
                    else if (node2.Left != null && node2.Right != null)
                    {
                        Node node3 = node2.Left;
                        if (node3.Right == null)
                        {
                            node3 = node2.Left;
                        }

                        while (node3.Right != null)
                        {
                            node3 = node3.Right;
                        }

                        Node node4 = node3;
                        if (node4.Left != null)
                        {
                            node2.Value = node3.Value;
                            node2.Key = node3.Key;
                            node4.Value = node4.Left.Value;
                            node4.Key = node4.Left.Key;
                            node4.Left = node4.Left.Left;
                        }
                        else if (node4.Left == null)
                        {
                            node2.Value = node3.Value;
                            node2.Key = node3.Key;
                            node2.Left.Right = node3.Left;
                        }

                        size--;
                        break;
                    }
                }
                else if (comparator.Compare(key, node2.Key) < 0)
                {
                    node = node2;
                    node2 = node2.Left;
                }
                else if (comparator.Compare(key, node2.Key) > 0)
                {
                    node = node2;
                    node2 = node2.Right;
                }
            }
        }

        public int Size()
        {
            return size;
        }

        public K FirstKey()
        {
            if (root != null)
            {
                return root.Key;
            }

            return default(K);
        }

        public K LastKey()
        {
            for (Node right = root; right != null; right = right.Right)
            {
                if (right.Right == null)
                {
                    return right.Key;
                }
            }

            return default(K);
        }

        public MyTreeMap<K, V> HeadMap(K end)
        {
            MyTreeMap<K, V> myTreeMap = new MyTreeMap<K, V>();
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    if (comparator.Compare(node.Key, end) < 0)
                    {
                        myTreeMap.Put(node.Key, node.Value);
                    }

                    stack.Push(node);
                    node = node.Left;
                }

                if (stack.Count > 0)
                {
                    node = stack.Pop();
                    if (comparator.Compare(node.Key, end) >= 0)
                    {
                        break;
                    }

                    node = node.Right;
                }
            }

            return myTreeMap;
        }

        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            MyTreeMap<K, V> myTreeMap = new MyTreeMap<K, V>();
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    if (comparator.Compare(node.Key, start) >= 0 && comparator.Compare(node.Key, end) < 0)
                    {
                        myTreeMap.Put(node.Key, node.Value);
                    }

                    stack.Push(node);
                    node = node.Left;
                }

                if (stack.Count > 0)
                {
                    node = stack.Pop();
                    node = node.Right;
                }
            }

            return myTreeMap;
        }

        public MyTreeMap<K, V> TailMap(K start)
        {
            MyTreeMap<K, V> myTreeMap = new MyTreeMap<K, V>();
            Node node = root;
            Stack<Node> stack = new Stack<Node>();
            while (node != null || stack.Count > 0)
            {
                while (node != null)
                {
                    if (comparator.Compare(node.Key, start) >= 0)
                    {
                        myTreeMap.Put(node.Key, node.Value);
                    }

                    stack.Push(node);
                    node = node.Left;
                }

                if (stack.Count > 0)
                {
                    node = stack.Pop();
                    node = node.Right;
                }
            }

            return myTreeMap;
        }

        public IEnumerable<KeyValuePair<K, V>> FirstEntry()
        {
            yield return new KeyValuePair<K, V>(root.Key, root.Value);
        }

        public IEnumerable<KeyValuePair<K, V>> LastEntry()
        {
            for (Node step = root; step != null; step = step.Right)
            {
                if (step.Right == null)
                {
                    yield return new KeyValuePair<K, V>(root.Key, root.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<K, V>> LowerEntry(K key)
        {
            Node step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0 || comparator.Compare(key, step.Key) == 0)
                {
                    step = step.Left;
                }
                else
                {
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<K, V>> FloorEntry(K key)
        {
            Node step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) < 0)
                {
                    step = step.Left;
                }
                else
                {
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<K, V>> HigherEntry(K key)
        {
            Node step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0 || comparator.Compare(key, step.Key) == 0)
                {
                    step = step.Right;
                }
                else
                {
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
                }
            }
        }

        public IEnumerable<KeyValuePair<K, V>> CeilingEntry(K key)
        {
            Node step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) > 0)
                {
                    step = step.Right;
                }
                else
                {
                    yield return new KeyValuePair<K, V>(step.Key, step.Value);
                }
            }
        }

        public K LowerKey(K key)
        {
            Node left = root;
            while (left != null)
            {
                if (comparator.Compare(key, left.Key) < 0 || comparator.Compare(key, left.Key) == 0)
                {
                    left = left.Left;
                    continue;
                }

                return left.Key;
            }

            return default(K);
        }

        public K FloorKey(K key)
        {
            Node left = root;
            while (left != null)
            {
                if (comparator.Compare(key, left.Key) < 0)
                {
                    left = left.Left;
                    continue;
                }

                return left.Key;
            }

            return default(K);
        }

        public K HigherKey(K key)
        {
            Node right = root;
            while (right != null)
            {
                if (comparator.Compare(key, right.Key) > 0 || comparator.Compare(key, right.Key) == 0)
                {
                    right = right.Right;
                    continue;
                }

                return right.Key;
            }

            return default(K);
        }

        public K CeilingKey(K key)
        {
            Node right = root;
            while (right != null)
            {
                if (comparator.Compare(key, right.Key) > 0)
                {
                    right = right.Right;
                    continue;
                }

                return right.Key;
            }

            return default(K);
        }

        public K PollFirstEntry()
        {
            K key = root.Key;
            Remove(root.Key);
            return key;
        }

        public K PollLastEntry()
        {
            K val = LastKey();
            Remove(val);
            return val;
        }

        //public void PrintTree()
        //{
        //    Node node = root;
        //    Print(node);
        //    static void Print(Node root)
        //    {
        //        if (root != null)
        //        {
        //            Print(root.Left);
        //            Console.WriteLine(root.Key);
        //            Print(root.Right);
        //        }
        //    }
        //}
        //}
    }
}
