using System;
using System.Text.RegularExpressions;
using laba23;

public class Program
{
    public static void Main(string[] args)
    {
        MyHashSet<string>? word = new MyHashSet<string>();
        string inputFile = ("input.txt");
        StreamReader str = new StreamReader(inputFile);
        string? line = str.ReadLine();
        string pattern = @"\b[a-zA-Z]+\b";
        if (line == null)
            Console.WriteLine("Строчка пуста");
        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                word.Add(match.Value.ToLower());
            }
            line = str.ReadLine();
        }
        str.Close();
        Console.WriteLine("уникальные словечки: ");
        string[] array = word.ToArray();
        foreach (string word2 in array)
        {
            Console.WriteLine(word2);
        }

    }


}





