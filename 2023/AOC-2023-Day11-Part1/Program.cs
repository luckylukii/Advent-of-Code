public static class Program
{
    private record Position(int x = 0, int y = 0);
    private record GalaxyPair(Position galaxy1, Position galaxy2);

    private static List<string>? inputs;

    private static void Main()
    {
        long sumOfDistances = 0;

        inputs = Reader.ReadFile("Inputs.txt");

        ExpandUniverse();

        GalaxyPair[] pairs = GetPairs();

        foreach (GalaxyPair pair in pairs)
        {
            long xDiff = Math.Abs(pair.galaxy1.x - pair.galaxy2.x);
            long yDiff = Math.Abs(pair.galaxy1.y - pair.galaxy2.y);

            long dist = xDiff + yDiff;

            sumOfDistances += dist;
        }

        Console.WriteLine(sumOfDistances);
        Console.ReadLine();
    }

    private static void ExpandUniverse()
    {
        //foreach row
        for (int i = inputs!.Count - 1; i >= 0; i--)
        {
            bool expands = !inputs[i].Contains('#');
            if (expands) inputs.Insert(i, inputs[i]);

            //Create column
            List<char> column = new();
            for (int j = 0; j < inputs.Count; j++)
            {
                column.Add(inputs[j][i]);
            }

            expands = !column.Contains('#');
            if (!expands) continue;

            for (int k = 0; k < inputs.Count; k++)
            {
                inputs[k] = inputs[k].Insert(i, ".");
            }

        }
    }

    private static GalaxyPair[] GetPairs()
    {
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
                pairs.Add(new(galaxies[i], galaxies[j]));
            }
        }

        return pairs.ToArray();
    }
}