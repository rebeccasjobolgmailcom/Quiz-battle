using Quiz_Battle.UI;

ConsoleUI ui = new ConsoleUI();

while (true)
{
    // Visa menyn och ta emot användarens val
    string choice = ui.ShowMenu();

    switch (choice)
    {
        case "1":

            // Starta spelet
            Console.Write("Starting game");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);


            // Fråga efter antal spelare och deras namn
            int numberOfPlayers = ui.AskNumberOfPlayers();

            List<string> players = ui.AskPlayerNames(numberOfPlayers);

            // Fråga efter antal rundor
            int rounds = ui.AskRounds();

            // Skapa QuizGame
            QuizGame quizGame = new QuizGame();

            // Skapa Player-objekt från namnen
            quizGame.CreatePlayers(players);

            // Starta rundorna
            quizGame.PlayRounds(rounds);

            Console.WriteLine("Spelet är slut!");
            Console.ReadKey();

            break;

        case "2":

            Console.WriteLine("Showing highscores");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            

            break;

        case "3":

            Console.WriteLine("Goodbye!");
            Console.ReadKey();
            return;
    }
}