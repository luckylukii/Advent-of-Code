internal static class Program
{
    private record Deck(string cards, ulong bid);
    private static char[] cardNumbers = "AKQT98765432J".ToCharArray();

    private static string[]? inputs;

    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();
        ulong result = 0;

        Deck[] decks = new Deck[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            string[] split = inputs[i].Split(' ');

            ulong bid = ulong.Parse(split[1]);
            Deck deck = new(split[0], bid);
            decks[i] = deck;
        }

        List<Deck> sorted = SortCardsByStrength(decks);
        for (int i = 0; i < sorted.Count; i++)
        {
            result += (ulong)(i + 1) * sorted[i].bid;
            Console.WriteLine(sorted[i].cards);
        }
        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static List<Deck> SortCardsByStrength(Deck[] unsortedCards)
    {
        List<Deck> sortedList = new();
        Comparison<Deck> comp = new((x, y) => x.cards.IsStrongerThan(y.cards) ? 1 : -1);

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

    //1: High Card
    //2: One pair
    //3: Two pair
    //4: Three of a kind
    //5: Full house
    //6: Four of a kind
    //7: Five of a kind
    private static int GetType(string card)
    {
        int numWildcards = card.Count((c) => c == 'J');

        if (numWildcards >= 4) return 7; //4 jokers is always a five of a kind
        

        int[] frequencies = new int[cardNumbers.Length - 1]; //-1 because we don't include J

        for (int i = 0; i < cardNumbers.Length - 1; i++) //-1 because we don't include J
        {
            frequencies[i] = card.Count((char c) => c == cardNumbers[i]);
        }

        if (frequencies.Any(f => f == 5)) return 7; //Five of a kind

        if (frequencies.Any(f => f == 4)) //Four of a kind
        {
            if (numWildcards > 0) return 7; //turns into five of a kind
            else return 6;
        }

        for (int i = 0; i < frequencies.Length; i++)
        {
            if (frequencies[i] < 2) continue; //Optimizations
            for (int j = 0; j < frequencies.Length; j++)
            {
                if (i == j || frequencies[j] < 2) continue; //Optimizations

                if (frequencies[i] == 3 && frequencies[j] == 2) //Full house
                {
                    return numWildcards switch
                    {
                        0 => 5,
                        1 => 6, //Turns into four of a kind
                        _ => 7, //Turns into five of a kind (2 or more)
                    };
                }
                else if (frequencies[i] == 2 && frequencies[j] == 2) //Two pair
                {
                    return numWildcards switch
                    {
                        0 => 3,
                        1 => 5, //Turns into full house
                        2 => 6, //Turns into four of a kind
                        _ => 7, //Turns into five of a kind (3 or more)
                    };
                }
            }
        }

        if (frequencies.Any(f => f == 3)) //Three of a kind
        {
            return numWildcards switch
            {
                0 => 4,
                1 => 6, //Turns into four of a kind
                _ => 7, //Turns into five of a kind (2 or more)
            };
        }
        else if (frequencies.Any(f => f == 2)) //One pair
        {
            return numWildcards switch
            {
                0 => 2,
                1 => 4, //Turns into three of a kind
                2 => 6, //Turns into four of a kind
                _ => 7, //Turns into five of a kind (3 or more)
            };
        }
        else //High card
        {
            return numWildcards switch
            {
                0 => 1,
                1 => 2, //turns into one pair
                2 => 4, //Turns into three of a kind
                3 => 6, //Turns into four of a kind
                _ => 7, //Turns into five of a kind (4 or more)
            };
        }
    }
}