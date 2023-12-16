internal static class Program
{
    private static List<string> inputs;
    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt");

        int result = 0;

        for (int i = 0; i < inputs.Count; i++)
        {
            var split = inputs[i].Split(' ');

            List<int> number = new();
            foreach (var num in split)
            {
                number.Add(int.Parse(num));
            }

            result += GetLastNumber(number.ToArray());
        }

        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static int GetLastNumber(int[] number)
    {
        List<int[]> diffs = new(){
            number
        };

        for (int i = 1; !AllZero(diffs[^1]); i++)
        {
            diffs.Insert(i, GetDifferences(diffs[i - 1]));
        }

        int[] placeholders = new int[diffs.Count];

        for (int i = diffs.Count - 2; i >= 0 ; i--)
        {
            placeholders[i] = placeholders[i + 1] + diffs[i][^1];
        }

        return placeholders[0];
    }

    private static int[] GetDifferences(int[] original)
    {
        int[] diffs = new int[original.Length - 1];

        for (int i = 0; i < original.Length - 1; i++)
        {
            diffs[i] = original[i + 1] - original[i];
        }

        return diffs;
    }

    private static bool AllZero(int[] arr)
    {
        foreach (int i in arr)
        {
            if (i != 0) return false;
        }
        return true;
    }
}