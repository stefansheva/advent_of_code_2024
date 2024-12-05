namespace day5;

public static class Executor
{
    static bool CheckRight(List<int> queue, List<(int, int)> rules, int index)
    {
        for (int i = index + 1; i < queue.Count; i++)
        {
            var isValid = rules.Any(rule => rule.Item2 == queue[i]);
            if (!isValid)
                return false;
        }
        return true;
    }
    
    static bool CheckLeft(List<int> queue, List<(int, int)> rules, int index)
    {
        for (int i = index - 1; i >= 0; i--)
        {
            var isValid = rules.Any(rule => rule.Item1 == queue[i]);
            if (!isValid)
                return false;
        }
        return true;
    }
    
    public static async Task PartOne()
    {
        var gameInput = await File.ReadAllTextAsync("./input.txt");

        var rules = gameInput.Split("\n\n")
            .First()
            .Split("\n")
            .Select(rule => (int.Parse(rule.Split("|").First()), int.Parse(rule.Split("|").Last())));
        var printingQueues = gameInput.Split("\n\n")
            .Last()
            .Split("\n")
            .Select(queue => queue.Split(",").Select(q => int.Parse(q)).ToList())
            .ToList();

        var sum = 0;
        foreach (var queue in printingQueues)
        {
            bool isValid = false;
            for (int i = 0; i < queue.Count; i++)
            {
                var applicableRules = rules
                    .Where(rule => rule.Item1 == queue[i] || rule.Item2 == queue[i])
                    .ToList();
                var isValidRight = CheckRight(queue, applicableRules, i);
                var isValidLeft = CheckLeft(queue, applicableRules, i);

                if (!isValidRight || !isValidLeft)
                {
                    isValid = false;
                    break;
                }

                isValid = true;
            }

            if (!isValid) continue;

            var midElement = queue.Count / 2;
            sum += queue[midElement];
        }

        Console.WriteLine($"Day 5 Part 1: {sum}");
    }
    
    static int[] SortBasedOnRules(List<(int, int)> rules, List<int> list)
    {
        var listSet = new HashSet<int>(list);
        var filteredRules = rules.Where(rule => listSet.Contains(rule.Item1) && listSet.Contains(rule.Item2)).ToList();
        var graph = new Dictionary<int, List<int>>();
        var inDegree = new Dictionary<int, int>();

        foreach (var (before, after) in filteredRules)
        {
            if (!graph.ContainsKey(before)) graph[before] = new List<int>();
            if (!inDegree.ContainsKey(before)) inDegree[before] = 0;
            if (!inDegree.ContainsKey(after)) inDegree[after] = 0;

            graph[before].Add(after);
            inDegree[after]++;
        }

        var sortedOrder = new List<int>();
        var queue = new Queue<int>();

        foreach (var num in list)
        {
            if (!inDegree.ContainsKey(num) || inDegree[num] == 0)
                queue.Enqueue(num);
        }

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            sortedOrder.Add(current);

            if (graph.ContainsKey(current))
            {
                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                        queue.Enqueue(neighbor);
                }
            }
        }

        var result = new List<int>();
        var sortedSet = new HashSet<int>(sortedOrder);
        foreach (var num in list)
        {
            if (!sortedSet.Contains(num))
                result.Add(num);
        }

        result.InsertRange(0, sortedOrder.Where(listSet.Contains));
        return result.ToArray();
    }

    public static async Task PartTwo()
    { 
        var gameInput = await File.ReadAllTextAsync("./input.txt");

        var rules = gameInput.Split("\n\n")
            .First()
            .Split("\n")
            .Select(rule => (int.Parse(rule.Split("|").First()), int.Parse(rule.Split("|").Last())));
        var printingQueues = gameInput.Split("\n\n")
            .Last()
            .Split("\n")
            .Select(queue => queue.Split(",").Select(q => int.Parse(q)).ToList())
            .ToList();

        var invalidQueues = new List<List<int>>();
        foreach (var queue in printingQueues)
        {
            for (int i = 0; i < queue.Count; i++)
            {
                var applicableRules = rules.Where(rule => rule.Item1 == queue[i] || rule.Item2 == queue[i]).ToList();
                var isValidRight = CheckRight(queue, applicableRules, i);
                var isValidLeft = CheckLeft(queue, applicableRules, i);

                if (isValidRight && isValidLeft) continue;
                
                invalidQueues.Add(queue);
                break;
            }
        }

        var invalidQueuesSum = 0;
        foreach (var queue in invalidQueues)
        {
            var sortedList = SortBasedOnRules(rules.ToList(), queue);
            
            var midElement = sortedList.Length / 2;
            invalidQueuesSum += sortedList[midElement];
        }
        
        Console.WriteLine($"Day 5 Part 2: {invalidQueuesSum}");
    }
}