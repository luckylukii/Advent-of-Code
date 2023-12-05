internal static class Program
{
    private static string[] seeds;

    private record RangePair(int srcStart, int srcEnd, int destStart, int destEnd);

    private static RangePair[][] maps;

    private static string[] inputs;
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day5-Part1/Inputs.txt").ToArray();
        
        string seed = inputs[0];
        seed = seed[7..];
        seeds = seed.Split(' ');

        string[] converted = seeds;

        int lowestLocation = int.MaxValue;

        for (int i = 0; i < maps.Length; i++)
        {
            foreach (string conv in converted)
            {
                int convertedValue = int.Parse(conv);
                if (conv.isInRange(i, out int convert)) convertedValue = convert;

                lowestLocation = Math.Min(lowestLocation, convertedValue);
            }
        }

        Console.WriteLine(lowestLocation);
        Console.ReadLine();
    }

    private static bool isInRange(this string str, int index, out int converted){
        int value = int.Parse(str);
        maps[index]
    }

    private static void InitalizeMaps()
    {
        string seed = inputs[0];
        seed = seed[7..];
        seeds = seed.Split(' ');

        string[][] mapLines = new string[][]{
            inputs[3..8],
            inputs[10..53],
            inputs[55..94],
            inputs[96..143],
            inputs[145..172],
            inputs[174..182],
            inputs[184..200]
       };

        for (int i = 0; i < maps.Length; i++)
        {
            for (int k = 0; k < maps[i].Length; k++)
            {
                
            }
        }
    }
}