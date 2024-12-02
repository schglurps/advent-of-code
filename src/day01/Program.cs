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

void PartOne()
{
    Array.Sort(left);
    Array.Sort(right);

    int result = left.Zip(right).Aggregate(
        0,
        (acc, cur) =>
        {
            var (first, second) = cur;
            return acc + Math.Abs(first - second);
        }
    );

    Console.WriteLine(result);
}

void PartTwo()
{
    var dic = right.Aggregate(
        new Dictionary<int, int>(),
        (acc, cur) =>
        {
            if(acc.TryGetValue(cur, out var count))
            {
                acc[cur] = count + 1;
            }
            else
            {
                acc.Add(cur, 1);
            }
            return acc;
        }
    );

    int result = left.Aggregate(0, (acc, cur) => acc + cur * (dic.TryGetValue(cur, out var count) ? count : 0));
    Console.WriteLine(result);
}

PartOne();
PartTwo();