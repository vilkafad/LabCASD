using laba16;
MyLinkedList<int> array = new MyLinkedList<int>();
int n = 6;
for (int i = 0; i < n; i++)
{
    array.add(i);
}
array.addFirst(3);
//for (int i = 0; i < array.Size(); i++)
//{
//    Console.WriteLine(array.get(i));
//}
Console.WriteLine(array.print());
array.removeLastOccurrence(3);
Console.WriteLine(array.print());



