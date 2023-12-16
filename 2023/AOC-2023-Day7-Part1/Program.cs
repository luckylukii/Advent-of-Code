internal static class Program
{
    private record Deck(string cards, ulong bid);
    private static char[] cardNumbers = "AKQJT98765432".ToCharArray();

    private static string[]? inputs;

    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();

        List<Deck> decks = new();
        foreach (var item in inputs)
        {
            string[] split = item.Split(' ');

            ulong bid = ulong.Parse(split[1]);
            Deck deck = new(split[0], bid);
            decks.Add(deck);
        }
        List<Deck> sorted = SortCardsByStrength(decks);
        ulong result = 0;
        for (int i = 0; i < sorted.Count; i++)
        {
            result += (ulong)(i + 1) * sorted[i].bid;
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

        List<char> list = cardNumbers.ToList();

        for (int i = 0; i < card1.Length; i++)
        {
            int index1 = list.IndexOf(card1[i]);
            int index2 = list.IndexOf(card2[i]);
            
            if (index1 == index2) continue;

            return index1 < index2;
        }
        return false;
    }

    /// <summary>
    /// Returns the type of the card as an int
    /// 
    /// 1: High Card
    /// 2: One pair
    /// 3: Two pair
    /// 4: Three of a kind
    /// 5: Full house
    /// 6: Four of a kind
    /// 7: Five of a kind
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    private static int GetType(string card)
    {
        List<int> frequencies = new();

        for (int i = 0; i < cardNumbers.Length; i++)
        {
            char num = cardNumbers[i];
            frequencies.Add(card.Count((char c) => c == num));
        }

        if (frequencies.Any(f => f == 5)) return 7;
        if (frequencies.Any(f => f == 4)) return 6;
        for (int i = 0; i < frequencies.Count; i++)
        {
            for (int j = 0; j < frequencies.Count; j++)
            {
                if (i == j) continue;

                if (frequencies[i] == 3 && frequencies[j] == 2) return 5;
                else if (frequencies[i] == 2 && frequencies[j] == 2) return 3;
            }
        }

        if (frequencies.Any(f => f == 3)) return 4;
        else if (frequencies.Any(f => f == 2)) return 2;
        else return 1;
    }
}