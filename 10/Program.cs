string[] data = File.ReadAllLines("data.txt");

int x = 0;
int y = 0;
var direction = Direction.South;
for(int i=0; i<data.Length; i++)
    for(int j=0; j<data.Count(); j++)
        if (data[i][j] == 'S')
        {
            x = i;
            y = j;
        }

int steps = 0;
var loopCompleted = false;
while(!loopCompleted)
{
    var current = data[x][y];
    direction = (current, direction) switch
    {
        ('L', Direction.South) => Direction.East,
        ('L', Direction.West) => Direction.North,
        ('J', Direction.South) => Direction.West,
        ('J', Direction.East) => Direction.North,
        ('7', Direction.East) => Direction.South,
        ('7', Direction.North) => Direction.West,
        ('F', Direction.West) => Direction.South,
        ('F', Direction.North) => Direction.East,
        _ => direction
    };

    if (direction is Direction.South) x++;
    else if (direction is Direction.North) x--;
    else if (direction is Direction.West) y--;
    else if (direction is Direction.East) y++;
    
    if (data[x][y] == 'S')
        loopCompleted = true;

    steps++;
}

Console.WriteLine($"result: {steps/2}");

enum Direction { North, West, East, South }