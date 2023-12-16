using System.Numerics;
internal static class Program
{
    private static string[] inputs;

    private record Destination(string left, string right);

    private static Dictionary<string, Destination> destinationMap = new();

    private static string current = "";

    private static string instructions = "";
    private static int steps = 0;
    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();
        InitMaps();

        current = "AAA";

        while (current != "ZZZ")
        {
            foreach (char instruction in instructions)
            {
                if (instruction == 'L')
                {
                    current = destinationMap[current].left;
                }
                else if (instruction == 'R')
                {
                    current = destinationMap[current].right;
                }

                steps++;

                if (current == "ZZZ") break;
            }
        }

        Console.WriteLine(steps);
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
        }
    }
}