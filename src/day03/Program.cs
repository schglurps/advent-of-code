using System.Text.RegularExpressions;

partial class Program
{
    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();

    public static async Task Main(string[] args)
    {
        Console.WriteLine(await NewMethod().SumAsync());
    }

    private static async IAsyncEnumerable<int> NewMethod()
    {
        StreamReader sr = new("input");
        string? line;
        while ((line = await sr.ReadLineAsync()) != null)
        {
            foreach(Match m in MulRegex().Matches(line))
            {
                yield return int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
            }
        }
    }
}