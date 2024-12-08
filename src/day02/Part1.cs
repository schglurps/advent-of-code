namespace day02;

public static class Part1
{
    static bool IsDiffValid(int value)
    {
        int absoluteDiff = Math.Abs(value);
        return absoluteDiff > 0 && absoluteDiff < 4;
    }

    static bool IsSafeInterval(Span<int> numbers, bool isIncreasing)
    {
        bool isWayValid = isIncreasing ? numbers[0] < numbers[1] : numbers[0] > numbers[1];
        if(!(isWayValid && IsDiffValid(numbers[0] - numbers[1])))
        {
            return false;
        }
        return numbers.Length == 2 || IsSafeInterval(numbers[1..], isIncreasing);
    }

    static bool IsSafe(int[] numbers)
    {
        int diff = numbers[0] - numbers[1];
        return IsDiffValid(diff) && IsSafeInterval(numbers.AsSpan()[1..], isIncreasing: diff < 0);
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
        int result = await reports.CountAsync(IsSafe);
        Console.WriteLine(result);
    }
}
