const int length = 1000;

int[] left = new int[length];
int[] right = new int[length];

using(StreamReader sr = new("input"))
{
    for(int i = 0; i < length; i++)
    {
        string line = (await sr.ReadLineAsync())!;
        var span = line.AsSpan();
        left[i] = int.Parse(span.Slice(0, 5));
        right[i] = int.Parse(span.Slice(8, 5));
    }
}

Array.Sort(left);
Array.Sort(right);

int result = left.Zip(right).Aggregate(
    0,
    (acc, cur) =>
    {
        var (first, second) = cur;
        if(second > first)
        {
            (first, second) = (second, first);
        }

        return acc + first - second;
    }
);

Console.WriteLine(result);