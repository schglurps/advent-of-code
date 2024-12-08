using StreamReader sr = new StreamReader("input");

HashSet<(int, int)> rules = new();

string? line;
while((line = (await sr.ReadLineAsync())!) != string.Empty)
{
    var arr = line.Split("|");
    var rule = (int.Parse(arr[0]), int.Parse(arr[1]));
    rules.Add(rule);
}

bool IsUpdateValid(Span<int> update) {
    int first = update[0];
    for(int i = 1; i < update.Length; i++)
        if(!rules.Contains((first, update[i])))
            return false;

    return update.Length == 2
        ? true
        : IsUpdateValid(update[1..]);
}

int result = 0;
while((line = (await sr.ReadLineAsync())!) != null)
{
    var currentUpdate = line.Split(",").Select(int.Parse).ToArray().AsSpan();
    if(IsUpdateValid(currentUpdate))
        result += currentUpdate[(currentUpdate.Length - 1) / 2];
}
Console.WriteLine(result);