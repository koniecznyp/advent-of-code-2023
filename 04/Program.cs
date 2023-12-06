using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("data.txt");

Card[] cards = [];
foreach(var card in input)
{
    var number = int.Parse(card.Substring(0, card.IndexOf(":")).Replace("Card ", ""));
    var data = card.Substring(card.IndexOf(":") + 1).Split("|");
    cards = cards.Append(new(
        number,
        Regex.Split(data[0].Trim(), "[ \t]+"),
        Regex.Split(data[1].Trim(), "[ \t]+")
    )).ToArray();
}

List<int> copies = [];
foreach(var card in cards)
{
    var matchedNumbers = 0;
    foreach(var ownedNumber in card.OwnedNumbers)
    {
        if (card.WinningNumbers.Contains(ownedNumber))
            matchedNumbers++;
    }

    copies.Add(card.Number);
    var existingCopies = copies.Count(x => x == card.Number);
    existingCopies = existingCopies == 0 ? 1 : existingCopies;
    copies.AddRange(
        Enumerable.Repeat(
            Enumerable.Range(card.Number + 1, matchedNumbers), existingCopies)
        .SelectMany(x => x));
}

Console.WriteLine($"{copies.Count}");

record Card(int Number, string[] WinningNumbers, string[] OwnedNumbers);