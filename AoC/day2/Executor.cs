namespace day2;

public static class Executor
{
    public const string Separator = " ";

    static bool Calculate(int num1, int num2)
    {
        return num1 < num2 && Math.Abs(num1 - num2) <= 3;
    }

    static bool IsDescending(List<int> numbers)
    {
        for (var i = 0; i < numbers.Count - 1; i++)
            if (!Calculate(numbers[i + 1], numbers[i]))
                return false;

        return true;
    }
    
    static bool IsAscending(List<int> numbers)
    {
        for (var i = 0; i < numbers.Count - 1; i++)
            if (!Calculate(numbers[i], numbers[i + 1]))
                return false;

        return true;
    }

    public static async Task FirstPart()
    {
        var gameInput = await File.ReadAllTextAsync("./input.txt");

        var safeReports = gameInput
            .Split("\n")
            .Select(level => level.Split(Separator))
            .Select(levels => levels.Select(level => int.Parse(level)).ToList())
            .Sum(report =>
            {
                if (IsDescending(report))
                    return 1;
                return IsAscending(report) ? 1 : 0;
            });

        Console.WriteLine($"Day 2 Part 1: {safeReports}");
    }

    static bool CheckToleranceAsc(List<int> numbers, int indexShifter = 0)
    {
        var invalidAsc = 0;

        for (var i = 0; i < numbers.Count - 1;)
        {
            if (Calculate(numbers[i], numbers[i + 1]))
                i++;
            else
            {
                invalidAsc += 1;
                if (invalidAsc > 1)
                    return false;

                numbers.RemoveAt(i + indexShifter);
                i = 0;
            }
        }

        return true;
    }

    static bool CheckToleranceDesc(List<int> numbers, int indexShifter = 0)
    {
        var invalidDesc = 0;
        for (var i = 0; i < numbers.Count - 1;)
        {
            if (Calculate(numbers[i + 1], numbers[i]))
                i++;
            else
            {
                invalidDesc += 1;
                if (invalidDesc > 1)
                    return false;

                numbers.RemoveAt(i + indexShifter);
                i = 0;
            }
        }

        return true;
    }

    public static async Task SecondPart()
    {
        var gameInput = await File.ReadAllTextAsync("./input.txt");
        
        var safeReports = gameInput
            .Split("\n")
            .Select(level => level.Split(Separator))
            .Select(levels => levels.Select(level => int.Parse(level)).ToList())
            .Sum(report =>
            {
                if (IsDescending(report))
                    return 1;
                if (IsAscending(report))
                    return 1;
                if (CheckToleranceAsc(report.Select(x => x).ToList()))
                    return 1;
                if (CheckToleranceDesc(report.Select(x => x).ToList()))
                    return 1;
                if (CheckToleranceAsc(report.Select(x => x).ToList(), 1))
                    return 1;
                if (CheckToleranceDesc(report.Select(x => x).ToList(), 1))
                    return 1;

                return 0;
            });

        Console.WriteLine($"Day 2 Part 2: {safeReports}");
    }
}