using System.Text.RegularExpressions;

namespace day3;

public static class Executor
{
    private const string SingleInputPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    
    public static async Task PartOne()
    {
        var regexPattern = @"(mul\([0-9]{1,3},[0-9]{1,3}\))";
        var gameInput = await File.ReadAllTextAsync("./input.txt");
        var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

        var result = Regex.Split(gameInput, regexPattern)
            .ToList()
            .Sum(inp =>
            {
                var match = Regex.Match(inp, SingleInputPattern);
                if (!match.Success) return 0;

                var numbers = match.Groups;
                return  int.Parse(numbers[1].Value) * int.Parse(numbers[2].Value);
            });

        Console.WriteLine($"Day 3 Part 1: {result}");
    }

    public static async Task PartTwo()
    {
        const string regexPattern = @"(mul\([0-9]{1,3},[0-9]{1,3}\)|do\(\)|don't\(\))";
        const string singleDoPattern = @"do\(\)";
        const string singleDontPattern = @"don't\(\)";
        var gameInput = await File.ReadAllTextAsync("./input.txt");
        var shouldSum = true;

        var result = Regex.Split(gameInput, regexPattern)
            .ToList()
            .Sum(inp =>
            {
                var match = Regex.Match(inp, SingleInputPattern);
                if (!match.Success)
                {
                    if (Regex.IsMatch(inp, singleDontPattern))
                        shouldSum = false;
                    if (Regex.IsMatch(inp, singleDoPattern))
                        shouldSum = true;
                    return 0;
                }
                
                if (!shouldSum) return 0;
                var numbers = match.Groups;
                return int.Parse(numbers[1].Value) * int.Parse(numbers[2].Value);
            });

        Console.WriteLine($"Day 3 Part 2: {result}");
    }

}