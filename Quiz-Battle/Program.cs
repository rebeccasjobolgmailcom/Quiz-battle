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