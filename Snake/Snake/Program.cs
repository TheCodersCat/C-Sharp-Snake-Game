using Snake;

Random random = new Random();
Coord gridDimensions = new Coord(50, 20);

Coord snakePos = new Coord(10, 1);
Direction movementDirection = Direction.Down;
List<Coord> snakePosHistory = new List<Coord>();
int tailLength = 1;

Coord applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
int frameDelayMilli = 100;
int score = 0;

while (true)
{
    Console.Clear();
    Console.WriteLine("Score: " + score);
    snakePos.ApplyMovementDirection(movementDirection);

    // Render the game to the Console
    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {
            Coord currentCoord = new Coord(x, y);

            if (snakePos.Equals(currentCoord) || snakePosHistory.Contains(currentCoord))
                Console.Write("■");
            else if (applePos.Equals(currentCoord))
                Console.Write("a");
            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
                Console.Write("#");
            else
                Console.Write(" ");
        }
        Console.WriteLine();
    }

    // Check if snake has picked up apple
    if (snakePos.Equals(applePos))
    {
        tailLength++;
        score++;
        applePos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
    }
    // Check for game over conditions - snake has hit wall or snake has hit tail
    else if (snakePos.X == 0 || snakePos.Y == 0 || snakePos.X == gridDimensions.X - 1 ||
        snakePos.Y == gridDimensions.Y - 1 || snakePosHistory.Contains(snakePos))
    {
        // Reset game
        score = 0;
        tailLength = 1;
        snakePos = new Coord(10, 1);
        snakePosHistory.Clear();
        movementDirection = Direction.Down;
        continue;
    }

    // Add the snake's current position to the snakePosHistory list
    snakePosHistory.Add(new Coord(snakePos.X, snakePos.Y));

    if (snakePosHistory.Count > tailLength)
        snakePosHistory.RemoveAt(0);


    // Delay the rendering of next frame for frameDelayMilli milliseconds whilst checking for player input
    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).Milliseconds < frameDelayMilli)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            // Assign snake new direction to move in based on what key was pressed
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movementDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movementDirection = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movementDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movementDirection = Direction.Down;
                    break;
            }
        }
    }
}