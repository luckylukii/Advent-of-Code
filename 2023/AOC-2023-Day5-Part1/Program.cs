internal static class Program
{
    private record MapRange(long srcStart, long srcEnd, long destDiff);
    private record SeedRange(long start, long length);

    private static List<SeedRange> seeds = new();

    private static MapRange[][]? maps;

    private static string[]? inputs;
    private static void Main()
    {
        InitalizeMaps();

        SeedRange[] converted = seeds.ToArray();

        for (int i = 0; i < maps!.Length; i++)
        {
            for (int j = 0; j < converted.Length; j++)
            {
                converted[j] = converted[j].Evaluate(i);
            }
        }

        long lowestLocation = converted.Min(c => c.start);

        Console.WriteLine(lowestLocation);
        Console.ReadLine();
    }

    private static SeedRange Evaluate(this SeedRange seed, int index)
    {
        /*
        long value = long.Parse(seed);

        for (int i = 0; i < maps![index].Length; i++)
        {
            MapRange range = maps[index][i];

            if (value < range.srcStart || value >= range.srcEnd) continue;

            return (value + range.destDiff).ToString();
        }

        return value.ToString();
        */
        return new(0, 0);
    }

    private static void InitalizeMaps()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();

        string seed = inputs[0];
        seed = seed[7..];
        string[] splitSeeds = seed.Split(' ');
        for (int i = 0; i < splitSeeds.Length; i += 2)
        {
            long seedRangeStart = long.Parse(splitSeeds[i]);
            long seedsRangeLength = long.Parse(splitSeeds[i + 1]);
            SeedRange range = new(seedRangeStart, seedsRangeLength);
        
            seeds.Add(range);
        }

        string[][] mapLines = new string[][]{
            inputs[3..9],
            inputs[11..54],
            inputs[56..95],
            inputs[97..144],
            inputs[146..173],
            inputs[175..183],
            inputs[185..201]
       };

        maps = new MapRange[mapLines.Length][];

        for (int i = 0; i < mapLines.Length; i++)
        {
            maps[i] = new MapRange[mapLines[i].Length];

            for (int j = 0; j < mapLines[i].Length; j++)
            {
                string[] numbers = mapLines[i][j].Split(' ');

                long rangeLength = long.Parse(numbers[2]);

                long sourceStart = long.Parse(numbers[1]);
                long sourceEnd = sourceStart + rangeLength;

                long destDiff = long.Parse(numbers[0]) - sourceStart;

                maps[i][j] = new MapRange(sourceStart, sourceEnd, destDiff);
            }
        }
    }
}