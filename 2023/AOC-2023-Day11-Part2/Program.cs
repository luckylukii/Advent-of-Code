public static class Program
{
    private const int EXPAND_BY = 999999;
    private record Position(int x = 0, int y = 0);
    private record GalaxyPair(Position galaxy1, Position galaxy2);

    private static List<string>? inputs;

    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt");

        long sumOfDistances = Evaluate();

        Console.WriteLine(sumOfDistances);
        Console.ReadLine();
    }

    private static long GetNumExpandedRowsBetween(int start, int end)
    {
        int startingIndex = Math.Min(start, end);
        int lastIndex = Math.Max(start, end);

        long result = 0;
        for (int i = startingIndex; i < lastIndex; i++)
        {
            bool expands = !inputs[i].Contains('#');
            if (expands) result++;
        }
        return result;
    }
    private static long GetNumExpandedColumnsBetween(int start, int end)
    {
        int startingIndex = Math.Min(start, end);
        int lastIndex = Math.Max(start, end);

        long result = 0;
        for (int i = startingIndex; i < lastIndex; i++)
        {
            bool expands = true;

            for (int j = 0; j < inputs!.Count; j++)
            {
                if (inputs[j][i] == '#')
                {
                    expands = false;
                    break;
                }
            }

            if (expands) result++;
        }
        return result;
    }

    private static long Evaluate()
    {
        long result = 0;

        List<GalaxyPair> pairs = new();

        List<Position> galaxies = new();
        for (int i = 0; i < inputs!.Count; i++)
        {
            if (!inputs[i].Contains('#')) continue;

            for (int j = 0; j < inputs[i].Length; j++)
            {
                if (inputs[i][j] == '#') galaxies.Add(new(i, j));
            }
        }

        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = 0; j < galaxies.Count; j++)
            {

                if (i == j || pairs.Contains(new(galaxies[j], galaxies[i]))) continue;

                GalaxyPair pair = new(galaxies[i], galaxies[j]);

                pairs.Add(pair);

                result += ComputeGalaxy(pair);
            }
        }

        return result;
    }

    private static long ComputeGalaxy(GalaxyPair pair)
    {
        long xDiff = Math.Abs(pair.galaxy1.x - pair.galaxy2.x) + (GetNumExpandedRowsBetween(pair.galaxy1.x, pair.galaxy2.x) * EXPAND_BY);
        long yDiff = Math.Abs(pair.galaxy1.y - pair.galaxy2.y) + (GetNumExpandedColumnsBetween(pair.galaxy1.y, pair.galaxy2.y) * EXPAND_BY);

        long dist = xDiff + yDiff;

        return dist;
    }
}