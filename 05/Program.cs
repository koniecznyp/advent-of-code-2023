var data = File.ReadAllLines("data.txt");

long[] seeds = data[0]
    .Replace("seeds: ", "")
    .Split(" ")
    .Select(long.Parse)
    .ToArray();

List<Map> maps = [];
foreach(var line in data.Skip(2))
{
    if (line == string.Empty)
        continue;

    if (line.EndsWith(":"))
    {
        maps.Add(Map.Empty());
        continue;
    }

    var rangeInfo = line
        .Split(" ")
        .Select(long.Parse)
        .ToArray();

    maps.Last().Ranges.Add(new RangeMap(rangeInfo[1], rangeInfo[0], rangeInfo[2]));
}

var minLocation = long.MaxValue;
long nextSource = 0;
foreach(var seed in seeds)
{
    var source = seed;
    foreach(var map in maps)
    {
        nextSource = source;
        foreach(var range in map.Ranges)
        {
            if (source >= range.Source &&
                source < range.Source + range.Length)
            {
                var offset = source - range.Source; 
                nextSource = range.Destination + offset;
            }
        }
        source = nextSource;
        if (map.Equals(maps.Last()))
        {
            if (source < minLocation)
               minLocation = source;
        }
    }
    
}

Console.WriteLine($"min location {minLocation}");

struct Map {
    public List<RangeMap> Ranges;
    public static Map Empty()
        => new Map() { Ranges = [] };
}
record RangeMap(long Source, long Destination, long Length);