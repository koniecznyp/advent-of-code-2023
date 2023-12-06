var schema = File.ReadAllText("data.txt");

var array = schema
    .Replace("\r","")
    .Split("\n")
    .Select(x => x.ToCharArray())
    .ToArray();

var buffer = "";
List<Gear> gears = [];
bool numberShouldBeAdded = false;
Point currentPoint = Point.Empty();
for(int i=0; i<array.Length; i++)
{
    for(int j=0; j<array[i].Length; j++)
    {
        if (char.IsDigit(array[i][j]))
        {
            buffer = buffer + array[i][j];

            CheckNeighbour(i-1, j-1);
            CheckNeighbour(i-1, j);
            CheckNeighbour(i-1, j+1);
            CheckNeighbour(i, j-1);
            CheckNeighbour(i, j+1);
            CheckNeighbour(i+1, j-1);
            CheckNeighbour(i+1, j);
            CheckNeighbour(i+1, j+1);

            void CheckNeighbour(int i, int j)
            {
                if (IndexesAreInArrayBounds(array, i, j) && array[i][j] == '*')
                {
                    numberShouldBeAdded = true;
                    currentPoint = new(i, j);
                    var gear = gears.SingleOrDefault(x=>x.Point == currentPoint);
                    if (gear is null)
                        gears.Add(new Gear(currentPoint, new List<int>()));
                }
            }
        }
        else
        {
            if (buffer.Length > 0)
            {
                if (buffer != string.Empty && currentPoint != Point.Empty())
                {
                    gears
                        .Single(x => x.Point == currentPoint)
                        .Numbers.Add(int.Parse(buffer));
                    currentPoint = Point.Empty();
                    numberShouldBeAdded = false;
                }
                buffer = string.Empty;
            }
        }
    }
}
var sum = gears
    .Where(x => x.Numbers.Count == 2)
    .Select(x => x.Numbers[0] * x.Numbers[1])
    .Sum();

Console.WriteLine($"sum: {sum}");

bool IndexesAreInArrayBounds(char[][] array, int i, int j)
    => i >= 0 && j >= 0 && i < array[0].GetLength(0) && j < array[0].GetLength(0);

record Point(int i, int j)
{
    public static Point Empty() => new(0, 0);
}
record Gear(Point Point, List<int> Numbers);