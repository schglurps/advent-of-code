namespace day02;

public static class Part2
{
    static bool IsDiffValid(int value)
    {
        int absoluteDiff = Math.Abs(value);
        return absoluteDiff > 0 && absoluteDiff < 4;
    }

    static bool IsSafe(int[] numbers, bool isDampened)
    {
        int firstDiff = numbers[0] - numbers[1];
        bool isIncreasing = numbers[0] - numbers[1] < 0;

        var result = !IsDiffValid(firstDiff)
            ? (numbers[0], numbers[1], 0)
            : Enumerable
                .Range(1, numbers.Length - 2)
                .Select(i => (numbers[i], numbers[i + 1], i))
                .FirstOrDefault(t =>
                {
                    (int x, int y, int i) = t;

                    int diff = x - y;

                    if(!IsDiffValid(diff))
                        return true;

                    bool currentIsIncreasing = x - y < 0;
                    if(isIncreasing != currentIsIncreasing)
                        return true;

                    return false;
                });

        if(result == default)
            return true;
    
        if(isDampened)
            return false;

        (int _, int _, int index) = result;

        int[] n1 = new int[numbers.Length - 1];
        Array.Copy(numbers, 0, n1, 0, index);
        Array.Copy(numbers, index + 1, n1, index, n1.Length - index);

        int[] n2 = new int[numbers.Length - 1];
        Array.Copy(numbers, 0, n2, 0, index + 1);
        Array.Copy(numbers, index + 2, n2, index + 1, n2.Length - index - 1);
        
        return IsSafe(n1, true) || IsSafe(n2, true);
    }

    static async IAsyncEnumerable<int[]> GetReports(StreamReader sr)
    {
        string? line;
        while((line = await sr.ReadLineAsync()) is not null)
        {
            yield return line.Split(' ').Select(int.Parse).ToArray();
        }
    }

    public static async Task Execute()
    {
        using StreamReader sr = new("input");
        var reports = GetReports(sr);
        int result = await reports.CountAsync(r => IsSafe(r, false));
        Console.WriteLine(result);
    }
}
