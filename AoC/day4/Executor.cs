namespace day4;

public static class Executor
{
     private static int CountHorizontally(char[,] grid, string searchString, int rows, int cols)
     {
         var count = 0;

         for (var i = 0; i < rows; i++)
         {
             for (var j = 0; j <= cols - searchString.Length; j++)
             {
                 if (CheckSubstring(grid, searchString, i, j, 0, 1)) count++;
                 if (CheckSubstring(grid, searchString, i, j + searchString.Length - 1, 0, -1)) count++;
             }
         }

         return count;
     }

     private static int CountVertically(char[,] grid, string searchString, int rows, int cols)
     {
         var count = 0;

         for (var i = 0; i <= rows - searchString.Length; i++)
         {
             for (var j = 0; j < cols; j++)
             {
                 if (CheckSubstring(grid, searchString, i, j, 1, 0)) count++;
                 if (CheckSubstring(grid, searchString, i + searchString.Length - 1, j, -1, 0)) count++;
             }
         }

         return count;
     }

     private static int CountDiagonally(char[,] grid, string searchString, int rows, int cols)
     {
         var count = 0;

         for (var i = 0; i <= rows - searchString.Length; i++)
         {
             for (var j = 0; j <= cols - searchString.Length; j++)
             {
                 if (CheckSubstring(grid, searchString, i, j, 1, 1)) count++;
                 if (CheckSubstring(grid, searchString, i, j + searchString.Length - 1, 1, -1)) count++;
             }
         }

         for (var i = searchString.Length - 1; i < rows; i++)
         {
             for (var j = 0; j <= cols - searchString.Length; j++)
             {
                 if (CheckSubstring(grid, searchString, i, j, -1, 1)) count++;
                 if (CheckSubstring(grid, searchString, i, j + searchString.Length - 1, -1, -1)) count++;
             }
         }

         return count;
     }

     private static bool CheckSubstring(char[,] grid, string searchString, int startX, int startY, int deltaX,
        int deltaY)
    {
        return !searchString.Where((t, k) => grid[startX + k * deltaX, startY + k * deltaY] != t).Any();
    }

     public static async Task PartOne()
     {
         var searchString = "XMAS";
         var gameInput = await File.ReadAllTextAsync("./input.txt");
        var input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

        var lines = gameInput.Split("\n");
        var rows = lines.Length;
        var cols = lines[0].Length;

        var grid = new char[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        var result = CountHorizontally(grid, searchString, rows, cols) +
                     CountVertically(grid, searchString, rows, cols) +
                     CountDiagonally(grid, searchString, rows, cols);

        Console.WriteLine($"Day 4 Part 1: {result}");
    }

    public static async Task PartTwo()
    {
        var searchString = "MAS";
        var gameInput = await File.ReadAllTextAsync("./input.txt");
        var input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";
        
        var lines = gameInput.Split("\n");
        var rows = lines.Length;
        var cols = lines[0].Length;
        
        var grid = new char[rows, cols];
        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }


        var count = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i, j] != 'A') continue;
                if (i - 1 < 0 || i + 1 >= rows || j - 1 < 0 || j + 1 >= cols) continue;

                bool isValidMas = (grid[i - 1, j - 1] == 'M' &&
                                   grid[i + 1, j + 1] == 'S') &&
                                  (grid[i - 1, j + 1] == 'M' &&
                                   grid[i + 1, j - 1] == 'S');

                bool isValidSam = (grid[i - 1, j - 1] == 'S' &&
                                   grid[i + 1, j + 1] == 'M') &&
                                  (grid[i - 1, j + 1] == 'S' &&
                                   grid[i + 1, j - 1] == 'M');

                bool isValidCombined = (grid[i - 1, j - 1] == 'M' &&
                                        grid[i + 1, j + 1] == 'S' &&
                                        grid[i - 1, j + 1] == 'S' &&
                                        grid[i + 1, j - 1] == 'M') || (grid[i - 1, j - 1] == 'S' &&
                                                                       grid[i + 1, j + 1] == 'M' &&
                                                                       grid[i - 1, j + 1] == 'M' &&
                                                                       grid[i + 1, j - 1] == 'S');

                if (isValidMas || isValidSam || isValidCombined) count++;
            }
        }


        Console.WriteLine($"Day 4 Part 2: {count}");
    }
}