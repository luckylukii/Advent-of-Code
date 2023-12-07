internal static class Program
{
    private static List<string> seeds = new();

    private record RangePair(long srcStart, long srcEnd, long destDiff);

    private static RangePair[][] maps;

    private static string[] inputs;
    private static void Main()
    {
        InitalizeMaps();

        string[] converted = seeds.ToArray();

        for (int i = 0; i < maps.Length; i++)
        {
            for (int j = 0; j < converted.Length; j++)
            {
                converted[j] = converted[j].Evaluate(i);
            }
        }

        long lowestLocation = long.Parse(converted.Min());

        Console.WriteLine(lowestLocation);
        Console.ReadLine();
    }

    private static string Evaluate(this string str, int index)
    {
        long value = long.Parse(str);

        for (int i = 0; i < maps[index].Length; i++)
        {
            RangePair range = maps[index][i];

            if (value < range.srcStart || value >= range.srcEnd) continue;

            return (value + range.destDiff).ToString();
        }

        return value.ToString();
    }

    private static void InitalizeMaps()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();

        string seed = inputs[0];
        seed = seed[7..];
        string[] splitSeeds = seed.Split(' ');
        for (int i = 0; i < splitSeeds.Length; i += 2)
        {
            (string, string) range = (splitSeeds[i], splitSeeds[i+1]);
            for (int j = 0; j < int.Parse(range.Item2); j++)
            {
                seeds.Add(range.Item1 + j);
            }
            Console.Write("=");
        }
        Console.WriteLine($"We have {seeds.Count} seeds!");

        string[][] mapLines = new string[][]{
            inputs[3..9],
            inputs[11..54],
            inputs[56..95],
            inputs[97..144],
            inputs[146..173],
            inputs[175..183],
            inputs[185..201]
       };

        maps = new RangePair[mapLines.Length][];

        for (int i = 0; i < mapLines.Length; i++)
        {
            maps[i] = new RangePair[mapLines[i].Length];

            for (int j = 0; j < mapLines[i].Length; j++)
            {
                string[] numbers = mapLines[i][j].Split(' ');

                long rangeLength = long.Parse(numbers[2]);

                long sourceStart = long.Parse(numbers[1]);
                long sourceEnd = sourceStart + rangeLength;

                long destDiff = long.Parse(numbers[0]) - sourceStart;

                maps[i][j] = new RangePair(sourceStart, sourceEnd, destDiff);
            }
        }
    }
}