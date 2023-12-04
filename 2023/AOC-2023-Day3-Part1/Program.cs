internal class Program
{
    private static string[] inputs;
    private static string currentPartNumber = "";
    private static int currentPartNumberStartIndex = 0;
    private static int currentPartNumberArrayIndex = 0;
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day3-Part1/Inputs.txt").ToArray();

        int sumOfPartNumbers = 0;

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < inputs[i].Length; j++)
            {
                char currentlyChecking = inputs[i][j];
                if (IsNumber(currentlyChecking))
                {
                    if (currentPartNumber == "") currentPartNumberStartIndex = j;
                    currentPartNumberArrayIndex = i;

                    currentPartNumber += currentlyChecking;
                }
                else
                {
                    if (currentPartNumber != "")
                    {
                        if (IsPartNumber(currentPartNumberArrayIndex, currentPartNumberStartIndex, currentPartNumber.Length))
                        {
                            sumOfPartNumbers += int.Parse(currentPartNumber);
                        }
                        else
                        {
                            Console.WriteLine($"{currentPartNumber} is not a part number ({currentPartNumberArrayIndex}; {currentPartNumberStartIndex}; {currentPartNumber.Length})");
                        }
                    }
                    currentPartNumber = "";
                }
            }
            if (currentPartNumber != "")
            {
                if (IsPartNumber(currentPartNumberArrayIndex, currentPartNumberStartIndex, currentPartNumber.Length))
                {
                    sumOfPartNumbers += int.Parse(currentPartNumber);
                }
                else
                {
                    Console.WriteLine($"{currentPartNumber} is not a part number ({currentPartNumberArrayIndex}; {currentPartNumberStartIndex}; {currentPartNumber.Length})");
                }
            }
            currentPartNumber = "";
        }

        Console.WriteLine(sumOfPartNumbers);
        Console.ReadLine();
    }

    private static bool IsPartNumber(int arrayIndex, int startIndex, int length)
    {
        for (int i = arrayIndex - 1; i <= arrayIndex + 1; i++)
        {
            for (int j = startIndex - 1; j < startIndex + length + 1; j++)
            {
                if (i >= 0 && i < inputs.Length && j >= 0 && j < inputs[i].Length && SymbolAtIndex(i, j)) return true;
            }
        }
        return false;
    }

    private static bool SymbolAtIndex(int arrayIndex, int charIndex)
    {
        char currentlyChecking = inputs[arrayIndex][charIndex];
        return !IsNumber(currentlyChecking) && currentlyChecking != '.';
    }

    private static bool IsNumber(char characterToCheck)
    {
        return int.TryParse(characterToCheck.ToString(), out int neverUsed);
    }
}