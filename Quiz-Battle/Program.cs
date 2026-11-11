while (true)
{
    Console.Clear();
    Console.WriteLine("=========Quiz Battle========");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("1. Spela");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.WriteLine("2. Topplista");
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("3. Avsluta");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
    Console.WriteLine("============================");
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("Ta ett val (1-3)");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Starting game.");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            break;

        case "2":
            Console.WriteLine("Showing highscores...");
            break;

     
        case "3":
            
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
 }

// Create a new quiz game.
QuizGame game = new QuizGame();

// Add two players.
game.CreatePlayers(new List<string>
{
    "Ludwig",
    "Erik"
});

// Get the players.
List<Player> players = game.GetPlayers();

// Give Ludwig two correct answers.
game.RecordPlayerAnswer(players[0], true, 10);
game.RecordPlayerAnswer(players[0], true, 20);

// Give Erik one correct and one incorrect answer.
game.RecordPlayerAnswer(players[1], true, 30);
game.RecordPlayerAnswer(players[1], false, 20);

// Show the results.
foreach (Player player in players)
{
    Console.WriteLine($"{player.Name}: {player.Score} points");
}

// Find the winner.
foreach (Player winner in game.GetWinners())
{
    Console.WriteLine($"Winner: {winner.Name}");
}