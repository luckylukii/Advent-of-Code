internal static class Program
{
    private record Deck(string cards, long bid);
    private static char[] cardNumbers = new char[]{
        'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'
    };

    private static string[] inputs;
    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();

        List<Deck> decks = new();
        foreach (var item in inputs)
        {
            string[] split = item.Split(' ');

            decks.Add(new(split[0], long.Parse(split[1])));
        }
        List<Deck> sorted = SortCardsByStrength(decks);
        long result = 0;
        for (int i = 0; i < sorted.Count; i++)
        {
            long rank = i+1;
            long numToAdd = rank * sorted[i].bid;
            result += numToAdd;
            Console.WriteLine($"Rank: {rank}, num: {numToAdd}, Deck: {sorted[i]}, Type: {GetType(sorted[i].cards)}");
            if (i>0)Console.WriteLine($"              Stronger than previous: {sorted[i].cards.IsStrongerThan(sorted[i-1].cards)}");
        }
       
        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static List<Deck> SortCardsByStrength(List<Deck> unsortedCards)
    {
        List<Deck> sortedList = new();
        var comp = new Comparison<Deck>((x, y) => x.cards.IsStrongerThan(y.cards) ? 1 : -1);
        sortedList = unsortedCards.ToList();
        sortedList.Sort(comp);

        return sortedList;
    }

    private static bool IsStrongerThan(this string card1, string card2)
    {
        int type1 = GetType(card1);
        int type2 = GetType(card2);
        if (type1 != type2) return type1 > type2;

        for (int i = 0; i < card1.Length; i++)
        {
            if (card1[i] != card2[i])
            {
                List<char> list = cardNumbers.ToList();
                return list.IndexOf(card1[i]) < list.IndexOf(card2[i]);
            }
        }
        return false;
    }

    private static int GetType(string card)
    {
        List<int> frequencies = new();

        for (int i = 0; i < cardNumbers.Length; i++)
        {
            char num = cardNumbers[i];
            frequencies.Add(card.Count((char c) => c == num));
        }

        for (int i = 0; i < frequencies.Count; i++)
        {
            if (frequencies[i] == 5) return 7;
            else if (frequencies[i] == 4) return 6;

            for (int j = 0; j < frequencies.Count; j++)
            {
                if (i == j) continue;

                if (frequencies[i] == 3 && frequencies[j] == 2) return 5;
                else if (frequencies[i] == 2 && frequencies[j] == 2) return 3;
            }

            if (frequencies[i] == 3) return 4;
            else if (frequencies[i] == 2) return 2;
        }
        return 1;
    }
}