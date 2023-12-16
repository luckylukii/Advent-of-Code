using System.Numerics;
internal static class Program
{
    private static string[] inputs;

    private record Destination(string left, string right);

    private static Dictionary<string, Destination> destinationMap = new();

    private static List<string> current = new();

    private static string instructions = "";
    private static BigInteger[]? steps;
    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();
        InitMaps();
        steps = new BigInteger[current.Count];

        BigInteger result = 1;

        for (int i = 0; i < current.Count; i++)
        {
            while (current[i][^1] != 'Z')
            {
                foreach (char instruction in instructions)
                {
                    if (instruction == 'L')
                    {
                        current[i] = destinationMap[current[i]].left;
                    }
                    else if (instruction == 'R')
                    {
                        current[i] = destinationMap[current[i]].right;
                    }

                    steps[i]++;

                    if (current[i][^1] == 'Z') break;
                }
            }

            result = GetSmallestCommonMultiple(result, steps[i]);
        }

        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static void InitMaps()
    {
        instructions = inputs[0];
        string[] maps = inputs[2..];

        foreach (string map in maps)
        {
            string[] split = map.Split(" = (");

            string[] destinationsSplit = split[1].Split(", ");

            Destination dest = new(destinationsSplit[0], destinationsSplit[1][0..^1]);
            destinationMap.Add(split[0], dest);

            if (split[0][^1] == 'A') current.Add(split[0]);
        }
    }

    private static BigInteger GetGreatestCommonDivisor(BigInteger a, BigInteger b)
    {
        while (b != 0)
        {
            BigInteger temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static BigInteger GetSmallestCommonMultiple(BigInteger a, BigInteger b)
    {
        return a * b / GetGreatestCommonDivisor(a, b);
    }
}