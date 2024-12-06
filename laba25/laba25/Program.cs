using laba25;
public class MyComparator<T> : Comparer<T> where T : IComparable
{
    public override int Compare(T f, T s)
    {
        string fl = f as string; //проверка на стринг, безопасное приведение, вернет нал
        string sl = s as string;
        string[] fLines = fl.Split(' ');
        string[] sLines = sl.Split(' ');
        int minF = fLines[0].Length;
        int minS = sLines[0].Length;
        MyHashSet<string> First = new MyHashSet<string>();
        MyHashSet<string> Second = new MyHashSet<string>();
        while (true)
        {
            int indF = 0;
            int indS = 0;
            for (int i = 0; i < fLines.Length; i++)
                if (fLines[i].Length < minF && !First.Contains(fLines[i])) { minF = fLines[i].Length; indF = i; }
            First.Add(fLines[indF]);
            for (int j = 0; j < sLines.Length; j++)
                if (sLines[j].Length < minS && !Second.Contains(sLines[j])) { minS = sLines[j].Length; indS = j; }
            Second.Add(sLines[indS]);
            if (minF < minS) return -1;
            else if (minF > minS) return 1;
            else if (First.Size() == fLines.Length && Second.Size() != sLines.Length) return -1;
            else if (First.Size() != fLines.Length && Second.Size() == sLines.Length) return 1;
        }
        return default(int);
    }
}
public class Program
{
    static public void Main(string[] args)
    {
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();
        List<string> lines = new List<string>();
        while (line != null)
        {
            lines.Add(line);
            line = sr.ReadLine();
        }
        sr.Close();
        lines.Sort(new MyComparator<string>());
        foreach (var lin in lines)
        {
            Console.WriteLine(lin);
        }
    }
}