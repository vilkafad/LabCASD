using System.Text.RegularExpressions;
using Task18;


//public class HTML
//{
    static void Main(string[] args)
    {
        MyHashMap<string, int> teg = new MyHashMap<string, int>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();
        string pattern = @"(?<=</?)?\w+ ?(?=/?>)";
        if (line == null) Console.WriteLine("Строчка пуста");
        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                if (!teg.containsKey(match.Value.ToLower()))
                    teg.put(match.Value.ToLower(), 1);
                else
                    teg.put(match.Value.ToLower(), teg.get(match.Value.ToLower()) + 1);
            }

            line = sr.ReadLine();
        }
        sr.Close();
        IEnumerable<KeyValuePair<string, int>> pairs = teg.entrySet();
        foreach (var pair in pairs)
            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
    }
//}

