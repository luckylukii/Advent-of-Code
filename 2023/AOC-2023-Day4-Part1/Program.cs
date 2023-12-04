internal class Program
{
    private static string[] inputs;

    private static int numCards = 0;
    private static List<int> instances = new();
    private static void Main()
    {
        inputs = Reader.ReadFile("C:/C# Projects/Advent-of-Code/2023/AOC-2023-Day4-Part1/Inputs.txt").ToArray();

        //initialze list with only 1s
        for (int i = 0; i < inputs.Length; i++)
        {
            instances.Insert(i, 1);
        }

        //for every card
        for (int i = 0; i < inputs.Length; i++)
        {
            int e = EvaluateCard(i);

            //repeat for instances
            for (int k = 0; k < instances[i]; k++)
            {
                numCards++;

                //increment instances for the next e cards
                for (int j = i + 1; j <= i + e; j++)
                {
                    instances[j]++;
                }
            }
        }

        Console.WriteLine(numCards);
        Console.ReadLine();
    }

    //return the matchhing nums
    private static int EvaluateCard(int i)
    {
        inputs[i] = inputs[i][8..];

        string[] arr = inputs[i].Split(" | ");
        string[] winningNumbers = arr[0].Split(' ');
        List<string> numbersYouHave = arr[1].Split(' ').ToList();

        numbersYouHave.RemoveAll((string s) => s == "");

        int cardsWon = 0;
        foreach (string number in numbersYouHave)
        {
            if (winningNumbers.Contains(number))
            {
                cardsWon++;
            }
        }
        return cardsWon;
    }
}