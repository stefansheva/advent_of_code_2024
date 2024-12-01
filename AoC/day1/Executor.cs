namespace AoC;

public static class Executor
{
    public const string Separator = "   ";
    public const string EmptySpace = " ";

    public static async Task FirstPart()
    {
        var gameInput = await File.ReadAllTextAsync("./input.txt");

        var numbers = gameInput.Replace(Separator, EmptySpace)
            .Split("\n")
            .ToList()
            .Select(item => item.Split(EmptySpace))
            .Select(x => (int.Parse(x[0]), int.Parse(x[1])));

        var totalDistance = numbers.Select(x => x.Item1).OrderBy(x => x)
            .Zip(numbers.Select(x => x.Item2).OrderBy(x => x), Tuple.Create)
            .Sum(x => Math.Abs(x.Item1 - x.Item2));

        Console.WriteLine($"Total Distance: {totalDistance}");
    }

    public static async Task SecondPart()
    {
        var gameInput = await File.ReadAllTextAsync("./input.txt");

        var numbers = gameInput.Replace(Separator, EmptySpace)
            .Split("\n")
            .ToList()
            .Select(item => item.Split(EmptySpace))
            .Select(x => (int.Parse(x[0]), int.Parse(x[1])));

        var similarityScore = numbers
            .Select(number => number.Item1)
            .Sum(num => num * numbers.Select(number => number.Item2).Count(x => x == num));

        Console.WriteLine($"Total Distance: {similarityScore}");
    }
}