var raceData = File.ReadAllLines("data.txt");
var race = new Race(
    long.Parse(raceData[0].Substring(raceData[0].IndexOf(":") + 1).Replace(" ", string.Empty)),
    long.Parse(raceData[1].Substring(raceData[1].IndexOf(":") + 1).Replace(" ", string.Empty)));

var beats = 0;
for (long i=0; i<=race.Time; i++)
{
    var speed = i;
    var route = race.Time - i;
    var distance = speed * route;
    if (distance > race.Distance)
        beats++;
}

Console.WriteLine($"{beats}");

public record Race(long Time, long Distance);