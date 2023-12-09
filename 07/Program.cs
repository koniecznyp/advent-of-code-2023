using System.Diagnostics;

char[] cards = "AKQT98765432J".ToCharArray();
string[] data = File.ReadAllLines("data.txt");

var sum = data
    .Select(x => {
        var hand = x.Split(" ");
        return new Hand(hand[0].ToCharArray(), int.Parse(hand[1]));
    })
    .OrderBy(x => GetHandType(x.Cards))
    .ThenBy(x => string.Join(string.Empty, x.Cards), new CamelCardSorter())
    .Select((x, i) => (i + 1) * x.Bid)
    .Sum();

Console.WriteLine($"sum: {sum}");

HandType GetHandType(char[] cards)
{
    if (!cards.Contains('J'))
        return GetType(cards);

    var highestType = GetType(cards);
    char[] variant = new char[cards.Length];
    foreach(var item in cards)
    {
        Array.Copy(cards, variant, cards.Length);
        cards.CopyTo(variant, 0);
        for (int i=0; i<cards.Length; i++)
        {
            if (variant[i] == 'J')
                variant[i] = item;
        }
        var variantType = GetType(variant);
        if (variantType > highestType)
            highestType = variantType;
    }
 
    return highestType;

    HandType GetType(char[] cards)
    {
        if (IsFiveOfKind(cards))
            return HandType.FiveOfKind;
        if (IsFourOfKind(cards))
            return HandType.FourOfKind;
        if (IsFullHouse(cards))
            return HandType.FullHouse;
        if (IsThreeOfKind(cards))
            return HandType.ThreeOfKind;
        if (IsOnePair(cards))
            return HandType.OnePair;
        if (IsTwoPair(cards))
            return HandType.TwoPair;
        if (IsHighCard(cards))
            return HandType.HighCard;
        throw new UnreachableException();
    }
}

bool IsFiveOfKind(char[] cards)
    => cards
        .GroupBy(x => x)
        .Count() == 1;

bool IsFourOfKind(char[] cards)
    => cards
        .GroupBy(x => x)
        .Any(x => x.Count() == 4);

bool IsFullHouse(char[] cards)
    => cards
        .GroupBy(x => x)
        .All(x => x.Count() == 3 || x.Count() == 2);

bool IsThreeOfKind(char[] cards)
{
    var grp = cards.GroupBy(x => x);
    return grp.Count() == 3 &&
           grp.Any(x=>x.Count() == 3);
}

bool IsOnePair(char[] cards)
{
    var grp = cards.GroupBy(x => x);
    return grp.Count() == 4 &&
           grp.Any(x => x.Count() == 2);
}

bool IsTwoPair(char[] cards)
{
    var grp = cards.GroupBy(x => x);
    return grp.Count() == 3 && 
           grp.Any(x => x.Count() == 2);
}

bool IsHighCard(char[] cards)
    => cards
        .GroupBy(x => x)
        .All(x => x.Count() == 1);

enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfKind,
    FullHouse,
    FourOfKind,
    FiveOfKind
}

record Hand(char[] Cards, int Bid);

class CamelCardSorter : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        var cards = "AKQT98765432J";
        for(int i=0; i<x.Length; i++)
        {
            if (x[i].Equals(y[i]))
                continue;

            return cards.IndexOf(x[i]) < cards.IndexOf(y[i]) ? 1: -1;
        }

        return 0;
    }
}