using System.Text.RegularExpressions;
namespace laba20
{
    internal class Program
    {
        enum TypeEnum
        {
            Integer,
            Double,
            Float
        }
        static void Main(string[] args)
        {
            try
            {
                MyHashMap<string, string> variable = new MyHashMap<string, string>();
                string pattern = @"(double|int|float) \S* ?(?:=) ?(\S)+?(?=;)";
                StreamReader sr = new StreamReader("input.txt");
                string? line = sr.ReadLine();
                if (line == null) Console.WriteLine("Строчка пуста");
                while (line != null)
                {
                    MatchCollection matches = Regex.Matches(line, pattern);
                    foreach (Match match in matches)
                    {
                        string[] parts = match.Value.Split(' ');
                        string type = parts[0].Trim();
                        string valuable = parts[3].Trim();
                        string name = parts[1].Trim();
                        string tv = type + " " + valuable;
                        if (!variable.ContainsKey(name)) variable.Put(name, tv);
                        else
                        {
                             Console.WriteLine("was changed:" + " " + $"{type} {name}={valuable}");
                            //variable.Put(name, tv);
                        }
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
                var pair = variable.EntrySet();
                StreamWriter sw = new StreamWriter("output.txt");
                foreach (var pai in pair)
                {
                    string vk = pai.Value + " " + pai.Key;
                    sw.WriteLine(vk);
                    Console.WriteLine(vk);
                }
                    sw.Close();
            }
            catch (Exception ex) { Console.WriteLine("Exception : " + ex.Message); }
        }
    }
}
