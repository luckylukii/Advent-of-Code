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

    private static List<string> testInputs = new(){
        "two1nine",
        "eightwothree",
        "abcone2threexyz",
        "xtwone3four",
        "4nineeightseven2",
        "zoneight234",
        "7pqrstsixteen"
    };
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day1/Inputs.txt");

        for (int i = 0; i < testInputs.Count; i++)
        {
            var replacedInput = testInputs[i];
            foreach (var item in replace)
            {
                replacedInput = replacedInput.Replace(item.Item1, item.Item2);
            }

            var v = replacedInput.Where((char c) => accepted.Contains(c));
            var cA = v.ToArray();

            int code = int.Parse(cA[0].ToString() + cA[^1].ToString());
            Console.WriteLine(code);
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
}
