using System.Text.RegularExpressions;

partial class Program
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"^mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulTokenRegex();

    [GeneratedRegex(@"^do\(\)")]
    private static partial Regex DoTokenRegex();

    [GeneratedRegex(@"^don't\(\)")]
    private static partial Regex DontTokenRegex();

    public static async Task Main(string[] args)
    {
        Console.WriteLine(await PartOne().SumAsync());
        Console.WriteLine(await PartTwo());
    }

    private static async IAsyncEnumerable<int> PartOne()
    {
        using StreamReader sr = new("input");
        string? line;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            foreach(Match m in MulRegex().Matches(line))
            {
                yield return int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
            }
        }
    }

    private static async Task<int> PartTwo()
    {
        using StreamReader sr = new("input");
        string? line;
        bool enabled = true;
        int sum = 0;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            string current = line;
            while(current.Length > 0)
            {
                Match m = DoTokenRegex().Match(current);
                if(m.Success)
                {
                    enabled = true;
                    current = current.Substring(m.Length);
                    continue;
                }

                m = DontTokenRegex().Match(current);
                if(m.Success)
                {
                    enabled = false;
                    current = current.Substring(m.Length);
                    continue;
                }

                m = MulTokenRegex().Match(current);
                if(m.Success)
                {
                    if(enabled)
                    {
                        sum += (int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value));
                    }
                    current = current.Substring(m.Length);
                    continue;
                }

                current = current.Substring(1);
            }
        }
        return sum;
    }
}