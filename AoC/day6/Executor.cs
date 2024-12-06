using Utils;

namespace day6;

enum Direction
{
    Up,
    Right,
    Down,
    Left,
}
public static class Executor
{
    static bool CanMove(Direction direction, int i, int j, int rows, int cols, char[,] grid)
    {
        return direction switch
        {
            Direction.Up => i - 1 >= 0 && grid[i - 1, j] != '#',
            Direction.Down => i + 1 <= rows - 1 && grid[i + 1, j] != '#',
            Direction.Left => j - 1 >= 0 && grid[i, j - 1] != '#',
            Direction.Right => j + 1 <= cols - 1 && grid[i, j + 1] != '#',
            _ => false
        };
    }
    
    static (int nextRow, int nextCol) GetNextIndexes(Direction direction, int i, int j)
    {
        return direction switch
        {
            Direction.Up => (i - 1, j),
            Direction.Down => (i + 1, j),
            Direction.Left => (i, j - 1),
            Direction.Right => (i, j + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    static bool IsFinished(int row, int col, int i, int j)
    {
        return row - 1 == i || col - 1 == j || i == 0 || j == 0;
    }

    static void PrintMatrix(char[,] grid)
    {
        for (var i = 0; i < 130; i++)
        {
            for (var j = 0; j < 130; j++)
            {
                Console.Write(grid[i,j]);
            }
            Console.WriteLine();
        }
        
        Thread.Sleep(500);
        Console.Clear();
    }
    
    public static async Task PartOne()
    {
        var input = await File.ReadAllTextAsync("./input.txt");
        var matrix = input.Split("\n")
            .Select(el => el.Select(x => x).ToList())
            .ToList();

        var startIndex = (0, 0);
        var rows = matrix.Count;
        var cols = matrix.First().Count;
        var grid = new char[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                grid[i, j] = matrix[i][j];
                if (grid[i, j] != '^') continue;
                
                startIndex = (i, j);
            }
        }

        PrintMatrix(grid);

        var direction = Direction.Up;
        var currRow = startIndex.Item1;
        var currCol = startIndex.Item2;
        while (true)
        {
            if (IsFinished(rows, cols, currRow, currCol))
            {
                grid[currRow, currCol] = 'X';
                break;
            }

            if (!CanMove(direction, currRow, currCol, rows, cols, grid))
                direction = direction.Next();

            grid[currRow, currCol] = 'X';

            PrintMatrix(grid);
            
            var (nextRow, nextCol) = GetNextIndexes(direction, currRow, currCol);
            
            currRow = nextRow;
            currCol = nextCol;
        }

        var count = 0;
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                if (grid[i, j] == 'X')
                    count++;
            }
        }
        Console.WriteLine($"Day 6 Part 1: {count}");
    }
}