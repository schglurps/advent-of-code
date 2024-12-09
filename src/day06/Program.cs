const int size = 130;
var matrix = new char[size * size];
using StreamReader sr = new("input");
for(int i = 0; i < size; i++)
{
    await sr.ReadAsync(new Memory<char>(matrix, size * i, size));
    sr.Read();
}

const int startX = 71;
const int startY = 93;

matrix[startX + startY * size] = '.';
var positions = new HashSet<(int, int)> { (startX, startY) };
var state = ('N', startX, startY);

while(true) 
{
    (char direction, int x, int y) = state;

    var coordToCheck = direction switch
    {
        'N' => (X: x, Y: y - 1),
        'E' => (X: x + 1, Y: y),
        'S' => (X: x, Y: y + 1),
        'W' => (X: x - 1, Y: y),
        _ => throw new NotImplementedException()
    };

    if(coordToCheck.X < 0 || coordToCheck.Y < 0 || coordToCheck.X >= size || coordToCheck.Y >= size)
    {
        Console.WriteLine(positions.Count);
        break;
    }

    if(matrix[coordToCheck.Y * size + coordToCheck.X] == '#')
    {
        char newDirection = direction switch
        {
            'N' => 'E',
            'E' => 'S',
            'S' => 'W',
            'W' => 'N',
            _ => throw new NotImplementedException()
        };
        state = (newDirection, x, y);
    }
    else
    {
        state = (direction, coordToCheck.X, coordToCheck.Y);
        positions.Add((coordToCheck.X, coordToCheck.Y));
    }
}