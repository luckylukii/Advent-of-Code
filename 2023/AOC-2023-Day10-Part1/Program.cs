public static class Program
{
    private enum PipeDirection
    {
        NorthToSouth, EastToWest,
        NorthToEast, NorthToWest,
        SouthToEast, SouthToWest,
        None, Animal
    }

    private enum Direction
    {
        north = 0, east = 3, west = 2, south = 1
    }

    private static string[] inputs;
    private static PipeDirection[,] pipes;
    private static int loopLength;
    private static Vector2 animalPos = new();
    private static Vector2 current;
    private static Vector2 last;

    private static void Main()
    {
        inputs = Reader.ReadFile("Inputs.txt").ToArray();

        FindAllPipes();

        current = animalPos;

        do
        {
            var pipe = pipes[current.x, current.y];
            current += Move(pipe, out var beforeMove);

            last = beforeMove;
            loopLength++;
        } while (current != animalPos);

        var result = loopLength / 2;
        Console.WriteLine(result);
        Console.ReadLine();
    }

    private static Vector2 Move(PipeDirection dir, out Vector2 beforeMove)
    {
        beforeMove = current;
        return dir switch
        {
            PipeDirection.NorthToSouth => last == current + new Vector2(1, 0) ? new(-1, 0) : new(1, 0),
            PipeDirection.SouthToEast => last == current + new Vector2(1, 0) ? new(0, 1) : new(1, 0),
            PipeDirection.NorthToEast => last == current + new Vector2(-1, 0) ? new(0, 1) : new(-1, 0),
            PipeDirection.EastToWest => last == current + new Vector2(0, -1) ? new(0, 1) : new(0, -1),
            PipeDirection.SouthToWest => last == current + new Vector2(1, 0) ? new(0, -1) : new(1, 0),
            PipeDirection.NorthToWest => last == current + new Vector2(-1, 0) ? new(0, -1) : new(-1, 0),
            _ => new()
        };
    }

    private static PipeDirection GetAnimalPipeType()
    {
        bool north = AnimalConnectedInDirection(Direction.north);
        bool south = AnimalConnectedInDirection(Direction.south);
        bool west = AnimalConnectedInDirection(Direction.west);
        bool east = AnimalConnectedInDirection(Direction.east);

        if (north && south) return PipeDirection.NorthToSouth;
        else if (north && west) return PipeDirection.NorthToWest;
        else if (north && east) return PipeDirection.NorthToEast;
        else if (south && west) return PipeDirection.SouthToWest;
        else if (south && east) return PipeDirection.SouthToEast;

        return PipeDirection.None;
    }

    private static bool AnimalConnectedInDirection(Direction direction)
    {
        if (direction == Direction.north)
        {
            PipeDirection pipeToCheck = pipes[animalPos.x - 1, animalPos.y];
            bool isConnected = pipeToCheck == PipeDirection.SouthToWest || pipeToCheck == PipeDirection.SouthToEast || pipeToCheck == PipeDirection.NorthToSouth;
            return isConnected;
        }
        else if (direction == Direction.south)
        {
            PipeDirection pipeToCheck = pipes[animalPos.x + 1, animalPos.y];
            bool isConnected = pipeToCheck == PipeDirection.NorthToSouth || pipeToCheck == PipeDirection.NorthToWest || pipeToCheck == PipeDirection.NorthToEast;
            return isConnected;
        }
        else if (direction == Direction.west)
        {
            PipeDirection pipeToCheck = pipes[animalPos.x, animalPos.y - 1];
            bool isConnected = pipeToCheck == PipeDirection.EastToWest || pipeToCheck == PipeDirection.SouthToEast || pipeToCheck == PipeDirection.NorthToEast;
            return isConnected;
        }
        else if (direction == Direction.east)
        {
            PipeDirection pipeToCheck = pipes[animalPos.x, animalPos.y + 1];
            bool isConnected = pipeToCheck == PipeDirection.EastToWest || pipeToCheck == PipeDirection.SouthToWest || pipeToCheck == PipeDirection.NorthToWest;
            return isConnected;
        }
        return false;
    }

    private static void FindAllPipes()
    {
        pipes = new PipeDirection[inputs[0].Length, inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            for (int j = 0; j < inputs[i].Length; j++)
            {
                pipes[i, j] = inputs[i][j] switch
                {
                    '|' => PipeDirection.NorthToSouth,
                    '-' => PipeDirection.EastToWest,
                    'L' => PipeDirection.NorthToEast,
                    'J' => PipeDirection.NorthToWest,
                    '7' => PipeDirection.SouthToWest,
                    'F' => PipeDirection.SouthToEast,
                    'S' => PipeDirection.Animal,
                    _ => PipeDirection.None,
                };

                if (inputs[i][j] == 'S') animalPos = new Vector2(i, j);
            }
        }

        pipes[animalPos.x, animalPos.y] = GetAnimalPipeType();
    }
}