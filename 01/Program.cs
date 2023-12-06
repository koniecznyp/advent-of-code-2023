var calibrations = File.ReadAllLines("data.txt");

var numbers = new Dictionary<string, string>(){
    { "one", "1" },
    { "two", "2" },
    { "three", "3" },
    { "four", "4" },
    { "five", "5" },
    { "six", "6" },
    { "seven", "7" },
    { "eight", "8" },
    { "nine", "9" }};

double sum = 0;
List<(string TextNumber, int Position)> occurences = [];

foreach(var calibration in calibrations)
{
    occurences = [];
    foreach(var number in numbers)
        occurences.Add(new(number.Key, calibration.IndexOf(number.Key)));

    occurences.Add(
        new(calibration.FirstOrDefault(char.IsDigit).ToString() ?? string.Empty,
        calibration.IndexOf(calibration.FirstOrDefault(char.IsDigit))));
    occurences.Add(
        new(calibration.LastOrDefault(char.IsDigit).ToString() ?? string.Empty,
        calibration.IndexOf(calibration.LastOrDefault(char.IsDigit))));

    sum += int.Parse(
        ReplaceTextToNumber(occurences.Where(x => x.Position >= 0).MinBy(x => x.Position).TextNumber) +
        ReplaceTextToNumber(occurences.Where(x => x.Position >= 0).MaxBy(x => x.Position).TextNumber));

    string ReplaceTextToNumber(string numberAsText)
        => numbers.ContainsKey(numberAsText)
            ? numbers[numberAsText]
            : numberAsText;
}
Console.WriteLine($"sum: {sum}");