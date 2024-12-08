const int size = 140;
var matrix = new char[size * size];
using StreamReader sr = new("input");
for(int i = 0; i < size; i++)
{
    await sr.ReadAsync(new Memory<char>(matrix, size * i, size));
    sr.Read();
}

int PartOne()
{
    var xmas = "XMAS".AsSpan();
    var samx = "SAMX".AsSpan();

    var buffer = new char[xmas.Length];
    var bufferSpan = buffer.AsSpan();

    int count = 0;
    for(int i = 0; i < size; i++)
    {
        for(int j = 0; j < size; j++)
        {
            if(j < size - 3)
            {
                var temp = new ReadOnlySpan<char>(matrix, i * size + j, xmas.Length);
                if(temp.SequenceEqual(xmas) || temp.SequenceEqual(samx))
                    count++;
            }

            if(i < size - 3)
            {
                for(int k = 0; k < xmas.Length; k++)
                    buffer[k] = matrix[(i + k) * size + j];

                if(bufferSpan.SequenceEqual(xmas) || bufferSpan.SequenceEqual(samx))
                    count++;
            }

            if(i < size - 3 && j < size - 3)
            {
                for(int k = 0; k < xmas.Length; k++)
                    buffer[k] = matrix[(i + k) * size + j + k];

                if(bufferSpan.SequenceEqual(xmas) || bufferSpan.SequenceEqual(samx))
                    count++;
            }

            if(i < size - 3 && j > 2)
            {
                for(int k = 0; k < xmas.Length; k++)
                    buffer[k] = matrix[(i + k) * size + j - k];

                if(bufferSpan.SequenceEqual(xmas) || bufferSpan.SequenceEqual(samx))
                    count++;
            }
        }
    }

    return count;
}

int PartTwo()
{
    int count = 0;
    var temp1 = new char[2];
    var temp2 = new char[2];
    char[][] expected = [['M', 'S'], ['S', 'M']];
    for(int i = 1; i < size - 1; i++)
    {
        for(int j = 1; j < size - 1; j++)
        {
            if(matrix[i * size + j] != 'A')
                continue;

            temp1[0] = matrix[(i - 1) * size + j - 1];
            temp1[1] = matrix[(i + 1) * size + j + 1];
            
            temp2[0] = matrix[(i - 1) * size + j + 1];
            temp2[1] = matrix[(i + 1) * size + j - 1];

            if(expected.Any(e => e.SequenceEqual(temp1)) && expected.Any(e => e.SequenceEqual(temp2)))
                count++;
        }
    }
    return count;
}

Console.WriteLine(PartOne());
Console.WriteLine(PartTwo());