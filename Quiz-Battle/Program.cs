using Quiz_Battle.Services;

ConsoleUI ui = new ConsoleUI();

while (true)
{
    // Visa menyn och ta emot användarens val
    string choice = ui.ShowMenu();

    switch (choice)
    {
        case "1":

            // Starta spelet
            Console.Write("Startar spel");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);


            // Fråga efter antal spelare och deras namn
            int numberOfPlayers= ui.AskNumberOfPlayers();

            List<string> players = ui.AskPlayerNames(numberOfPlayers);

            // Fråga efter antal rundor
            int rounds = ui.AskRounds();

            // Skapa QuizGame
            QuizGame quizGame = new QuizGame();

            // Skapa Player-objekt från namnen
            quizGame.CreatePlayers(players);

            // Starta rundorna
            QuestionBank questionBank = new QuestionBank();
            quizGame.PlayRounds(rounds, questionBank);

            Console.WriteLine("Spelet är slut!");
            Console.ReadKey();

            break;

        case "2":

            Console.Write("Visar highscores");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            Console.Write(" .");
            Thread.Sleep(500);
            ui.ShowHighScores();

            break;

        case "3":

            Console.WriteLine("Hejdå!");
            Console.ReadKey();
            return;
    }
}