string[] data = File.ReadAllLines("data.txt");

char[] directions = data[0].ToCharArray();

HashSet<Node> nodes = [];
foreach(var row in data.Skip(2))
{
    var parent = row.Substring(0, 3);
    var childs = row.Substring(row.IndexOf("=") +  1).Trim(' ', '(', ')').Split(",");

    nodes.Add(new(parent, childs[0].Trim(), childs[1].Trim()));
}

var itemFound = false;
int steps = 0;
var currentNode = nodes.First();

while(!itemFound)
{
    if (currentNode.Value.Equals("ZZZ"))
    {
        itemFound = true;
        break;
    }

    currentNode = directions[steps % directions.Length] == 'L'
        ? nodes.First(x => x.Value == currentNode.Left)
        : nodes.First(x => x.Value == currentNode.Right);

    steps++;
}

Console.WriteLine($"steps: {steps}");

public record Node(string Value, string Left, string Right);