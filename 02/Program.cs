string[] games = File.ReadAllLines("data.txt");

var sum = 0;
foreach (var game in games)
{
    var data = game.Split(":");
    var gameNumber = int.Parse(data[0].Replace("Game ", string.Empty));
    var sets = data[1]
        .Split(";")
        .Select(x => x.Trim().Split(","))
        .Select(x => x
            .Select(y => {
                var cube = y.Trim().Split(" ");
                return new CubeResult(cube[1], int.Parse(cube[0]));
            }).ToArray());

    sum += GetPowerOfSets(sets);
}

int GetPowerOfSets(IEnumerable<CubeResult[]> sets)
{
    return GetMaxOfColor("red") *
            GetMaxOfColor("green") * 
            GetMaxOfColor("blue");

    int GetMaxOfColor(string color)
        => sets
            .SelectMany(x => x.Where(y => y.Color == color))
            .MaxBy(x => x.Number).Number;
}

Console.WriteLine($"sum: {sum}");

record CubeResult(string Color, int Number);