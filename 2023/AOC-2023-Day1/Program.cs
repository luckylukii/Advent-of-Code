internal class Program
{
    private static List<string> inputs = new();
    private static List<int> codes = new();
    private static char[] accepted = new char[]{
        '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };
    private static (string, string)[] replace = new (string, string)[]{
        ("one", "1"),
        ("two", "2"),
        ("three", "3"),
        ("four", "4"),
        ("five", "5"),
        ("six", "6"),
        ("seven", "7"),
        ("eight", "8"),
        ("nine", "9")
    };
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day1/Inputs.txt");

        for (int i = 0; i < inputs.Count; i++)
        {
            var replacedInput = inputs[i];

            string first = GetFirstReplacable(replacedInput, out string firstReplacer);
            string last = GetLastReplacable(replacedInput, out string lastReplacer);
            if (first != "") replacedInput = replacedInput.Replace(first, firstReplacer);
            if (last != "") replacedInput = replacedInput.Replace(last, lastReplacer);

            var v = replacedInput.Where((char c) => accepted.Contains(c));
            var cA = v.ToArray();

            int code = int.Parse(cA[0].ToString() + cA[^1].ToString());
            codes.Add(code);
        }

        Console.WriteLine(Add(codes));
        Console.ReadLine();
    }

    private static int Add(List<int> codes)
    {
        int output = 0;
        foreach (var item in codes)
        {
            output += item;
        }
        return output;
    }

    private static string GetFirstReplacable(string stringToCheck, out string replacer)
    {
        int first = int.MaxValue;
        string output = "";
        replacer = "";
        foreach (var rep in replace)
        {
            int i = stringToCheck.IndexOf(rep.Item1);
            if (i != -1 && i < first)
            {
                first = i;
                output = rep.Item1;
                replacer = rep.Item2;
            }
        }
        return output;
    }
    private static string GetLastReplacable(string stringToCheck, out string replacer)
    {
        int last = int.MinValue;
        string output = "";
        replacer = "";
        foreach (var rep in replace)
        {
            int i = stringToCheck.IndexOf(rep.Item1);
            if (i != -1 && i > last)
            {
                last = i;
                output = rep.Item1;
                replacer = rep.Item2;
            }
        }
        return output;
    }
}
