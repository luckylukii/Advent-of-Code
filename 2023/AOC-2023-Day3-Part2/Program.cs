internal class Program
{
    private static string[] inputs;
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day3-Part2/Inputs.txt").ToArray();

        int sumOfGearRatios = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < inputs[i].Length; j++)
            {
                if (inputs[i][j] != '*') continue;

                if (IsGear(i, j, out int ratio))
                {
                    sumOfGearRatios += ratio;
                }
            }
        }

        Console.WriteLine(sumOfGearRatios);
        Console.ReadLine();
    }

    private static bool IsGear(int arrayIndex, int charIndex, out int ratio)
    {
        int num1 = 0;
        int num2 = 0;

        for (int i = arrayIndex - 1; i <= arrayIndex + 1; i++)
        {
            if (i < 0) continue;

            for (int j = charIndex - 1; j <= charIndex + 1; j++)
            {
                if (j < 0) continue;

                if (IsPartNumber(i, j, out int number, num1))
                {
                    if (num1 == 0) num1 = number;
                    else if (num2 == 0) num2 = number;
                }
                if (num1 != 0 && num2 != 0){
                    ratio = num1 * num2;
                    Console.WriteLine($"{arrayIndex}, {charIndex} is a gear (ratio: {num1}*{num2}={ratio})");
                    return true;
                }
            }
        }
        Console.WriteLine($"{arrayIndex}, {charIndex} is not a gear");
        ratio = 0;
        return false;
    }

    private static bool IsPartNumber(int arrayIndex, int indexInNumber, out int number, params int[] invalid)
    {
        if (!IsNumber(inputs[arrayIndex][indexInNumber]))
        {
            number = 0;
            return false;
        }

        string num = "";

        int startIndex = indexInNumber;
        while (startIndex > 0 && IsNumber(inputs[arrayIndex][startIndex - 1]))
        {
            startIndex--;
        }

        for (int i = startIndex; i < inputs[arrayIndex].Length && IsNumber(inputs[arrayIndex][i]); i++)
        {
            num += inputs[arrayIndex][i];
        }

        number = int.Parse(num);
        return !invalid.Contains(number);
    }
    private static bool IsNumber(char characterToCheck)
    {
        return int.TryParse(characterToCheck.ToString(), out int neverUsed);
    }
}