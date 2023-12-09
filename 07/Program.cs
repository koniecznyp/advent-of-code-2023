string[] data = File.ReadAllLines("data.txt");

var hands = new List<Hand>();
foreach(var row in data)
{
    var hand = row.Split(" ");
    hands.Add(new(hand[0].ToCharArray(), int.Parse(hand[1])));
}

var sortedHands = hands
    .OrderBy(x => IsFiveOfKind(x.Cards))
    .ThenBy(x => IsFourOfKind(x.Cards))
    .ThenBy(x => IsFullHouse(x.Cards))
    .ThenBy(x => IsThreeOfKind(x.Cards))
    .ThenBy(x => IsTwoPair(x.Cards))
    .ThenBy(x => IsOnePair(x.Cards))
    .ThenBy(x => IsHighCard(x.Cards))
    .ThenBy(x => string.Join("", x.Cards), new CamelCardSorter())
    .ToArray();

var sum = 0;
for(int i=0; i< sortedHands.Length; i++)
    sum += (i + 1) * sortedHands[i].Bid;

Console.WriteLine($"sum: {sum}");

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

record Hand(char[] Cards, int Bid);

class CamelCardSorter : IComparer<string>
{
    public int Compare(string? x, string? y)
    {
        var chars = "AKQJT98765432J";

        for(int i=0; i<x.Length; i++)
        {
            if (x[i].Equals(y[i]))
                continue;

            return chars.IndexOf(x[i]) < chars.IndexOf(y[i])
                ? 1
                : -1;
        }

        return 0;
    }
}