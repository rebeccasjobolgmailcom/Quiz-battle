using Quiz_Battle.UI;

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
            Console.Clear();
            CreateGame newGame = new CreateGame();
            List<string> players = newGame.CreatePlayers();
            newGame.AskRounds();
            QuizGame quizGame = new QuizGame();
            quizGame.CreatePlayers(players);

            break;

        case "2":
            Console.Write("Showing highscores.");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            break;

     
        case "3":
            
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
            return;

        default:
            Console.WriteLine("Invalid choice!");
            Console.ReadKey();
            break;
    }



}