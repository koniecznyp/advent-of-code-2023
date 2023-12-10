string[] data = File.ReadAllLines("data.txt");

var sum = 0;
foreach(var row in data)
{
    var history = row.Split(" ").Select(x=>int.Parse(x)).ToArray();
    sum += GetNextNumber(history);
}

Console.WriteLine($"sum: {sum}");

int GetNextNumber(int[] data)
{
    if (data.All(x => x == 0))
        return 0;

    int[] diffs = new int[data.Length - 1];
    for(int i=0; i < data.Length - 1; i++)
    {
        var diff = data[i+1] - data[i];
        diffs[i] = diff;
    }

    return data[0] - GetNextNumber(diffs);
}